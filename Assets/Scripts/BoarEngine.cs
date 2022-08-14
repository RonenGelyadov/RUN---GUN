using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoarEngine : MonoBehaviour
{
    public static float xPos;
    float yPos;
    float rightMax;
    float leftMax;
    Transform playerTrans;
    Animator animator;
    bool movingRight;
    int health;
    bool isDead;
    Collider2D coll;
    GameObject impact;
    public Rigidbody2D rb;

    void Start()
    {
        leftMax = transform.position.x - 3;
        rightMax = transform.position.x + 3;

        movingRight = true;
        health = 90;
        isDead = false;
    }


    void Update()
    {
        xPos = transform.position.x;
        yPos = transform.position.y;

        playerTrans = GameObject.Find("Player").transform;

        impact = (GameObject)Resources.Load("Prefabs/Impact", typeof(GameObject));

        animator = GetComponent<Animator>();
        coll = GetComponent<PolygonCollider2D>();
        rb = GetComponent<Rigidbody2D>();

        animator.SetBool("isRunning", false);


        if (xPos >= rightMax)
        {
            movingRight = false;
        }
        else if (xPos <= leftMax)
        {
            movingRight = true;
        }


        if (Vector2.Distance(playerTrans.position, transform.position) < 7 && isDead == false)
        {
            if (playerTrans.position.x > xPos)
            {
                transform.localScale = new Vector2(1, 1);
                transform.Translate(3 * Time.deltaTime, 0, 0);
            }
            else if (playerTrans.position.x < xPos)
            {
                transform.localScale = new Vector2(-1, 1);
                transform.Translate(-3 * Time.deltaTime, 0, 0);
            }

            animator.SetBool("isRunning", true);
        }
        else if (isDead == false)
        {
            Patrol();
        }

        if (health <= 0)
        {
            isDead = true;
            animator.SetTrigger("isDead");
            transform.position = new Vector3(xPos, yPos, 1);
            rb.constraints = RigidbodyConstraints2D.FreezePositionY;
            coll.isTrigger = true;
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
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Arrow" && isDead == false)
        {
            Destroy(collision.gameObject);
            Instantiate(impact, transform.position, transform.rotation);
            health -= 45;
        }

        if (collision.gameObject.tag == "Danger" && isDead == false)
        {
            health = 0;
        }
    }
}
