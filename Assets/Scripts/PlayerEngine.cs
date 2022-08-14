using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerEngine : MonoBehaviour
{
    float xPos;
    float yPos;
    int speed;
    bool canJump;
    bool isDead;
    public Rigidbody2D rb;
    public Animator animator;
    public GameObject arrow;
    float shootingTime;
    public Text healthDisplay;
    public Text coinsDisplay;
    public Text arrowsDisplay;
    float healthNum;
    float coinNum;
    float arrowsNum;
    public AudioSource audioSource;
    public AudioClip shootSound;
    public AudioClip jump;
    GameObject impact;

    void Start()
    {
        xPos = -4;
        yPos = -1.8f;

        transform.position = new Vector2(xPos, yPos);

        isDead = false;

        shootingTime = 0;

        healthNum = 100;
        coinNum = 0;
        arrowsNum = 10;
    }


    void Update()
    {
        xPos = transform.position.x;
        yPos = transform.position.y;

        speed = 7;

        shootingTime -= Time.deltaTime;

        impact = (GameObject)Resources.Load("Prefabs/Impact", typeof(GameObject));

        arrowsDisplay.text = arrowsNum.ToString();
        healthDisplay.text = healthNum.ToString();
        coinsDisplay.text = coinNum.ToString();


        if (shootingTime <= 0)
        {
            animator.SetBool("isShooting", false);
        }


        if (Input.GetKey(KeyCode.RightArrow) && isDead == false)
        {
            transform.Translate(speed * Time.deltaTime, 0, 0);
            transform.localScale = new Vector2(1, 1);
            animator.SetBool("isRuning", true);
        }
        else if (Input.GetKey(KeyCode.LeftArrow) && isDead == false)
        {
            transform.Translate(-speed * Time.deltaTime, 0, 0);
            transform.localScale = new Vector2(-1, 1);
            animator.SetBool("isRuning", true);
        }
        else
        {
            animator.SetBool("isRuning", false);
        }

       
        if (Input.GetKeyDown(KeyCode.UpArrow) && canJump == true && isDead == false)
        {
            rb.AddForce(new Vector2(0, 500));
            audioSource.PlayOneShot(jump, 0.3f);
            canJump = false;
            animator.SetBool("isJumping", true);
            animator.SetBool("isRuning", false);    
        }

       
        if (Input.GetKeyDown(KeyCode.Space) && arrowsNum > 0 && isDead == false)
        {
            if (transform.localScale.x > 0)
            {
                shootingTime = 0.3f;
                animator.SetBool("isShooting", true);
                Instantiate(arrow, new Vector2(xPos + 1, yPos), transform.rotation);
            }
            
            else if (transform.localScale.x < 0)
            {
                shootingTime = 0.3f;
                animator.SetBool("isShooting", true);
                Instantiate(arrow, new Vector2(xPos - 1, yPos), transform.rotation);
            }

            audioSource.PlayOneShot(shootSound, 0.3f);
            arrowsNum--;
        }


        if (healthNum > 100)
        {
            healthNum = 100;
        }
        else if (healthNum <= 0)
        {
            animator.SetTrigger("isDead");
            healthNum = 0;
            isDead = true;
            SceneManager.LoadScene(4);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            canJump = true;
            animator.SetBool("isJumping", false);
        }


        if (collision.gameObject.tag == "Danger")
        {
            healthNum = 0;
        }


        if (collision.gameObject.tag == "Boar")
        {
            Instantiate(impact, transform.position, transform.rotation);
            healthNum -= 20;

            if (BoarEngine.xPos > xPos)
            {
                rb.AddForce(new Vector2(-25000 * Time.deltaTime, 0));
            }
            else
            {
                rb.AddForce(new Vector2(25000 * Time.deltaTime, 0));
            }           
        }


        if (collision.gameObject.tag == "Bee")
        {
            Instantiate(impact, transform.position, transform.rotation);
            healthNum -= 20;

            if (BeeEngine.xPos > xPos)
            {
                rb.AddForce(new Vector2(-25000 * Time.deltaTime, 0));
            }
            else
            {
                rb.AddForce(new Vector2(25000 * Time.deltaTime, 0));
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "EnemyArrow")
        {
            Instantiate(impact, transform.position, transform.rotation);
            healthNum = float.Parse(healthDisplay.text);
            healthNum = healthNum - 20;                    
        }


        if (collision.gameObject.tag == "Coin")
        {
            Destroy(collision.gameObject);
            coinNum++;           
        }


        if (collision.gameObject.tag == "PickArrow")
        {
            Destroy(collision.gameObject);
            arrowsNum = arrowsNum + 5;        
        }


        if (collision.gameObject.tag == "Potion")
        {
            Destroy(collision.gameObject);
            healthNum += 60;
        }

        if (collision.gameObject.tag == "Finish")
        {
            SceneManager.LoadScene(5);
        }
    }
}
