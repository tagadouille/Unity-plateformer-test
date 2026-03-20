using System.Collections.Generic;
using UnityEngine;

public class MovingPlateform : MonoBehaviour
{
    /*Passengers that are on the plateform*/
    private List<Rigidbody> passengers = new List<Rigidbody>();
    private Vector3 lastPosition;
    private Quaternion lastRotation;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        lastPosition = rb.position;
        lastRotation = rb.rotation;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody playerRb = collision.rigidbody;

            if (playerRb != null && !passengers.Contains(playerRb))
            {
                passengers.Add(playerRb);
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody playerRb = collision.rigidbody;

            if (playerRb != null)
            {
                passengers.Remove(playerRb);
            }
        }
    }

    void FixedUpdate()
    {

        // Apply the transformations to the passengers
        Vector3 deltaPosition = rb.position - lastPosition;
        Quaternion deltaRotation = rb.rotation * Quaternion.Inverse(lastRotation);

        foreach (Rigidbody passenger in passengers)
        {
            passenger.MovePosition(passenger.position + deltaPosition);
            passenger.MoveRotation(deltaRotation * passenger.rotation);
        }

        lastPosition = rb.position;
        lastRotation = rb.rotation;
    }
}