using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChaserEnemy : Enemies
{
    private float totalLife;
    [SerializeField] GameObject explosion;

    [SerializeField] Image lifeBar;
    void Start()
    {
        totalLife = lifeEnemy;
    }

    
    void Update()
    {
        if(Player.lose == false)
        {
            DetectPlayer();
            ChaserEnemyAttack();
            LifeBar();

            EnemyAnim(totalLife);
        }

        
    }
    private void ChaserEnemyAttack()
    {
        if(attack)
        {
            
           transform.position = Vector2.MoveTowards(
              transform.position,
              GameObject.Find("Player Ship").transform.position,
              speedEnemy * Time.deltaTime);
            
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 6)
        {
            Instantiate(explosion, transform.position, transform.rotation);
            Player.score = Player.score + 1;
            Destroy(gameObject);
        }
    }
}
