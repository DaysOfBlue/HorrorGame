using System;
using UnityEngine;

public class DangerBall : MonoBehaviour
{
    public AudioClip rollingSound;
    private AudioSource _audioSource;

    void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _audioSource.PlayOneShot(rollingSound);
        Destroy(this.gameObject, 5.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerRayShooter.Instance.GetDamage();
        }
    }
}
