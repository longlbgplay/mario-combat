using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deadMushroom : MonoBehaviour
{
    Vector2 deadLocation;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        deadLocation = transform.localPosition;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player" && collision.contacts[0].normal.y < 0)
        {
            Destroy(gameObject);
            GameObject turtle = (GameObject)Instantiate(Resources.Load("Prefab/deadMushroom"));
            turtle.transform.localPosition = deadLocation;
        }
        else if(collision.collider.tag == "fireBall")
        {
            Destroy(gameObject);
            GameObject turtle = (GameObject)Instantiate(Resources.Load("Prefab/deadMushroom"));
            turtle.transform.localPosition = deadLocation;
        }
    }
}
