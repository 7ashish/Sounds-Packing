using System;
using System.Collections.Generic;

static class Algorithms
{
    static public void First_Fit_Decreasing_Algorithm(Pair<string, TimeSpan>[] Line)
    {
        double FolderLength = 10000000000;
        List<Pair<string, TimeSpan>> Temp = new List<Pair<string, TimeSpan>>(Line.Length);
        Array.Sort(Line);       //O(nlogn) where n = Line.Count
        List<List<Pair<string, TimeSpan>>> FolderList = new List<List<Pair<string, TimeSpan>>>(Line.Length);
        int counter = 0;
        List<double> Seconds = new List<double>(Line.Length);
        //n = FileCount, m = FolderCount
        for (int i = 0; i < Line.Length; i++) // O(n * m)
        {
            if (FolderList.Count == 0)  //O(1)
            {
                Temp.Add(Line[i]);
                FolderList.Add(Temp);
                Seconds.Add(Line[i].Second.TotalSeconds);
                continue;
            }
            if (FolderLength == Seconds[counter]) //O(1)
            {
                if (counter + 1 == FolderList.Count)
                {
                    counter++;
                    Temp = new List<Pair<string, TimeSpan>>(Line.Length);
                    Temp.Add(Line[i]);
                    FolderList.Add(Temp);
                    Seconds.Add(Line[i].Second.TotalSeconds);
                    continue;
                }
            }
            if ((Seconds[counter] + Line[i].Second.TotalSeconds) <= FolderLength) //O(1)
            {
                FolderList[counter].Add(Line[i]);
                Seconds[counter] += Line[i].Second.TotalSeconds;
                continue;
            }
            if ((Seconds[counter] + Line[i].Second.TotalSeconds) > FolderLength) //O(m)
            {
                if (counter + 1 == FolderList.Count)//O(1)
                {
                    Temp = new List<Pair<string, TimeSpan>>(Line.Length);
                    Temp.Add(Line[i]);
                    FolderList.Add(Temp);
                    Seconds.Add(Line[i].Second.TotalSeconds);
                    continue;
                }
                bool entered = false;
                for (int k =0; k < FolderList.Count; k++) //O(m)
                {
                    if ((Seconds[k] + Line[i].Second.TotalSeconds) <= FolderLength) //O(1)
                    {
                        FolderList[k].Add(Line[i]);
                        Seconds[k] += Line[i].Second.TotalSeconds;
                        entered = true;
                        break;
                    }
                }
                if (!entered) //O(1)
                {
                    counter++;
                    Temp = new List<Pair<string, TimeSpan>>(Line.Length);
                    Temp.Add(Line[i]);
                    FolderList.Add(Temp);
                    Seconds.Add(Line[i].Second.TotalSeconds);
                    continue;
                }
                
            }
        }
        //shrinking lists to the exact count of elements
        foreach(List<Pair<string, TimeSpan>> lp in FolderList)
        {
            lp.TrimExcess();
        }
        //shrinking folder list
        FolderList.TrimExcess();
        //clearing seconds list because it was never nessecary........
        Seconds.Clear();
        FileOperations.FinializeDirectory(FolderList, @"Sample 3\INPUT\Audios");
    }
    static public void Best_Fit_Algorithm(Pair<string, TimeSpan>[] Line)
    {
        throw new NotImplementedException();
    }
    static public void Best_Fit_Decreasing_Algorithm(Pair<string, TimeSpan>[] Line)
    {
        throw new NotImplementedException();
    }
    static public void Worst_Fit_Algorithm(Pair<string, TimeSpan>[] Line)
    {
        throw new NotImplementedException();
    }
    static public void Worst_Fit_Deacreasing_Algorithm(Pair<string, TimeSpan>[] Line)
    {
        throw new NotImplementedException();
    }
}