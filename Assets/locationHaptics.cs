/*
 * EcholocationHaptics.cs
 *
 * Attach this to the XR rig (카메라가 달린 GameObject).
 * 필요한 전제:
 *   • Meta XR SDK 가 XR Plugin-Management에 설정돼 있을 것
 *   • bHaptics SDK2 → [bHaptics] 프리팹이 첫 씬에 존재할 것
 *   • ‘NAV_PULSE’ 라는 1초짜리 Vest 이벤트(32모터 동시 펄스)를
 *     bHaptics Designer/Portal 에서 만들어 둘 것
 *
 * 마이크 입력 없음.
 */
using UnityEngine;
using Bhaptics.SDK2;   // bHaptics SDK2 네임스페이스

[AddComponentMenu("XR/Assistive/EcholocationHaptics")]
public class locationHaptics : MonoBehaviour
{
    /* ---------- Raycast 파라미터 ---------- */
    [Header("Raycast Settings")]
    [Tooltip("한 번의 스캔에서 쏘는 Ray 개수")]
    [Range(12, 360)] public int raysPerScan = 60;
    [Tooltip("Ray 최대 거리(m)")] public float maxDistance = 5f;
    [Tooltip("스캔 주기(s)")] public float scanInterval = 0.25f;
    [Tooltip("Ray가 맞을 레이어")] public LayerMask obstructionLayers = ~0;
    public bool drawDebug = true;
    public Transform sight;

    /* ---------- Haptic 파라미터 ---------- */
    [Header("Haptics")]
    [Tooltip("이 거리 이하에서는 진동을 최소 강도로 클램프")]
    public float minDistance = 0.5f;
    [Tooltip("Vest 이벤트 이름(Designer에서 생성)")]
    public string bhapticsEvent = "NAV_PULSE";
    [Tooltip("1회 재생 길이(초)")] public float duration = 1.0f;

    private float _timer;

    void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= scanInterval)
        {
            _timer = 0f;
            PerformScan();
        }
    }

    /* ============================================================= */

    private void PerformScan()
    {
        float step = 360f / raysPerScan;

        for (int i = 0; i < raysPerScan; i++)
        {
            float yaw = i * step;
            Vector3 dir = Quaternion.Euler(0f, yaw, 0f) * sight.forward;

            if (Physics.Raycast(transform.position, dir, out RaycastHit hit,
                                maxDistance, obstructionLayers, QueryTriggerInteraction.Ignore))
            {
                float intensity = MapDistanceToIntensity(hit.distance);
                PlayHaptics(yaw, intensity);

                if (drawDebug)
                {
                    Color c = Color.Lerp(Color.red, Color.green, intensity); // red(멀리)→green(가까이)
                    Debug.DrawLine(transform.position, hit.point, c, scanInterval * 0.9f);
                }
            }
        }
    }

    // 멀면 1.0(강함), 가까우면 0.0(약함)으로 선형 매핑
    private float MapDistanceToIntensity(float distance)
    {
        distance = Mathf.Clamp(distance, minDistance, maxDistance) / 100;
        float t = Mathf.InverseLerp(minDistance, maxDistance, distance); // 가까울수록 t↓
        return 1f - t; // 멀수록 진동 ↑
    }

    // bHaptics SDK2: BhapticsLibrary.PlayParam() 사용 (SDK 문서 참조) :contentReference[oaicite:0]{index=0}
    private void PlayHaptics(float angleDeg, float intensity)
    {
        Debug.Log("intensity" + intensity + "/" + duration + "/" + angleDeg);
        BhapticsLibrary.PlayParam(
            BhapticsEvent.FOOTSTEP,          // 미리 정의한 이벤트
            intensity,              // 0~1
            duration,               // 원본 이벤트 길이에 곱해짐
            - angleDeg,               // Vest 좌우 각도
            0f                      // offsetY: 위아래 이동(0이면 중앙)
        );
    }
}
