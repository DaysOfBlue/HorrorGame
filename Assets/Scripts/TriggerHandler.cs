using UnityEngine;
using UnityEngine.Events;

public class TriggerHandler : MonoBehaviour
{
    public string playerTag = "Player"; // Tag assigned to your player GameObject
    public bool triggerOnce = true;     // Should the event trigger only once?
    private bool hasBeenTriggered = false;

    // Option 1: Direct method call (if you know the specific script and method)
    // public MyGameManager gameManager; // Assign your game manager or other script in Inspector

    // Option 2: UnityEvent (flexible, assign listeners in the Inspector)
    public UnityEvent onPlayerEnter;
    public UnityEvent onPlayerExit;

    void OnTriggerEnter(Collider other)
    {
        // Check if the object that entered is the player (by tag)
        if (other.CompareTag(playerTag))
        {
            if (triggerOnce && hasBeenTriggered)
            {
                return; // Don't trigger again if it's a one-time event
            }

            Debug.Log("Player entered the trigger zone!");
            hasBeenTriggered = true;

            // --- This is where you trigger your event ---

            // Option 1: Direct method call
            // if (gameManager != null)
            // {
            //     gameManager.PlayerReachedCheckpoint();
            // }

            // Option 2: Invoke UnityEvent
            onPlayerEnter.Invoke();

            // Option 3: Call a method on another component on this trigger GameObject
            // GetComponent<SomeOtherScript>().DoSomething();

            // Option 4: Find an object and call a method (less performant, use sparingly)
            // GameObject.FindObjectOfType<ScoreManager>().IncrementScore(10);
        }
    }

    // Optional: Other trigger messages
    /*
    void OnTriggerStay(Collider other)
    {
        // Called once per frame for every Collider other that is touching the trigger.
        if (other.CompareTag(playerTag))
        {
            Debug.Log("Player is still in the trigger zone.");
        }
    }

    */

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            onPlayerExit.Invoke();
            Debug.Log("Player exited the trigger zone.");
            // You might want to reset hasBeenTriggered here if it's not a one-time event
            // if (!triggerOnce) hasBeenTriggered = false;
        }
    }

    // Gizmo to visualize the trigger area in the editor (optional)
    void OnDrawGizmos()
    {
        Collider col = GetComponent<Collider>();
        if (col != null)
        {
            Gizmos.color = new Color(0, 1, 0, 0.3f); // Green, semi-transparent
            if (col is BoxCollider)
            {
                BoxCollider boxCol = col as BoxCollider;
                Gizmos.matrix = Matrix4x4.TRS(transform.TransformPoint(boxCol.center), transform.rotation, transform.lossyScale);
                Gizmos.DrawCube(Vector3.zero, boxCol.size);
            }
            else if (col is SphereCollider)
            {
                SphereCollider sphereCol = col as SphereCollider;
                Gizmos.matrix = Matrix4x4.TRS(transform.TransformPoint(sphereCol.center), transform.rotation, transform.lossyScale);
                Gizmos.DrawSphere(Vector3.zero, sphereCol.radius);
            }
            // Add more for other collider types if needed
        }
    }
}
