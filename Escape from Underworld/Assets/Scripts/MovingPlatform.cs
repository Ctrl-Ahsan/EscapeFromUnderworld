using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
	Transform _transform;

	public float moveSpeed = 5f;
	public float waitAtWaypointTime = 1f;
	public bool loop = true;
	float _moveTime;
	bool _moving = true;
	public int _myWaypointIndex = 0;

	public GameObject platform;
	public GameObject[] myWaypoints;
	public Transform player;


	// Use this for initialization
	void Start()
	{
		_transform = platform.GetComponent<Transform>();
		// _transform = platform.transform;
		_moveTime = 0f;
		_moving = true;
		player = GameObject.FindWithTag("Player").GetComponent<Transform>();
		_transform.position = myWaypoints[0].transform.position;
	}

	// Update is called once per frame
	void Update()
	{
		// if beyond _moveTime, then start moving
		if (Time.time >= _moveTime)
		{
			Movement();
			// Respawn();
		}
	}

	void Movement()
	{
		// if there isn't anything in My_Waypoints
		if ((myWaypoints.Length != 0) && (_moving))
		{

			// move towards waypoint
			_transform.position = Vector3.MoveTowards(_transform.position, myWaypoints[_myWaypointIndex].transform.position, moveSpeed * Time.deltaTime);

			// if the enemy is close enough to waypoint, make it's new target the next waypoint
			if (Vector3.Distance(myWaypoints[_myWaypointIndex].transform.position, _transform.position) <= 0)
			{
				_myWaypointIndex++;
				_moveTime = Time.time + waitAtWaypointTime;
			}

			// reset waypoint back to 0 for looping, otherwise flag not moving for not looping
			if (_myWaypointIndex >= myWaypoints.Length)
			{
				if (loop)
				{
					_myWaypointIndex = 0;

				}
				else
				{
					_moving = false;

				}
			}
		}
	}

	void Respawn()
	{
		if (player.position.y <= -3 && _myWaypointIndex != 0)
		{
			_myWaypointIndex = 0;
		}
	}

	/*
	 * void OnCollisionEnter2D(Collision2D collision) 
	{ 
		if (collision.gameObject.tag == "Player")
        {
			collision.transform.parent = this.transform;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
	{	
		if (collision.gameObject.tag == "Player")
		{
			collision.transform.parent = null;
		}
	}
	 */
}
