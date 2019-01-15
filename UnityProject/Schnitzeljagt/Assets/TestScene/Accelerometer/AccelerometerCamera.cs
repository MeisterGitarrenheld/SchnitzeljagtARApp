using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccelerometerCamera : MonoBehaviour {


    Quaternion fixRotation;
    Quaternion offsetRotation;

    Vector3 offsetAcceleration;

    void Start()
    {
        Input.gyro.enabled = true;
        fixRotation = Quaternion.Euler(90, 0, 0);
        offsetRotation = Quaternion.Euler(0, 0, 0);
        offsetAcceleration = Vector3.zero;
    }

    protected void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            ResetGyroCamera();
            ResetAcceleration();
            transform.position = Vector3.zero + Vector3.up * 2;
        }
        GyroModifyCamera();
        transform.Translate(Input.acceleration.x + offsetAcceleration.x, 0, -Input.acceleration.z + offsetAcceleration.z);
    }

    /********************************************/

    // The Gyroscope is right-handed.  Unity is left handed.
    // Make the necessary change to the camera.
    void GyroModifyCamera()
    {
        transform.rotation = offsetRotation * fixRotation * GyroToUnity(Input.gyro.attitude);
    }

    private static Quaternion GyroToUnity(Quaternion q)
    {
        return new Quaternion(q.x, q.y, -q.z, -q.w);
    }
    public void ResetGyroCamera()
    {
        offsetRotation = Quaternion.Inverse(fixRotation * GyroToUnity(Input.gyro.attitude));
    }

    public void ResetAcceleration()
    {
        offsetAcceleration = new Vector3(-Input.acceleration.x, 0, Input.acceleration.z);
    }
}
