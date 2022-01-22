using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public Vector3 rotationSpeed;

    void FixedUpdate()
    {
        transform.localEulerAngles += rotationSpeed;
    }
}
