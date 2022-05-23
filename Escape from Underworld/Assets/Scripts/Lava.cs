using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour
{
    Transform _transform;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        _transform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _transform.position = new Vector2(_transform.position.x, _transform.position.y + (speed/100));
    }
}
