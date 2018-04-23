using System;
using System.Collections;
using System.Collections.Generic;

static class Algorithms
{
    static public int Max_Folder_Length = 152;
    static public void First_Fit_Decreasing_Algorithm(Pair<string, TimeSpan>[] Line)
    {
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
            if (Max_Folder_Length == Seconds[counter]) //O(1)
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
            if ((Seconds[counter] + Line[i].Second.TotalSeconds) <= Max_Folder_Length) //O(1)
            {
                FolderList[counter].Add(Line[i]);
                Seconds[counter] += Line[i].Second.TotalSeconds;
                continue;
            }
            if ((Seconds[counter] + Line[i].Second.TotalSeconds) > Max_Folder_Length) //O(m)
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
                for (int k = 0; k < FolderList.Count; k++) //O(m)
                {
                    if ((Seconds[k] + Line[i].Second.TotalSeconds) <= Max_Folder_Length) //O(1)
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
        foreach (List<Pair<string, TimeSpan>> lp in FolderList)
        {
            lp.TrimExcess();
        }
        //shrinking folder list
        FolderList.TrimExcess();
        //clearing seconds list because it was never nessecary........
        Seconds.Clear();
        FileOperations.FinializeDirectory(FolderList);
    }
    static public void Best_Fit_Algorithm(Pair<string, TimeSpan>[] Line)
    {
        List<Pair<string, TimeSpan>> Temp = new List<Pair<string, TimeSpan>>(Line.Length);
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
                Seconds.Add(Max_Folder_Length - Line[i].Second.TotalSeconds);
                continue;
            }
            if (FolderList.Count == 1)
            {
                if (Line[i].Second.TotalSeconds <= Seconds[counter])
                {
                    FolderList[counter].Add(Line[i]);
                    Seconds[counter] -= Line[i].Second.TotalSeconds;
                    if (Max_Folder_Length + Seconds[counter] == 0)
                    {
                        Temp = new List<Pair<string, TimeSpan>>(Line.Length);
                        Temp.Add(Line[i]);
                        FolderList.Add(Temp);
                        Seconds.Add(100);
                        counter++;
                    }
                    continue;
                }
                else
                {
                    Temp = new List<Pair<string, TimeSpan>>(Line.Length);
                    Temp.Add(Line[i]);
                    FolderList.Add(Temp);
                    Seconds.Add(Max_Folder_Length - Line[i].Second.TotalSeconds);
                    continue;
                }
            }
            bool Done = false;
            double min = Max_Folder_Length;
            int index = 0;
            for (int k = 0; k < FolderList.Count; k++)
            {
                if (Seconds[k] < Line[i].Second.TotalSeconds)
                {
                    continue;
                }
                if (Seconds[k] - Line[i].Second.TotalSeconds == Max_Folder_Length)
                {
                    FolderList[k].Add(Line[i]);
                    Seconds[k] = 0;
                    Done = true;
                    if (k == counter)
                    {
                        Temp = new List<Pair<string, TimeSpan>>(Line.Length);
                        Temp.Add(Line[i]);
                        FolderList.Add(Temp);
                        Seconds.Add(100);
                        counter++;
                    }
                    break;
                }
                if (Seconds[k] - Line[i].Second.TotalSeconds < min)
                {
                    min = Seconds[k] - Line[i].Second.TotalSeconds;
                    index = k;
                }

            }
            if (!Done)
            {
                if (min == Max_Folder_Length)
                {
                    Temp = new List<Pair<string, TimeSpan>>(Line.Length);
                    Temp.Add(Line[i]);
                    FolderList.Add(Temp);
                    Seconds.Add(Max_Folder_Length - Line[i].Second.TotalSeconds);
                    counter++;
                }
                else
                {
                    FolderList[index].Add(Line[i]);
                    Seconds[index] -= Line[i].Second.TotalSeconds;
                    if (Max_Folder_Length + Seconds[counter] == 0)
                    {
                        Temp = new List<Pair<string, TimeSpan>>(Line.Length);
                        Temp.Add(Line[i]);
                        FolderList.Add(Temp);
                        Seconds.Add(100);
                        counter++;
                    }
                }
            }
        }
        //shrinking lists to the exact count of elements
        foreach (List<Pair<string, TimeSpan>> lp in FolderList)
        {
            lp.TrimExcess();
        }
        //shrinking folder list
        FolderList.TrimExcess();
        //clearing seconds list because it was never nessecary........
        Seconds.Clear();
        FileOperations.FinializeDirectory(FolderList);
    }
    static public void Best_Fit_Decreasing_Algorithm(Pair<string, TimeSpan>[] Line)
    {
        Array.Sort(Line);
        List<Pair<string, TimeSpan>> Temp = new List<Pair<string, TimeSpan>>(Line.Length);
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
                Seconds.Add(Max_Folder_Length - Line[i].Second.TotalSeconds);
                continue;
            }
            if (FolderList.Count == 1)
            {
                if (Line[i].Second.TotalSeconds <= Seconds[counter])
                {
                    FolderList[counter].Add(Line[i]);
                    Seconds[counter] -= Line[i].Second.TotalSeconds;
                    if (Max_Folder_Length + Seconds[counter] == 0)
                    {
                        Temp = new List<Pair<string, TimeSpan>>(Line.Length);
                        Temp.Add(Line[i]);
                        FolderList.Add(Temp);
                        Seconds.Add(100);
                        counter++;
                    }
                    continue;
                }
                else
                {
                    Temp = new List<Pair<string, TimeSpan>>(Line.Length);
                    Temp.Add(Line[i]);
                    FolderList.Add(Temp);
                    Seconds.Add(Max_Folder_Length - Line[i].Second.TotalSeconds);
                    continue;
                }
            }
            bool Done = false;
            double min = Max_Folder_Length;
            int index = 0;
            for (int k = 0; k < FolderList.Count; k++)
            {
                if (Seconds[k] < Line[i].Second.TotalSeconds)
                {
                    continue;
                }
                if (Seconds[k] - Line[i].Second.TotalSeconds == Max_Folder_Length)
                {
                    FolderList[k].Add(Line[i]);
                    Seconds[k] = 0;
                    Done = true;
                    if (k == counter)
                    {
                        Temp = new List<Pair<string, TimeSpan>>(Line.Length);
                        Temp.Add(Line[i]);
                        FolderList.Add(Temp);
                        Seconds.Add(100);
                        counter++;
                    }
                    break;
                }
                if (Seconds[k] - Line[i].Second.TotalSeconds < min)
                {
                    min = Seconds[k] - Line[i].Second.TotalSeconds;
                    index = k;
                }

            }
            if (!Done)
            {
                if (min == Max_Folder_Length)
                {
                    Temp = new List<Pair<string, TimeSpan>>(Line.Length);
                    Temp.Add(Line[i]);
                    FolderList.Add(Temp);
                    Seconds.Add(Max_Folder_Length - Line[i].Second.TotalSeconds);
                    counter++;
                }
                else
                {
                    FolderList[index].Add(Line[i]);
                    Seconds[index] -= Line[i].Second.TotalSeconds;
                    if (Max_Folder_Length + Seconds[counter] == 0)
                    {
                        Temp = new List<Pair<string, TimeSpan>>(Line.Length);
                        Temp.Add(Line[i]);
                        FolderList.Add(Temp);
                        Seconds.Add(100);
                        counter++;
                    }
                }
            }
        }
        //shrinking lists to the exact count of elements
        foreach (List<Pair<string, TimeSpan>> lp in FolderList)
        {
            lp.TrimExcess();
        }
        //shrinking folder list
        FolderList.TrimExcess();
        //clearing seconds list because it was never nessecary........
        Seconds.Clear();
        FileOperations.FinializeDirectory(FolderList);
    }
    static public void Worst_Fit_Algorithm(Pair<string, TimeSpan>[] Line)
    {
        List<Pair<string, TimeSpan>> Temp = new List<Pair<string, TimeSpan>>(Line.Length);
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
                Seconds.Add(Max_Folder_Length - Line[i].Second.TotalSeconds);
                continue;
            }
            if (FolderList.Count == 1)
            {
                if (Seconds[counter] > Line[i].Second.TotalSeconds)
                {
                    FolderList[counter].Add(Line[i]);
                    Seconds[counter] -= Line[i].Second.TotalSeconds;
                    continue;
                }
                else
                {
                    Temp = new List<Pair<string, TimeSpan>>(Line.Length);
                    Temp.Add(Line[i]);
                    FolderList.Add(Temp);
                    Seconds.Add(Max_Folder_Length - Line[i].Second.TotalSeconds);
                    continue;
                }
            }
            if (Seconds[counter] == Max_Folder_Length)
            {
                if (counter + 1 == FolderList.Count)
                {
                    Temp = new List<Pair<string, TimeSpan>>(Line.Length);
                    Temp.Add(Line[i]);
                    FolderList.Add(Temp);
                    Seconds.Add(Max_Folder_Length - Line[i].Second.TotalSeconds);
                    continue;
                }
                counter++;
                continue;
            }
            int index = 0;
            double max = 0;
            for (int k = 0; k < Seconds.Count; k++)
            {
                if (Seconds[k] < Line[i].Second.TotalSeconds)
                {
                    continue;
                }
                if (Seconds[k] > max)
                {
                    max = Seconds[k];
                    index = k;
                }
            }
            if (max != 0)
            {
                FolderList[index].Add(Line[i]);
                Seconds[index] -= Line[i].Second.TotalSeconds;
                continue;
            }
            else
            {
                Temp = new List<Pair<string, TimeSpan>>(Line.Length);
                Temp.Add(Line[i]);
                FolderList.Add(Temp);
                Seconds.Add(Max_Folder_Length - Line[i].Second.TotalSeconds);
                continue;
            }
        }
        //shrinking lists to the exact count of elements
        foreach (List<Pair<string, TimeSpan>> lp in FolderList)
        {
            lp.TrimExcess();
        }
        //shrinking folder list
        FolderList.TrimExcess();
        //clearing seconds list because it was never nessecary........
        Seconds.Clear();
        FileOperations.FinializeDirectory(FolderList);
    }
    static public void Worst_Fit_Deacreasing_Algorithm(Pair<string, TimeSpan>[] Line)
    {
        Array.Sort(Line);
        List<Pair<string, TimeSpan>> Temp = new List<Pair<string, TimeSpan>>(Line.Length);
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
                Seconds.Add(Max_Folder_Length - Line[i].Second.TotalSeconds);
                continue;
            }
            if (FolderList.Count == 1)
            {
                if (Seconds[counter] > Line[i].Second.TotalSeconds)
                {
                    FolderList[counter].Add(Line[i]);
                    Seconds[counter] -= Line[i].Second.TotalSeconds;
                    continue;
                }
                else
                {
                    Temp = new List<Pair<string, TimeSpan>>(Line.Length);
                    Temp.Add(Line[i]);
                    FolderList.Add(Temp);
                    Seconds.Add(Max_Folder_Length - Line[i].Second.TotalSeconds);
                    continue;
                }
            }
            if (Seconds[counter] == 0)
            {
                if (counter + 1 == FolderList.Count)
                {
                    Temp = new List<Pair<string, TimeSpan>>(Line.Length);
                    Temp.Add(Line[i]);
                    FolderList.Add(Temp);
                    Seconds.Add(Max_Folder_Length - Line[i].Second.TotalSeconds);
                    continue;
                }
                counter++;
                continue;
            }
            int index = 0;
            double max = 0;
            for (int k = 0; k < Seconds.Count; k++)
            {
                if (Seconds[k] < Line[i].Second.TotalSeconds)
                {
                    continue;
                }
                if (Seconds[k] > max)
                {
                    max = Seconds[k];
                    index = k;
                }
            }
            if (max != 0)
            {
                FolderList[index].Add(Line[i]);
                Seconds[index] -= Line[i].Second.TotalSeconds;
                continue;
            }
            else
            {
                Temp = new List<Pair<string, TimeSpan>>(Line.Length);
                Temp.Add(Line[i]);
                FolderList.Add(Temp);
                Seconds.Add(Max_Folder_Length - Line[i].Second.TotalSeconds);
                continue;
            }
        }
        //shrinking lists to the exact count of elements
        foreach (List<Pair<string, TimeSpan>> lp in FolderList)
        {
            lp.TrimExcess();
        }
        //shrinking folder list
        FolderList.TrimExcess();
        //clearing seconds list because it was never nessecary........
        Seconds.Clear();
        FileOperations.FinializeDirectory(FolderList);
    }
    static public void Worst_Fit_Algorithm_Priority_Queue(Pair<string, TimeSpan>[] Line)
    {
        SortedSet<Pair<int, double>> PQueue = new SortedSet<Pair<int, double>>();
        List<Pair<string, TimeSpan>> Temp = new List<Pair<string, TimeSpan>>(Line.Length);
        List<List<Pair<string, TimeSpan>>> FolderList = new List<List<Pair<string, TimeSpan>>>(Line.Length);
        Pair<int, double> Tempo = new Pair<int, double>();
        for (int i = 0; i < Line.Length; i++)
        {
            if (FolderList.Count == 0)
            {
                Temp.Add(Line[i]);
                FolderList.Add(Temp);
                Tempo.First = 0;
                Tempo.Second = Max_Folder_Length - Line[i].Second.TotalSeconds;
                PQueue.Add(Tempo);
                continue;
            }
            if (PQueue.Count != 0)
            {
                if (PQueue.Min.Second >= Line[i].Second.TotalSeconds)
                {
                    FolderList[PQueue.Min.First].Add(Line[i]);
                    if (PQueue.Min.Second == Line[i].Second.TotalSeconds)
                    {
                        PQueue.Remove(PQueue.Min);
                        continue;
                    }
                    var P = PQueue.Min;
                    Tempo = new Pair<int, double>
                    {
                        First = P.First,
                        Second = P.Second
                    };
                    Tempo.Second -= Line[i].Second.TotalSeconds;
                    PQueue.Remove(PQueue.Min);
                    PQueue.Add(Tempo);
                    continue;
                }
                else
                {
                    Temp = new List<Pair<string, TimeSpan>>();
                    Tempo = new Pair<int, double>();
                    Temp.Add(Line[i]);
                    FolderList.Add(Temp);
                    Tempo.First = FolderList.Count - 1;
                    Tempo.Second = Max_Folder_Length - Line[i].Second.TotalSeconds;
                    PQueue.Add(Tempo);
                    continue;
                }

            }
            else
            {
                Temp = new List<Pair<string, TimeSpan>>();
                Tempo = new Pair<int, double>();
                Temp.Add(Line[i]);
                FolderList.Add(Temp);
                Tempo.First = FolderList.Count - 1;
                Tempo.Second = Max_Folder_Length - Line[i].Second.TotalSeconds;
                PQueue.Add(Tempo);
                continue;
            }

        }
        //shrinking lists to the exact count of elements
        foreach (List<Pair<string, TimeSpan>> lp in FolderList)
        {
            lp.TrimExcess();

        }
        //shrinking folder list
        FolderList.TrimExcess();
        FileOperations.FinializeDirectory(FolderList);
    }
    static public void Worst_Fit_Decreasing_Algorithm_Priority_Queue(Pair<string, TimeSpan>[] Line)
    {
        Array.Sort(Line);
        SortedSet<Pair<int, double>> PQueue = new SortedSet<Pair<int, double>>();
        List<Pair<string, TimeSpan>> Temp = new List<Pair<string, TimeSpan>>(Line.Length);
        List<List<Pair<string, TimeSpan>>> FolderList = new List<List<Pair<string, TimeSpan>>>(Line.Length);
        Pair<int, double> Tempo = new Pair<int, double>();
        for (int i = 0; i < Line.Length; i++)
        {
            if (FolderList.Count == 0)
            {
                Temp.Add(Line[i]);
                FolderList.Add(Temp);
                Tempo.First = 0;
                Tempo.Second = Max_Folder_Length - Line[i].Second.TotalSeconds;
                PQueue.Add(Tempo);
                continue;
            }
            if (PQueue.Count != 0)
            {
                if (PQueue.Min.Second >= Line[i].Second.TotalSeconds)
                {
                    FolderList[PQueue.Min.First].Add(Line[i]);
                    if (PQueue.Min.Second == Line[i].Second.TotalSeconds)
                    {
                        PQueue.Remove(PQueue.Min);
                        continue;
                    }
                    var P = PQueue.Min;
                    Tempo = new Pair<int, double>
                    {
                        First = P.First,
                        Second = P.Second
                    };
                    Tempo.Second -= Line[i].Second.TotalSeconds;
                    PQueue.Remove(PQueue.Min);
                    PQueue.Add(Tempo);
                    continue;
                }
                else
                {
                    Temp = new List<Pair<string, TimeSpan>>();
                    Tempo = new Pair<int, double>();
                    Temp.Add(Line[i]);
                    FolderList.Add(Temp);
                    Tempo.First = FolderList.Count - 1;
                    Tempo.Second = Max_Folder_Length - Line[i].Second.TotalSeconds;
                    PQueue.Add(Tempo);
                    continue;
                }
            }
            else
            {
                Temp = new List<Pair<string, TimeSpan>>();
                Tempo = new Pair<int, double>();
                Temp.Add(Line[i]);
                FolderList.Add(Temp);
                Tempo.First = FolderList.Count - 1;
                Tempo.Second = Max_Folder_Length - Line[i].Second.TotalSeconds;
                PQueue.Add(Tempo);
                continue;
            }
        }
        //shrinking lists to the exact count of elements
        foreach (List<Pair<string, TimeSpan>> lp in FolderList)
        {
            lp.TrimExcess();
        }
        //shrinking folder list
        FolderList.TrimExcess();
        FileOperations.FinializeDirectory(FolderList);
    }
    static public void Folder_Filling_Algorithm(Pair<string, TimeSpan>[] Line)
    {
        int Count = Line.Length;
        bool[] Selected = new bool[Line.Length + 5];
        int[,] dp = new int[Line.Length + 5, Max_Folder_Length + 5];
        for (int i = 0; i < Line.Length; i++)
        {
            Selected[i] = false;
        }
        void IterativeKnapsack()
        {
            for (int i = Line.Length; i >= 0; i--)
            {
                for (int w = Max_Folder_Length; w >= 0; w--)
                {
                    if (i == Line.Length)
                        dp[i, w] = 0;
                    else if (Selected[i])
                        dp[i, w] = dp[i + 1, w];
                    else
                    {
                        int ans = 0, time = (int)Line[i].Second.TotalSeconds;
                        if (w + time <= Max_Folder_Length)
                            ans = dp[i + 1, w + time] + time;
                        dp[i, w] = Math.Max(ans, dp[i + 1, w]);
                    }
                }
            }
        }
        List<Pair<string, TimeSpan>> Traversal()
        {
            List<Pair<string, TimeSpan>> ans = new List<Pair<string, TimeSpan>>();
            int w = 0;
            for (int i = 0; i < Line.Length; i++)
            {
                int time = (int)Line[i].Second.TotalSeconds;
                if (!Selected[i] && w + time <= Max_Folder_Length && dp[i + 1, w + time] + time >= dp[i + 1, w])
                {
                    Selected[i] = true;
                    Count--;
                    ans.Add(Line[i]);
                    w += time;
                }
            }
            return ans;
        }
        List<List<Pair<string, TimeSpan>>> Folders = new List<List<Pair<string, TimeSpan>>>();
        while (Count > 0)
        {
            IterativeKnapsack();
            Folders.Add(Traversal());
        }
        FileOperations.FinializeDirectory(Folders);
    }
}