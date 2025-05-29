using System.Collections;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    public Light lightSource;
    public float minOnTime = 0.05f;
    public float maxOnTime = 0.2f;
    public float minOffTime = 0.05f;
    public float maxOffTime = 0.3f;

    void Start()
    {
        if (lightSource == null)
        {
            lightSource = GetComponent<Light>();
        }

        if (lightSource != null)
        {
            StartCoroutine(FlickerOnOff());
        }
        else
        {
            Debug.LogError("OnOffFlicker: No Light component found.");
            enabled = false;
        }
    }

    IEnumerator FlickerOnOff()
    {
        while (true)
        {
            lightSource.enabled = true;
            yield return new WaitForSeconds(Random.Range(minOnTime, maxOnTime));
            lightSource.enabled = false;
            yield return new WaitForSeconds(Random.Range(minOffTime, maxOffTime));
        }
    }
}
