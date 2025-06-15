using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCondition : MonoBehaviour
{
    public SceneChanger nextScene;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") {
            nextScene.NextScene();
        }
    }
}
