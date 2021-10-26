using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Transform target;
    private Vector3 offset;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        offset = transform.position - target.position;
    }

    void FixedUpdate()
    {
        if (target.rotation.y >-80.0f)
        {
            Vector3 newPosition = new Vector3(transform.position.x, transform.position.y, offset.z + target.position.z);
            transform.position = Vector3.Lerp(transform.position, newPosition, 0.6f);
            

            Debug.Log("1");

        }
        else if (target.rotation.y < -81.0f)
        {
            Vector3 newPosition = new Vector3(offset.x + target.position.x, transform.position.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, newPosition, 0.6f);

            //Vector3 newRotation = new Vector3(target.position.x, transform.position.y, target.transform.position.z);
            //transform.LookAt(newRotation);

            Debug.Log("2");

        }
        Vector3 newRotation = new Vector3(target.position.x, transform.position.y, target.transform.position.z);
        transform.LookAt(newRotation);
        //Quaternion newRotation = Quaternion.Euler(new Vector3(0, target.rotation.y, 0));
        //Quaternion newRotation = new Vector3(transform.rotation.x, target.rotation.y, transform.rotation.z);
        //transform.rotation = newRotation;
        //transform.rotation = Quaternion.Euler(new Vector3(target.rotation.x, target.rotation.y, target.rotation.z));
    }
}
