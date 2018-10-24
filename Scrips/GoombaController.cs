using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoombaController : MonoBehaviour {

    private Animator anim;

    public float speed;

    public LayerMask isGround;
    public LayerMask isPlayer;
    private bool facingRight = false;
    public Transform wallHitBox;
    private Rigidbody2D rb2d;
    private bool wallHit;
    private bool playerHit;
    public float wallHitWidth;
    public float wallHitHeight;
   

    public float Height;
    public float Width;


    // Use this for initialization
    void Start ()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("isDead", false);
        playerHit = false;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(speed * Time.deltaTime, 0, 0);

        wallHit = Physics2D.OverlapBox(wallHitBox.position, new Vector2(wallHitWidth, wallHitHeight), 0, isGround);
        if (wallHit == true)
        {
            speed = speed * -1;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player" && playerHit == true)
        {
            Debug.Log("Goomba dead");
            Destroy(gameObject);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(wallHitBox.position, new Vector3(wallHitWidth, wallHitHeight, 1));
    }
}
