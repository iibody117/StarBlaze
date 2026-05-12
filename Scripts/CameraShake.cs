using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] float shakeDuration = 0.5f; //shake Time
    [SerializeField] float shakeMagnitude = 0.5f; // shake Strength
    Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.position;
    }
    public void Play()
    {
        StartCoroutine(ShakeCamera());
    }

    IEnumerator ShakeCamera() // the funtion that does the shaking
    {
        float timeElapsed = 0;
        while (timeElapsed < shakeDuration)
        {
            transform.position = initialPosition + (Vector3)Random.insideUnitCircle * shakeMagnitude;
            timeElapsed += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }


    }

}
