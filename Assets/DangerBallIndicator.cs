using UnityEngine;

public class DangerBallIndicator : MonoBehaviour
{
    private Material _material;
    private float _alpha = 0f;
    private bool _increasing = true;

    public float fadeSpeed = 2f; // 높을수록 빠르게 왕복함

    void Start()
    {
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            _material = renderer.material; // 인스턴스화된 마테리얼 사용
        }
    }

    void Update()
    {
        if (_material == null) return;

        // 알파값 증가/감소 계산
        _alpha += (_increasing ? 1 : -1) * fadeSpeed;
        _alpha = Mathf.Clamp01(_alpha);

        // 알파값 적용
        Color color = _material.color;
        color.a = _alpha;
        _material.color = color;

        // 방향 전환
        if (_alpha >= 1f)
        {
            _increasing = false;
        }
        else if (_alpha <= 0f)
        {
            _increasing = true;
        }
    }
}
