using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHandController : MonoBehaviour
{
    [Header("Player Hands")] 
    public GameObject rightHand;
    public GameObject leftHand;
    public InputActionReference rightTriggerAction;
    public InputActionReference leftTriggerAction;

    private float _rightTriggerInput, _leftTriggerInput;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rightTriggerAction.action.Enable();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _rightTriggerInput = rightTriggerAction.action.ReadValue<float>();
        _leftTriggerInput = leftTriggerAction.action.ReadValue<float>();
        if (_rightTriggerInput >= 1)
        {
            Debug.Log("Hello World with Right Hand!");
        }
        if (_leftTriggerInput >= 1)
        {
            Debug.Log("Hello World with Left Hand!");
        }

    }
    
}
