using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightning : MonoBehaviour
{
    [SerializeField] private AudioSource lightningCrackle;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.LeftShift) || Player.boltCharging == false)
        {
            Destroy(gameObject);
        }
    }

    private void lightningSound()
    {
        lightningCrackle.Play();
    }
}
