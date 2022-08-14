using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeEngine : MonoBehaviour
{
    public static float xPos;
    float yPos;
    float maxRight;
    float maxLeft;
    bool movingRight;
    int health;
    bool isDead;
    Transform playerPos;
    Animator animator;
    Collider2D coll;
    GameObject impact;


    void Start()
    {
        maxLeft = transform.position.x - 2;
        maxRight = transform.position.x + 2;

        movingRight = true;


        health = 90;
    }


    void Update()
    {
        xPos = transform.position.x;
        yPos = transform.position.y;

        playerPos = GameObject.Find("Player").transform;

        impact = (GameObject)Resources.Load("Prefabs/Impact", typeof(GameObject));

        animator = GetComponent<Animator>();
        coll = GetComponent<PolygonCollider2D>();

        if (xPos <= maxLeft)
        {
            movingRight = true;
        }
        else if (xPos >= maxRight)
        {
            movingRight = false;
        }

        if (Vector2.Distance(playerPos.position, transform.position) < 7 && isDead == false)
        {
            if (playerPos.position.x > xPos)
            {
                transform.localScale = new Vector2(1, 1);
                transform.Translate(3 * Time.deltaTime, 0, 0);
            }
            else if (playerPos.position.x < xPos)
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

        animator.SetBool("isRunning", false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Arrow" && isDead == false)
        {
            Destroy(collision.gameObject);
            Instantiate(impact, transform.position, transform.rotation);
            health -= 90;
        }
    }
}
