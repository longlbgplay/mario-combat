using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingItems : MonoBehaviour
{
    public float creepSpeed;
    public bool goLeft = false;
    // Start is called before the first frame update
    private void FixedUpdate()
    {
        Vector2 movingObject = transform.localPosition;
        if (goLeft) movingObject.x -= creepSpeed * Time.deltaTime;
        else movingObject.x += creepSpeed * Time.deltaTime;
        transform.localPosition = movingObject;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if(collision.contacts[0].normal.x > 0)
        //{
        //    goLeft = false;
        //    turnAround();
        //}
        //else
        //{
        //    goLeft = true;
        //    turnAround();
        //}
        if (collision.collider.tag != "Player" && collision.contacts[0].normal.x > 0)
        {
            goLeft = false;
            turnAround();
        }
        else if (collision.collider.tag != "Player" && collision.contacts[0].normal.x < 0)
        {
            goLeft = true;
            turnAround();
        }
    }
    void turnAround()
    {
        //goLeft = !goLeft;
        Vector2 turnBack = transform.localScale;
        turnBack.x *= -1;
        transform.localScale = turnBack;
    }
}
