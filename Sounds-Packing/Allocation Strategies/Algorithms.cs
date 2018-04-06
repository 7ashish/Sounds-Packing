using System;
using System.Collections.Generic;
using System.IO;

static class Algorithms
{
    static public void First_Fit_Decreasing_Algorithm(Pair<string, TimeSpan>[] Line)
    {
        double TotalSecondsforeachFile = 100;
        List<Pair<string, TimeSpan>> Temp = new List<Pair<string, TimeSpan>>();
        Array.Sort(Line);       //O(nlogn) where n = Line.Count
        List<List<Pair<string, TimeSpan>>> Files_List = new List<List<Pair<string, TimeSpan>>>();
        int counter = 0;
        List<double> seconds = new List<double>();
        for (int i = 0; i < Line.Length; i++)
        {
            if (Files_List.Count == 0)
            {
                Temp.Add(Line[i]);
                Files_List.Add(Temp);
                seconds.Add(Line[i].Second.TotalSeconds);
                continue;
            }
            if (TotalSecondsforeachFile == seconds[counter])
            {
                if (counter + 1 == Files_List.Count)
                {
                    counter++;
                    Temp = new List<Pair<string, TimeSpan>>();
                    Temp.Add(Line[i]);
                    Files_List.Add(Temp);
                    seconds.Add(Line[i].Second.TotalSeconds);
                    continue;
                }
            }
            if ((seconds[counter] + Line[i].Second.TotalSeconds) <= TotalSecondsforeachFile)
            {
                Files_List[counter].Add(Line[i]);
                seconds[counter] += Line[i].Second.TotalSeconds;
                if((seconds[counter] + Line[i].Second.TotalSeconds) == TotalSecondsforeachFile)
                {
                    counter++;
                }
                continue;
            }
            if ((seconds[counter] + Line[i].Second.TotalSeconds) > TotalSecondsforeachFile)
            {
                if (counter + 1 == Files_List.Count)
                {
                    Temp = new List<Pair<string, TimeSpan>>();
                    Temp.Add(Line[i]);
                    Files_List.Add(Temp);
                    seconds.Add(Line[i].Second.TotalSeconds);
                    continue;
                }
                bool entered = false;
                int t = 0;
                for (int k =0; k < Files_List.Count; k++)
                {
                    if ((seconds[k] + Line[i].Second.TotalSeconds) < TotalSecondsforeachFile)
                    {
                        Files_List[k].Add(Line[i]);
                        seconds[k] += Line[i].Second.TotalSeconds;
                        entered = true;
                        break;
                    }
                    if ((seconds[k] + Line[i].Second.TotalSeconds) == TotalSecondsforeachFile)
                    {
                        Files_List[k].Add(Line[i]);
                        seconds[k] += Line[i].Second.TotalSeconds;
                        entered = true;
                        break;
                    }
                    t = k;
                }
                if (!entered)
                {
                    Temp[counter]=Line[i];
                    Files_List.Add(Temp);
                    seconds.Add(Line[i].Second.TotalSeconds);
                    continue;
                }
            }
        }
        string filepath = Directory.GetCurrentDirectory() + @"\sample 1\INPUT\Audios";
        for (int i = 0; i < Files_List.Count; i++)
        {
            Directory.CreateDirectory("F"+@i);
        }
    }
    static public void Best_Fit_Algorithm(Pair<string, TimeSpan>[] Line)
    {

    }
    static public void Best_Fit_Decreasing_Algorithm(Pair<string, TimeSpan>[] Line)
    {

    }
    static public void Worst_Fit_Algorithm(Pair<string, TimeSpan>[] Line)
    {

    }
    static public void Worst_Fit_Deacreasing_Algorithm(Pair<string, TimeSpan>[] Line)
    {

    }
}