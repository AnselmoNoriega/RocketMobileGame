using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpaceShip : MonoBehaviour
{
    [SerializeField]
    private Transform spaceShip;
    [SerializeField]
    private GameObject[] hearts;
    private int health;

    [Space, Header("Lose Info")]
    [SerializeField]
    private TextMeshProUGUI myScore;
    [SerializeField]
    private GameObject deathPanel;
    [SerializeField]
    private HighScores highscores;

    [Space, Header("Asteroids Info")]
    [SerializeField]
    private Rigidbody2D[] asteroids;
    [SerializeField]
    public Vector3 speed;
    private float limitPosition;

    private void Start()
    {
        Time.timeScale = 1;

        for (int i = 0; i < asteroids.Length; i++)
        {
            asteroids[i].velocity = speed;
        }

        limitPosition = -3.3f;
        health = hearts.Length + 1;
    }

    private void Update()
    {
        MovementHandler();
        HealthManger();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        health -= 1;
    }

    private void MovementHandler()
    {
        for (int i = 0; i < asteroids.Length; i++)
        {
            if (asteroids[i].position.x <= limitPosition)
            {
                asteroids[i].position = new Vector2(Random.Range(3.3f, 7.0f), Random.Range(-3.4f, 2.1f));
            }
        }
    }

    private void HealthManger()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].SetActive(health > i);
        }

        if(health <= 0)
        {
            Time.timeScale = 0;
            deathPanel.SetActive(true);
        }
    }
}
