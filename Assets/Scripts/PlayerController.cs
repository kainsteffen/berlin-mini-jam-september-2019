using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float accelerationSpeed;
    public float maxSpeed;
    public float maxRotationSpeed;

    public Transform cam;
    public Rigidbody rb;


    [Header("PID Controller")]
    //for PID Controller
    public float pGain = 1f;
    public float iGain = 1f;
    public float dGain = 0.4f;
    float lastPError = 0;

    /* https://robotics.stackexchange.com/questions/167/what-are-good-strategies-for-tuning-pid-loops
    * 
    * 1. Set all gains to zero.
      2. Increase the P gain until the response to a disturbance is steady oscillation.
      3. Increase the D gain until the the oscillations go away (i.e. it's critically damped).
      4. Repeat steps 2 and 3 until increasing the D gain does not stop the oscillations.
      5. Set P and D to the last stable values.
      6.Increase the I gain until it brings you to the setpoint with the number of oscillations desired (normally zero but a quicker response can be had if you don't mind a couple oscillations of overshoot)
    */

    Vector3 desiredMovementVector;


    private void Awake()
    {
        GameManager.Instance.activePlayers.Add(transform);
    }


    private void Update()
    {
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");

        Vector3 camRight = (cam.right).normalized;
        Vector3 horV = camRight * hor;
        Vector3 verV = new Vector3(-camRight.z, 0f, camRight.x) * ver;
        desiredMovementVector = horV + verV;

        //transform.forward = Vector3.Slerp(transform.forward, desiredMovementVector, maxRotationSpeed * Time.deltaTime * desiredMovementVector.magnitude);
        //Debug.Log("desiredMovementVector magnitude: " + desiredMovementVector.magnitude);

        //Debug.Log("acceleration force: " + accelerationSpeed * Time.deltaTime * desiredMovementVector.magnitude);
        rb.AddForce(transform.forward * accelerationSpeed * Time.deltaTime*desiredMovementVector.magnitude);
        if(rb.velocity.magnitude> maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }

        
        float deltaTime = Time.deltaTime;
        //PID Code
        float pError = Vector3.SignedAngle(transform.forward, desiredMovementVector, transform.up);
        Debug.Log("pError: " + pError);
        float iError = pError * deltaTime;
        float dError = (pError - lastPError) / deltaTime;
        lastPError = pError;

        float torque = (pGain * pError + iGain * iError + dGain * dError);// * rotationAcceleration;
        //we do nod necessary ned to multiply with rotationAcceleration - but it would be nice if this also makes a difference
        //Debug.Log("torque: " + torque);
        //cklamp - set a max rotation velocity
        if (torque > maxRotationSpeed) torque = maxRotationSpeed;
        else if (torque < -maxRotationSpeed) torque = -maxRotationSpeed;
        //Debug.Log("torque: " + torque);
        rb.AddTorque(transform.up * torque* desiredMovementVector.magnitude);
    }
    
}
