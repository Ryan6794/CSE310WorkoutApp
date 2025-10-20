using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace WorkoutPlannerWPF
{
    public class WorkoutManager
    {
        private const string ExerciseFile = "exercises.csv";
        private const string WorkoutFile = "workouts.csv";

        public List<Exercise> Exercises { get; private set; } = new List<Exercise>();
        public List<Workout> Workouts { get; private set; } = new List<Workout>();

        public WorkoutManager()
        {
            LoadExercises();
            LoadWorkouts();
        }

        public void AddExercise(Exercise exercise)
        {
            Exercises.Add(exercise);
            SaveExercises();
        }

        public void AddWorkout(Workout workout)
        {
            Workouts.Add(workout);
            SaveWorkouts();
        }

        public void ListExercises()
        {
            if (Exercises.Count == 0)
            {
                Console.WriteLine("No exercises found.");
                return;
            }
            for (int i = 0; i < Exercises.Count; i++)
                Console.WriteLine($"{i + 1}. {Exercises[i]}");
        }

        public void ListWorkouts()
        {
            if (Workouts.Count == 0)
            {
                Console.WriteLine("No workouts found.");
                return;
            }
            for (int i = 0; i < Workouts.Count; i++)
                Console.WriteLine($"{i + 1}. {Workouts[i]}");
        }

        private void SaveExercises()
        {
            using var writer = new StreamWriter(ExerciseFile);
            foreach (var ex in Exercises)
                writer.WriteLine(ex.ToCsv());
        }

        private void SaveWorkouts()
        {
            using var writer = new StreamWriter(WorkoutFile);
            foreach (var w in Workouts)
                writer.WriteLine(w.ToCsv());
        }

        private void LoadExercises()
        {
            if (!File.Exists(ExerciseFile)) return;
            var lines = File.ReadAllLines(ExerciseFile);
            Exercises = lines.Where(l => !string.IsNullOrWhiteSpace(l))
                            .Select(Exercise.FromCsv).ToList();
        }

        private void LoadWorkouts()
        {
            if (!File.Exists(WorkoutFile)) return;
            var lines = File.ReadAllLines(WorkoutFile);
            Workouts = lines.Where(l => !string.IsNullOrWhiteSpace(l))
                            .Select(line => Workout.FromCsv(line, Exercises)).ToList();
        }
    }
}
