using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI textScore;
    public float score;

    [SerializeField]
    private TextMeshProUGUI textTime;
    public float time;

    [SerializeField]
    private TextMeshProUGUI textPoints;
    public float points;
    public bool isClicked;
    public bool clickOnTime;
    public int pointsCheck;

    public uint scenario;

    [Space, Header("Temp States")]
    public TextMeshProUGUI OnGame;
    public TextMeshProUGUI caseScenario;
    public TextMeshProUGUI textPonitsOnCheck;

    private void Start()
    {
        score = 0;

        scenario = 1;
        pointsCheck = Random.Range(1, 9);
    }

    private void Update()
    {
        TimeManager();
        TextManager();

        OnGame.text = CheckScenario().ToString();
        caseScenario.text = scenario.ToString();

        if (time <= 0)
        {
            RestartScenario();
        }
    }

    private void TimeManager()
    {
        score += Time.deltaTime;
        time -= 1 * Time.deltaTime;
        points -= 1 * Time.deltaTime;
    }

    private void TextManager()
    {
        textScore.text = Mathf.RoundToInt(score).ToString();
        textTime.text = Mathf.RoundToInt(time).ToString();
        textPoints.text = Mathf.RoundToInt(points).ToString();
        textPonitsOnCheck.text = Mathf.RoundToInt(pointsCheck).ToString();
    }

    private bool CheckScenario()
    {
        switch (scenario)
        {
            case 0:
                if(points <= 0)
                {
                    return false;
                }
                break;
            case 1:
                if(!clickOnTime)
                {
                    return false;
                }
                break;
            case 2:
                if(isClicked)
                {
                    return false;
                }
                break;
            default: break;
        }

        return true;
    }

    private void RestartScenario()
    {
        pointsCheck = Random.Range(1, 9);
        //scenario = (uint)Random.Range(0, 3);

        time = 10;
        points = 10;
        isClicked = false;
        clickOnTime = false;
    }

}
