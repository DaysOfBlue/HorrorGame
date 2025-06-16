using UnityEngine;

public class HapticFeedbackManager : Singleton<HapticFeedbackManager>
{
    private bool _isHapticFeedbackActive = false;

    public bool IsHapticFeedbackActive()
    {
        return _isHapticFeedbackActive;
    }

    public void SetHapticFeedbackActive(bool active)
    {
        _isHapticFeedbackActive = active;
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
