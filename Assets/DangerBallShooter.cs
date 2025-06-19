using System;
using UnityEngine;
using System.Collections;

public class DangerBallShooter : MonoBehaviour
{
    public GameObject dangerBallPrefab;
    public Transform shootPos;
    public GameObject indicatorObject;
    public float fadeSpeed = 1.0f;

    public float shootRate = 5.0f;

    private float shootTimer = 0.0f;

    public AudioClip shootSound;
    private AudioSource _audioSource;

    public void StartShootSequence()
    {
        StartCoroutine(ShowIndicatorAndShoot());
    }

    private IEnumerator ShowIndicatorAndShoot()
    {
        // Indicator 활성화
        indicatorObject.SetActive(true);

        // 1초 대기
        yield return new WaitForSeconds(1.0f);

        // Indicator 비활성화
        indicatorObject.SetActive(false);

        // 발사
        Shoot();
    }

    [Obsolete("Obsolete")]
    public void Shoot()
    {
        GameObject dangerBall = Instantiate(dangerBallPrefab, shootPos.position, transform.rotation);
        _audioSource.PlayOneShot(shootSound);
        Rigidbody rb = dangerBall.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = transform.forward * 10.0f;
        }
    }

    void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    void Start()
    {
        // 예시: 시작하자마자 발사 순서 실행
        // 원하지 않으면 삭제하세요.
        //StartShootSequence();
    }

    void Update()
    {
        // 예시: shootRate 주기로 자동 발사
        if (Input.GetKeyDown(KeyCode.A))
        {
            StartShootSequence();
        }
    }
}
