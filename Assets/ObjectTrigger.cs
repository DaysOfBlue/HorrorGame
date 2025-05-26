using UnityEngine;

public class ObjectTrigger : MonoBehaviour
{
    public GameObject targetObject;

    private BoxCollider _boxCollider;

    void Awake()
    {
        _boxCollider = GetComponent<BoxCollider>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision other)
    {
        Debug.Log(other.gameObject.name);
        if (other.transform.name == targetObject.transform.name)
        {
            Renderer renderer = other.gameObject.GetComponent<Renderer>();
            Debug.Log("Target Object Detected!");
            Destroy(other.gameObject);
        }
    }
}
