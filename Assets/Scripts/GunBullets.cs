using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBullets : MonoBehaviour
{

    [SerializeField]float speed;
    Rigidbody2D body;
    Collider2D coll;
    [SerializeField]DroneController droneController;
    Vector2 direction;

	void Start ()
    {
        body = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        if(droneController.GetIsTurnedright())
        {
            direction = new Vector2(speed,0);
        }
        if (!droneController.GetIsTurnedright())
        {
            direction = new Vector2(-speed, 0);
        }
    }
	
	void Update ()
    {
        
	}

    private void FixedUpdate()
    {
        body.velocity = direction;
    }
}
