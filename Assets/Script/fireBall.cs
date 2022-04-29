using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireBall : MonoBehaviour
{
    public Vector2 direction;
    Vector2 fPos;
    private void Start()
    {
        GameObject Mario = GameObject.FindGameObjectWithTag("Player");
        fPos = Mario.transform.localPosition;
    }
    private void Update()
    {
        this.transform.Translate(direction * 3 * Time.deltaTime);
        if(transform.localPosition.y >= fPos.y + 1f)
        {
            direction = new Vector2(direction.x, -direction.y);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.contacts[0].normal.x != 0 || collision.collider.tag == "creep")
        {
            GameObject explosion = (GameObject)Instantiate(Resources.Load("Prefab/explosion"));
            explosion.transform.localPosition = this.transform.localPosition;
            Destroy(explosion, 0.25f);
            Destroy(gameObject);
        }
        if(collision.collider.tag == "ground")
        {
            direction = new Vector2(direction.x, -direction.y);
        }
    }
}
