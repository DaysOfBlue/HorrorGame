using System;
using UnityEngine;
using Object = UnityEngine.Object;

public class ModalityController : MonoBehaviour
{
    [SerializeField]
    bool soundEnabled = true;
    [SerializeField]
    bool hapticsEnabled = true;
    [SerializeField]
    bool visualsEnabled = true;

    public int progress = 0;

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            switch (progress)
            {
                case 0:
                    ToggleSound(true);
                    Debug.Log("Current Modality : Visual, Sound");
                    break;
                case 1:
                    ToggleSound(false);
                    ToggleHaptics(true);
                    Debug.Log("Current Modality : Visual, Haptic");
                    break;
                case 2:
                    ToggleSound(true);
                    ToggleHaptics(true);
                    Debug.Log("Current Modality : Visual, Haptic, Sound");
                    break;
                default:
                    break;
            }
        }
    }
}
