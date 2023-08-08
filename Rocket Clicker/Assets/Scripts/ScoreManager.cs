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
    private ExtraPointsController extraPointsController;

    public int score;
    private float time;

    public float hitStrength;
    public float sliderReductor;

    [SerializeField]
    private HighScores highScoreManager;
    public bool isChecked;

    private void Start()
    {
        extraPointsController = GetComponent<ExtraPointsController>();
        sliderReductor = 10f;
        hitStrength = 10f;
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
        time = 3;
        texthighScores.text += highScoreManager.highScores[0].ToString();
    }

    private void LevelFinished()
    {
        score += 10;
        sliderReductor += 5f;
        var extraPoints = GetExtraPoints();
        extraPointsController.ExtraPoints(extraPoints);
        score += extraPoints;

        time = 3;
    }

    public bool CheckIfLost()
    {
        return balanceSlider.value <= 10 || balanceSlider.value >= 75;
    }

    private void SetScores()
    {
        for (int i = 0; i < highScoreManager.highScores.Length; i++)
        {
            HighScoretext.text += "\n" + (i + 1) + ": " + highScoreManager.highScores[i];
        }

        isChecked = true;
    }

    private int GetExtraPoints()
    {
        if (balanceSlider.value <= 50)
        {
            return (int)(balanceSlider.value / 10);
        }
        else
        {
            return 5 - (int)((balanceSlider.value - 50) / 10);
        }
    }
}
