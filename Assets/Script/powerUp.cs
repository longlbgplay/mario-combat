using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerUp : MonoBehaviour
{
    GameObject mario;
    private void Awake()
    {
        mario = GameObject.FindGameObjectWithTag("Player");
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Player")
        {
            if (mario.GetComponent<playerScript>().level < 2)
            {
                mario.GetComponent<playerScript>().level += 1;
                mario.GetComponent<playerScript>().powerUp = true;
                Destroy(gameObject);
            }
        }
    }
}
