using ConsoleApp2.Projekts.Models;
using System.Diagnostics;
using System.Text.Json;


namespace MD2
{
    public partial class MainPage : ContentPage
    {
        private readonly IDataManager _dataManager;
        public MainPage()
        {
            InitializeComponent();
            _dataManager = new UniDataManager(); 
            _dataManager.CreateTestData(); 
            LoadData();
            BindingContext = _dataManager; 

        }
        private void LoadData() {

            StudentsCollectionView.ItemsSource = null; // Reseto datus
            AssignmentsCollectionView.ItemsSource = null;
            CoursesCollectionView.ItemsSource = null;
            SubmissionsCollectionView.ItemsSource = null;

            // Parādīt Teachers
            TeachersCollectionView.ItemsSource = _dataManager.Teachers;

            // Parādīt Students
            StudentsCollectionView.ItemsSource = _dataManager.Students;

            // Parādīt Courses
            CoursesCollectionView.ItemsSource = _dataManager.Courses;

            // Parādīt Assignments
            AssignmentsCollectionView.ItemsSource = _dataManager.Assignments;

            // Parādīt Submissions
            SubmissionsCollectionView.ItemsSource = _dataManager.Submissions;

            //Pārāda Picker opcijas, lai pievienotu assignment vai submission
            CoursePicker.ItemsSource = _dataManager.Courses;
            CoursePicker.ItemDisplayBinding = new Binding("Name");

            SubmissionAssignmentPicker.ItemsSource = _dataManager.Assignments;
            SubmissionAssignmentPicker.ItemDisplayBinding = new Binding("Description");

            SubmissionStudentPicker.ItemsSource= _dataManager.Students;
            SubmissionStudentPicker.ItemDisplayBinding = new Binding("FullName");
        }
        //Iespēja lietotātājam izveidot testa datus
        private async void CreateTestDataBtn(object sender, EventArgs e)
        {
            _dataManager.CreateTestData();
            LoadData();
            DisplayAlert("Test Data Created", "Test data has been created successfully!", "OK");
            
        }
        // Iespēja lietotājam nolasīt datus no faila
        private async void OnLoadDataButtonClicked(object sender, EventArgs e) {

            await DisplayAlert("File Upload Information",
            "Only JSON data can be uploaded.",
            "OK");


            // Dod iespēju izvēlēties failu, no kā ielādēt datus
            var fileResult = await FilePicker.PickAsync();
            if (fileResult != null)
            {
                try
                {
                    // Ielādē datus no attiecīgā faila
                    _dataManager.Load(fileResult.FullPath);

                    // Upadeito UI
                    LoadData();

                    await DisplayAlert("Data Loaded", "Data loaded successfully from the file.", "OK");
                } // Ja error
                catch (Exception ex)
                {
                    await DisplayAlert("Error", $"Failed to load data: {ex.Message}", "OK");
                }
            }
            else
            {   //Ja netika atlasīts fails
                await DisplayAlert("No File", "No file was selected.", "OK");
            }
        }
        //Dod iespēju lietotājam saglabāt datus
        private async void OnSaveDataButtonClicked(object sender, EventArgs e)
        {
            //Liek lietotājam ievadīt faila nosaukumu
            string fileName = await DisplayPromptAsync("File Name", "Enter the name of the file (without extension):");

            //Ja lietotājs atsaka, tad nekas nenoteik
            if (string.IsNullOrWhiteSpace(fileName))
            {
                await DisplayAlert("No File Name", "No file name was entered.", "OK");
                return;
            }

            //Faila saglabāšanas direktorija
            string directory = @"C:\Temp"; 

            //Faila direktorijas pilnais nosaukums
            string fullPath = Path.Combine(directory, $"{fileName}.json"); 

            try
            {
                //dati uz json formātu
                var jsonData = JsonSerializer.Serialize(_dataManager);

                //Saglabā datus
                await File.WriteAllTextAsync(fullPath, jsonData);

                await DisplayAlert("Success", $"Data saved successfully as {fullPath}", "OK");

            } //Ja notiek error, tad kļūdas paziņojums
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Failed to save data: {ex.Message}", "OK");
            }
        }

