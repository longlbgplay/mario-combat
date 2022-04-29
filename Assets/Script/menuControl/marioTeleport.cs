using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class marioTeleport : MonoBehaviour
{
    private GameObject mario;
 
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if(mario != null)
            {
                transform.position = mario.GetComponent<enterUnderworld>().getDestination().position;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("teleporter"))
        {
            mario = collision.gameObject;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("teleporter"))
        {
            mario = collision.gameObject;
        }
    }
}
