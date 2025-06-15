using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public Rigidbody2D rb;
    public float speed;
    public float jumpSpeed ;
    public bool isGrounded = true;

    public Animator playerAnim;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        rb
            .velocity = new Vector2(Input.GetAxis("Horizontal")* speed, rb.velocity.y); //Movimiento del personaje (GetAxis devulve 1 o -1, es decir la direccion en x) 

        if (Input.GetAxis("Horizontal") == 0) {
            playerAnim.SetBool("isWalking", false);
        } else if (Input.GetAxis("Horizontal") > 0){
            playerAnim.SetBool("isWalking", true);
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else if (Input.GetAxis("Horizontal") < 0) {
            playerAnim.SetBool("isWalking", true);
            GetComponent<SpriteRenderer>().flipX = true;
        }

        if (isGrounded){
            if (Input.GetKeyDown(KeyCode.Space))
            {
                GetComponent<AudioSource>().Play();
                rb.AddForce(Vector2.up * jumpSpeed);
                isGrounded = false;
                playerAnim.SetTrigger("Jump");
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground")) {  //Si con lo que estamos chocando es ground.
            isGrounded = true;
        }
    }
}
