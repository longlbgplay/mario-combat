using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScript : MonoBehaviour
{
    private float marioVelocity = 7f;
    private float marioJump = 450f;
    private float highJump = 5;
    private float lowJump = 5;
    private float speed = 0;
    private bool onTheGround = true;
    private bool changeDirection = false;
    private bool moveToRight = true;
    private float holdZ = 0.2f;
    private float holdCheck = 0;
    public int level = 0;
    public bool powerUp = false;
    public bool immortal = false;
    public bool canAttact = false;
    public bool isUnderground = false;

    private Vector2 deadLocation;
    private Animator animations;
    private Rigidbody2D r2d;
    private AudioSource soundEffect;
    public static Vector2 lastCheckPoint = new Vector2(-3.5f, 0.07f);
    public Transform a, b;

    private void Awake()
    {
        GameObject.FindGameObjectWithTag("Player").transform.position = lastCheckPoint;
    }

    // Start is called before the first frame update
    void Start()
    {
        r2d = GetComponent<Rigidbody2D>();
        animations = GetComponent<Animator>();
        soundEffect = GetComponent<AudioSource>();
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        animations.SetFloat("speed", speed);
        animations.SetBool("onTheGround", onTheGround);
        animations.SetBool("changeDirection", changeDirection);
        jump();
        shootingAndSpeedUp();
        if(powerUp == true)
        {
            switch (level)
            {
                case 0:
                    {
                        StartCoroutine(MarioPowerDown());
                        soundControl("powerupDisappears");
                        powerUp = false;
                        break;
                    }
                case 1:
                    {
                        StartCoroutine(MarioPowerUp1());
                        soundControl("powerUp");
                        powerUp = false;
                        break;
                    }
                case 2:
                    {
                        StartCoroutine(MarioPowerUp2());
                        soundControl("powerUp");
                        powerUp = false;
                        canAttact = true;
                        break;
                    }
                default: {
                        powerUp = false;
                        canAttact = false;
                        break;
                    }
            }
        }
        if (gameObject.transform.position.y < -10)
        {
            //marioDead();
            //Destroy(gameObject);
            isUnderground = true;
        }
    }
    private void FixedUpdate()
    {
        Moves();
    }
    void Moves()
    {
        float moveLeftRight = Input.GetAxis("Horizontal");
        r2d.velocity = new Vector2(marioVelocity * moveLeftRight, r2d.velocity.y);
        speed = Mathf.Abs(marioVelocity * moveLeftRight);
        if (moveLeftRight > 0 && !moveToRight) marioDirection();
        if (moveLeftRight < 0 && moveToRight) marioDirection();
    }

    void marioDirection()
    {
        moveToRight = !moveToRight;
        transform.Rotate(0f, 180f, 0f);
        if (speed > 0) StartCoroutine(ChangePlayerDirection());
    }
    void jump()
    {
        if(Input.GetKeyDown(KeyCode.X) && onTheGround == true)
        {
            r2d.AddForce((Vector2.up) * marioJump);
            soundControl("jump");
            onTheGround = false;
        }
        if(r2d.velocity.y < 0)
        {
            r2d.velocity += Vector2.up * Physics2D.gravity.y * (highJump - 1) * Time.deltaTime;
        }else if(r2d.velocity.y > 0 && !Input.GetKey(KeyCode.X))
        {
            r2d.velocity += Vector2.up * Physics2D.gravity.y * (lowJump - 1) * Time.deltaTime;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "ground" || collision.tag == "pipe" || collision.tag == "teleporter")
        {
            onTheGround = true;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "ground" || collision.tag == "pipe" || collision.tag == "teleporter")
        {
            onTheGround = true;
        } 
    }

    IEnumerator ChangePlayerDirection()
    {
        changeDirection = true;
        yield return new WaitForSeconds(0.2f);
        changeDirection = false;
    }
    void shootingAndSpeedUp()
    {
        if (Input.GetKey(KeyCode.Z)){
            holdCheck += Time.deltaTime;
            if (holdCheck < holdZ)
            {
                print("shoot");
            }
            else
            {
                marioVelocity = marioVelocity + 5;
                if (marioVelocity > 12) marioVelocity = 12;
            }
        }
        if (Input.GetKeyUp(KeyCode.Z))
        {
            marioVelocity = 7f;
            holdCheck = 0;
        }
        if (Input.GetKeyDown(KeyCode.LeftControl) && canAttact == false)
        {
            Vector2 direct = b.position - a.position;
            GameObject fireBall = (GameObject)Instantiate(Resources.Load("Prefab/fireBall"));
            Vector2 marioLocation = transform.position;
            if (moveToRight)
                fireBall.transform.localPosition = new Vector2(marioLocation.x + 1f, marioLocation.y + 1f);
            else
                fireBall.transform.localPosition = new Vector2(marioLocation.x - 1f, marioLocation.y + 1f);
            fireBall.GetComponent<fireBall>().direction = direct;
        }
    }
    IEnumerator MarioPowerUp1()
    {
        float delayTime = 0.2f;
        animations.SetLayerWeight(animations.GetLayerIndex("SmallMario"), 0);
        animations.SetLayerWeight(animations.GetLayerIndex("BigMario"), 1);
        animations.SetLayerWeight(animations.GetLayerIndex("WhiteMario"), 0);
        yield return new WaitForSeconds(delayTime);
        animations.SetLayerWeight(animations.GetLayerIndex("SmallMario"), 0);
        animations.SetLayerWeight(animations.GetLayerIndex("BigMario"), 0);
        animations.SetLayerWeight(animations.GetLayerIndex("WhiteMario"), 0);
        yield return new WaitForSeconds(delayTime);
        animations.SetLayerWeight(animations.GetLayerIndex("SmallMario"), 0);
        animations.SetLayerWeight(animations.GetLayerIndex("BigMario"), 1);
        animations.SetLayerWeight(animations.GetLayerIndex("WhiteMario"), 0);
        yield return new WaitForSeconds(delayTime);
        animations.SetLayerWeight(animations.GetLayerIndex("SmallMario"), 0);
        animations.SetLayerWeight(animations.GetLayerIndex("BigMario"), 0);
        animations.SetLayerWeight(animations.GetLayerIndex("WhiteMario"), 0);
        yield return new WaitForSeconds(delayTime);
        animations.SetLayerWeight(animations.GetLayerIndex("SmallMario"), 0);
        animations.SetLayerWeight(animations.GetLayerIndex("BigMario"), 1);
        animations.SetLayerWeight(animations.GetLayerIndex("WhiteMario"), 0);
        yield return new WaitForSeconds(delayTime);
    }
    IEnumerator MarioPowerUp2()
    {
        float delayTime = 0.2f;
        animations.SetLayerWeight(animations.GetLayerIndex("SmallMario"), 0);
        animations.SetLayerWeight(animations.GetLayerIndex("BigMario"), 0);
        animations.SetLayerWeight(animations.GetLayerIndex("WhiteMario"), 1);
        yield return new WaitForSeconds(delayTime);
        animations.SetLayerWeight(animations.GetLayerIndex("SmallMario"), 0);
        animations.SetLayerWeight(animations.GetLayerIndex("BigMario"), 1);
        animations.SetLayerWeight(animations.GetLayerIndex("WhiteMario"), 0);
        yield return new WaitForSeconds(delayTime);
        animations.SetLayerWeight(animations.GetLayerIndex("SmallMario"), 0);
        animations.SetLayerWeight(animations.GetLayerIndex("BigMario"), 0);
        animations.SetLayerWeight(animations.GetLayerIndex("WhiteMario"), 1);
        yield return new WaitForSeconds(delayTime);
        animations.SetLayerWeight(animations.GetLayerIndex("SmallMario"), 0);
        animations.SetLayerWeight(animations.GetLayerIndex("BigMario"), 1);
        animations.SetLayerWeight(animations.GetLayerIndex("WhiteMario"), 0);
        yield return new WaitForSeconds(delayTime);
        animations.SetLayerWeight(animations.GetLayerIndex("SmallMario"), 0);
        animations.SetLayerWeight(animations.GetLayerIndex("BigMario"), 0);
        animations.SetLayerWeight(animations.GetLayerIndex("WhiteMario"), 1);
        yield return new WaitForSeconds(delayTime);
    }
    IEnumerator MarioPowerDown()
    {
        immortal = true;
        float delayTime = 0.2f;
        animations.SetLayerWeight(animations.GetLayerIndex("SmallMario"), 1);
        animations.SetLayerWeight(animations.GetLayerIndex("BigMario"), 0);
        animations.SetLayerWeight(animations.GetLayerIndex("WhiteMario"), 0);
        yield return new WaitForSeconds(delayTime);
        animations.SetLayerWeight(animations.GetLayerIndex("SmallMario"), 0);
        animations.SetLayerWeight(animations.GetLayerIndex("BigMario"), 1);
        animations.SetLayerWeight(animations.GetLayerIndex("WhiteMario"), 0);
        yield return new WaitForSeconds(delayTime);
        animations.SetLayerWeight(animations.GetLayerIndex("SmallMario"), 1);
        animations.SetLayerWeight(animations.GetLayerIndex("BigMario"), 0);
        animations.SetLayerWeight(animations.GetLayerIndex("WhiteMario"), 0);
        yield return new WaitForSeconds(delayTime);
        animations.SetLayerWeight(animations.GetLayerIndex("SmallMario"), 0);
        animations.SetLayerWeight(animations.GetLayerIndex("BigMario"), 1);
        animations.SetLayerWeight(animations.GetLayerIndex("WhiteMario"), 0);
        yield return new WaitForSeconds(delayTime);
        animations.SetLayerWeight(animations.GetLayerIndex("SmallMario"), 1);
        animations.SetLayerWeight(animations.GetLayerIndex("BigMario"), 0);
        animations.SetLayerWeight(animations.GetLayerIndex("WhiteMario"), 0);
        yield return new WaitForSeconds(1f);
        immortal = false;
    }
    public void soundControl(string fileName)
    {
        soundEffect.PlayOneShot(Resources.Load<AudioClip>("Audio/" + fileName));
    }
    public void marioDead()
    {
        if (!immortal)
        {
            deadLocation = transform.localPosition;
            GameObject marioDead = (GameObject)Instantiate(Resources.Load("Prefab/characters_13"));
            marioDead.transform.localPosition = deadLocation;
            Destroy(gameObject);
            FindObjectOfType<checkPoint>().restart();
        }
        
        //StartCoroutine(deadAnim());
    }

    //IEnumerator deadAnim()
    //{
    //    while (true)
    //    {
    //        float bump = 2f;
    //        transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y + bump * Time.deltaTime);
    //        if (transform.localPosition.y >= deadLocation.y + 3.5f) break;
    //        yield return null;
    //        transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y - bump * Time.deltaTime);
    //        if (transform.localPosition.y <= -10f)
    //        {
    //            Destroy(gameObject);
    //            break;
    //        }
    //        yield return null;
    //    }
    //}
}
