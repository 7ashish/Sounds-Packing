using System;
using System.Collections.Generic;
using System.IO;

static class Algorithms
{
    static public void First_Fit_Decreasing_Algorithm(Pair<string, TimeSpan>[] Line)
    {
        Array.Sort(Line);       //O(nlogn) where n = Line.Count
        foreach (Pair<string, TimeSpan> s in Line)
        {
            Console.WriteLine(s.First);
            Console.WriteLine(s.Second.ToString());
        }
        List<List<string>> Folders_List = new List<List<string>>();
        List<string> Files_List = new List<string>();

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