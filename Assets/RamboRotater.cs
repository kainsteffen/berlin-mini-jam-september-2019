using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RamboRotater : MonoBehaviour
{

    public float slowRot;
    public float fastRot;

    public void UpdateRotation(float speed)
    {
        Quaternion slowQuat = transform.rotation;
        slowQuat = Quaternion.Euler(slowRot,slowQuat.eulerAngles.y, slowQuat.eulerAngles.z);

        Quaternion fastQuat = transform.rotation;
        fastQuat = Quaternion.Euler(fastRot, slowQuat.eulerAngles.y, slowQuat.eulerAngles.z);


        transform.rotation = Quaternion.Lerp(slowQuat, fastQuat, speed);
    }
}
