using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem
{
    private static int countOfSavedScores = 10;

    public static int CountOfSavedScores { get => countOfSavedScores; set => countOfSavedScores = value; }

    public static void SaveScores(List<Score> scores)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        
        for (int i = 0; i < scores.Count; i++)
        {
            string path = Application.persistentDataPath + "/myresults" + i + ".fun";
            FileStream stream = new FileStream(path, FileMode.Create);
            //stream.Position = 0;
            formatter.Serialize(stream, scores[i]);
            stream.Close();
        }
    }

    public static List<Score> LoadScores()
    {
        List<Score> loadedScores = new List<Score>();

        for (int i = 0; i < CountOfSavedScores; i++)
        {

            string path = Application.persistentDataPath + "/myresults" + i + ".fun";
            if (File.Exists(path))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(path, FileMode.Open);
                Score score = formatter.Deserialize(stream) as Score;
                stream.Close();
                loadedScores.Add(score);
            }
            else break;
        }

        return loadedScores;
    }

}
