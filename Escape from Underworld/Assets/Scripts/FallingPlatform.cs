using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    Rigidbody2D _rigidbody;
    Transform _transform;
    public GameObject spawner;
    public FallingPlatformSpawn fps;

    public bool triggered;
    public float stallTime = 0.5f;
    public float lifeTime = 2;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _transform = GetComponent<Transform>();
        spawner = this.transform.parent.gameObject;
        fps = spawner.GetComponent<FallingPlatformSpawn>();
    }

    // Update is called once per frame
    void Update()
    {
        if (triggered)
        {
            stallTime -= Time.deltaTime;
        }

        if (lifeTime <= 0)
        {
            Destroy(gameObject);
            fps.Respawn();
        }
        if (stallTime <= 0)
        {
            triggered = false;
            _rigidbody.isKinematic = false;
            lifeTime -= Time.deltaTime;
        
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            triggered = true;
        }
    }
}
