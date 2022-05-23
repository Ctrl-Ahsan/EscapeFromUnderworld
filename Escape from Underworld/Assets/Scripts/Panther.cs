using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panther : MonoBehaviour
{
    [SerializeField] private float leftCap;
    [SerializeField] private float rightCap;
    [SerializeField] private float speed;
    [SerializeField] private AudioSource roar;

    private Rigidbody2D rb;
    private bool facingLeft = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (facingLeft)
        {
            if (transform.position.x > leftCap)
            {
                if (transform.localScale.x != 1)
                {
                    transform.localScale = new Vector3(1, 1, 1);
                }

                rb.velocity = new Vector2(-speed, rb.velocity.y);
            }
            else
            {
                facingLeft = false;
            }
        }

        else
        {
        
            if (transform.position.x < rightCap)
            {
                if (transform.localScale.x != -1)
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                }

                rb.velocity = new Vector2(speed, rb.velocity.y);
            }
            else
            {
                facingLeft = true;
            }
            
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !Player.undead)
        {
            roar.Play();
        }
    }

}
