using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    Animator anim;                  // the animator
    private Rigidbody pcontroller;
    private CapsuleCollider playerCollider;
    public float moveSpeed = 10;
    public float leftrightSpeed = 5;
    private bool slide = false;
    private float yVelocity;
    private bool go = false;
    private bool completedlvl = false;
    private GameManager gameManager;
    private Animation cAnimation;
    float distanceTravelled;
    bool IsGrounded;

    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;
    [SerializeField] private Transform pointC;
    [SerializeField] private Transform p0;
    [SerializeField] private Transform p1;
    [SerializeField] private Transform p01;
    //private LineRenderer linerender;
    //private int numpoints = 20;
    //int index = 0;
    //private Vector3[] pointPositions = new Vector3[20];
    private float interpolateAmount;

    void Start()
    {
        cAnimation = GameObject.Find("Camera").GetComponent<Animation>();
        anim = GetComponent<Animator>();
        anim.ResetTrigger("isDead");
        anim.ResetTrigger("won");
        pcontroller = GetComponent<Rigidbody>();
        playerCollider = GetComponent<CapsuleCollider>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        

        //due to player prefabs created for chatacter selecting
        if (GameManager.level == 1)
        {
            pointA = GameObject.Find("road/Path_90.002/pointA").transform;
            pointB = GameObject.Find("road/Path_90.002/pointB").transform;
            pointC = GameObject.Find("road/Path_90.002/pointC").transform;
            p0 = GameObject.Find("road/Path_90.002/p0").transform;
            p1 = GameObject.Find("road/Path_90.002/p1").transform;
            p01 = GameObject.Find("road/Path_90.002/p01").transform;
            //linerender = GameObject.Find("road/Path_90.002").GetComponent<LineRenderer>();
            //linerender.positionCount = numpoints;
        }
    }
    private void FixedUpdate()
    {
        anim.SetBool("movingf", false);
        anim.SetBool("movingb", false);
        anim.SetBool("runright", false);
        anim.SetBool("runleft", false);

        if (go == false && completedlvl == false)
        {
            //move foward
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                transform.position += transform.forward * Time.deltaTime * moveSpeed;
                interpolateAmount += Time.deltaTime / 2  % 1f;
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
            //Jump
            if (Input.GetKey(KeyCode.Space) && anim.GetBool("inAir") == false && slide == false && anim.GetBool("movingb") == false)
            {
                IsGrounded = false;
                anim.SetBool("inAir", true);
                yVelocity = 7;
                pcontroller.velocity += yVelocity * Vector3.up;
            }
        }
    }
    public void Update()
    {
        
        anim.ResetTrigger("slide");

        if (go == false && completedlvl == false)
        {
            Vector3 direction = new Vector3(0, 0, 0);
            Vector3 velocity = direction * moveSpeed;

            velocity.y = yVelocity;
            //pcontroller.MovePosition(velocity * Time.deltaTime);

            if (transform.position.y < -1 && go == false)
            {
                StartCoroutine(PlatformLimit());
            }

            //slide
            if (Input.GetKey(KeyCode.LeftShift) && slide == false && anim.GetBool("inAir") == false && anim.GetBool("movingb") == false && anim.GetBool("movingf") == true)
            {
                //slide = true;
                StartCoroutine(SlideController());
            }

            Vector3 centerY = playerCollider.center;
            

            if (slide == true)
            {
                anim.SetTrigger("slide");
                transform.Translate(Vector3.forward * Time.deltaTime * 3);
                //centerY.y = -1f;
                if(PlayerPrefs.GetInt("selectCharacter") < 1)
                {
                    playerCollider.center = new Vector3(0, 0.3f, 0);
                    playerCollider.height = 0.6f;
                }
                else
                {
                    playerCollider.center = new Vector3(0, 0.5f, 0);
                    playerCollider.height = 0.3f;
                }
                
            }
            else if (slide == false)
            {
                anim.ResetTrigger("slide");
                //centerY.y = 0.77f;
                if (PlayerPrefs.GetInt("selectCharacter") < 1)
                {

                    playerCollider.center = new Vector3(0, 0.77f, 0);
                    playerCollider.height = 1.5f;
                }
                else
                {
                    playerCollider.center = new Vector3(0, 0.42f, 0);
                    playerCollider.height = 1.6f;
                }
            }

            IEnumerator PlatformLimit()
            {
                yield return new WaitForSeconds(1f);
                GameManager.gameover = true;
                go = true;
                gameManager.GameOver();
            }
            
            IEnumerator SlideController()
            {
                slide = true;
                yield return new WaitForSeconds(1f);
                slide = false;
            }

        }
    }

    public void OnCollisionEnter(Collision hit)
    {
        if (hit.transform.tag == "Floor")
        {
            IsGrounded = true;
            anim.SetBool("inAir", false);
        }

        if (hit.transform.tag == "Traps" && go == false)
        {
            if (anim.GetBool("isDead") == false)
            {
                anim.SetTrigger("isDead");
                
            }
            cAnimation.Play("cameraAni-shake");
            
            GameManager.gameover = true;
            go = true;
        }

        if(hit.transform.tag == "End" && completedlvl == false)
        {
            if (anim.GetBool("won") == false)
            {
                anim.SetTrigger("won");
                anim.SetInteger("danceSelection", PlayerPrefs.GetInt("selectDance"));
            }
            cAnimation.Play("camera-win");
            GameManager.gamewon = true;
            completedlvl = true;
        }

        if (hit.transform.tag == "Curve")
        {
            transform.position = QuadrationCurve(pointA, pointB, pointC, interpolateAmount);
            Vector3 targetPosition = new Vector3(pointC.transform.position.x, transform.position.y, pointC.transform.position.z);
            transform.LookAt(targetPosition);
        } else {
            if(transform.eulerAngles.y > 200 )
            {
                transform.eulerAngles = new Vector3( transform.eulerAngles.x, 270, transform.eulerAngles.z );
            }
        }
    }

    //quadratic bezier curves
    public Vector3 QuadrationCurve(Transform a, Transform b, Transform c, float t)
    {
        p0.position = Vector3.Lerp(a.position, b.position, interpolateAmount);
        p1.position = Vector3.Lerp(b.position, c.position, interpolateAmount);
        return Vector3.Lerp(p0.position, p1.position, interpolateAmount);


        //float tt = t * t;
        //float u = 1f - t;
        //float uu = u * u;

        //Vector3 p = uu * a;
        //p += 2f * u * t * b;
        //p += tt * c;

        //return p;
    }

    //Vector3[] curve()
    //{
    //    for (int i = 1; i < numpoints + 1; i++)
    //    {
    //        float t = i / (float)numpoints;
    //        pointPositions[i-1] = QuadrationCurve(pointA, pointB, pointC, t);
            
    //    }
    //    linerender.SetPositions(pointPositions);
    //    return pointPositions;

    //}

    public void VictoryAnimationEvent()
    {
        gameManager.GameWon();
    }

    public void DeathAnimationEvent()
    {
        gameManager.GameOver();
    }
}
