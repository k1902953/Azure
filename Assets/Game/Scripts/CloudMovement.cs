using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.VFX;

public class CloudMovement : MonoBehaviour
{
    private NavMeshAgent nma;
    public Transform waypointsParent;
    public GameObject player;
    float enemydist;
    List<Transform> waypoints = new List<Transform>();
    private int wpIndex = 0;
    public VisualEffect myEffect;
    public BoxCollider myEffectCollider;
    bool attacking = false;
    float moveSpeed = 3;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        nma = GetComponent<NavMeshAgent>();
        foreach (Transform child in waypointsParent) waypoints.Add(child);
        nma.autoBraking = true;
        nma.destination = waypoints[wpIndex].transform.position;
    }

    void Update()
    {
        // Detect the player
        enemydist = Vector3.Distance(player.transform.position, transform.position);

        if(Vector3.Distance(transform.position, waypoints[wpIndex].transform.position) <= 2f)
        {
            //patrol
            IncreaseIndex();
        }


        if (enemydist < 10f && enemydist > 4.2f)
        {
            Find();
        }
        else if (enemydist <= 4.2f && attacking == false)
        {
            attacking = true;
            Attack();
        }
        else if (enemydist > 10f)
            {
            nma.speed = 3.5f;
            nma.destination = waypoints[wpIndex].transform.position;
        }
    }

    IEnumerator waiting()
    {
        
        yield return new WaitForSeconds(1f);
        myEffectCollider.enabled = false;
        attacking = false;
    }

    void Attack()
    {
        myEffect.Play();
        myEffectCollider.enabled = true;
        StartCoroutine(waiting());
    }

    void Find()
    {
        nma.speed = 6f;
        nma.destination = new Vector3(player.transform.position.x, 4.427262f, player.transform.position.z);
    }

    void IncreaseIndex()
    {
        if (wpIndex < waypoints.Count - 1)
        {
            wpIndex++;
        }
        else
        {
            wpIndex = 0;
        }
        nma.destination = waypoints[wpIndex].transform.position;
    }
}
