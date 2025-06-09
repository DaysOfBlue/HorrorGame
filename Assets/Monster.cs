using UnityEngine;

public class Monster : MonoBehaviour
{
    [Header("Monster Properties")]
    public int maxHealth = 100;


    [Header("Sound Settings")] 
    public AudioClip growlSound;
    public AudioClip stepSound;
    public float growlRate = 1.0f;
    private float growlTimer = 0.0f;
    private AudioSource _audioSource;

    private GameObject _player;
    private float _distFromPlayer;

    void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        
        _player = GameObject.Find("PlayerOVR");
        if (_player != null)
        {
            Debug.Log("Object Found! : " + _player.name);
            _distFromPlayer = Vector3.Distance(gameObject.transform.position, _player.transform.position);
            Debug.Log(_distFromPlayer);
        }
        else
        {
            Debug.LogWarning("Object not Found!");
        }

    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (growlTimer >= growlRate)
        {
            //MakeGrowlSound();
            growlTimer = 0.0f;
        }
        else
        {
            growlTimer += Time.deltaTime;
        }
    }

    void MakeGrowlSound()
    {
        Debug.Log("Growl....!");
        //_audioSource.clip = growlSound;
        _distFromPlayer = Vector3.Distance(gameObject.transform.position, _player.transform.position);
        float volumeRatio = 1 - Mathf.InverseLerp(1, 100, _distFromPlayer);
        _audioSource.volume = Mathf.Clamp01(volumeRatio);
        _audioSource.PlayOneShot(growlSound);
    }
}
