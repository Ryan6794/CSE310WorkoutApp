using System;
using System.Collections.Generic;
using System.Linq;

namespace WorkoutPlannerWPF
{
    public class Workout
    {
        public string Name { get; set; }
        public List<Exercise> Exercises { get; set; }

        public Workout(string name, List<Exercise> exercises)
        {
            Name = name;
            Exercises = exercises;
        }

        public int TotalCalories => Exercises.Sum(e => e.CaloriesBurned);
        public int TotalLength => Exercises.Sum(e => e.LengthMinutes);
        public string AllEquipment
        {
            get
            {
                var equipmentList = Exercises
                    .Select(e => e.EquipmentUsed)
                    .Distinct()
                    .ToList();

                // If there’s any real equipment, remove "None"
                if (equipmentList.Count > 1 && equipmentList.Contains("None"))
                {
                    equipmentList.Remove("None");
                }

                return string.Join(", ", equipmentList);
            }
        }

        public override string ToString()
        {
            return $"{Name} - {Exercises.Count} exercises - {TotalLength} min - {TotalCalories} cal - Equipment: {AllEquipment}";
        }

        public string ToCsv()
        {
            var exerciseNames = string.Join("|", Exercises.Select(e => e.Name));
            return $"{Name},{exerciseNames}";
        }

        public static Workout FromCsv(string line, List<Exercise> allExercises)
        {
            var parts = line.Split(',');
            var name = parts[0];
            var exerciseNames = parts[1].Split('|');
            var exercises = allExercises.Where(e => exerciseNames.Contains(e.Name)).ToList();
            return new Workout(name, exercises);
        }
    }
}
