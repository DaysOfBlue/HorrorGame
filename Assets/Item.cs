using System;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class Item : MonoBehaviour
{
    public float rotationSpeed = 20f; // 초당 회전 속도 (도 단위)

    private AudioSource _audioSource;
    private MeshRenderer _meshRenderer;
    public AudioClip itemObtain;

    void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _meshRenderer = GetComponent<MeshRenderer>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up + Vector3.left, -rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.AddItemCount(1);
            _meshRenderer.enabled = false;
            _audioSource.PlayOneShot(itemObtain);
            Destroy(this.gameObject, 1.0f);
        }
    }
}
