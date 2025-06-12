using UnityEngine;
using Bhaptics.SDK2;
public class PlayerHapticFeedback : MonoBehaviour
{
    public Transform player;

    public Transform soundSource;

    public string tactFileName = "SoundFeedback";
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Vector3 dirToSound = soundSource.position - player.position;
            float angle = Vector3.SignedAngle(player.forward, dirToSound, Vector3.up);
            
            float rotationAngle = (angle + 360f) % 360f;

        }
    }
}
