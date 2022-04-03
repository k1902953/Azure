using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelMovement : MonoBehaviour
{
    public float moveSpeed = 5;
    //public GameObject[] characterPref;
    public GameObject player;
    float distance;
    public GameObject[] objects = new GameObject[3];
    Rotate scripts;
    Rotate scripts2;
    Rotate scripts3;
    bool barrelActive = false;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        scripts = objects[0].GetComponent<Rotate>();
        scripts.enabled = false;
        scripts2 = objects[1].GetComponent<Rotate>();
        scripts2.enabled = false;
        scripts3 = objects[2].GetComponent<Rotate>();
        scripts3.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(player.transform.position, transform.position);
        if (distance < 15)
        {
            barrelActive = true;
        }
        if (barrelActive == true)
        {
            scripts.enabled = true;
            scripts2.enabled = true;
            scripts3.enabled = true;
            transform.position -= transform.forward * Time.deltaTime * moveSpeed;
        }
    }
}
