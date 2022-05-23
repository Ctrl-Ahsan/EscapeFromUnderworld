using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    [SerializeField] private AudioSource PortalSound;
    public string sceneToLoad;
    public static bool teleporting = false;
    private bool pressingUp;

    private void Update()
    {
        if (Player.touchingPortal && Input.GetKey(KeyCode.UpArrow))
        {
            pressingUp = true;
        }
        else
        {
            pressingUp = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            UI.permanent.message.SetActive(true);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player.touchingPortal = true;

            if (!Player.undead && pressingUp)
            {
                BGM.bgMusic.PortalSound.Play();
                SceneManager.LoadScene(sceneToLoad);
                UI.permanent.message.SetActive(false);

            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player.touchingPortal = false;
            UI.permanent.message.SetActive(false);
        }
    }

    private void teleportingFalse()
    {
        teleporting = false;
    }
    
}
