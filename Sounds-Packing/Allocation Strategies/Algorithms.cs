using System;
using System.Collections.Generic;

static class Algorithms
{
    static public void First_Fit_Decreasing_Algorithm(Pair<string, TimeSpan>[] Line)
    {
        double FolderLength = 100;
        List<Pair<string, TimeSpan>> Temp = new List<Pair<string, TimeSpan>>();
        Array.Sort(Line);       //O(nlogn) where n = Line.Count
        List<List<Pair<string, TimeSpan>>> FolderList = new List<List<Pair<string, TimeSpan>>>();
        int counter = 0;
        List<double> Seconds = new List<double>();
        //n = FileCount, m = FolderCount
        for (int i = 0; i < Line.Length; i++) // O(n * (n + m)) ~ O(n^2 + n*m)
        {
            if (FolderList.Count == 0)  //O(n)
            {
                Temp.Add(Line[i]);
                FolderList.Add(Temp);
                Seconds.Add(Line[i].Second.TotalSeconds);
                continue;
            }
            if (FolderLength == Seconds[counter]) //O(n)
            {
                if (counter + 1 == FolderList.Count)
                {
                    counter++;
                    Temp = new List<Pair<string, TimeSpan>>();
                    Temp.Add(Line[i]);
                    FolderList.Add(Temp);
                    Seconds.Add(Line[i].Second.TotalSeconds);
                    continue;
                }
            }
            if ((Seconds[counter] + Line[i].Second.TotalSeconds) <= FolderLength) //O(n)
            {
                FolderList[counter].Add(Line[i]);
                Seconds[counter] += Line[i].Second.TotalSeconds;
                continue;
            }
            if ((Seconds[counter] + Line[i].Second.TotalSeconds) > FolderLength) //O(n + m)
            {
                if (counter + 1 == FolderList.Count)//O(n)
                {
                    Temp = new List<Pair<string, TimeSpan>>();
                    Temp.Add(Line[i]);
                    FolderList.Add(Temp);
                    Seconds.Add(Line[i].Second.TotalSeconds);
                    continue;
                }
                bool entered = false;
                for (int k =0; k < FolderList.Count; k++) //O(m + n)
                {
                    if ((Seconds[k] + Line[i].Second.TotalSeconds) <= FolderLength) //O(n)
                    {
                        FolderList[k].Add(Line[i]);
                        Seconds[k] += Line[i].Second.TotalSeconds;
                        entered = true;
                        break;
                    }
                }
                if (!entered) //O(n)
                {
                    counter++;
                    Temp = new List<Pair<string, TimeSpan>>();
                    Temp.Add(Line[i]);
                    FolderList.Add(Temp);
                    Seconds.Add(Line[i].Second.TotalSeconds);
                    continue;
                }
                
            }
        }
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