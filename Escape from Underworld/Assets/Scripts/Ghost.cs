using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    Vector2[] locs = new Vector2[5];
    private Vector2 zeroVec = new Vector2(0f, 0f);
    private Vector2 loc;
    [SerializeField] private Vector2 loc1;
    [SerializeField] private Vector2 loc2;
    [SerializeField] private Vector2 loc3;
    [SerializeField] private Vector2 loc4;
    [SerializeField] private float idleTime;
    private float idleTimer;
    private Animator anim;
    private AudioSource hit;
    private int i = 0;

    // Start is called before the first frame update
    void Start()
    {
        hit = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        anim.SetBool("appear", false);
        anim.SetBool("vanish", false);
        anim.SetBool("shriek", false);
        idleTimer = idleTime;
        loc = gameObject.transform.position;
        
        
        locs[0] = loc;
        locs[1] = loc1;
        locs[2] = loc2;
        locs[3] = loc3;
        locs[4] = loc4;
    }

    private void Update()
    {
        if (idleTimer <= 0)
        {
            anim.SetBool("vanish", true);
            idleTimer = idleTime;
            Invoke("appearAgain", .84f);
        }
        else
        {
            idleTimer -= Time.deltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (!Player.undead)
            {
                anim.SetBool("shriek", true);
                hit.Play();
                Invoke("idleAgain", 1.25f);
            }
        }
    }

    private void ghostTP()
    {
        if (locs[i + 1] != zeroVec)
        {
            i += 1;
            gameObject.transform.position = locs[i];
        }
        else
        {
            i = 0;
            gameObject.transform.position = locs[i];
        }

    }
    private void idleAgain()
    {
        anim.SetBool("shriek", false);
    }

    private void appearAgain()
    {
        anim.SetBool("vanish", false);
        anim.SetBool("appear", true);

    }

    private void idle()
    {
        anim.SetBool("appear", false);
    }

}
