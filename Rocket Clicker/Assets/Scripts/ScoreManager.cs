using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [Header("Sliders")]
    [SerializeField]
    private Slider clickSlider;
    [SerializeField]
    private Image sliderClickTime;
    [SerializeField]
    private Transform rotationSliderTime;

    [Space, Header("Texts"), SerializeField]
    private TextMeshProUGUI textScore;
    public float score;

    [SerializeField]
    private TextMeshProUGUI textTime;
    public float time;

    [SerializeField]
    private TextMeshProUGUI textPoints;
    public int timeCheck;
    public float points;
    public bool isClicked;
    public bool clickOnTime;

    public uint scenario;

    private int pointsReduction;


    [Space, Header("Temp States")]
    public TextMeshProUGUI OnGame;
    public TextMeshProUGUI caseScenario;
    public TextMeshProUGUI textPonitsOnCheck;

    private void Start()
    {
        score = 0;
        pointsReduction = 1;
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
        points -= pointsReduction * Time.deltaTime;
        points -= 1 * Time.deltaTime;

        clickSlider.value = time;
    }

    private void TextManager()
    {
        textScore.text = Mathf.RoundToInt(score).ToString();
        textTime.text = Mathf.RoundToInt(time).ToString();
        textPoints.text = Mathf.RoundToInt(points).ToString();
        textPonitsOnCheck.text = Mathf.RoundToInt(timeCheck).ToString();
    }

    private bool CheckScenario()
    {
        switch (scenario)
        {
            case 0:
                if (points <= 0)
                {
                    return false;
                }
                break;
            case 1:
                if (!clickOnTime)
                {
                    return false;
                }
                break;
            case 2:
                if (isClicked)
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
        timeCheck = Random.Range(1, 9);
        scenario = (uint)Random.Range(0, 3);

        time = 10;
        points = 10;
        isClicked = false;
        clickOnTime = false;

        if (scenario == 0)
        {
            pointsReduction += 2;
            sliderClickTime.fillAmount = 0;
        }
        else if (scenario == 1)
        {
            sliderClickTime.fillAmount = 0.1f;
            var mathForRotation = 360 - (360 / 10 * timeCheck);
            rotationSliderTime.eulerAngles = new Vector3(0, 0, mathForRotation);
        }
    }

}
