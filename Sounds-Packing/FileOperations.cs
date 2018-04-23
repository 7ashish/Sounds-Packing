using System;
using System.Collections.Generic;
using System.IO;


static class FileOperations
{
    static public string DefaultPath = "";
    static public void FinializeDirectory(List<List<Pair<string, TimeSpan>>> FilesList)
    {
        string FilePath = DefaultPath;
        for (int i = 0; i < FilesList.Count; i++)
        {
            string DirectoryPath = FilePath + @"\F" + (i + 1);
            Directory.CreateDirectory(DirectoryPath);
            FileStream file = new FileStream(FilePath + @"\F" + (i + 1) + "_METADATA.txt", FileMode.Create, FileAccess.Write);
            StreamWriter writer = new StreamWriter(file);
            writer.WriteLine("F" + (i + 1));
            TimeSpan s = new TimeSpan();
            foreach (Pair<string, TimeSpan> p in FilesList[i])
            {
                writer.WriteLine(p.First + ' ' + p.Second.ToString());
                s += p.Second;
            }
            writer.WriteLine(s);
            writer.Close();
            for (int j = 0; j < FilesList[i].Count; j++)
            {
                string DistPath = DirectoryPath + @"\" + FilesList[i][j].First;
                string SourcePath = FilePath + @"\" + FilesList[i][j].First;
                File.Move(SourcePath, DistPath);
            }
        }
    }
    static public void CleanUp()
    {
        string FilePath = DefaultPath;
        foreach (string s in Directory.EnumerateFiles(FilePath))
        {
            if(s.Substring(s.Length - 3) == "mp3" || s.Substring(s.Length - 3).ToLower() == "amr")
            {
                return;
            }
            File.Delete(s);
        }
        foreach (string s in Directory.EnumerateDirectories(FilePath))
        {
            foreach (string z in Directory.EnumerateFiles(s))
            {
                File.Move(z, FilePath + z.Substring(z.LastIndexOf('\\') + 1));
            }
            Directory.Delete(s);
        }
    }
    static public bool CheckAnswer(string InputPath, string OutputPath)
    {
        foreach (string s in Directory.EnumerateDirectories(InputPath))
        {
            string z = OutputPath + '\\' + s.Substring(s.LastIndexOf('\\') + 1);
            if (!Directory.Exists(z))
            {
                Console.WriteLine("Directory " + s.Substring(s.LastIndexOf('\\') + 1) + " Should not exist");
                return false;
            }
            SortedSet<string> set = new SortedSet<string>();
            foreach (string ss in Directory.EnumerateFiles(s))
            {
                string zz = z + '\\' + ss.Substring(ss.LastIndexOf('\\') + 1);
                if (!File.Exists(zz))
                {
                    Console.WriteLine("File " + ss.Substring(ss.LastIndexOf('\\') + 1) + " In Directory " + s.Substring(s.LastIndexOf('\\') + 1) + " Should not exist");
                    return false;
                }
                set.Add(ss.Substring(ss.LastIndexOf('\\') + 1));
            }
            foreach (string zz in Directory.EnumerateFiles(z))
            {
                if (!set.Contains((zz.Substring(zz.LastIndexOf('\\') + 1))))
                {
                    Console.WriteLine("File " + zz.Substring(zz.LastIndexOf('\\') + 1) + " In Directory " + s.Substring(s.LastIndexOf('\\') + 1) + " Is Missing");
                    return false;
                }
            }
        }
        return true;
    }
}
