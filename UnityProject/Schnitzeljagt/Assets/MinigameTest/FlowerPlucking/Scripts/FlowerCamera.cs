using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerCamera : MonoBehaviour {

    public float cameraSpeed;

    Vector2 mousePosition;
    Vector2 oldMousePosition;

    void Start()
    {
        Input.gyro.enabled = true;
    }

    protected void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mousePosition = Input.mousePosition;
            oldMousePosition = mousePosition;
        }
        else if (Input.GetMouseButton(0))
        {
            mousePosition = Input.mousePosition;
            Vector2 mouseOffset = (mousePosition - oldMousePosition) * cameraSpeed;
            transform.Rotate(Vector3.up, mouseOffset.x, Space.World);
            transform.Rotate(transform.right, -mouseOffset.y, Space.World);
            oldMousePosition = mousePosition;
        }
        else
        {
            GyroModifyCamera();
        }
    }

    /********************************************/

    // The Gyroscope is right-handed.  Unity is left handed.
    // Make the necessary change to the camera.
    void GyroModifyCamera()
    {
        transform.rotation = GyroToUnity(Input.gyro.attitude);
    }

    private static Quaternion GyroToUnity(Quaternion q)
    {
        return new Quaternion(q.x, q.y, -q.z, -q.w);
    }
}
