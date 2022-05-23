using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM : MonoBehaviour
{
    public AudioSource PortalSound;

    public static BGM bgMusic;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);

        // Singleton pattern
        if (!bgMusic)
        {
            bgMusic = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        
    }
}
