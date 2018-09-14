using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneController : MonoBehaviour
{

    Rigidbody2D body;
    Collider2D coll;
    float horizontal;
    float vertical;
    [SerializeField]float horizontalSpeed;
    [SerializeField]float verticalSpeed;
    float basicHorizontalSpeed;
    float basicVertcialSpeed;

    [SerializeField]GameObject gunBullet;
    [SerializeField]GameObject gunShotZone;

    [SerializeField]bool isTurnedRight;

    public bool GetIsTurnedright()
    {
        return isTurnedRight;
    }

	void Start ()
    {
        body = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        basicHorizontalSpeed = horizontalSpeed;
        basicVertcialSpeed = verticalSpeed;
	}
	
	void Update ()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        
        if(horizontal > 0 && !isTurnedRight)
        {
            Flip();
        }    
        if(horizontal < 0 && isTurnedRight)
        {
            Flip();
        }
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            GunShot();
        }
    }

    void GunShot()
    {
        Instantiate(gunBullet, gunShotZone.transform.position, gunShotZone.transform.rotation);
    }

    private void FixedUpdate()
    {
        body.velocity = new Vector2(horizontalSpeed * horizontal,(verticalSpeed * vertical) - 9.81f );
    }

    void Flip()
    {
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
        isTurnedRight = !isTurnedRight;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            horizontalSpeed = 0;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            horizontalSpeed = basicHorizontalSpeed;
        }
    }
}
