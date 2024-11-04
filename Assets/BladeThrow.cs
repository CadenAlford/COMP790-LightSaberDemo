using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeThrow : MonoBehaviour
{
    public Transform blade;           // Reference to the lightsaber blade
    public Transform handPosition;     // The original position in the player's hand
    public float throwSpeed = 20f;     // Speed of the throw
    public float returnSpeed = 25f;    // Speed of the return
    public float maxThrowDistance = 20f; // Maximum throw distance

    private bool isThrown = false;
    private bool isReturning = false;
    private Vector3 throwTarget;

    void Update()
    {
        // Throw the lightsaber with Q
        if (Input.GetKeyDown(KeyCode.Q) && !isThrown && !isReturning)
        {
            throwTarget = transform.position + transform.forward * maxThrowDistance;
            isThrown = true;
        }

        // Return the lightsaber with E
        if (Input.GetKeyDown(KeyCode.E) && isThrown)
        {
            isReturning = true;
        }

        // Handle lightsaber throw
        if (isThrown && !isReturning)
        {
            blade.position = Vector3.MoveTowards(blade.position, throwTarget, throwSpeed * Time.deltaTime);
            
            // Stop throw if target reached
            if (Vector3.Distance(blade.position, throwTarget) < 0.1f)
            {
                isThrown = false;
            }
        }

        // Handle lightsaber return
        if (isReturning)
        {
            blade.position = Vector3.MoveTowards(blade.position, handPosition.position, returnSpeed * Time.deltaTime);

            // Stop return if back in hand
            if (Vector3.Distance(blade.position, handPosition.position) < 0.1f)
            {
                isReturning = false;
                isThrown = false;
            }
        }
    }
}
