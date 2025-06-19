using UnityEngine;
using Bhaptics.SDK2;
using UnityEngine.XR.OpenXR.Input;

public class PlayerHapticFeedback : Singleton<PlayerHapticFeedback>
{
    public GameObject player;
    public GameObject soundSource;

    public string tactFileName = "SoundFeedback";
    private float _angle = 0.0f;
    
    public void OnShoot(string bEvent)
    {
        if (HapticFeedbackManager.Instance.IsHapticFeedbackActive())
        {
            Vector3 directionToPlayer = transform.position - soundSource.transform.position;
            _angle = Vector3.SignedAngle(soundSource.transform.forward, directionToPlayer, Vector3.up);
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
    public void OnShootByAngle(string bEvent, float angle)
    {
        if (HapticFeedbackManager.Instance.IsHapticFeedbackActive())
        {
            Vector3 directionToPlayer = transform.position - soundSource.transform.position;
            //BhapticsLibrary.Play(BhapticsEvent.GROWLING_LEFT);
            BhapticsLibrary.Play(
                bEvent,
                0,
                1.0f,
                1.0f,
                -angle,
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnShoot(BhapticsEvent.FOOTSTEP);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            HapticFeedbackManager.Instance.SetHapticFeedbackActive(true);
        }
    }
}
