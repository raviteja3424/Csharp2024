using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace marksoperationapp
{
    class marksoperation
    {
        public static void Main(string[] args)
        {
            int[] marks = new int[10];
            for (int i = 0; i < marks.Length; i++)
            {
                Console.WriteLine($"enter mark{i + 1}:");
                marks[i] = int.Parse(Console.ReadLine());
            }
            int total = calculateTotal(marks);
            double average = calculateAverage(marks);
            int min = FindMinimum(marks);
            int max = FindMaximum(marks);

            int[] ascending = sortAscending(marks);
            int[] descending = sortDescending(marks);

            Console.WriteLine("Total marks: " + total);
            Console.WriteLine("Average marks: " + average);
            Console.WriteLine("Minimum marks: " + min);
            Console.WriteLine("Maximum marks: " + max);



            Console.WriteLine("Marks in ascending order:" + string.Join(",", ascending));
            Console.WriteLine("Marks in descending order:" + string.Join(",", descending));
            Console.Read();
        }
        static int calculateTotal(int[] marks)
        {
            int sum = 0;
            foreach (int mark in marks)
            {
                sum += mark;
            }
            return sum;
        }
        static double calculateAverage(int[] marks)
        {
            return (double)calculateTotal(marks) / marks.Length;
        }
        static int FindMinimum(int[] marks)
        {
            int min = marks[0];
            foreach (int mark in marks)
            {
                if (mark < min)
                {
                    min = mark;
                }
            }
            return min;
        }
        static int FindMaximum(int[] marks)
        {
            int max = marks[0];
            foreach (int mark in marks)
            {
                if (mark > max)
                {
                    max = mark;
                }

            }
            return max;

        }
        static int[] sortAscending(int[] marks)
        {
            int[] sortedMarks = new int[marks.Length];
            Array.Copy(marks, sortedMarks, marks.Length);
            for (int i = 0; i < sortedMarks.Length - 1; i++)
            {
                for (int j = i + 1; j < sortedMarks.Length; j++)
                {
                    if (sortedMarks[i] > sortedMarks[j])
                    {
                        int temp = sortedMarks[i];
                        sortedMarks[i] = sortedMarks[j];
                        sortedMarks[j] = temp;
                    }

                }
            }
            return sortedMarks;
        }
        static int[] sortDescending(int[] marks)
        {
            int[] sortedMarks = sortAscending(marks);
            Array.Reverse(sortedMarks);
            return sortedMarks;

        }
    }
}}
