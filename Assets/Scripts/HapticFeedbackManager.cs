using UnityEngine;
using Bhaptics.SDK2;

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
    
    private void OnShoot(string bEvent, GameObject shooter, GameObject target)
    {
        if (HapticFeedbackManager.Instance.IsHapticFeedbackActive())
        {
            Vector3 directionToPlayer = target.transform.position - shooter.transform.position;
            float _angle = Vector3.SignedAngle(shooter.transform.forward, directionToPlayer, Vector3.up);
            Debug.Log(_angle);
            //BhapticsLibrary.Play(BhapticsEvent.GROWLING_LEFT);
            BhapticsLibrary.Play(
                bEvent,
                0,
                1.0f,
                1.0f,
                -_angle,
                0.0f
            );
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
