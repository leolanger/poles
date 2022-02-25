using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public static Player instance;

    private Rigidbody2D rb2d;
    private int magnetism;
    private bool isForced = false;
    private bool isAlive = true;
    private bool isWin = false;
    private bool isOnVoid;
    private bool isNextToMagnet;
    private Vector2 lookDirection;
    private Vector2 movePosition;
    //private int count = 0;
    private Animator anim;
    private float waitTimer;
    private float finishTimer;
    private bool canMove = true;
    private bool goNext = false;
    private bool isMoving = false;
    private bool finished = false;

    private float fixedDeltaTime;

    public float force = 1f;
    public float speed = 1f;
    public int myMagnetism { get { return magnetism; } }
    public Vector2 myPosition { get { return rb2d.position; } }
    public bool myIsMoving { get { return isMoving; } }
    //public int myCount { get { return count; } }



    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
            instance = this;
        else if(instance != this)
            Destroy(gameObject);

        magnetism = 0;// 0 means non-megnatic, 1 means north-pole, 2 means south-pole
        isOnVoid = false;
        isNextToMagnet = false;
        //lookDirection = Vector2.up;
        movePosition = Vector2.zero;
        waitTimer = 2.7f;
        finishTimer = 1f;

        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        this.fixedDeltaTime = Time.fixedDeltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        changeMagnetism();
        if (isOnVoid || isOnVoid && isNextToMagnet)
        { 
            isAlive = false;
            canMove = false;
        }
        if(isAlive == false)
        {
            anim.SetTrigger("die");
            Debug.Log("You're lose!");
            waitTimer -= Time.deltaTime;
            if (waitTimer <= 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }

        
        if(goNext )
        {
            finishTimer -= Time.deltaTime;
            if(finishTimer <=0)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        if (finished)
        {
            if (gameController.instance.myPieceCount >= 8)
            {
                finishTimer -= Time.deltaTime;
                if (finishTimer <= 0)
                    SceneManager.LoadScene("egg");
            }
            else
            {
                finishTimer -= Time.deltaTime;
                if (finishTimer <= 0)
                    SceneManager.LoadScene("finish");
            }
        }
    }

    private void FixedUpdate()
    {
        if(canMove)
            changePosition();
    }

    private void changePosition()
    {
        if(movePosition.y == 0)
            movePosition.x = Input.GetAxisRaw("Horizontal");
        if(movePosition.x ==0)
            movePosition.y = Input.GetAxisRaw("Vertical");

        Vector2 position = rb2d.position;
        position += movePosition * speed * Time.deltaTime;
        rb2d.MovePosition(position);

        if (movePosition != Vector2.zero)
        {
            anim.SetFloat("speed", 1);
            isMoving = true;
            lookDirection = movePosition;
        }
        else
        { 
            anim.SetFloat("speed", 0);
            isMoving = false;
        }

        anim.SetFloat("moveX", lookDirection.x);
        anim.SetFloat("moveY", lookDirection.y);
    }

    public void moveByForce(Vector2 direct)
    {
        //float distance = Mathf.Pow(direct.x * direct.x + direct.y * direct.y, 0.5f);
        //Vector2 standard;
        //standard = direct / distance;
        rb2d.AddForce(direct * force);
        isForced = true;
    }

    private void changeMagnetism()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            magnetism = 1;
            anim.SetTrigger("north");
            gameController.instance.addCount();
        }
        else if(Input.GetKeyDown(KeyCode.E))
        {
            magnetism = 2;
            anim.SetTrigger("south");
            gameController.instance.addCount();
        }
        else if(Input.GetKeyDown(KeyCode.Space))
        {
            magnetism = 0;
            anim.SetTrigger("non");
            isForced = false;
            gameController.instance.addCount();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "piece")
        {
            piece pc = collision.GetComponent<piece>();
            pc.picked();
        }

        if (collision.tag == "destination")
        {
            goNext = true;
                 // have question,how to defined which scene is the next; 
        }
        if (collision.tag == "final_destination")
        {
            finished = true;
            //SceneManager.LoadScene("finish");
            //UIController.instance.show();
        }

        if (collision.gameObject.tag == "void" && isForced == false)
        {
            Debug.Log(collision.gameObject.tag);
            isOnVoid = true;
        }
        if (collision.gameObject.tag == "ground")
            isOnVoid = false;

        if(collision.gameObject.tag == "EditorOnly")
        {
            Time.timeScale = 0.2f;
            Time.fixedDeltaTime = this.fixedDeltaTime * Time.timeScale;
            Debug.Log("慢动作！");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag =="void")
            isOnVoid = false;
        if (collision.gameObject.tag == "ground" && isForced == false)
        {
            Debug.Log(collision.gameObject.tag);
            isOnVoid = true;
        }
        if (collision.tag == "EditorOnly")
        {
            Time.timeScale = 1f;
            Time.fixedDeltaTime = this.fixedDeltaTime * Time.timeScale;
            Debug.Log("恢复动作！");
        }
        if(collision.tag == "scope")
        {
            isForced = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "magnet")
            isNextToMagnet = true;
        if (collision.gameObject.tag == "trap")
            isAlive = false;
    }
}
