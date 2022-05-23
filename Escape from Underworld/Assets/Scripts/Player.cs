using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    // Start() Variables
    private Rigidbody2D rb;
    private Animator anim;
    

    // FSM
    private enum State { idle, running, jumping, falling, hurt, rise, stand, walk, death}
    private State state = State.idle;
    

    // Inspector Variables
    [SerializeField] private LayerMask ground;
    [SerializeField] private BoxCollider2D groundCheck;
    [SerializeField] private float horizontalDamping;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float jumpForce = 20f;
    [SerializeField] private float hangTime = .2f;
    [SerializeField] private float hurtForce = 10f;
    [SerializeField] private AudioSource footstep;
    [SerializeField] private AudioSource grunt;
    [SerializeField] private AudioSource deathGrunt;
    [SerializeField] private AudioSource deathBlast;
    [SerializeField] private AudioSource skull;
    [SerializeField] private AudioSource fireSkull;
    [SerializeField] private AudioSource whoosh;
    [SerializeField] private AudioSource flashJump;

    // General Player Variables

    private Vector2 dir;
    public static Vector2 playerPosition;
    private DialogueManager dMan;
    public static float deathDelay = 1f;
    public static bool touchingPortal = false;
    private bool isGrounded;
    private float hangCounter;

    //Powerup Variables

    public static bool hermesBoots = false;
    public static bool canJump;

    public static bool zeusBolt = false;
    public static bool canTP;
    public static bool boltCharging = false;
    public static float boltChargeTime = 0;
    public static float boltCD;
    public GameObject bolt;

    public static bool hadesFlame = false;
    public static bool undead;
    public static bool transforming;
    public static float transformingCD = 2f;

    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        undead = false;
        dMan = FindObjectOfType<DialogueManager>();
        

        //999 Skulls
        //UI.permanent.skulls = 999;
        //UI.permanent.skullCounter.text = UI.permanent.skulls.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (dMan == null) dMan = FindObjectOfType<DialogueManager>();
        isGrounded = groundCheck.IsTouchingLayers(ground);
        if (deathDelay <= 1) deathDelay += Time.deltaTime;
        if (isGrounded) canTP = true;

        if(state != State.hurt && state != State.death && !dMan.dialogueActive)
        {
            if (!undead)
            {
                Jump();
                if (boltCD >= 1f)
                {
                    if(canTP) lightningTP();
                }
                else
                {
                    boltCD += Time.deltaTime;
                }
            }

            reanimate();
        }
        

        AnimationState();
        anim.SetInteger("state", (int)state);
    }

    private void FixedUpdate()
    {
        dir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (state != State.hurt && state != State.death)
        {
            if (!boltCharging && !transforming && !dMan.dialogueActive)
            {
                Movement();
            }
            else
            {
                rb.velocity = new Vector2(0, 0f);
            }
            
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Skull"))
        {
            Destroy(collision.gameObject);
            skull.Play();
            UI.permanent.skulls += 1;
            UI.permanent.skullCounter.text = UI.permanent.skulls.ToString();
        }

        if (collision.CompareTag("FireSkull"))
        {
            Destroy(collision.gameObject);
            fireSkull.Play();
            UI.permanent.fireSkulls += 1;
            UI.permanent.fireSkullCounter.text = UI.permanent.fireSkulls.ToString();
        }

        if (collision.gameObject.tag == "Death")
        {
            state = State.death;

            
            if (deathDelay >= 1f)
            {
                UI.permanent.skulls -= 5;
                if (UI.permanent.skulls >= 0) UI.permanent.skullCounter.text = UI.permanent.skulls.ToString();
                deathDelay = 0;
            }
            
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if (!undead)
            {
                state = State.hurt;
                UI.permanent.skulls -= 1;
                
                if (UI.permanent.skulls < 0)
                {
                    state = State.death;
                    Invoke("loadScene", .67f);
                }
                else
                {
                    UI.permanent.skullCounter.text = UI.permanent.skulls.ToString();
                }

                if (collision.transform.position.x > transform.position.x)
                {
                    // Enemy is to the right
                    rb.velocity = new Vector2(-hurtForce, rb.velocity.y + 1);
                }

                else
                {
                    // Enemy is to the left
                    rb.velocity = new Vector2(hurtForce, rb.velocity.y + 1);
                }
            }
        }

        if (collision.gameObject.tag == "MovingPlatform")
        {
            this.transform.parent = collision.transform;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "MovingPlatform")
        {
            this.transform.parent = null;
        }
    }



    private void Movement()
    {
        float horizontalVelocity = rb.velocity.x;
        horizontalVelocity += dir.x;
        horizontalVelocity *= Mathf.Pow(1f - horizontalDamping, Time.deltaTime * 5f);
        rb.velocity = new Vector2(horizontalVelocity, rb.velocity.y);

        
        // Moving left
        if (dir.x < 0)
        {
            if (rb.transform.localEulerAngles.y == 0)
            {
                rb.transform.Rotate(0, 180, 0);
            }
        }

        // Moving right
        else if (dir.x > 0)
        {
            if (rb.transform.localEulerAngles.y == 180)
            {
                rb.transform.Rotate(0, -180, 0);
            }
        }

        //Max speed
        if (!undead)
        {
            if (Mathf.Abs(rb.velocity.x) > maxSpeed)
            {
                if (!boltCharging)
                {
                    rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * maxSpeed, rb.velocity.y);
                }
            }
        }
        //Max speed as undead
        else
        {
            if (Mathf.Abs(rb.velocity.x) > 10f)
            {
                rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * 10f, rb.velocity.y);
            }
        }
    }

    private void Jump()
    {
        // Jump check
        if (isGrounded)
        {
            canJump = true;
            hangCounter = hangTime;
        }
        else
        {
            hangCounter -= Time.deltaTime;
        }


        if (Input.GetButtonDown("Jump") && canJump == true)
        {
            if (hangCounter > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                state = State.jumping;
            }

            else
            {
                if (hermesBoots == true)
                {
                    rb.velocity = new Vector2(rb.velocity.x, 1.5f * jumpForce);
                    hermesJump();
                    state = State.jumping;
                    canJump = false;
                }
            }

        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * .5f);
        }
    }

    private void lightningTP()
    {
        if (zeusBolt)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift) && boltChargeTime < 1.5f)
            {
                boltCharging = true;
            }

            if (boltCharging)
            {
                boltChargeTime += Time.deltaTime;
            }

            if (Input.GetKeyUp(KeyCode.LeftShift) && boltCharging)
            {
                transform.position = boltShoot.boltPos;
                Instantiate(bolt, new Vector3(transform.position.x, transform.position.y + 5), transform.rotation);
                boltCharging = false;
                boltChargeTime = 0f;
                boltCD = 0f;
                canTP = false;
            }

            
        }
    }

    private void reanimate()
    {
        if (hadesFlame)
        {
            if (transformingCD >= 2f)
            {
                if (Input.GetKeyDown(KeyCode.DownArrow) && state == State.idle)
                {
                    state = State.rise;
                    transforming = true;
                    Invoke("animationStand", 1.42f);

                    rb.transform.localScale = new Vector3(-1, 1, 1);
                    groundCheck.size = new Vector2(.8f, 1f);
                    transformingCD = 0f;
                    undead = true;
                    UI.permanent.skulls -= 5;
                    if (UI.permanent.skulls < 0)
                    {
                        state = State.death;
                        transforming = false;
                        undead = false;
                        Invoke("loadScene", .67f);
                    }
                    else
                    {
                        UI.permanent.skullCounter.text = UI.permanent.skulls.ToString();
                    }
                }

                else if (Input.GetKeyDown(KeyCode.DownArrow) && state == State.stand)
                {
                    undead = false;
                    rb.transform.localScale = new Vector3(1, 1, 1);
                    groundCheck.size = new Vector2(.8f, 0.025f);
                    transformingCD = 0f;
                }
            }
            else
            {
                transformingCD += Time.deltaTime;
            }
        }
    }

    private void AnimationState()
    {
        if (state != State.death)
        {
            if (!undead)
            {
                if (rb.velocity.y < -5f && state != State.hurt)
                {
                    state = State.falling;
                }

                if (state == State.jumping)
                {
                    if (rb.velocity.y < -.1f)
                    {
                        state = State.falling;
                    }
                    else if (isGrounded && Mathf.Abs(rb.velocity.y) < 2f)
                    {
                        state = State.idle;
                    }
                }

                else if (state == State.falling)
                {
                    if (isGrounded && Mathf.Abs(rb.velocity.y) < 2f)
                    {
                        state = State.idle;
                    }
                }

                else if (state == State.hurt)
                {
                    if (Mathf.Abs(rb.velocity.x) < .1f)
                    {
                        state = State.idle;
                    }
                }

                else if (Mathf.Abs(rb.velocity.x) > 2f && Mathf.Abs(rb.velocity.y) < 2f)
                {
                    state = State.running;
                }

                else
                {
                    state = State.idle;
                }
            }

            else
            {
                if (Mathf.Abs(rb.velocity.x) > 2f)
                {
                    state = State.walk;
                }
                else
                {
                    if (!transforming) state = State.stand;
                }
            }
        }
    }

    private void loadScene()
    {
        if (UI.permanent.skulls < 0)
        {
            SceneManager.LoadScene("Hub");
            UI.permanent.skulls = 0;
            UI.permanent.skullCounter.text = UI.permanent.skulls.ToString();
            UI.permanent.message.SetActive(false);
        }
    }

    private void zeroHVelocity()
    {
        rb.velocity = new Vector2(0f, rb.velocity.y);
    }

    private void zeroVelocity()
    {
        rb.velocity = new Vector2(0f, 0f);
    }

    private void animationStand()
    {
        state = State.stand;
        transforming = false;
    }
    private void footStep()
    {
        footstep.Play();
    }

    private void hurt()
    {
        grunt.Play();
    }

    private void deathSound()
    {
        deathGrunt.Play();
    }

    private void fireSound()
    {
        deathBlast.Play();
    }

    private void jump()
    {
        whoosh.Play();
    }

    private void hermesJump()
    {
        flashJump.Play();
    }

}
