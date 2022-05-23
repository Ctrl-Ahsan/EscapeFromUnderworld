using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FireBreath : MonoBehaviour
{

    private void Start()
    {
        Invoke("selfDestruct", 2f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Player.undead = false;
            Invoke("loadScene", .67f);

        }
    }

    private void loadScene()
    {
        if (UI.permanent.skulls < 0)
        {
            SceneManager.LoadScene("Hub");
            UI.permanent.skulls = 0;
            UI.permanent.skullCounter.text = UI.permanent.skulls.ToString();
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void selfDestruct()
    {
        Destroy(gameObject);
    }
}
