using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Extend : MonoBehaviour
{
     // Instance variables
    public GameObject blade;
    public float extendSpeed = 0.1f; // Extend/collapse speed
    private bool weaponActive = true; // Lightsaber state
    private float scaleMin = 0f; // Minimum scale value
    private float scaleMax; // Maximum scale value 
    private float extendDelta; // Interpolation value for scaling
    private float scaleCurrent; // Current y scale of the lightsaber blade
    private float initialScaleX, initialScaleZ; // Initial local x and z scale values

    // Audio variables
    public AudioClip extendSound; 
    public AudioClip retractSound; 
    private AudioSource audioSource;

    
    void Start()
    {
        // Save the initial local x and z scale values
        initialScaleX = transform.localScale.x;
        initialScaleZ = transform.localScale.z;
        
        // maximum y scale to the current y scale
        scaleMax = transform.localScale.y;
        
        // Start lightsaber extended
        scaleCurrent = scaleMax;
        
        // Calculate interpolation value
        extendDelta = scaleMax / extendSpeed;
        
        // Set the weapon state to active 
        weaponActive = true;

        audioSource = gameObject.AddComponent<AudioSource>();
    }

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
            blade.SetActive(weaponActive); 
        }
    }

    private void PlaySound()
    {
        if (extendDelta > 0 && extendSound != null)
        {
            audioSource.PlayOneShot(extendSound); 
        }
        else if (extendDelta < 0 && retractSound != null)
        {
            audioSource.PlayOneShot(retractSound); 
        }
    }
}