        private void OnCreateStudentClicked(object sender, EventArgs e)
        {
            // Iegūst datus par Studenta gender kā string un pārtaisa par enum
            string selectedGender = StudentGenderPicker.SelectedItem as string;

            // Ja gender tika korekti pārtaisīts
            if (Enum.TryParse(selectedGender, out Person.Gender gender))
            {
                // Tad dabū studenta vārdu, uzvārdu
                string name = StudentNameEntry.Text;
                string surname = StudentSurnameEntry.Text;
                // Izveido jaunu studenta objektu
                var student = new Student
                {
                    Name = name,
                    Surname = surname,
                    GenderType = gender 
                };
                //Mēģina pievienot jauni izveidoto studnetu dataManagera
                try { 
                    _dataManager.Students.Add(student);

                    LoadData();

                    DisplayAlert("Student info","Student has been added succesfully!","OK");

                }
                catch (Exception ex)
                {
                     DisplayAlert("Error", $"Failed to add student: {ex.Message}", "OK");
                }

            }
        }
        //Iespēja lietotājam pievienot Assignment
        private void OnCreateAssignmentClicked(object sender, EventArgs e)
        {
            string description = AssignmentDescriptionEntry.Text;
            DateOnly deadline = DateOnly.FromDateTime(AssignmentDeadlinePicker.Date);
            Course course = (Course)CoursePicker.SelectedItem;

            _dataManager.CreateAssignment(description, deadline, course);
            LoadData();
            DisplayAlert("Success", "Assignment created successfully!", "OK");
        }
        //Iespēja lietotājam pievienot submission
        private void OnCreateSubmissionClicked(object sender, EventArgs e)
        {
            Assignment assignment = (Assignment)SubmissionAssignmentPicker.SelectedItem;
            Student student = (Student)SubmissionStudentPicker.SelectedItem;
            DateTime submissionTime = SubmissionDatePicker.Date;
            int score = int.Parse(SubmissionScoreEntry.Text);

            _dataManager.CreateSubmission(assignment, student, submissionTime, score);
            LoadData();
            DisplayAlert("Success", "Submission created successfully!", "OK");
        }

        private Assignment _selectedAssignment;

        //Edit assignments funckija
        private void OnEditAssignmentClicked(object sender, EventArgs e)
        {


            if (sender is Button button && button.CommandParameter is Assignment assignment)
            {
                // Ja edito, tad assignments isEditi = true
                assignment.IsEditing = true;

                // Izlabo izmaiņas
                AssignmentsCollectionView.ItemsSource = null;
                AssignmentsCollectionView.ItemsSource = _dataManager.Assignments; 
            }
        }
        //Saglabāt assignments labotos datus funkcija
        private void OnSaveAssignmentClicked(object sender, EventArgs e)
        {
            //dabū konrkrēto assignment, ka nepieciešams labot
            var button = (Button)sender;

            var selectedAssignment = (Assignment)button.BindingContext;

            // dabū stacklayout konnroļus
            var stackLayout = (StackLayout)button.Parent;

            var editDescriptionEntry = stackLayout.FindByName<Entry>("EditDescriptionEntry");
            var editDeadlinePicker = stackLayout.FindByName<DatePicker>("EditDeadlinePicker");

            // izlabo izmaiņas
            selectedAssignment.Description = editDescriptionEntry.Text;
            selectedAssignment.DeadLine = DateOnly.FromDateTime(editDeadlinePicker.Date);

            // izmaina isEdit uz false
            selectedAssignment.IsEditing = false;

            // Izlabo izmaiņas
            AssignmentsCollectionView.ItemsSource = null;
            AssignmentsCollectionView.ItemsSource = _dataManager.Assignments;
        }
        //izdzēš assignments datus
        private void OnDeleteAssignmentClicked(object sender, EventArgs e)
        {
           
            var button = (Button)sender;

            var assignmentToDelete = (Assignment)button.BindingContext;

            _dataManager.Assignments.Remove(assignmentToDelete);

            AssignmentsCollectionView.ItemsSource = null; 
            AssignmentsCollectionView.ItemsSource = _dataManager.Assignments; 
        }
        //Izlabo Submission datus
        private void OnEditSubmissionClicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var selectedSubmission = (Submission)button.CommandParameter;


            selectedSubmission.IsEditing = true;

            SubmissionsCollectionView.ItemsSource = null;
            SubmissionsCollectionView.ItemsSource = _dataManager.Submissions;
        }
        //Izdzēš submission datus
        private void OnDeleteSubmissionClicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var selectedSubmission = (Submission)button.CommandParameter;

            _dataManager.Submissions.Remove(selectedSubmission);

            
            SubmissionsCollectionView.ItemsSource = null;
            SubmissionsCollectionView.ItemsSource = _dataManager.Submissions; 
        }
        //Saglabā submission datus
        private void OnSaveSubmissionClicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var selectedSubmission = (Submission)button.CommandParameter;

            var stackLayout = (StackLayout)button.Parent;

            var editDescriptionEntry = stackLayout.FindByName<Entry>("EditSubmissionDescriptionEntry");
            var editSubmissionDatePicker = stackLayout.FindByName<DatePicker>("EditSubmissionDatePicker");

            selectedSubmission.Assignement.Description = editDescriptionEntry.Text;
            selectedSubmission.SubmissionTime = editSubmissionDatePicker.Date; 

            selectedSubmission.IsEditing = false;

            SubmissionsCollectionView.ItemsSource = null;
            SubmissionsCollectionView.ItemsSource = _dataManager.Submissions; 
        }


    }

}
