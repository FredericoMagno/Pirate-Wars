using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    [SerializeField] private float speedShip,speedRotation,speedShoot,areaAttackRadius,lifePlayer;

    [SerializeField] Image lifeBar;

    [SerializeField] GameObject cannonBall,explosion;

    

    private Rigidbody2D rb;
    private float shootTime = 0,totalLife;
    private bool attack;

    public static bool lose = false, win = false;
    public static int score;

    
    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        totalLife = lifePlayer;
        score = 0;
    }

    
    void Update()
    {
        if(lose == false)
        {
            MovePlayer();
            RotatePlayer();
            PlayerAreaAttack();
            PlayerAnimation();
            Shoot();
            MoveCamera();
            LifeBar();
        }
  
    }

    private void MovePlayer()
    {
        Vector3 dir = transform.up;
        if(Input.GetButton("Jump")) //use space bar to accelerate the ship
        {
            rb.velocity = dir*speedShip;
        }
        else
        {
            rb.velocity = Vector3.zero;
        }
        
        
    }

    private void RotatePlayer()
    {
        Vector2 mousePos = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.up = (Vector3)(mousePos - new Vector2(transform.position.x, transform.position.y));
    }

    private void Shoot()
    {

        shootTime = shootTime + Time.deltaTime;
        if ((Input.GetMouseButtonDown(0) && shootTime>=0.5f) && attack)
        {
            GameObject newCannonBall = Instantiate(
                cannonBall,
                GameObject.Find("Shoot Pos").transform.position,
                transform.rotation);
            newCannonBall.AddComponent<Rigidbody2D>();
            newCannonBall.GetComponent<Rigidbody2D>().gravityScale = 0;
            newCannonBall.GetComponent<Rigidbody2D>().velocity = transform.up * speedShoot;
            shootTime = 0;
        }
            
    }
    private void PlayerAreaAttack()
    {
        if(Physics2D.OverlapCircle(transform.position,areaAttackRadius,LayerMask.GetMask("Enemy")))
        {
            attack = true;
        }
        else
        {
            attack = false;
        }
    }
    private void PlayerAnimation()
    {
        // 70 % player damage
        if (lifePlayer <= (0.7 * totalLife) && lifePlayer > (0.3 * lifePlayer))
        {
            GetComponent<Animator>().SetTrigger("Damage1");
        }
        // 30% de player damage
        else if (lifePlayer <= 0.3 * totalLife && lifePlayer > 0)
        {
            GetComponent<Animator>().SetTrigger("Damage2");
        }
        else if (lifePlayer <= 0)
        {
            lose = true;
            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    private void MoveCamera()
    {
        GameObject.Find("Main Camera").transform.position = new Vector3(
            transform.position.x,
            transform.position.y,
            - 10);   
    }

    private void LifeBar()
    {
        lifeBar.fillAmount = (lifePlayer / totalLife);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, areaAttackRadius);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("CannonBall"))
        {
            lifePlayer = lifePlayer - 10;
            
            Destroy(collision.gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.layer == 7)// 7 == layer enemy
        {
            lifePlayer = lifePlayer - 30;
        }
    }
}
