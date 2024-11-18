using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

static class FileIO
{
    public static List<Score> ReadScoreboard(String filename)
    {
        try
        {
            StreamReader sr = new StreamReader(filename);

            // throw away the unnecessary lines
            string line = "";
            while (!line.Equals("START"))
            {
                line = sr.ReadLine();
            }

            // read in the scoreboard
            List<Score> scores = new List<Score>();
            while (sr.Peek() >= 0)
            {
                line = sr.ReadLine();
                if (line.Equals(""))
                {
                    break;
                }
                string[] values = line.Split("   ");
                Console.WriteLine(line);
                scores.Add(new Score(values[0], Double.Parse(values[1])));
            }
            scores.Sort();

            // print scores
            Console.WriteLine("File reading complete:");
            foreach (Score n in scores)
            {
                Console.Write(n.GetScore() + ", ");
            }
            Console.WriteLine();


            sr.Close();
            return scores;
        }
        catch (FileNotFoundException e)
        {
            Console.WriteLine("The file " + filename + " could not be found");
            return null;
        }
    }

    // overrides everything in the scoreboard
    public static void WriteScoreboard(String filename, List<Score> scores)
    {
        try
        {
            // get current list of scores for the level
            List<Score> currentScores = ReadScoreboard(filename);

            // combine existing scores with new scores
            scores.AddRange(currentScores);

            // open stream writer
            StreamWriter sw = File.CreateText(filename);

            // write heading
            sw.WriteLine("NAME  SCORE");
            sw.WriteLine("START");

            // write scores
            List<Score> top10 = GetTopX(scores, 10);
            foreach (Score s in top10)
            {
                sw.WriteLine(s.ToString());
            }

            // print scores
            Console.WriteLine("File writing complete:");
            foreach (Score n in scores)
            {
                Console.Write(n.GetScore() + ", ");
            }
            Console.WriteLine();


            sw.Close();
        }
        catch (FileNotFoundException e)
        {
            Console.WriteLine("The file " + filename + " could not be found");
        }
    }

    public static char[][] ReadLevel(String filename)
    {
        try
        {
            StreamReader sr = new StreamReader(filename);

            bool readyToStart = false;
            int width = 0;
            int height = 0;

            // throw away the unnecessary lines
            while (!readyToStart)
            {
                string line = sr.ReadLine();
                string[] words = line.Split(" ");
                if (line.Equals("START"))
                {
                    readyToStart = true;
                }
                else if (words[0].Equals("Size:"))
                {
                    width = int.Parse(words[1]);
                    height = int.Parse(words[2]);
                }
            }

            // read in the level
            char[][] levelLayout = new char[height][];
            int level = 0;
            while (sr.Peek() >= 0)
            {
                string line = sr.ReadLine().Substring(3);
                levelLayout[level] = line.ToCharArray();
                Console.WriteLine(levelLayout[level]);
                level++;
            }
            Console.WriteLine("File reading complete");
            sr.Close();
            return levelLayout;
        }
        catch (FileNotFoundException e)
        {
            Console.WriteLine("The file " + filename + " could not be found");
            return null;
        }
    }

    public static String[] GetBackground(String filename)
    {
        try
        {
            StreamReader sr = new StreamReader(filename);

            bool readyToStart = false;

            // throw away the unnecessary lines
            while (!readyToStart)
            {
                string line = sr.ReadLine();
                if (line.Equals("BACKGROUND"))
                {
                    readyToStart = true;
                }
            }

            // read in the backgrounds
            String[] backgrounds = new String[Layers.NumBackground];
            for(int i = 0; i < backgrounds.Length; i++)
            {
                backgrounds[i] = sr.ReadLine();
            }

            return backgrounds;
        }
        catch (FileNotFoundException e)
        {
            Console.WriteLine("The file " + filename + " could not be found");
            return null;
        }
    }

    public static List<Score> GetTopX(List<Score> scores, int x)
    {
        Score[] topX = new Score[x];
        for (int i = 0; i < x; i++)
        {
            topX[i] = new Score(Double.MinValue);
        }

        // check if there are more than x scores in scores
        if (scores.Capacity < x)
        {
            return scores;
        }

        // save top x scores
        for (int i = 0; i < x; i++)
        {
            topX[i] = scores[i];
        }
        // for all remaining scores
        for (int i = x; i < scores.Count; i++)
        {
            // find the smallest score currently saved in topX
            int smallestIndex = 0;
            for (int j = 1; j < topX.Length; j++)
            {
                // if the score at j is smaller than the currently saved smallest score
                if (topX[j].CompareTo(topX[smallestIndex]) >= 1)
                {
                    smallestIndex = j;
                }
            }
            // if the current distance is greater than the smallest distance in topX, then replace it with the current distance
            if (scores[i].CompareTo(topX[smallestIndex]) <= -1)
            {
                topX[smallestIndex] = scores[i];
            }
        }

        // sort from largest score to smallest score
        Array.Sort(topX);

        return new List<Score>(topX);
    }
}
