using UnityEngine;
using Bhaptics.SDK2;
using Unity.Collections;
using Unity.Jobs;
using System.Collections;

public class NavigationManager : Singleton<NavigationManager>
{
    public Transform[] destinations;
    public Transform player;
    private int _progress = 0;
    private float _angle = 0f;

    private float _vibRate = 0.0f;

    [Header("Ping Settings")]
    public int rays = 90;
    public float minDist = 0.3f;                  // m
    public float maxDist = 5f;
    public bool triggerKey = OVRInput.GetDown(OVRInput.Button.One);

    public LayerMask collisionMask = ~0;          // 기본: 모든 레이어

    [Header("Haptics")]
    public int hapticDurationMs = 100;            // Suit pulse time


    /* ───────────── Haptic mapping ──────────── */

    void MapHitsToHaptics(NativeArray<RaycastHit> hits)
    {
        var xList = new System.Collections.Generic.List<float>();
        var yList = new System.Collections.Generic.List<float>();
        var iList = new System.Collections.Generic.List<int>();

        Vector3 fwd = player.transform.forward;

        for (int k = 0; k < hits.Length; ++k)
        {
            if (!hits[k].collider) continue;      // miss

            float dist = hits[k].distance;
            float norm = Mathf.InverseLerp(minDist, maxDist, dist); // 0 = near
            int inten = Mathf.RoundToInt(Mathf.Lerp(10f, 100f, norm)); // far=100, near=10

            Vector3 dir = hits[k].point - transform.position;
            float az = Vector3.SignedAngle(fwd, dir, Vector3.up);     // −180°~180°
            float xCoord = (az + 180f) / 360f;                            // 0-1

            xList.Add(xCoord);
            yList.Add(0.5f);  // Suit 수직 중앙
            iList.Add(inten);
        }

        if (xList.Count == 0) return;

        BhapticsLibrary.PlayPath(
            (int)PositionType.Vest,          // 디바이스
            xList.ToArray(),
            yList.ToArray(),
            iList.ToArray(),
            hapticDurationMs
        );
    }
    public void SetProgress(int progress)
    {
        _progress += progress;
        Debug.Log("Progress : " + _progress);
        VibrateByProgress();
    }

    private void VibrateByProgress()
    {
        Vector3 origin = player.transform.position + Vector3.up * 0.1f; // head height
        Vector3 forward = player.transform.forward;
        Transform target = destinations[_progress];
        // 플레이어의 목적지 방향
        Vector3 toTarget = (target.transform.position - player.transform.position).normalized;
        // 월드에서의 위 방향을 기준으로 각도 계산 (Y축 기준 회전)

        NativeArray<RaycastCommand> cmds = new NativeArray<RaycastCommand>(rays, Allocator.TempJob);
        NativeArray<RaycastHit> hits = new NativeArray<RaycastHit>(rays, Allocator.TempJob);
        QueryParameters qp = new QueryParameters(collisionMask, false, QueryTriggerInteraction.Ignore, false);
        float sector = 360f / rays;

        for (int i = 0; i < rays; ++i)
        {
            float ang = i * sector;
            Vector3 dir = Quaternion.Euler(0f, ang, 0f) * forward;
            cmds[i] = new RaycastCommand(origin, dir.normalized, qp, collisionMask);
        }

        JobHandle handle = RaycastCommand.ScheduleBatch(cmds, hits, 1); // 병렬 스케줄
        handle.Complete();

        MapHitsToHaptics(hits);

        cmds.Dispose();
        hits.Dispose();


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

        /*BhapticsLibrary.Play(
            BhapticsEvent.NAVIGATE,
            0,
            0.1f,
            1.0f,
            -angle,
            0.0f
        );*/

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
        if (!triggerKey) return;
        if (_vibRate >= 1.0f)
        {
            VibrateByProgress();
            _vibRate = 0.0f;
        }
        else
            _vibRate += Time.deltaTime;
    }


}
