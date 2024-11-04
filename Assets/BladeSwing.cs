using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeSwing : MonoBehaviour
{
   public AudioClip swingSound;
    public float swingSpeed = 500f; // Speed of the swing
    public float swingDuration = 0.2f; // Duration of the swing in seconds

    private AudioSource audioSource;
    private Quaternion originalRotation; // To reset rotation after swing
    private bool isSwinging = false;

    void Start()
    {
        // Set up the audio source
        audioSource = gameObject.AddComponent<AudioSource>();

        // Save the initial rotation
        originalRotation = transform.rotation;
    }

    void Update()
    {
        // Trigger swing on left-click (mouse button 0)
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

        // Rotate quickly to simulate a swing
        Quaternion targetRotation = Quaternion.Euler(90, 180, 90) * originalRotation; // 90 degrees as an example
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
