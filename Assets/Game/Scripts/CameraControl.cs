using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Transform target;
    private Vector3 offset;
    public float trailDistance = 12.5f;
    public float heightOffset = 5.0f;
    public float cameraDelay = 0.002f;
    
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        offset = transform.position - target.position;
    }

    void FixedUpdate()
    {
        if (target.rotation.y >=-0.4f && target.rotation.y <=-0.0f)
        {
            Vector3 newPosition = new Vector3(transform.position.x, transform.position.y, offset.z + target.position.z);
            transform.position = Vector3.Lerp(transform.position, newPosition, 0.6f);
            //Debug.Log("1");
        }
        else if (target.rotation.y < -0.4f && target.rotation.y > -0.8f)
        {
            transform.LookAt(target);
            //transform.eulerAngles = new Vector3( transform.eulerAngles.x, target.transform.eulerAngles.y, transform.eulerAngles.z );
            
            Vector3 newRotation = new Vector3(transform.position.x, transform.position.y, target.position.z);
            transform.eulerAngles = new Vector3( 8.1f, target.transform.eulerAngles.y, transform.eulerAngles.z );
            //transform.position = newRotation;
            transform.position = Vector3.Lerp(transform.position, newRotation, 0.6f);
            //Debug.Log("2");
        }
        else if(target.rotation.y >= 0.7f){
            //transform.eulerAngles = new Vector3( transform.eulerAngles.x, target.transform.eulerAngles.y, transform.eulerAngles.z );
            
            Vector3 newPosition = new Vector3(15 + target.position.x, transform.position.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, newPosition, 0.6f);
            transform.eulerAngles = new Vector3( 8.1f, 270.0f, transform.eulerAngles.z );
            //Debug.Log("3");
            //transform.position = newPosition;
        }else{
            Debug.Log("nothing");
        }
    }
}
