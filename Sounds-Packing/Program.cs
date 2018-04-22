using System;
using System.IO;

namespace Sounds_Packing
{
    class Program
    {
        static void Main(string[] args)
        {
            Pair<string, TimeSpan>[] Line;
            for (int j = 1; j <= 3; j++)
            {
                //Console.WriteLine("->Sample #" + j.ToString());
                string folderPath = Directory.GetCurrentDirectory() + @"\Sample " + j.ToString() + @"\INPUT";
                FileOperations.DefaultPath = folderPath + @"\Audios";
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
                /*
                FileOperations.CleanUp(folderPath + @"\Audios\");
                Algorithms.Best_Fit_Decreasing_Algorithm((Pair<string, TimeSpan>[])Line.Clone());
                if (!FileOperations.CheckAnswer(folderPath + @"\Audios", folderPath.Substring(0, folderPath.LastIndexOf('\\')) + @"\output\[2] worstfit decreasing"))
                {
                    Console.WriteLine("Error !");
                }
                FileOperations.CleanUp(folderPath + @"\Audios\");
                Algorithms.Best_Fit_Algorithm_Priority_Queue((Pair<string, TimeSpan>[])Line.Clone());
                if (!FileOperations.CheckAnswer(folderPath + @"\Audios", folderPath.Substring(0, folderPath.LastIndexOf('\\')) + @"\output\[2] worstfit decreasing"))
                {
                    Console.WriteLine("Error !");
                }
                FileOperations.CleanUp(folderPath + @"\Audios\");
                Algorithms.Best_Fit_Decreasing_Algorithm_Priority_Queue((Pair<string, TimeSpan>[])Line.Clone());
                if (!FileOperations.CheckAnswer(folderPath + @"\Audios", folderPath.Substring(0, folderPath.LastIndexOf('\\')) + @"\output\[2] worstfit decreasing"))
                {
                    Console.WriteLine("Error !");
                }
                
                FileOperations.CleanUp(folderPath + @"\Audios\");
                Algorithms.Worst_Fit_Algorithm((Pair<string, TimeSpan>[])Line.Clone());
               /* if (!FileOperations.CheckAnswer(folderPath + @"\Audios", folderPath.Substring(0, folderPath.LastIndexOf('\\')) + @"\OUTPUT\[1] worstfit"))
                {
                    Console.WriteLine("Worest Fit Error !");
                }
                else
                {
                    Console.WriteLine("Worest Fit Correct!");
                }*/
                FileOperations.CleanUp(folderPath + @"\Audios\");
                Algorithms.Worst_Fit_Algorithm_Priority_Queue((Pair<string, TimeSpan>[])Line.Clone());
                /*if (!FileOperations.CheckAnswer(folderPath + @"\Audios", folderPath.Substring(0, folderPath.LastIndexOf('\\')) + @"\OUTPUT\[1] worstfit"))
                {
                    Console.WriteLine("Worest Fit PQ Error !");
                }
                else
                {
                    Console.WriteLine("Worest Fit PQ Correct!");
                }/*
                FileOperations.CleanUp(folderPath + @"\Audios\");
                Algorithms.Worst_Fit_Decreasing_Algorithm_Priority_Queue((Pair<string, TimeSpan>[])Line.Clone());
                if (!FileOperations.CheckAnswer(folderPath + @"\Audios", folderPath.Substring(0, folderPath.LastIndexOf('\\')) + @"\OUTPUT\[2] worstfit decreasing"))
                {
                    Console.WriteLine("Worest Firt Decreasing PQ Error !");
                }
                else
                {
                    Console.WriteLine("Worest Fit Decreasing PQ Correct!");
                }
                FileOperations.CleanUp(folderPath + @"\Audios\");
                Algorithms.First_Fit_Decreasing_Algorithm((Pair<string, TimeSpan>[])Line.Clone());
                if (!FileOperations.CheckAnswer(folderPath + @"\Audios", folderPath.Substring(0, folderPath.LastIndexOf('\\')) + @"\OUTPUT\[3] firstfit decreasing"))
                {
                    Console.WriteLine("First Firt Decreasing Error !");
                }
                else
                {
                    Console.WriteLine("First Fit Decreasing Correct!");
                }
                FileOperations.CleanUp(folderPath + @"\Audios\");*/
            }
        }
    }

}
