using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DebugCanvas : Singleton<DebugCanvas>
{
    public TextMeshPro debugText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (debugText == null)
        {
            Debug.LogWarning("no debug text");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateDebugText(string input)
    {
        debugText.text = input;
        Debug.Log("Updated to : " + input);
    }
}
