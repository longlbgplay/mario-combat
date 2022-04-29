using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class checkPoint : MonoBehaviour
{
    Vector2 playerPosition;
    private void Start()
    {
        playerPosition = FindObjectOfType<playerScript>().transform.position;
    }
    public void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        FindObjectOfType<playerScript>().transform.position = playerPosition;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Player")
        {
            playerScript.lastCheckPoint = transform.position;
        }
    }
}
