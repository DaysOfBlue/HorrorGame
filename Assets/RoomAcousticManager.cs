using UnityEngine;

public class RoomAcousticManager : MonoBehaviour
{

    public GameObject roomAcoustics;
    public MetaXRAudioRoomAcousticProperties _roomScript;
    void Awake()
    {
        _roomScript = roomAcoustics.GetComponent<MetaXRAudioRoomAcousticProperties>();
    }

    // Update is called once per frame
    // Æ®¸®°Å¿¡ ºÎµúÈú ¶§
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("trigger on");
        Debug.Log(other.name + "/" + other.tag);
        switch (other.tag)
        {
            case "AcousticZone1":
                ApplyRoomChange(new Vector3(23.77155f, -3.414309f, 9.712563f), new Vector3(54f, 3f, 4f));
                break;
            case "AcousticZone2":
                ApplyRoomChange(new Vector3(-1.34f, -3.414309f, 4.93f), new Vector3(4f, 3f, 13f));
                break;
            case "AcousticZone3":
                ApplyRoomChange(new Vector3(-31.34f, -3.414309f, 0.14f), new Vector3(63f, 3f, 4f));
                break;
            case "AcousticZone4":
                ApplyRoomChange(new Vector3(-61.2f, -3.414309f, -29.1f), new Vector3(4f, 3f, 63f));
                break;
            case "AcousticZone5":
                ApplyRoomChange(new Vector3(-90.7f, -3.414309f, -59f), new Vector3(63f, 3f, 4f));
                break;
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
