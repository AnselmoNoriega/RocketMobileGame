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
    [SerializeField]
    private GameObject[] asteroids;
    private float timertoLose;
    public float speed;

    private void Update()
    {
        asteroids[0].transform.position += new Vector3(speed, 0, 0) * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Heewdsaaa");
    }

    private void MovementHandler()
    {
        transform.position = spaceShip.position;
    }
}
