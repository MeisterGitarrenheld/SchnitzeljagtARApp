using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuestenObject : MonoBehaviour
{

    public Transform otherObject;

    private void OnEnable()
    {
        if (otherObject != null)
            otherObject.gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        if (otherObject != null)
            otherObject.gameObject.SetActive(false);
    }

    void Update()
    {
        if (otherObject != null)
        {
            otherObject.position = Vector3.Lerp(otherObject.position, transform.position, 0.3f);
            otherObject.rotation = Quaternion.Lerp(otherObject.rotation, transform.rotation, 0.3f);
        }
    }
}
