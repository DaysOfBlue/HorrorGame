using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class GrabbableObject : MonoBehaviour
{
    public Color objectColor;
    private XRGrabInteractable grabInteractable;
    private Renderer objectRenderer;

    void Start()
    {
        // XR Grab Interactable 컴포넌트 가져오기
        grabInteractable = GetComponent<XRGrabInteractable>();
        objectRenderer = GetComponent<Renderer>();

        // 잡힐 때와 놓일 때 이벤트 등록
        grabInteractable.selectEntered.AddListener(OnGrab);
        grabInteractable.selectExited.AddListener(OnRelease);
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        // 물체가 잡혔을 때 실행
        Debug.Log("Object Grabbed!");
        objectRenderer.material.color = objectColor; // 색상 변경 예시
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        // 물체가 놓였을 때 실행
        Debug.Log("Object Released!");
        objectRenderer.material.color = Color.white; // 원래 색상으로 복구
    }

    void OnDestroy()
    {
        // 이벤트 해제
        grabInteractable.selectEntered.RemoveListener(OnGrab);
        grabInteractable.selectExited.RemoveListener(OnRelease);
    }
}
