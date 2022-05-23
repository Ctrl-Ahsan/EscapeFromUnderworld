using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Death : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;

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
            print("going to hub");
            UI.permanent.skulls = 0;
            UI.permanent.skullCounter.text = UI.permanent.skulls.ToString();
        }
        else
        {
            print("loading scene to load");
            SceneManager.LoadScene(sceneToLoad);
        }
    }

}
