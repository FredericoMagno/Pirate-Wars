using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    public float speedEnemy,lifeEnemy,areaToattack,followAfterAttack;
    public bool attack;
    private bool canFollow = false,follow;
    
    


    public void DetectPlayer()
    {
      
        attack = Physics2D.OverlapCircle(transform.position, areaToattack,LayerMask.GetMask("Player"));
        follow = Physics2D.OverlapCircle(transform.position, 6, LayerMask.GetMask("Player"));

        if (attack)
        {
            transform.up = (Vector3)(GameObject.Find("Player Ship").transform.position - new Vector3(transform.position.x, transform.position.y));
            canFollow = true;
        }

        if(attack == false && canFollow == true)
        {
            transform.position = Vector2.MoveTowards(
                transform.position,
                GameObject.Find("Player Ship").transform.position,
                speedEnemy * Time.deltaTime);
            transform.up = (Vector3)(GameObject.Find("Player Ship").transform.position - new Vector3(transform.position.x, transform.position.y));
        }
        if(follow == false)
        {
            canFollow = false;
        }
       
    }

    public void EnemyAnim(float totalLife)
    {
        
        // 70 % enemy damage 
        if (lifeEnemy <= (0.7*totalLife) && lifeEnemy>(0.3*totalLife))
        {
            GetComponent<Animator>().SetTrigger("Damage1");
        }
        // 30% enemy damage 
        else if( lifeEnemy <= 0.3*totalLife && lifeEnemy>0)
        {
            GetComponent<Animator>().SetTrigger("Damage2");
        }
        //else if(lifeEnemy<=0)
        //{
        //    Player.score = Player.score + 1;
        //    Destroy(gameObject);
        //}
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, areaToattack);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 6f); // Area to shooter enemy follow the player after player exit from areaToAttack. 
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("CannonBall"))
        {
            lifeEnemy = lifeEnemy - 10;
            Destroy(collision.gameObject);
        }
    }
}
