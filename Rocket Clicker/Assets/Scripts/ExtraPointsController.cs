using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ExtraPointsController : MonoBehaviour
{
    private bool levelPassed;
    private float timer;
    [SerializeField]
    private GameObject midleLine;
    [SerializeField]
    private TextMeshProUGUI extraPointsText;

    private void Start()
    {
        midleLine.SetActive(false);
        levelPassed = false;
    }

    private void Update()
    {
        if (levelPassed)
        {
            Timer();
        }
    }
    public void ExtraPoints(int extraPoints)
    {
        levelPassed = true;
        extraPointsText.text = "Extra Points: " + extraPoints.ToString();
        midleLine.SetActive(true);
    }

    private void Timer()
    {
        timer += Time.deltaTime;

        if (timer >= 1.5)
        {
            midleLine.SetActive(false);
            levelPassed = false;
            timer = 0;
        }
    }
}
