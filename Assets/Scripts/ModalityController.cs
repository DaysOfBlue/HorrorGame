using UnityEngine;

public class ModalityController : MonoBehaviour
{
    [SerializeField]
    bool soundEnabled = true;
    [SerializeField]
    bool hapticsEnabled = true;
    [SerializeField]
    bool visualsEnabled = true;

    void Start()
    {
        ToggleHaptics(hapticsEnabled);
        ToggleSound(soundEnabled);
        ToggleVisuals(visualsEnabled);
    }

    public void ToggleSound(bool isOn)
    {
        // Use the new FindObjectsByType method with FindObjectsSortMode.None for better performance.
        AudioSource[] allAudioSources = Object.FindObjectsByType<AudioSource>(FindObjectsSortMode.None);

        foreach (AudioSource source in allAudioSources)
        {
            source.mute = !isOn; // Adjust mute based on the 'isOn' parameter.
        }

        Debug.Log((isOn ? "Unmuted " : "Muted ") + allAudioSources.Length + " audio sources.");
    }

    public void ToggleHaptics(bool isOn)
    {
        HapticFeedbackManager.Instance.SetHapticFeedbackActive(isOn);
    }

    public void ToggleVisuals(bool isOn)
    {
        // Implement visual effects toggle logic here
    }
}
