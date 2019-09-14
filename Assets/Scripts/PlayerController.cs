using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float accelerationSpeed;
    public float maxSpeed;
    public float rotationSpeed;

    public Transform cam;
    public Rigidbody rb;

    Vector3 desiredMovementVector;


    private void Update()
    {
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");

        Vector3 camRight = (cam.right).normalized;
        Vector3 horV = camRight * hor;
        Vector3 verV = new Vector3(-camRight.z, 0f, camRight.x) * ver;
        desiredMovementVector = horV + verV;

        transform.forward = Vector3.Lerp(transform.forward, desiredMovementVector, rotationSpeed * Time.deltaTime);

        Debug.Log("acceleration force: " + accelerationSpeed * Time.deltaTime * desiredMovementVector.magnitude);
        rb.AddForce(transform.forward * accelerationSpeed * Time.deltaTime*desiredMovementVector.magnitude);
    }
    
}
