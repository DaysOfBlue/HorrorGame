using UnityEngine;
using Bhaptics.SDK2;
public class NavigationManager : Singleton<NavigationManager>
{
    public Transform[] destinations;
    public Transform player;
    private int _progress = 0;
    private float _angle = 0f;

    private float _vibRate = 0.0f;

    public void SetProgress(int progress)
    {
        _progress += progress;
        Debug.Log("Progress : " + _progress);
        VibrateByProgress();
    }
    
    private void VibrateByProgress()
    {
        Transform target = destinations[_progress];
        // 플레이어의 정면 방향
        Vector3 forward = player.transform.forward;
        // 플레이어 기준, 목적지 방향
        Vector3 toTarget = (target.transform.position - player.transform.position).normalized;
        // 월드에서의 위 방향을 기준으로 각도 계산 (Y축 기준 회전)
        float angle = Vector3.SignedAngle(forward, toTarget, Vector3.up);
        if (angle >= -30.0 && angle <= -45.0f)
        {
            angle = -50.0f;
        }
        if (angle <= 45.0 && angle >= 30.0f)
        {
            angle = 50.0f;
        }

        float distance = Vector3.Distance(player.transform.position.normalized, target.transform.position.normalized);

        BhapticsLibrary.Play(
            BhapticsEvent.NAVIGATE,
            0,
            0.1f,
            1.0f,
            -angle,
            0.0f
        );
        
        Debug.Log("Angle : " + angle);
        Debug.Log("Distance : " + distance);
    }
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetProgress(0);
        //PlayerHapticFeedback.Instance.OnShootByAngle(BhapticsEvent.NAVIGATE, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (_vibRate >= 1.0f)
        {
            VibrateByProgress();
            _vibRate = 0.0f;
        }
        else
            _vibRate += Time.deltaTime;
    }
}
