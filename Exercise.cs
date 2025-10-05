namespace WorkoutApp
{
    public class Exercise
    {
        public string Name { get; set; }
        public int Reps { get; set; }
        public int LengthMinutes { get; set; }
        public int CaloriesBurned { get; set; }
        public string EquipmentUsed { get; set; }

        public Exercise(string name, int reps, int lengthMinutes, int caloriesBurned, string equipmentUsed)
        {
            Name = name;
            Reps = reps;
            LengthMinutes = lengthMinutes;
            CaloriesBurned = caloriesBurned;
            EquipmentUsed = equipmentUsed;
        }

        public override string ToString()
        {
            return $"{Name} - {Reps} reps - {LengthMinutes} min - {CaloriesBurned} cal - Equipment: {EquipmentUsed}";
        }

        public string ToCsv()
        {
            return $"{Name},{Reps},{LengthMinutes},{CaloriesBurned},{EquipmentUsed}";
        }

        public static Exercise FromCsv(string line)
        {
            var parts = line.Split(',');
            return new Exercise(parts[0], int.Parse(parts[1]), int.Parse(parts[2]), int.Parse(parts[3]), parts[4]);
        }
    }
}
