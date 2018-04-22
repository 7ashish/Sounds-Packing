using System;
using System.Collections;
using System.Collections.Generic;

static class Algorithms
{
    static public void First_Fit_Decreasing_Algorithm(Pair<string, TimeSpan>[] Line)
    {
        double FolderLength = 100;
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
                for (int k = 0; k < FolderList.Count; k++) //O(m)
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
        double FolderLength = 100;
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
                Seconds.Add(FolderLength - Line[i].Second.TotalSeconds);
                continue;
            }
            if (FolderList.Count == 1)
            {
                if (Line[i].Second.TotalSeconds <= Seconds[counter])
                {
                    FolderList[counter].Add(Line[i]);
                    Seconds[counter] -= Line[i].Second.TotalSeconds;
                    if (FolderLength + Seconds[counter] == 0)
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
                    Seconds.Add(FolderLength - Line[i].Second.TotalSeconds);
                    continue;
                }
            }
            bool Done = false;
            double min = FolderLength;
            int index = 0;
            for (int k = 0; k < FolderList.Count; k++)
            {
                if (Seconds[k] < Line[i].Second.TotalSeconds)
                {
                    continue;
                }
                if (Seconds[k] - Line[i].Second.TotalSeconds == FolderLength)
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
                if (min == FolderLength)
                {
                    Temp = new List<Pair<string, TimeSpan>>(Line.Length);
                    Temp.Add(Line[i]);
                    FolderList.Add(Temp);
                    Seconds.Add(FolderLength - Line[i].Second.TotalSeconds);
                    counter++;
                }
                else
                {
                    FolderList[index].Add(Line[i]);
                    Seconds[index] -= Line[i].Second.TotalSeconds;
                    if (FolderLength + Seconds[counter] == 0)
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
    static public void Best_Fit_Algorithm_Priority_Queue(Pair<string, TimeSpan>[] Line)
    {
        double FolderLength = 100;
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
                Tempo.Second = FolderLength - Line[i].Second.TotalSeconds;
                PQueue.Add(Tempo);
                continue;
            }
            if (PQueue.Count == 0)
            {
                Temp = new List<Pair<string, TimeSpan>>();
                Tempo = new Pair<int, double>();
                Temp.Add(Line[i]);
                FolderList.Add(Temp);
                Tempo.First = FolderList.Count - 1;
                Tempo.Second = FolderLength - Line[i].Second.TotalSeconds;
                PQueue.Add(Tempo);
                continue;
            }
            if (PQueue.Max.Second >= Line[i].Second.TotalSeconds)
            {
                FolderList[PQueue.Max.First].Add(Line[i]);
                if ((FolderLength-PQueue.Max.Second) + Line[i].Second.TotalSeconds < FolderLength)
                {
                    Tempo = new Pair<int, double>();
                    Tempo.First = PQueue.Max.First;
                    Tempo.Second = PQueue.Max.Second - Line[i].Second.TotalSeconds;
                    PQueue.Remove(PQueue.Max);
                    PQueue.Add(Tempo);
                }
                else
                {
                    PQueue.Remove(PQueue.Max);
                }

                continue;
            }
            if (PQueue.Min.Second < Line[i].Second.TotalSeconds)
            {
                Temp = new List<Pair<string, TimeSpan>>();
                Tempo = new Pair<int, double>();
                Temp.Add(Line[i]);
                FolderList.Add(Temp);
                Tempo.First = FolderList.Count - 1;
                Tempo.Second = FolderLength - Line[i].Second.TotalSeconds;
                PQueue.Add(Tempo);
                continue;
            }
            else
            {
                bool Done = false;
                IEnumerator en = PQueue.GetEnumerator();
                while (en.MoveNext())
                {
                    SortedSet<Pair<int, double>> n = (SortedSet<Pair<int, double>>)en.Current;
                    if (n.Max.Second >= Line[i].Second.TotalSeconds)
                    {
                        Tempo = new Pair<int, double>();
                        Done = true;
                        FolderList[n.Max.First].Add(Line[i]);
                        Tempo.First = n.Max.First;
                        Tempo.Second = n.Max.Second;
                        PQueue.Add(Tempo);
                        PQueue.Remove(Tempo);
                        break;
                    }
                }
                if (!Done)
                {
                    Temp = new List<Pair<string, TimeSpan>>();
                    Tempo = new Pair<int, double>();
                    Temp.Add(Line[i]);
                    FolderList.Add(Temp);
                    Tempo.First = FolderList.Count - 1;
                    Tempo.Second = FolderLength - Line[i].Second.TotalSeconds;
                    PQueue.Add(Tempo);
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
        FileOperations.FinializeDirectory(FolderList);
    }
    static public void Best_Fit_Decreasing_Algorithm(Pair<string, TimeSpan>[] Line)
    {
        Array.Sort(Line);
        double FolderLength = 100;
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
                Seconds.Add(FolderLength - Line[i].Second.TotalSeconds);
                continue;
            }
            if (FolderList.Count == 1)
            {
                if (Line[i].Second.TotalSeconds <= Seconds[counter])
                {
                    FolderList[counter].Add(Line[i]);
                    Seconds[counter] -= Line[i].Second.TotalSeconds;
                    if (FolderLength + Seconds[counter] == 0)
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
                    Seconds.Add(FolderLength - Line[i].Second.TotalSeconds);
                    continue;
                }
            }
            bool Done = false;
            double min = FolderLength;
            int index = 0;
            for (int k = 0; k < FolderList.Count; k++)
            {
                if (Seconds[k] < Line[i].Second.TotalSeconds)
                {
                    continue;
                }
                if (Seconds[k] - Line[i].Second.TotalSeconds == FolderLength)
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
                if (min == FolderLength)
                {
                    Temp = new List<Pair<string, TimeSpan>>(Line.Length);
                    Temp.Add(Line[i]);
                    FolderList.Add(Temp);
                    Seconds.Add(FolderLength - Line[i].Second.TotalSeconds);
                    counter++;
                }
                else
                {
                    FolderList[index].Add(Line[i]);
                    Seconds[index] -= Line[i].Second.TotalSeconds;
                    if (FolderLength + Seconds[counter] == 0)
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
    static public void Best_Fit_Decreasing_Algorithm_Priority_Queue(Pair<string, TimeSpan>[] Line)
    {
        Array.Sort(Line);

        double FolderLength = 100;
        SortedSet<Pair<int, double>> PriorityQueue = new SortedSet<Pair<int, double>>();
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
                Tempo.Second = FolderLength - Line[i].Second.TotalSeconds;
                PriorityQueue.Add(Tempo);
                continue;
            }
            if (PriorityQueue.Count == 0)
            {
                Temp = new List<Pair<string, TimeSpan>>();
                Tempo = new Pair<int, double>();
                Temp.Add(Line[i]);
                FolderList.Add(Temp);
                Tempo.First = FolderList.Count - 1;
                Tempo.Second = FolderLength - Line[i].Second.TotalSeconds;
                PriorityQueue.Add(Tempo);
                continue;
            }
            if (PriorityQueue.Max.Second >= Line[i].Second.TotalSeconds)
            {
                FolderList[PriorityQueue.Max.First].Add(Line[i]);
                if ((FolderLength - PriorityQueue.Max.Second) + Line[i].Second.TotalSeconds < FolderLength)
                {
                    Tempo = new Pair<int, double>();
                    Tempo.First = PriorityQueue.Max.First;
                    Tempo.Second = PriorityQueue.Max.Second - Line[i].Second.TotalSeconds;
                    PriorityQueue.Remove(PriorityQueue.Max);
                    PriorityQueue.Add(Tempo);
                }
                else
                {
                    PriorityQueue.Remove(PriorityQueue.Max);
                }

                continue;
            }
            if (PriorityQueue.Min.Second < Line[i].Second.TotalSeconds)
            {
                Temp = new List<Pair<string, TimeSpan>>();
                Tempo = new Pair<int, double>();
                Temp.Add(Line[i]);
                FolderList.Add(Temp);
                Tempo.First = FolderList.Count - 1;
                Tempo.Second = FolderLength - Line[i].Second.TotalSeconds;
                PriorityQueue.Add(Tempo);
                continue;
            }
            else
            {
                bool Done = false;
                IEnumerator en = PriorityQueue.GetEnumerator();
                while (en.MoveNext())
                {
                    SortedSet<Pair<int, double>> n = (SortedSet<Pair<int, double>>)en.Current;
                    if (n.Max.Second >= Line[i].Second.TotalSeconds)
                    {
                        Tempo = new Pair<int, double>();
                        Done = true;
                        FolderList[n.Max.First].Add(Line[i]);
                        Tempo.First = n.Max.First;
                        Tempo.Second = n.Max.Second;
                        PriorityQueue.Add(Tempo);
                        PriorityQueue.Remove(Tempo);
                        break;
                    }
                }
                if (!Done)
                {
                    Temp = new List<Pair<string, TimeSpan>>();
                    Tempo = new Pair<int, double>();
                    Temp.Add(Line[i]);
                    FolderList.Add(Temp);
                    Tempo.First = FolderList.Count - 1;
                    Tempo.Second = FolderLength - Line[i].Second.TotalSeconds;
                    PriorityQueue.Add(Tempo);
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
        FileOperations.FinializeDirectory(FolderList);
    }
    static public void Worst_Fit_Algorithm(Pair<string, TimeSpan>[] Line)
    {
        double FolderLength = 100;
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
                Seconds.Add(FolderLength - Line[i].Second.TotalSeconds);
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
                    Seconds.Add(FolderLength - Line[i].Second.TotalSeconds);
                    continue;
                }
            }
            if (Seconds[counter] == FolderLength)
            {
                if (counter + 1 == FolderList.Count)
                {
                    Temp = new List<Pair<string, TimeSpan>>(Line.Length);
                    Temp.Add(Line[i]);
                    FolderList.Add(Temp);
                    Seconds.Add(FolderLength - Line[i].Second.TotalSeconds);
                    continue;
                }
                counter++;
                continue;
            }
            int index = 0;
            double max = 0;
            for(int k = 0; k < Seconds.Count; k++)
            {
                if (Seconds[k] < Line[i].Second.TotalSeconds)
                {
                    continue;
                }
                if(Seconds[k] > max)
                {
                    max = Seconds[k];
                    index = k;
                }
            }
            if (max != 0)
            {
                FolderList[index].Add(Line[i]);
                Seconds[index]-=Line[i].Second.TotalSeconds;
                continue;
            }
            else
            {
                Temp = new List<Pair<string, TimeSpan>>(Line.Length);
                Temp.Add(Line[i]);
                FolderList.Add(Temp);
                Seconds.Add(FolderLength - Line[i].Second.TotalSeconds);
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
        double FolderLength = 100;
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
                Seconds.Add(FolderLength - Line[i].Second.TotalSeconds);
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
                    Seconds.Add(FolderLength - Line[i].Second.TotalSeconds);
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
                    Seconds.Add(FolderLength - Line[i].Second.TotalSeconds);
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
                Seconds.Add(FolderLength - Line[i].Second.TotalSeconds);
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
        double FolderLength = 100;
        SortedSet<Pair<int, double>> PQueue = new SortedSet<Pair<int, double>>();
        List<Pair<string, TimeSpan>> Temp = new List<Pair<string, TimeSpan>>(Line.Length);
        List<List<Pair<string, TimeSpan>>> FolderList = new List<List<Pair<string, TimeSpan>>>(Line.Length);
        Pair<int, double> Tempo = new Pair<int, double>();
        for(int i = 0; i < Line.Length; i++)
        {
            if (FolderList.Count == 0)
            {
                Temp.Add(Line[i]);
                FolderList.Add(Temp);
                Tempo.First = 0;
                Tempo.Second = FolderLength-Line[i].Second.TotalSeconds;
                PQueue.Add(Tempo);
                continue;
            }
            if (PQueue.Count != 0)
            {
                if (PQueue.Min.Second >= Line[i].Second.TotalSeconds)
                {
                    FolderList[PQueue.Min.First].Add(Line[i]);
                    if(PQueue.Min.Second == Line[i].Second.TotalSeconds)
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
                    Tempo.Second = FolderLength - Line[i].Second.TotalSeconds;
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
                Tempo.Second = FolderLength - Line[i].Second.TotalSeconds;
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
        double FolderLength = 100;
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
                Tempo.Second = FolderLength - Line[i].Second.TotalSeconds;
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
                    Tempo.Second = FolderLength - Line[i].Second.TotalSeconds;
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
                Tempo.Second = FolderLength - Line[i].Second.TotalSeconds;
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
    static public void Folder_Filling_Algorithm(Pair<string,TimeSpan>[] Line)
    {

    }
}