using System;
using System.IO;
using System.Collections.Generic;

namespace Sounds_Packing
{
    class Program
    {
        static void Main(string[] args)
        {
            Pair<string, TimeSpan>[] Line;
            string folderPath = Directory.GetCurrentDirectory() + @"\sample 1\INPUT";
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
            Array.Sort(Line);       //O(nlogn) where n = Line.Count
            foreach (Pair<string, TimeSpan> s in Line)
            {
                Console.WriteLine(s.First);
                Console.WriteLine(s.Second.ToString());
            }
            List<List<string>> Folders_List = new List<List<string>>();
            List<string> Files_List = new List<string>();
        }
    }

}
