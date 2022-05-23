using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zeusBolt : MonoBehaviour
{
    public GameObject spark;
    public GameObject lightning;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && !Player.undead && Player.zeusBolt)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        Instantiate(spark, transform.position, transform.rotation);
        Instantiate(lightning, new Vector3(transform.position.x, transform.position.y + 5), transform.rotation);
    }
}
