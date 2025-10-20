using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace WorkoutPlannerWPF
{
    public partial class MainWindow : Window
    {
        private WorkoutManager manager = new WorkoutManager();

        public MainWindow()
        {
            InitializeComponent();
            RefreshExerciseList();
            RefreshWorkoutList();
        }

        private void AddExercise_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ExerciseNameBox.Text) ||
                string.IsNullOrWhiteSpace(RepsBox.Text) ||
                string.IsNullOrWhiteSpace(LengthBox.Text) ||
                string.IsNullOrWhiteSpace(CaloriesBurnedBox.Text))
            {
                MessageBox.Show("Please fill out all required fields.");
                return;
            }

            if (!int.TryParse(RepsBox.Text, out int reps) ||
                !int.TryParse(LengthBox.Text, out int length) ||
                !int.TryParse(CaloriesBurnedBox.Text, out int calories))
            {
                MessageBox.Show("Reps, length, and calories must be numbers.");
                return;
            }

            var exercise = new Exercise(
                ExerciseNameBox.Text,
                reps,
                length,
                calories,
                string.IsNullOrWhiteSpace(EquipmentBox.Text) ? "None" : EquipmentBox.Text
            );

            manager.AddExercise(exercise);
            RefreshExerciseList();

            ExerciseNameBox.Clear();
            RepsBox.Clear();
            LengthBox.Clear();
            CaloriesBurnedBox.Clear();
            EquipmentBox.Clear();
        }

        private void AddWorkout_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(WorkoutNameBox.Text))
            {
                MessageBox.Show("Enter a workout name.");
                return;
            }

            if (WorkoutExerciseSelector.SelectedItems.Count == 0)
            {
                MessageBox.Show("Select at least one exercise for this workout.");
                return;
            }

            var selectedExercises = WorkoutExerciseSelector.SelectedItems
                .Cast<string>()
                .Select(name => manager.Exercises.FirstOrDefault(e => e.Name == name))
                .Where(e => e != null)
                .ToList();

            var workout = new Workout(WorkoutNameBox.Text, selectedExercises);
            manager.AddWorkout(workout);

            WorkoutNameBox.Clear();
            RefreshWorkoutList();
        }

        private void RefreshExerciseList()
        {
            ExerciseList.Items.Clear();
            WorkoutExerciseSelector.Items.Clear();

            foreach (var e in manager.Exercises)
            {
                ExerciseList.Items.Add(e.ToString());
                WorkoutExerciseSelector.Items.Add(e.Name);
            }
        }

        private void RefreshWorkoutList()
        {
            WorkoutList.Items.Clear();
            foreach (var w in manager.Workouts)
            {
                WorkoutList.Items.Add(w.ToString());
            }
        }
    }
}
