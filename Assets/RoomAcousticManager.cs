using UnityEngine;

public class RoomAcousticManager : MonoBehaviour
{

    public GameObject roomAcoustics;
    private MetaXRAudioRoomAcousticProperties _roomScript;

    public Vector3 position;
    public Vector3 scale;

    void Awake()
    {
        _roomScript = roomAcoustics.GetComponent<MetaXRAudioRoomAcousticProperties>();
    }

    // Update is called once per frame
    // Æ®¸®°Å¿¡ ºÎµúÈú ¶§
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("trigger on");
        
        if(other.CompareTag("Player"))
        {
            ApplyRoomChange(position, scale);
        }
    }

    private void ApplyRoomChange(Vector3 newPosition, Vector3 newSize)
    {
        if (roomAcoustics != null)
        {
            roomAcoustics.transform.position = newPosition;
            Debug.Log("change position");
        }
        Debug.Log("in function");
        if (_roomScript != null) 
        {
            _roomScript.width = newSize.x;
            _roomScript.height = newSize.y;
            _roomScript.depth = newSize.z;
            Debug.Log("change size");
        }
    }

}
