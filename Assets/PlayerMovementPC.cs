using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovementPC : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float gravity = -9.81f;

    private CharacterController _controller;
    private Vector3 _velocity;
    public Transform _mainCamera;

    void Start()
    {
        _controller = GetComponent<CharacterController>();
        //_mainCamera = Camera.main.transform;
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        // 카메라 방향 기준으로 전후좌우 입력 처리
        Vector3 forward = _mainCamera.forward;
        Vector3 right = _mainCamera.right;
        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();

        float h = Input.GetAxisRaw("Horizontal"); // A (-1) / D (+1)
        float v = Input.GetAxisRaw("Vertical"); // W (+1) / S (-1)

        Vector3 moveDir = (forward * v + right * h).normalized;

        _controller.Move(moveDir * moveSpeed * Time.deltaTime);

        // 중력 적용
        if (_controller.isGrounded && _velocity.y < 0)
        {
            _velocity.y = -2f; // 땅에 붙어 있도록 약간 음수로 설정
        }

        _velocity.y += gravity * Time.deltaTime;
        _controller.Move(_velocity * Time.deltaTime);
    }
}