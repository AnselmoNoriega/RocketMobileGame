using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TochAction : MonoBehaviour
{
    [SerializeField]
    private InputActionReference input;
    [SerializeField]
    private ScoreManager scoreManager;

    private void OnEnable()
    {
        input.action.Enable();
        input.action.performed += OnClicked;
    }

    void Update()
    {

    }

    private void OnDisable()
    {
        input.action.performed -= OnClicked;
        input.action.Disable();
    }

    void OnClicked(InputAction.CallbackContext context)
    {
        switch (scoreManager.scenario)
        {
            case 0:
                scoreManager.points++;
                break;
            case 1:
                if (!scoreManager.isClicked && scoreManager.pointsCheck == Mathf.RoundToInt(scoreManager.points))
                {
                    scoreManager.clickOnTime = true;
                    scoreManager.isClicked = true;
                }
                else 
                { 
                    scoreManager.clickOnTime = false; 
                    scoreManager.isClicked = true; 
                }
                    break;
            case 2:
                scoreManager.isClicked = true;
                break;
            default: break;
        }
    }
}
