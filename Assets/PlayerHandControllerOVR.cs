using UnityEngine;

public class PlayerHandControllerOVR : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        BtnDown();
    }

    void BtnDown()
    {
        if (OVRInput.GetDown(OVRInput.Button.One))
        {
            Debug.Log("Press Down");
        }
        if (OVRInput.GetDown(OVRInput.Button.Two))
        {
            Debug.Log("Press Two");
        }
    }
}
