using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bolt : MonoBehaviour
{
    [SerializeField] private AudioSource lightningBolt;

    // Update is called once per frame
    void Update()
    {
        Invoke("selfDestruct", 1f);
    }

    private void boltSound()
    {
        lightningBolt.Play();
    }

    void selfDestruct()
    {
        Destroy(gameObject);
    }
}
