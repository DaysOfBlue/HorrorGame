using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player Properties")]
    public int maxHealth;
    private int _currentHealth;

    private Rigidbody _rb;
    

    void Awake()
    {
        
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InitStatus();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void InitStatus()
    {
        _currentHealth = maxHealth;
    }

    void InitComponents()
    {
        _rb = GetComponent<Rigidbody>();
    }
}
