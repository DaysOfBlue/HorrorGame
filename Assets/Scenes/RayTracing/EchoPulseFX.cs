// EchoPulseFX.cs
// Attach to XR Rig / Player root (the object you regard as “listener”)
using System.Collections;
using UnityEngine;
#if UNITY_ANDROID || UNITY_STANDALONE
using Bhaptics.SDK2;                    // bHaptics v2 (rename if SDK namespace differs)
#endif

[RequireComponent(typeof(AudioSource))]
public class EchoPulseFX : MonoBehaviour
{
    /* ───── Inspector-exposed fields ───── */
    [Header("References")]
    [Tooltip("Fullscreen ShaderGraph material for the echo-pulse ring")]
    [SerializeField] private Material pulseMat;           // EchoPulse_Mat
    [Tooltip("5-50 ms broadband click sound")]
    [SerializeField] private AudioClip clickSfx;
    [Tooltip("Name of the .tact pattern (StreamingAssets/bHaptics/Patterns)")]
//    [SerializeField] private string tactFile = "NavPing.tact";

    [Header("Ping Parameters")]
    [SerializeField, Min(0.5f)] private float maxRangeMeters = 10f;
    [SerializeField] private float waveSpeed = 4f;         // m/s → _Radius Δ
    [SerializeField] private float cooldown = 0.8f;        // seconds between pings
    [SerializeField, Range(8, 128)] private int rayCount = 64;    // radial sample density
    [SerializeField] private LayerMask geometryMask = ~0;             // layers to ray-test

    /* ───── private state ───── */
    private AudioSource _src;
    private float _radius;       // current expanding radius
    private bool _animating;
    private float _nextTime;     // next allowed ping time

    /* ───── MonoBehaviour lifecycle ───── */
    private void Awake()
    {
        if (!pulseMat) Debug.LogError("Pulse Material not assigned!", this);

        _src = GetComponent<AudioSource>();
        _src.playOnAwake = false;
        _src.spatialBlend = 1f;   // 3D
        _src.loop = false;
    }

    private void Update()
    {
        /* 1) 입력 ‒ Quest A/X 버튼 또는 PC Space */
        bool pingInput = Input.GetKeyDown(KeyCode.Space) ||
                         OVRInput.GetDown(OVRInput.Button.One);

        if (pingInput && Time.time >= _nextTime)
        {
            _nextTime = Time.time + cooldown;
            StartCoroutine(PingRoutine());
        }

        /* 2) 화면-파동 애니메이션 */
        if (_animating)
        {
            _radius += waveSpeed * Time.deltaTime;
            pulseMat.SetFloat("_Radius", _radius);

            if (_radius > 1.2f)            // 충분히 화면을 덮으면 정지
                _animating = false;
        }
    }

    /* ───── coroutine: single ping ───── */
    private IEnumerator PingRoutine()
    {
        /* A. 사운드 */
        if (clickSfx) _src.PlayOneShot(clickSfx);

        /* B. 최단 거리 샘플링 (반사까지 거리) */
        float nearest = maxRangeMeters;
        Vector3 origin = transform.position;

        for (int i = 0; i < rayCount; ++i)
        {
            float ang = i * 360f / rayCount;
            Vector3 dir = Quaternion.Euler(0f, ang, 0f) * Vector3.forward;
            if (Physics.Raycast(origin, dir, out RaycastHit hit,
                                maxRangeMeters, geometryMask,
                                QueryTriggerInteraction.Ignore))
            {
                if (hit.distance < nearest) nearest = hit.distance;
            }
        }

        /* C. 셰이더 파라미터 세팅 */
        if (pulseMat)
        {
            Camera cam = Camera.main;
            if (!cam) { Debug.LogWarning("MainCamera not found"); yield break; }

            Vector3 uv = cam.WorldToViewportPoint(origin);    // 0-1
            pulseMat.SetVector("_OriginUV", new Vector4(uv.x, uv.y, 0, 0));
            pulseMat.SetFloat("_MaxRange", maxRangeMeters);
            pulseMat.SetFloat("_Radius", 0f);

            _radius = 0f;
            _animating = true;
        }


        yield return null;
    }
}
