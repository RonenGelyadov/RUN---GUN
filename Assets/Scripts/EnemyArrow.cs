using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyArrow : MonoBehaviour
{
    float xPos;
    Transform playerScale;
    Rigidbody2D rb;

    private void Awake()
    {
        xPos = transform.position.x;

        rb = GetComponent<Rigidbody2D>();

        playerScale = GameObject.Find("Player").transform;

        if (playerScale.position.x > xPos)
        {
            transform.localScale = new Vector2(1, 1);
            rb.AddForce(new Vector2(250000 * Time.deltaTime, 0));
        }
        else if (playerScale.position.x < xPos)
        {
            transform.localScale = new Vector2(-1, 1);
            rb.AddForce(new Vector2(-250000 * Time.deltaTime, 0));
        }
    }

    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            Destroy(gameObject);
        }
        
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}
