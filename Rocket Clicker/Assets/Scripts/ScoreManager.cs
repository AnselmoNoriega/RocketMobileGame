using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Slider balanceSlider;

    [SerializeField]
    private TextMeshProUGUI textScore;

    public int score;
    private float time;

    public float hitStrength;
    private float sliderReductor;

    private void Start()
    {
        sliderReductor = 10f;
        hitStrength = 30f;
        StartLevel();
    }

    private void Update()
    {
        LextraPoints();
        TextManager();

        if(time <= 0)
        {
            LevelFinished();
        }
    }

    private void LextraPoints()
    {
        time -= 1 * Time.deltaTime;
        balanceSlider.value -= sliderReductor * Time.deltaTime;
    }

    private void TextManager()
    {
        textScore.text = Mathf.RoundToInt(score).ToString();
    }

    private void StartLevel()
    {
        balanceSlider.value = 50;
        time = 5;
    }

    private void LevelFinished()
    {
        score += 10;
        hitStrength /= 1.1f;
        sliderReductor += 1f;
        var extraPoints = (int)(5 - ((50 - balanceSlider.value) / 10));
        if (extraPoints < 0) extraPoints *= (-1);

        score += extraPoints;
        StartLevel();
    }

    private bool CheckIfLost()
    {
        return balanceSlider.value <= 10 || balanceSlider.value >= 75;
    }
}
