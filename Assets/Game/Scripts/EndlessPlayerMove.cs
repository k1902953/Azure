using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessPlayerMove : MonoBehaviour
{
    Animator anim;                  // the animator
    private Rigidbody pcontroller;
    private CapsuleCollider playerCollider;
    public float moveSpeed = 10;
    public float leftrightSpeed = 5;
    private bool slide = false;
    private float yVelocity;
    private bool go = false;
    private bool beenHit = false;
    private bool completedlvl = false;
    private GameManager gameManager;
    public Animation cAnimation;
    float distanceTravelled;
    bool IsGrounded;
    
    //public Collider stone1;
    //public static bool hitObstacle;

    void Start()
    {
        anim = GetComponent<Animator>();
        anim.ResetTrigger("isDead");
        anim.ResetTrigger("won");
        pcontroller = GetComponent<Rigidbody>();
        playerCollider = GetComponent<CapsuleCollider>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void FixedUpdate()
    {
        anim.SetBool("movingb", false);
        anim.SetBool("runright", false);
        anim.SetBool("runleft", false);

        if (go == false && completedlvl == false)
        {
            //move forward 
            Vector3 moveForward = transform.forward * Time.deltaTime * moveSpeed;
            transform.position += moveForward;
            anim.SetBool("movingf", true);

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
            if ((Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) && anim.GetBool("inAir") == false && slide == false)
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
            pcontroller.MovePosition(velocity * Time.deltaTime);

            if (transform.position.y < -1 && go == false)
            {
                go = true;
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
                centerY.y = -1f;
                playerCollider.center = new Vector3(0, 0.3f, 0);
                playerCollider.height = 0.7f;
            }
            else if (slide == false)
            {
                anim.ResetTrigger("slide");
                centerY.y = 0.77f;
                playerCollider.center = new Vector3(0, 0.64f, 0);
                playerCollider.height = 1.46f;
            }

            IEnumerator PlatformLimit()
            {
                GameManager.gamewon = true;
                GameManager.finishTimer = true;
                yield return new WaitForSeconds(1f);
                gameManager.GameWon();
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
        if (hit.transform.tag != "Untagged")
        {
            Debug.Log(hit.gameObject);
        }

        if (hit.transform.tag == "Floor")
        {
            IsGrounded = true;
            anim.SetBool("inAir", false);
        }
        

        if ((hit.transform.tag == "Traps" || hit.transform.tag == "Obstacle") && go == false)
        {
            if (beenHit == false)
            {
                beenHit = true;
                GameManager.health -= 1;
                if (anim.GetBool("isDead") == false && GameManager.health < 1)
                {
                    anim.SetTrigger("isDead");
                    GameManager.finishTimer = true;
                    cAnimation.Play("cameraAni-shake");
                    GameManager.gamewon = true;
                    go = true;
                }
                else
                {
                    //player goes through object till one life left
                    hit.gameObject.GetComponent<MeshCollider>().enabled = false;
                    StartCoroutine(WaitSecs());
                }

                IEnumerator WaitSecs()
                {
                    yield return new WaitForSeconds(1f); //may need to change as the level progresses due to player speed
                    beenHit = false;
                }
            }

        }

    }

    //public void VictoryAnimationEvent()
    //{
    //    gameManager.GameWon();
    //}

    public void DeathAnimationEvent()
    {
        gameManager.GameWon();
    }
}
