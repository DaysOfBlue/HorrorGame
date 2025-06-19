using UnityEngine;
using UnityEngine.UI;

public enum PlayerStatus
{
    Danger, // Red
    Normal, // Orange
    Safe // Blue
}
public class PlayerRayShooter : Singleton<PlayerRayShooter>
{
    public float maxDistance = 100.0f;
    public Image frontImage;
    public GameObject hitEffect;

    public PlayerStatus playerStatus = PlayerStatus.Safe;
    public Color dangerColor = Color.red;
    public Color normalColor = Color.yellow;
    public Color safeColor = Color.blue;

    private int hp = 5;

    private AudioSource _audioSource;

    public AudioClip hitSound;

    void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Ray ray = new Ray(transform.position, transform.forward);
        //RaycastHit hit;

        if (Input.GetKeyDown(KeyCode.B))
        {
            GetDamage();
        }
        switch (playerStatus)
        {
            case PlayerStatus.Safe:
                frontImage.color = safeColor;
                Debug.Log("Blue!");
                break;
            case PlayerStatus.Normal:
                frontImage.color = normalColor;
                break;
            case PlayerStatus.Danger:
                frontImage.color = dangerColor;
                break;
            default:
                break;
        }

        /*
        if (Physics.Raycast(ray, out hit, maxDistance))
        {
            //Debug.Log("Hit Object : " + hit.collider.name);
            //Debug.Log("Distance to Object : " + hit.distance);
            // 거리에 따라 색상 지정
            Color nearColor = Color.red;
            Color midColor = Color.yellow;
            Color farColor = Color.blue;

            float distance = hit.distance;

            if (distance < 0.5f)
            {
                frontImage.color = nearColor;
            }
            else if (distance < 1.0f)
            {
                float t = (distance - 0.5f) / 0.5f; // 0.5~1.0 사이 → 0~1로 정규화
                frontImage.color = Color.Lerp(nearColor, midColor, t);
            }
            else if (distance < 2.0f) // 확장된 범위 (선택사항)
            {
                float t = (distance - 1.0f) / 1.0f; // 1.0~2.0 사이 → 0~1로 정규화
                frontImage.color = Color.Lerp(midColor, farColor, t);
            }
            else
            {
                frontImage.color = farColor;
            }
        }
        else
        {
            Debug.Log("No object detected within range. ");
        }
        */
        
    }
    public void GetDamage()
    {
        hp -= 1;
        hitEffect.GetComponent<Animator>().SetTrigger("HitTrigger");
        _audioSource.PlayOneShot(hitSound);
        if (hp <= 0)
        {
            Destroy(gameObject);
        }

    }
}
