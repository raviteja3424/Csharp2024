using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assignment3
{
    class Student
    {

        public int RollNo { get; set; }
        public string Name { get; set; }
        public string ClassName { get; set; }
        public string Semester { get; set; }
        public string Branch { get; set; }
        public int[] Marks { get; set; }
        public Student(int rollNo, string name, string className, string semester, string branch)
        {
            RollNo = rollNo;
            Name = name;
            ClassName = className;
            Semester = semester;
            Branch = branch;
            Marks = new int[5];

        }
        public void GetMarks()
        {
            for (int i = 0; i < Marks.Length; i++)
            {
                Console.Write($"enter marks for subject {i + 1}: ");
                Marks[i] = Convert.ToInt32(Console.ReadLine());

            }

        }
        public void DisplayResult()
        {
            int total = 0;
            bool failed = false;
            for (int i = 0; i < Marks.Length; i++)
            {
                if (Marks[i] < 35)
                {
                    failed = true;
                    break;
                }
                total += Marks[i];
            }
            double average = total / 5.0;
            if (failed || average < 50)
            {
                Console.WriteLine("Result:Failed");
            }
            else
            {
                Console.WriteLine("Result:passed");
            }

        }
        public void DisplayData()
        {
            Console.WriteLine($"Roll No: {RollNo}");
            Console.WriteLine($"Name: {Name}");
            Console.WriteLine($"ClassName: {ClassName}");
            Console.WriteLine($"Semester: {Semester}");
            Console.WriteLine($"Branch: {Branch}");
            Console.WriteLine("Marks: " + string.Join(",", Marks));
            Console.ReadLine();
        }

    }
}