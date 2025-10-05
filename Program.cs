using System;
using System.Collections.Generic;

namespace WorkoutApp
{
    class Program
    {
        static void Main(string[] args)
        {
            WorkoutManager manager = new WorkoutManager();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("🏋️ Workout Planner");
                Console.WriteLine("1. Add Exercise");
                Console.WriteLine("2. View All Exercises");
                Console.WriteLine("3. Create Workout from Exercises");
                Console.WriteLine("4. View Workouts");
                Console.WriteLine("5. Exit");
                Console.Write("Select an option: ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddExercise(manager);
                        break;
                    case "2":
                        ViewExercises(manager);
                        break;
                    case "3":
                        CreateWorkout(manager);
                        break;
                    case "4":
                        ViewWorkouts(manager);
                        break;
                    case "5":
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Press Enter to try again...");
                        Console.ReadLine();
                        break;
                }
            }
        }

        static void AddExercise(WorkoutManager manager)
        {
            Console.Clear();
            Console.Write("Exercise name: ");
            string name = Console.ReadLine();

            Console.Write("Reps: ");
            int reps = int.Parse(Console.ReadLine());

            Console.Write("Length (minutes): ");
            int length = int.Parse(Console.ReadLine());

            Console.Write("Calories burned: ");
            int calories = int.Parse(Console.ReadLine());

            Console.Write("Equipment used: ");
            string equipment = Console.ReadLine();

            manager.AddExercise(new Exercise(name, reps, length, calories, equipment));
            Console.WriteLine("\n✅ Exercise added! Press Enter to continue...");
            Console.ReadLine();
        }

        static void ViewExercises(WorkoutManager manager)
        {
            Console.Clear();
            Console.WriteLine("📋 All Exercises:");
            manager.ListExercises();
            Console.WriteLine("\nPress Enter to return...");
            Console.ReadLine();
        }

        static void CreateWorkout(WorkoutManager manager)
        {
            Console.Clear();
            if (manager.Exercises.Count == 0)
            {
                Console.WriteLine("⚠️ No exercises available. Add some first!");
                Console.ReadLine();
                return;
            }

            Console.Write("Workout name: ");
            string workoutName = Console.ReadLine();
            List<Exercise> chosen = new List<Exercise>();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("📦 Select exercises to add (enter 0 to finish):");
                manager.ListExercises();

                Console.Write("Choice: ");
                int choice = int.Parse(Console.ReadLine());
                if (choice == 0) break;

                if (choice < 1 || choice > manager.Exercises.Count)
                {
                    Console.WriteLine("Invalid choice.");
                    Console.ReadLine();
                    continue;
                }

                chosen.Add(manager.Exercises[choice - 1]);
            }

            if (chosen.Count == 0)
            {
                Console.WriteLine("⚠️ No exercises selected.");
                Console.ReadLine();
                return;
            }

            manager.AddWorkout(new Workout(workoutName, chosen));
            Console.WriteLine("\n✅ Workout created! Press Enter to continue...");
            Console.ReadLine();
        }

        static void ViewWorkouts(WorkoutManager manager)
        {
            Console.Clear();
            Console.WriteLine("📋 All Workouts:");
            manager.ListWorkouts();
            Console.WriteLine("\nPress Enter to return...");
            Console.ReadLine();
        }
    }
}
