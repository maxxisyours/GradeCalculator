using System;
using System.Collections.Generic;
using System.Linq;

namespace GradeCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            // Updated modules
            List<Module> modules = new List<Module>
            {
                new Module("CET254", "Cybersecurity", 20, 67),
                new Module("CET255", "Digital Forensics", 20, 72),
                new Module("CET256", "Secure Networks", 20, 45),
                new Module("CET257", "Digital Forensics Computer", 20, 60),
            };

            // Not the most optimized way, but it works :)
            double averageMark = CalculateWeightedAverage(modules);
            Console.WriteLine($"Average Mark: {averageMark:F2}");

            // Remove lowest mark and calculate again
            double bestAverageMark = CalculateBestAverage(modules);
            Console.WriteLine($"Best Average (excluding lowest mark): {bestAverageMark:F2}");

            GradeClassification classification = GetClassification(averageMark);
            Console.WriteLine($"Classification: {classification}");

            GradeClassification bestClassification = GetClassification(bestAverageMark);
            Console.WriteLine($"Best Classification: {bestClassification}");
        }

        static double CalculateWeightedAverage(List<Module> modules)
        {
            // Some basic calc
            double totalWeightedMarks = modules.Sum(m => m.Mark * m.Credits);
            double totalCredits = modules.Sum(m => m.Credits);
            return totalWeightedMarks / totalCredits;
        }

        static double CalculateBestAverage(List<Module> modules)
        {
            // Just skipping the worst grade for best average
            var bestModules = modules.OrderByDescending(m => m.Mark).Skip(1).ToList();
            return CalculateWeightedAverage(bestModules);
        }

        static GradeClassification GetClassification(double mark)
        {
            // Simple classification based on mark
            if (mark >= 70) return GradeClassification._1;
            if (mark >= 60) return GradeClassification._21;
            if (mark >= 50) return GradeClassification._22;
            if (mark >= 40) return GradeClassification._3;
            return GradeClassification._U;
        }
    }

    class Module
    {
        public string ID { get; set; }
        public string Title { get; set; }
        public int Credits { get; set; }
        public double Mark { get; set; }

        public Module(string id, string title, int credits, double mark)
        {
            ID = id;
            Title = title;
            Credits = credits;
            Mark = mark;
        }
    }

    enum GradeClassification
    {
        _1,   // First-class (>=70)
        _21,  // Upper second-class (>=60)
        _22,  // Lower second-class (>=50)
        _3,   // Third-class (>=40)
        _U    // Fail (<40)
    }
}
