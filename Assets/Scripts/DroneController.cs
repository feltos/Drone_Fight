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

    Vector2 shootDirection;
    bool isEnemy = false;
    [SerializeField]int health;


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

        if (isTurnedRight)
        {
            shootDirection = new Vector2(1, 0); 
        }
        if (!isTurnedRight)
        {
            shootDirection = new Vector2(-1, 0);
        }
    }
	
	void Update ()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        
        if(horizontal > 0 && !isTurnedRight)
        {
            shootDirection = new Vector2(1, 0);
            Flip();
        }    
        if(horizontal < 0 && isTurnedRight)
        {
            shootDirection = new Vector2(-1, 0);
            Flip();
        }
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            GunShot();
        }
    }

    void GunShot()
    {
        var shootTransform = Instantiate(gunBullet, gunShotZone.transform.position, gunShotZone.transform.rotation);
        shootTransform.transform.position = gunShotZone.transform.position;
        Projectiles proj = shootTransform.gameObject.GetComponent<Projectiles>();
        if(proj !=null)
        {
            proj.Direction = shootDirection.normalized;
            proj.isEnemyShot = isEnemy;
        }
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
        if(collision.gameObject.layer == LayerMask.NameToLayer("Projectile"))
        {
            health -= 1;
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
