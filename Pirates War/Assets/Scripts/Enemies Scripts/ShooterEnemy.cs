using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShooterEnemy : Enemies
{
    private float totalLife,timeToShoot=0;
    [SerializeField] private GameObject cannonBall;
    [SerializeField] private float speedShoot;
    [SerializeField] GameObject explosion;
    [SerializeField] private Image lifeBar;

    void Start()
    {
        totalLife = lifeEnemy;
    }

    
    void Update()
    {
        if(Player.lose == false)
        {
            DetectPlayer();
            ShooterEnemyAttack();
            EnemyAnim(totalLife);
            LifeBar();
        }
        
        
    }

    private void ShooterEnemyAttack()
    {
        if (attack)
        {
            timeToShoot = timeToShoot + Time.deltaTime;
            if(timeToShoot == 0 || timeToShoot>=0.5f)
            {
                GameObject newCannonBall = Instantiate(
                cannonBall,
                GameObject.Find("Shoot Pos Enemy").transform.position,
                transform.rotation);

                newCannonBall.AddComponent<Rigidbody2D>();
                newCannonBall.GetComponent<Rigidbody2D>().gravityScale = 0;
                newCannonBall.GetComponent<Rigidbody2D>().velocity = transform.up * speedShoot;
            }

            if(timeToShoot>=0.5)
            {
                timeToShoot = 0;
            }
            
        }

    }

    private void LifeBar()
    {
        lifeBar.fillAmount = lifeEnemy / totalLife;
        if (lifeEnemy <= 0)
        {
            Instantiate(explosion, transform.position, transform.rotation);
            Player.score = Player.score + 1;
            Destroy(gameObject);
        }
    }



}
