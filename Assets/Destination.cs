using System;
using UnityEngine;

public class Destination : MonoBehaviour
{
    private BoxCollider _boxCollder;
    private bool _isChecked = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!_isChecked)
            {
                NavigationManager.Instance.SetProgress(1);
                _isChecked = true;
            }
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
