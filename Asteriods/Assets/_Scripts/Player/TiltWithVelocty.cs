using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiltWithVelocty : MonoBehaviour
{
    [SerializeField] int degrees = 30;

    public bool tiltTowards = true;

    // Cache slow operations
    private int prevDegrees = int.MaxValue;

    private float tan;

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // Figures out how far away from the vector the z-axis is
        if (degrees != prevDegrees)
        {
            prevDegrees = degrees;
            tan = Mathf.Tan(Mathf.Deg2Rad * degrees);
        }

        Vector3 pitchDir = (tiltTowards) ? -rb.velocity : rb.velocity;

        pitchDir += Vector3.forward / tan * PlayerShip.MAX_SPEED;

        transform.LookAt(transform.position + pitchDir);
    }
}
