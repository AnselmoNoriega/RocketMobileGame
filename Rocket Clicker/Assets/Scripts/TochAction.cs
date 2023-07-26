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
        if (Time.timeScale != 0)
        {
            scoreManager.balanceSlider.value += scoreManager.hitStrength;
        }
    }
}
