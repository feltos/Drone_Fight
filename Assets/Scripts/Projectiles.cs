using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectiles : MonoBehaviour
{

    Rigidbody2D body;
    Collider2D coll;
    [SerializeField]Vector2 speed;
    public Vector2 Direction;
    private Vector2 movement;
    [SerializeField]DroneController droneController;
    public bool isEnemyShot;

	void Start ()
    {
        body = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();

        Destroy(gameObject, 5);
    }
	
	void Update ()
    {
        movement = new Vector2(
            speed.x * Direction.x,
            speed.y * Direction.y);
	}

    void FixedUpdate()
    {
        body.velocity = movement;
    }
}
