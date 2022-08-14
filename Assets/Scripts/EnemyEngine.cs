using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEngine : MonoBehaviour
{
    float xPos;
    float yPos;
    float maxLeft;
    float maxRight;
    float timer;
    Transform playerTrans;
    public Rigidbody2D rb;
    public GameObject enemyArrow;
    int health;
    bool movingRight;
    bool isDead;
    Animator animator;
    Collider2D coll;
    GameObject impact;

    void Start()
    {
        maxLeft = transform.position.x - 2;
        maxRight = transform.position.x + 2;

        transform.position = new Vector3(transform.position.x, transform.position.y, 0);

        movingRight = false;

        isDead = false;

        health = 90;
    }


    void Update()
    {
        playerTrans = GameObject.Find("Player").transform;

        impact = (GameObject)Resources.Load("prefabs/Impact", typeof(GameObject));

        animator = GetComponent<Animator>();
        coll = GetComponent<PolygonCollider2D>();

        timer -= Time.deltaTime;

        xPos = transform.position.x;
        yPos = transform.position.y;

        if (Vector2.Distance(playerTrans.position, transform.position) < 8 && isDead == false)
        {
            animator.SetBool("isWalking", false);

            if (playerTrans.position.x > xPos)
            {
                transform.localScale = new Vector2(1, 1);
            }
            else if (playerTrans.position.x < xPos)
            {
                transform.localScale = new Vector2(-1, 1);
            }

            if (transform.localScale.x > 0 && timer <= 0)
            {
                animator.SetBool("isShooting", true);
                Instantiate(enemyArrow, new Vector2(xPos + 1, yPos), transform.rotation);
                timer = 3;
            }
            else if (transform.localScale.x < 0 && timer <= 0)
            {
                animator.SetBool("isShooting", true);             
                Instantiate(enemyArrow, new Vector2(xPos - 1, yPos), transform.rotation);
                timer = 3;
            }

            if (timer <= 2.5)
            {
                animator.SetBool("isWalking", false);
                animator.SetBool("isShooting", false);
            }
        }
        else if(isDead == false)
        {
            Patrol();
        }

        if (xPos <= maxLeft)
        {
            movingRight = true;
        }
        else if (xPos >= maxRight)
        {
            movingRight = false;
        }

        if (health == 0)
        {
            isDead = true;
            animator.SetTrigger("isDead");
            transform.position = new Vector3(xPos, yPos, 1);
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            coll.isTrigger = true;           
        }

        if (timer >= 1)
        {
            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Arrow" && isDead == false)
        {
            Instantiate(impact, transform.position, transform.rotation);
            Destroy(collision.gameObject);
            health = health - 30;
        }
    }

    void Patrol()
    {
        if (movingRight == true)
        {
            transform.localScale = new Vector2(1, 1);
            transform.Translate(1 * Time.deltaTime, 0, 0);
        }
        else
        {
            transform.localScale = new Vector2(-1, 1);
            transform.Translate(-1 * Time.deltaTime, 0, 0);
        }

        animator.SetBool("isWalking", true);
        animator.SetBool("isShooting", false);
    }
}
