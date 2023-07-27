using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [Header("Slider")]
    public Slider balanceSlider;
    [SerializeField]
    private Image handleImage;

    [Space, Header("Info"), SerializeField]
    private TextMeshProUGUI textScore;
    [SerializeField]
    private TextMeshProUGUI texthighScores;
    [SerializeField]
    private TextMeshProUGUI HighScoretext;

    public int score;
    private float time;

    public float hitStrength;
    private float sliderReductor;

    [SerializeField]
    private HighScores highScoreManager;
    private bool isChecked;

    private void Start()
    {
        sliderReductor = 10f;
        hitStrength = 30f;
        isChecked = false;
        StartLevel();
    }

    private void Update()
    {
        LextraPoints();
        TextManager();

        if (time <= 0)
        {
            LevelFinished();
        }
        if (Time.timeScale == 0 && !isChecked)
        {
            HandleManager();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("-------------------------------");
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

    private void HandleManager()
    {
        for (int i = 0; i < highScoreManager.highScores.Length; ++i)
        {
            if (score > highScoreManager.highScores[i])
            {
                ChangeScores(i);
                highScoreManager.SaveHighScores(highScoreManager.highScores);
                isChecked = true;
                break;
            }
        }

        SetScores();
    }

    private void ChangeScores(int index)
    {
        var firstSave = score;
        int secondSave;

        for (int i = index; i < highScoreManager.highScores.Length; ++i)
        {
            secondSave = highScoreManager.highScores[i];
            highScoreManager.highScores[i] = firstSave;
            firstSave = secondSave;
        }
    }

    private void StartLevel()
    {
        balanceSlider.value = 50;
        time = 5;
        texthighScores.text += highScoreManager.highScores[0].ToString();
    }

    private void LevelFinished()
    {
        score += 10;
        hitStrength /= 1.1f;
        sliderReductor += 1f;
        var extraPoints = (int)(5 - ((50 - balanceSlider.value) / 10));
        if (extraPoints < 0) extraPoints *= (-1);

        score += extraPoints;

        time = 5;
    }

    private bool CheckIfLost()
    {
        return balanceSlider.value <= 10 || balanceSlider.value >= 75;
    }
    private void SetScores()
    {
        for (int i = 0; i < highScoreManager.highScores.Length; i++)
        {
            HighScoretext.text += "\n" + (i + 1) + ": " + highScoreManager.highScores[i];
        }
    }
}
