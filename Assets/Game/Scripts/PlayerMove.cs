using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    Animator anim;                  // the animator
    private CharacterController pcontroller;
    //private PathFollower pf;
    public float moveSpeed = 10;
    public float leftrightSpeed = 5;
    private bool slide = false;
    private float _gravity = 1;
    private float yVelocity = 0.0f;
    private bool go = false;
    private bool completedlvl = false;
    private GameManager gameManager;
    float distanceTravelled;
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;
    [SerializeField] private Transform pointC;
    [SerializeField] private Transform p0;
    [SerializeField] private Transform p1;
    [SerializeField] private Transform p01;
    private float interpolateAmount;
    private bool curveStarted = false;

    void Start()
    {
        anim = GetComponent<Animator>();
        anim.ResetTrigger("isDead");
        anim.ResetTrigger("won");
        pcontroller = GetComponent<CharacterController>();
        
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    
    // Update is called once per frame
    void Update()
    {
        anim.SetBool("movingf", false);
        anim.SetBool("movingb", false);
        anim.SetBool("runright", false);
        anim.SetBool("runleft", false);
        anim.ResetTrigger("slide");

        if (go == false && completedlvl == false)
        {
            Vector3 direction = new Vector3(0, 0, 0);
            Vector3 velocity = direction * moveSpeed;
            if (pcontroller.isGrounded == true)
            {
                anim.SetBool("inAir", false);

            }
            else
            {
                yVelocity -= _gravity;
            }
            velocity.y = yVelocity;
            pcontroller.Move(velocity * Time.deltaTime);

            //move foward
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                transform.position += transform.forward * Time.deltaTime * moveSpeed;
                //Vector3 move = new Vector3(0, Input.GetAxis("Vertical"), 0);
                //pcontroller.Move(move * Time.deltaTime * moveSpeed);
                interpolateAmount = (interpolateAmount + Time.deltaTime) % 1f;
                anim.SetBool("movingf", true);
            }
            //move backwards
            if (slide == false && (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)))
            {
                transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed * -1);
                anim.SetBool("movingb", true);
            }
            //move left
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                transform.Translate(Vector3.left * Time.deltaTime * leftrightSpeed);
                anim.SetBool("runleft", true);
            }
            //move right
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                transform.Translate(Vector3.left * Time.deltaTime * leftrightSpeed * -1);
                anim.SetBool("runright", true);
            }
            //jump
            if (Input.GetKey(KeyCode.Space) && anim.GetBool("inAir") == false && anim.GetBool("movingb") == false)
            {
                anim.SetBool("inAir", true);
                yVelocity = 11;
            }

            //slide
            if (Input.GetKey(KeyCode.K) && slide == false && anim.GetBool("inAir") == false && anim.GetBool("movingb") == false && anim.GetBool("movingf") == true)
            {
                //slide = true;
                StartCoroutine(SlideController());
            }

            Vector3 centerY = pcontroller.center;
            //transform.LookAt(pointC);
            //transform.rotation = Quaternion.Euler(0, targetPosition.y, 0);
            //Vector3 rot = new Vector3(0, (Input.GetAxisRaw("Horizontal") * moveSpeed * Time.deltaTime)-180 , 0);
            //transform.LookAt(rot);
            //transform.LookAt(pointC);

            if (slide == true)
            {
                anim.SetTrigger("slide");
                transform.Translate(Vector3.forward * Time.deltaTime * 4);
                //centerY.y = -1f;
                pcontroller.center = new Vector3(0, 0.3f, 0);
                pcontroller.height = 0.7f;
            }
            else if (slide == false)
            {
                anim.ResetTrigger("slide");
                //centerY.y = 0.77f;
                pcontroller.center = new Vector3(0, 0.77f, 0);
                pcontroller.height = 1.5f;
            }

            IEnumerator SlideController()
            {
                slide = true;
                yield return new WaitForSeconds(1f);
                slide = false;
            }

            if(curveStarted == true)
            {
                //Vector3 targetPosition = new Vector3(pointC.transform.position.x, transform.position.y, pointC.transform.position.z);
                //transform.LookAt(targetPosition);
                //transform.Rotate(pointC.rotation);
            }
        }
    }

    public void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.transform.tag == "Traps" && go == false)
        {
            if (anim.GetBool("isDead") == false)
            {
                anim.SetTrigger("isDead");
                
            }
            GameManager.gameover = true;
            go = true;
        }

        if(hit.transform.tag == "End" && completedlvl == false)
        {
            if (anim.GetBool("won") == false)
            {
                anim.SetTrigger("won");
            }
            completedlvl = true;
        }

        if (hit.transform.tag == "Curve")
        {
            transform.position = QuadrationCurve(pointA, pointB, pointC, interpolateAmount);
            Vector3 targetPosition = new Vector3(pointC.transform.position.x, transform.position.y, pointC.transform.position.z);
            transform.LookAt(targetPosition);
            curveStarted = true;
        }
    }

    //quadratic bezier curves
    public Vector3 QuadrationCurve(Transform a, Transform b, Transform c, float t)
    {
        //t = (t + Time.deltaTime) % 1f;
        p0.position = Vector3.Lerp(a.position,b.position, interpolateAmount);
        p1.position = Vector3.Lerp(b.position, c.position, interpolateAmount);
        return Vector3.Lerp(p0.position, p1.position, interpolateAmount);
    }

    public void DeathAnimationEvent()
    {
        gameManager.GameOver();
    }
}
