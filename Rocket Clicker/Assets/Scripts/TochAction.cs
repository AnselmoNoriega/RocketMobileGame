using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TochAction : MonoBehaviour
{
    [SerializeField]
    private InputActionReference input;

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
        Debug.Log("Clicked");
    }
}
