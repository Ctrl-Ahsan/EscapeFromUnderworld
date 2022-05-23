using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demon : MonoBehaviour
{
    [SerializeField] private GameObject fireHitBox;
    [SerializeField] private AudioSource fireAudio;

    private Animator anim;
    private bool outOfDanger = true;

    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!Player.undead)
            {
                anim.SetBool("fire", true);
                outOfDanger = false;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            outOfDanger = true;
        }
    }

    private void fireFalse()
    {
        if (outOfDanger) anim.SetBool("fire", false);
    }

    private void fireDamage()
    {
        
        Instantiate(fireHitBox, new Vector3(transform.position.x, transform.position.y), transform.rotation);
        
    }

    private void fireSound()
    {
        fireAudio.Play();
    }

}
