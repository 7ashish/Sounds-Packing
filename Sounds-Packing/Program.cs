using System;
using System.IO;

namespace Sounds_Packing
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Yeah boiiiiiiiiiiiiiiiiiiiiiii");
            Pair<string, TimeSpan>[] Line;
            string folderPath = Directory.GetCurrentDirectory() + @"\Sample 3\INPUT";
            {
                FileStream file = new FileStream(folderPath + @"\AudiosInfo.txt", FileMode.Open, FileAccess.Read);
                StreamReader reader = new StreamReader(file);
                int n = int.Parse(reader.ReadLine());
                Line = new Pair<string, TimeSpan>[n];
                for (int i = 0; i < n; i++)  //O(n)
                {
                    string[] fields = reader.ReadLine().Split(' ');
                    Line[i] = new Pair<string, TimeSpan>()
                    {
                        First = fields[0],
                        Second = TimeSpan.Parse(fields[1])
                    };
                }
                reader.Close();
            }
            Algorithms.First_Fit_Decreasing_Algorithm((Pair<string, TimeSpan>[])Line.Clone());
        }
    }

}
