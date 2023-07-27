using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor.Experimental.RestService;
using UnityEngine;

public class HighScores : MonoBehaviour
{
    public int[] highScores;

    private void Awake()
    {
        highScores=LoadScores();

        if(highScores == null )
        {
            EmptyData();
        }
    }

    public void EmptyData()
    {
        highScores = new int[5];
        for (int i = 0; i < highScores.Length; i++)
        {
            highScores[i] = 0;
        }
    }

    public void SaveHighScores(int[] scores)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/scores.anr";
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, scores);
        stream.Close();
    }

    public static int[] LoadScores()
    {
        string path = Application.persistentDataPath + "/scores.anr"; ;
        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            int[] data = formatter.Deserialize(stream) as int[];
            stream.Close();

            return data;
        }

        Debug.Log("Not found");
        return null;
    }
}
