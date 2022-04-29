using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mysteryBlockControl : MonoBehaviour
{
    public float mysteryBlock = 0.5f;
    public float blockSpeed = 4f;
    public bool changeState = true;
    private Vector3 blockLocation;

    public bool isMushroom = false;
    public bool isCoin = false;
    public bool isStar = false;
    GameObject mario;
    private void Awake()
    {
        mario = GameObject.FindGameObjectWithTag("Player");
    }
    // Start is called before the first frame update
    void Start()
    {
        blockLocation = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Player" && collision.contacts[0].normal.y > 0)
        {
            //blockLocation = transform.position;
            chageBlockState();
        }
    }
    private void chageBlockState()
    { 
        if (changeState)
        {
            StartCoroutine(changeBlock());
            changeState = false;
            if (isMushroom) powerUp();
            else if (isCoin) showCoin();
        }
    }
    IEnumerator changeBlock()
    {
        while (true)
        {
            transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y + blockSpeed * Time.deltaTime) ;
            if (transform.localPosition.y >= blockLocation.y + mysteryBlock) break;
            yield return null;
        }
        while (true)
        {
            transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y - blockSpeed * Time.deltaTime);
            if (transform.localPosition.y <= blockLocation.y) break;
            Destroy(gameObject);
            GameObject blankBlock = (GameObject)Instantiate(Resources.Load("Prefab/blankBlock"));
            blankBlock.transform.position = blockLocation;
            yield return null;
        }
    }
    public void powerUp()
    {
        int currentLevel = mario.GetComponent<playerScript>().level;
        GameObject mushroom = null;
        if(currentLevel == 0)
        {
            mushroom = (GameObject)Instantiate(Resources.Load("Prefab/powerMushroom"));
        }
        else
        {
            mushroom = (GameObject)Instantiate(Resources.Load("Prefab/flower"));
        }
        mario.GetComponent<playerScript>().soundControl("powerupDisappears");
        mushroom.transform.SetParent(this.transform.parent);
        mushroom.transform.localPosition = new Vector2(blockLocation.x, blockLocation.y + 1f);
    }
    public void showCoin()
    {
        GameObject coin = (GameObject)Instantiate(Resources.Load("Prefab/coin"));
        coin.transform.SetParent(this.transform.parent);
        coin.transform.localPosition = new Vector2(blockLocation.x, blockLocation.y + 1f);
        StartCoroutine(coinSpawn(coin));
    }
    IEnumerator coinSpawn(GameObject coin)
    {
        while (true)
        {
            coin.transform.localPosition = new Vector2(coin.transform.localPosition.x, coin.transform.localPosition.y + blockSpeed * Time.deltaTime);
            if (coin.transform.localPosition.y >= blockLocation.y + 10f) break;
            yield return null;
        }
        while (true)
        {
            coin.transform.localPosition = new Vector2(coin.transform.localPosition.x, coin.transform.localPosition.y - blockSpeed * Time.deltaTime);
            if (transform.localPosition.y <= blockLocation.y) break;
            Destroy(coin.gameObject);
            yield return null;
        }
    }
}
