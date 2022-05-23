using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissapearingPlatform : MonoBehaviour
{
    Animator _animator;
    public float startTime;
    public float maxTime;
    public bool isActive;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // if the startTime reaches maxtime, then it switches off. When it goes back down to 1, it switches on.
        // when the platform is active, starttime will count up, otherwise it will count down
        if(isActive && startTime > maxTime)
        {
            isActive = false;
        }
        else if(!isActive && startTime < 0)
        {
            isActive = true;
        }
        if(isActive)
        {
            startTime += Time.deltaTime;
        }
        else
        {
            startTime -= Time.deltaTime;
        }
        _animator.SetBool("isActive", isActive);
    }
}
