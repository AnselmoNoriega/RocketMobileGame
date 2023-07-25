using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShip : MonoBehaviour
{
    [SerializeField]
    private Transform spaceShip;

    [Space, Header("Lose Info")]
    [SerializeField]
    private GameObject dangerZone;
    private float timertoLose;

    [Space, Header("Asteroids Info")]
    [SerializeField]
    private Rigidbody2D[] asteroids;
    [SerializeField]
    public Vector3 speed;
    private float limitPosition;

    private void Start()
    {
        for (int i = 0; i < asteroids.Length; i++)
        {
            asteroids[i].velocity = speed;
        }

        limitPosition = -3.3f;
    }

    private void Update()
    {
        MovementHandler();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Heewdsaaa");
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
}
