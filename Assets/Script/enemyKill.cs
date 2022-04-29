using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyKill : MonoBehaviour
{
    GameObject mario;
    private void Awake()
    {
        mario = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update()
    {

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player" && (collision.contacts[0].normal.x > 0 || collision.contacts[0].normal.x < 0))
        {
            if(mario.GetComponent<playerScript>().level > 0)
            {
                mario.GetComponent<playerScript>().level = 0;
                mario.GetComponent<playerScript>().powerUp = true;
            }
            else
            {
                mario.GetComponent<playerScript>().marioDead();
            }
        }
    }
}
