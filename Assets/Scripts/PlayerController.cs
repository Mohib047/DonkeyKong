using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Variable Declaration 
    private Rigidbody2D playerRb;
    private Collider2D playerCol;
    private Collider2D[] listOfCol;


    private float speed = 2;
    public float climbSpeed = 2;
    public float jumpForce = 2;
    
    public bool isOnGround ;
    public bool isClimbing ;

    private Vector2 moveDir;
    public GameObject gameManager;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        playerCol = GetComponent<Collider2D>();
        listOfCol = new Collider2D[4];
        gameManager = GameObject.Find("GameManager");
    }


    //Collision checks
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Barrel" || collision.collider.tag == "DonkeyKong")
        {
            gameManager.GetComponent<GameManager>().FailLevel();
        }
        else if (collision.collider.tag == "Princess")
        {
            gameManager.GetComponent<GameManager>().CompleteLevel();
        }
    }
    void CheckCollisions()
    {
        isClimbing = false;
        Vector2 size = playerCol.bounds.size;
        size.y += 0.5f;
        size.x /= 2;
        int count = Physics2D.OverlapBoxNonAlloc(transform.position, size, 0, listOfCol);

        for (int i = 0; i < count; i++)
        {
            GameObject hit = listOfCol[i].gameObject;
            if (hit.layer == LayerMask.NameToLayer("Ground"))
            {
                isOnGround = hit.transform.position.y < (transform.position.y - 0.5f);
                Physics2D.IgnoreCollision(playerCol, listOfCol[i], !isOnGround);
            }
            else if (hit.layer == LayerMask.NameToLayer("Ladder"))
            {
                Debug.Log("Ladder");
                isClimbing = true;
            }
        }
    }

    //Player Movement Inputs
    void Movement()
    {

        moveDir.x = Input.GetAxis("Horizontal") * speed;
        if (isOnGround)
        {
            moveDir.y = Mathf.Max(moveDir.y, -1f);
        }
        //Movement direction
        if (moveDir.x > 0)
        {
            transform.eulerAngles = Vector3.zero;
        }
        else if (moveDir.x < 0)
        {
            transform.eulerAngles = new Vector3(0f, 180f, 0);

        }
        

    }

    //Player Jumping
    void Jumping()
    {

        moveDir = Vector2.up * jumpForce;
        isOnGround = false;

    }

    //Player Climbing
    void Climbing()
    {
        moveDir.y = Input.GetAxis("Vertical") * speed;
    }

    // Update is called once per frame
    void Update()
    {
        CheckCollisions();
        if (isClimbing)
        {
            Climbing();
        }
        else if (isOnGround && Input.GetKeyDown(KeyCode.Space))
        {
            Jumping();
        }
        else 
        {
            moveDir += Physics2D.gravity * Time.deltaTime;
        }
        Movement();

    }
    private void FixedUpdate()
    {
        //Actual Player Movement
        playerRb.MovePosition(playerRb.position + moveDir * Time.deltaTime * speed);

    }
}
