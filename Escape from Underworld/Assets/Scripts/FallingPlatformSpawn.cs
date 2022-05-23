using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatformSpawn : MonoBehaviour
{

    public GameObject fallingPlatform;
    Transform _transform;

    // Start is called before the first frame update
    void Start()
    {
        _transform = GetComponent<Transform>();
        // GameObject newPlatform = Instantiate(fallingPlatform, new Vector3(_transform.position.x, _transform.position.y, _transform.position.z), Quaternion.identity) as GameObject;
    }

    public void Respawn()
    {
        GameObject newPlatform;
        newPlatform = Instantiate(fallingPlatform, new Vector3(_transform.position.x, _transform.position.y, _transform.position.z), Quaternion.identity) as GameObject;
        newPlatform.transform.parent = _transform;
    }
}

