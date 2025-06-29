using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Audio;

public class Monster : MonoBehaviour
{
    [Header("Monster Properties")]
    public int maxHealth = 100;
    public float moveSpeed = 1.0f;
    public float speedWhenSeen = 5f;
    public float speedWhenUnSeen = 2f;
    public float sightThreshold = 0.7f;
    public GameObject playerSight;
    public float animSmoothTime = 0.1f;

    [Header("Sound Settings")]
    public AudioSource audioFootsteps;
    public AudioSource audioVocal;
    public AudioClip growlSound;
    public AudioClip[] stepSounds;
    public float growlRate = 1.0f;
    private float growlTimer = 0.0f;

    private GameObject _player;
    private float _distFromPlayer;
    
    private NavMeshAgent _agent;
    private Animator _animator;

    private bool _canAttack = true;
    private float _attackTimer = 0.0f;

    void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();

        _player = GameObject.FindGameObjectWithTag("Player");
        playerSight = GameObject.Find("CenterEyeAnchor");
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

    void Attack()
    {
        if (_canAttack)
        {
            Debug.Log("Attack!");
            PlayerRayShooter.Instance.GetDamage();
            _canAttack = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_player == null) return;
        Vector3 toEnemy = (transform.position - _player.transform.position).normalized;
        float dist = Vector3.Distance(transform.position, _player.transform.position);
        //Debug.Log(dist);
        float dot = Vector3.Dot(playerSight.transform.forward, toEnemy);

        if (2.0f < dist && dist <= 5.0f)
        {
            PlayerRayShooter.Instance.playerStatus = PlayerStatus.Danger;
            //Debug.Log("YESAH!");
        }
        else if (dist > 5.0f && dist < 8.0f)
        {
            PlayerRayShooter.Instance.playerStatus = PlayerStatus.Normal;
        }
        else if (dist >= 0.0f && dist <= 2.0f)
        {
            Attack();
        }
        else
        {
            PlayerRayShooter.Instance.playerStatus = PlayerStatus.Safe;
        }
        


        if (growlTimer >= growlRate)
        {
            MakeGrowlSound();
            growlTimer = 0.0f;
        }
        else
        {
            growlTimer += Time.deltaTime;
        }

        if (dot > sightThreshold)
        {
            _agent.speed = speedWhenSeen;
            //PlayerRayShooter.Instance.playerStatus = PlayerStatus.Danger;
        }
        else
        {
            _agent.speed = speedWhenUnSeen;
            //PlayerRayShooter.Instance.playerStatus = PlayerStatus.Safe;
        }

        _agent.SetDestination(_player.transform.position);

        float currentSpeed = _agent.velocity.magnitude;
        _animator.SetFloat("Speed", currentSpeed, animSmoothTime, Time.deltaTime);
        _animator.SetBool("IsRunning", currentSpeed > 0.1f);

        if (!_canAttack)
        {
            _attackTimer += Time.deltaTime;
        }

        if (_attackTimer >= 3.0f)
        {
            _canAttack = true;
            _attackTimer = 0.0f;
        }

    }

    public void SD()
    {
        _agent.SetDestination(_player.transform.position);
    }

    void MakeGrowlSound()
    {
        Debug.Log("Growl....!");
        //_audioSource.clip = growlSound;
        _distFromPlayer = Vector3.Distance(gameObject.transform.position, _player.transform.position);
        float volumeRatio = 1 - Mathf.InverseLerp(1, 100, _distFromPlayer);
        audioVocal.volume = Mathf.Clamp01(volumeRatio);
        audioVocal.PlayOneShot(growlSound);
    }

    public void PlayFootstep()
    {
        if (stepSounds.Length == 0 || audioFootsteps == null) return;

        int index = Random.Range(0, stepSounds.Length);
        audioFootsteps.PlayOneShot(stepSounds[index]);
    }
    
}
