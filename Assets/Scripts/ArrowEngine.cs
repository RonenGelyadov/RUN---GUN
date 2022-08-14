using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowEngine : MonoBehaviour
{
    Transform playerScale;
    Rigidbody2D rb;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        playerScale = GameObject.Find("Player").transform;

        if (playerScale.localScale.x > 0)
        {
            transform.localScale = new Vector2(1, 1);
            rb.AddForce(new Vector2(420000 * Time.deltaTime, 0));
        }
        else if (playerScale.localScale.x < 0)
        {
            transform.localScale = new Vector2(-1, 1);
            rb.AddForce(new Vector2(-420000 * Time.deltaTime, 0));
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
    }
}
