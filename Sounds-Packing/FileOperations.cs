using System;
using System.Collections.Generic;
using System.IO;


static class FileOperations
{
    static public void FinializeDirectory(List<List<Pair<string, TimeSpan>>> FilesList, string FilePath)
    {
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
}
