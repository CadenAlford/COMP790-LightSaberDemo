using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeSwing : MonoBehaviour
{
    public AudioClip swingSound;
    public float swingSpeed = 500f; 
    public float swingDuration = 0.2f; 

    private AudioSource audioSource;
    private Quaternion originalRotation; 
    private bool isSwinging = false;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();

        originalRotation = transform.rotation;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isSwinging)
        {
            StartCoroutine(Swing());
        }
    }

    private IEnumerator Swing()
    {
        isSwinging = true;

        // Play swing sound
        if (swingSound != null)
        {
            audioSource.PlayOneShot(swingSound);
        }

        // Rotate to simulate a swing
        Quaternion targetRotation = Quaternion.Euler(90, 180, 90) * originalRotation; 
        float elapsedTime = 0;

        while (elapsedTime < swingDuration)
        {
            transform.rotation = Quaternion.Slerp(originalRotation, targetRotation, (elapsedTime / swingDuration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Return to the original rotation
        transform.rotation = originalRotation;
        isSwinging = false;
    }
}
