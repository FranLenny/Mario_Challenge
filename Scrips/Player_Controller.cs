using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private bool facingRight = true;
    public float speed;
    public float jumpForce;

    private AudioSource source;
    public AudioClip jumpClip;
    public AudioClip coinClip;
    public AudioClip WinClip;
    private float volLowRange = .5f;
    private float volHighRange = 1.0f;

    private bool isOnGround;
    public Transform groundcheck;
    public float checkRadius;
    public LayerMask allGround;

    private Animator anim;
    private bool playerHit;

    // Use this for initialization
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();

        anim = GetComponent<Animator>();
    }

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("escape"))
            Application.Quit();

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
        {
            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            anim.SetTrigger("Jump");
        }
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");

        //Vector2 movement = new Vector2(moveHorizontal, 0);

        //rb2d.AddForce(movement * speed);

        rb2d.velocity = new Vector2(moveHorizontal * speed, rb2d.velocity.y);

        isOnGround = Physics2D.OverlapCircle(groundcheck.position, checkRadius, allGround);

        Debug.Log(isOnGround);


        if (facingRight == false && moveHorizontal > 0)
        {
            Flip();
        }
        else if (facingRight == true && moveHorizontal < 0)
        {
            Flip();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            source.PlayOneShot(coinClip);
        }
        if (other.gameObject.CompareTag("coinBox"))
        {
            source.PlayOneShot(coinClip);
        }
        if (other.gameObject.CompareTag("win"))
        {
            source.PlayOneShot(WinClip);
        }
    }
    void Flip()
    {
        facingRight = !facingRight;
        Vector2 Scaler = transform.localScale;
        Scaler.x = Scaler.x * -1;
        transform.localScale = Scaler;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {

        if (collision.collider.tag == "Player" && playerHit == true)
        {
            anim.SetBool("isDead", true);
            Debug.Log("Goomba dead");
            Destroy(gameObject);
        }

        if (collision.collider.tag == "ground" && isOnGround)
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                //rb2d.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);

                rb2d.velocity = Vector2.up * jumpForce;

                float vol = Random.Range(volLowRange, volHighRange);

                source.PlayOneShot(jumpClip);
            }
        }
    }

}
