using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hound : MonoBehaviour
{
    [SerializeField]private float leftCap;
    [SerializeField]private float rightCap;
    [SerializeField] private AudioSource barkSound;

    [SerializeField] private float speed = 5f;

    private Rigidbody2D rb;
    private Animator anim;

    private bool facingLeft = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        anim.SetBool("running", true);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !Player.undead)
        {
            barkSound.Play();
        } 
    }

    private void Move()
    {
        if (facingLeft)
        {
            if (transform.position.x > leftCap)
            {
                if (transform.localScale.x != 1)
                {
                    transform.localScale = new Vector3(1, 1, 1);
                }

                if (anim.GetBool("running")) { 
                    rb.velocity = new Vector2(-speed, rb.velocity.y);
                }
            }

            else
            {
                
                anim.SetBool("running", false);
                rb.velocity = new Vector2(0f, 0f);
                Invoke("runAgainR", 3f);
                
            }
        }

        else
        {
            {
                if (transform.position.x < rightCap)
                {
                    if (transform.localScale.x != -1)
                    {
                        transform.localScale = new Vector3(-1, 1, 1);
                    }

                    if(anim.GetBool("running")) {
                        rb.velocity = new Vector2(speed, rb.velocity.y);
                    }
                }

                else
                {
                    
                    anim.SetBool("running", false);
                    rb.velocity = new Vector2(0f, 0f);
                    Invoke("runAgainL", 3f);
                    
                }
            }
        }
    }

    private void runAgainR()
    {
        anim.SetBool("running", true);
        facingLeft = false;

    }

    private void runAgainL()
    {
        anim.SetBool("running", true);
        facingLeft = true;

    }

}
