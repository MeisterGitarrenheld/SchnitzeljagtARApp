using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerCamera : MonoBehaviour {

    public float cameraSpeed;

    Quaternion fixRotation;

    void Start()
    {
        Input.gyro.enabled = true;
        fixRotation = Quaternion.Euler(90, 0, 0);
    }

    protected void Update()
    {
        GyroModifyCamera();
    }

    /********************************************/

    // The Gyroscope is right-handed.  Unity is left handed.
    // Make the necessary change to the camera.
    void GyroModifyCamera()
    {
        transform.rotation =  fixRotation * GyroToUnity(Input.gyro.attitude);
    }

    private static Quaternion GyroToUnity(Quaternion q)
    {
        return new Quaternion(q.x, q.y, -q.z, -q.w);
    }
}
