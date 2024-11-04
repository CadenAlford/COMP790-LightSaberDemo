using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Extend : MonoBehaviour
{
     // Instance variables
    public GameObject blade;
    public float extendSpeed = 0.1f; // Extend/collapse speed
    private bool weaponActive = true; // Lightsaber state (on/off)
    private float scaleMin = 0f; // Minimum scale value
    private float scaleMax; // Maximum scale value (initial y scale of the blade)
    private float extendDelta; // Interpolation value for scaling
    private float scaleCurrent; // Current y scale of the lightsaber blade
    private float initialScaleX, initialScaleZ; // Initial local x and z scale values

    // Audio variables
    public AudioClip extendSound; // Sound to play when extending
    public AudioClip retractSound; // Sound to play when retracting
    private AudioSource audioSource;

    // Initialization
    void Start()
    {
        // Save the initial local x and z scale values
        initialScaleX = transform.localScale.x;
        initialScaleZ = transform.localScale.z;
        
        // Set the maximum y scale to the current y scale
        scaleMax = transform.localScale.y;
        
        // Start with the lightsaber fully extended
        scaleCurrent = scaleMax;
        
        // Calculate the initial interpolation value
        extendDelta = scaleMax / extendSpeed;
        
        // Set the weapon state to active (on)
        weaponActive = true;

        // Initialize the audio source
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // Toggle lightsaber state on spacebar press
        if (Input.GetKeyDown(KeyCode.Space))
        {
            extendDelta = weaponActive ? -Mathf.Abs(extendDelta) : Mathf.Abs(extendDelta);
            PlaySound(); // Play the appropriate sound on toggle
        }

        // Adjust current scale based on interpolation and deltaTime
        scaleCurrent += extendDelta * Time.deltaTime;
        
        // Clamp scale within min and max bounds
        scaleCurrent = Mathf.Clamp(scaleCurrent, scaleMin, scaleMax);

        // Apply new scale to the local y axis of the blade
        transform.localScale = new Vector3(initialScaleX, scaleCurrent, initialScaleZ);

        // Update lightsaber state based on scale
        bool newWeaponState = scaleCurrent > 0;
        if (newWeaponState != weaponActive)
        {
            weaponActive = newWeaponState;
            blade.SetActive(weaponActive); // Toggle blade rendering based on state
        }
    }

    // Play the correct sound based on the direction of the lightsaber
    private void PlaySound()
    {
        if (extendDelta > 0 && extendSound != null)
        {
            audioSource.PlayOneShot(extendSound); // Play extend sound
        }
        else if (extendDelta < 0 && retractSound != null)
        {
            audioSource.PlayOneShot(retractSound); // Play retract sound
        }
    }
}

