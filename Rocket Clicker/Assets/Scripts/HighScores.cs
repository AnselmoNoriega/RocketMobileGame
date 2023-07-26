using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScores : MonoBehaviour
{
    public int[] highScores = new int[5];

    private void Awake()
    {
        for (int i = 0; i < highScores.Length; i++)
        {
            highScores[i] = 0;
        }
    }
}
