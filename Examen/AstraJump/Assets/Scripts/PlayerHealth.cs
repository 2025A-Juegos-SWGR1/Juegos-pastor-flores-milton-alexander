using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerHealth : MonoBehaviour
{
    int health = 3;
    public Image[] hearts;
    bool hasCoolDown = false; //inmunidad despues de resivir danio, sino tiene esto la vida bajara rapidamente

    public SceneChanger chageScene;


    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (GetComponent<PlayerMovement>().isGrounded)
            {
                SubtractHealth();
            }
        }

        if (collision.gameObject.CompareTag("Enemy2"))
        {
            SubtractHealth();
        }
    }

    void SubtractHealth() { //Quitar uno de vida

        if (!hasCoolDown){
            if (health > 0)
            {
                health--;
                hasCoolDown = true;
                StartCoroutine(Cooldown()); //Tiempo de inmunidad
            }
            if (health <= 0)
            {
                chageScene.ChangeSceneTo("LoseScene");
            }

            EmptyHearts(); //Quitar corazones
        }
    }

    void EmptyHearts() {
        for (int i = 0; i< hearts.Length; i++) {
            if (health - 1 < i) {
                hearts[i].gameObject.SetActive(false);
            }
        }
    }

    IEnumerator Cooldown() {
        yield return new WaitForSeconds(5f);
        hasCoolDown = false;

        StopCoroutine(Cooldown());
    }



}
