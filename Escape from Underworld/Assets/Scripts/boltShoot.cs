using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boltShoot : MonoBehaviour
{
    public float boltSpeed = 10f;
    public Rigidbody2D rb;
    public static Vector3 boltPos;
    

    // Start is called before the first frame update
    void Start()
    {
        boltPos = transform.position;
    }

    private void Update()
    {
        if (Player.boltCharging && Player.boltChargeTime < 1.5f)
        {
            rb.velocity = transform.right * boltSpeed;
        }
        else
        {
            rb.velocity = new Vector2(0f, 0f);
            Destroy(gameObject);
        }
        boltPos = transform.position;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }

}
