using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class enemy_controller : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (GameConfig.enemy_target.IndexOf(collision.gameObject) == GameConfig.enemy.IndexOf(gameObject))
        {
            //collision.gameObject.transform.position = GameConfig.RandomPosition();
        }
    }
}
