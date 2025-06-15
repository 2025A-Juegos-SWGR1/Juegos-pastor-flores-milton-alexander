using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    Rigidbody2D enemyRb;
    SpriteRenderer enemySpriteRen;
    Animator enemyAnim;
    ParticleSystem enemyParticle;

    public float speed = .3f;

    //variabled del tiempo para el cambio de direccion
    float timeBeforeChange;
    public float delay = 5.0f;


    void Start()
    {
        enemyRb = GetComponent<Rigidbody2D>();
        enemySpriteRen = GetComponent<SpriteRenderer>();
        enemyAnim = GetComponent<Animator>();
        enemyParticle = GameObject.Find("EnemyParticle").GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        enemyRb.velocity = Vector2.right * speed;

        if (speed < 0) {
            enemySpriteRen.flipX = true;
        }else {
            enemySpriteRen.flipX = false;
        }

        if (timeBeforeChange < Time.time) { //Time.time es el tiempo transcurrido
            speed *= -1;  //cambio de direccion
            timeBeforeChange = Time.time + delay;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")){
            if (transform.position.y + .03f < collision.transform.position.y) { //si el player colisina con el enemigo desde la parte superior 
                enemyAnim.SetBool("isDead", true);
                enemyParticle.transform.position = new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z);
                enemyParticle.Play();
                GetComponent<AudioSource>().Play();
            }
        }
    }

    public void DisableEnemy() {
        gameObject.SetActive(false);
        //Destroy(collision.gameObject);
    }

    public void DisableCollider()
    {
        GetComponent<Collider2D>().enabled = false;
        //Destroy(collision.gameObject);
    }
}
