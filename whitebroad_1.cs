namespace AverageRating
{
    using System;
    using System.Collections.Generic;

    public class Star
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int NumberOfStars { get; set; }

        public Star(int id, string name, int numberOfStars)
        {
            Id = id;
            Name = name;
            NumberOfStars = numberOfStars;
        }
    }

    public class AverageRating
    {
        public static void CalculateAverageRating()
        {
            List<Star> stars = new List<Star>();
            stars.Add(new Star(1, "A", 5));
            stars.Add(new Star(2, "B", 3));
            stars.Add(new Star(3, "C", 4));
            stars.Add(new Star(4, "D", 2));
            stars.Add(new Star(5, "E", 5));
            stars.Add(new Star(6, "F", 1));
            stars.Add(new Star(7, "G", 4));
            stars.Add(new Star(8, "H", 5));
            stars.Add(new Star(9, "I", 3));
            stars.Add(new Star(10, "J", 2));

            Console.WriteLine("Average rating: " + CalculateAverageRating(stars));
        }

        public static double CalculateAverageRating(List<Star> stars)
        {
            double sum = 0;
            foreach (Star star in stars)
            {
                sum += star.NumberOfStars;
            }
            return Adjust(sum / stars.Count);
        }

        public static double Adjust(double input)
        {
            double whole = Math.Truncate(input);
            double remainder = input - whole;
            if (remainder < 0.3)
            {
                remainder = 0;
            }
            else if (remainder < 0.8)
            {
                remainder = 0.5;
            }
            else
            {
                remainder = 1;
            }
            return whole + remainder;
        }
    }
}


