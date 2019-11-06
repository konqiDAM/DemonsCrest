using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float speed = 3;
    [SerializeField] float JumpForce = 5;
    [SerializeField] Transform shotPrefab;
    private float height;
    private DateTime lastJump;
    private bool lookingLeft;
    private float shootOffsetY;
    private float shootOffsetX;
    private float baseShootOffsetX;
    private bool isFlying;
    private bool grounded;
    private bool playerCanMove;
    private bool playerDying;
    private RaycastHit2D hit;
    float disanceToFloor;
    float jumpTime;
    private Rigidbody2D body;
    private Animator animator;

    void Start()
    {
        height = GetComponent<Collider2D>().bounds.size.y;
        lookingLeft = true;
        playerCanMove = true;
        shootOffsetY = 0.2f;
        shootOffsetX = 0.2f;
        jumpTime = 0.1f;
        baseShootOffsetX = shootOffsetX;
        isFlying = false;
        grounded = true;
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerDying = false;
    }

    private void FlipModel()
    {
        GetComponent<SpriteRenderer>().flipX = lookingLeft;
        if (lookingLeft)
            shootOffsetX = baseShootOffsetX;
        else
            shootOffsetX = -baseShootOffsetX;
    }

    private void checkGround()
    {

        hit = Physics2D.Raycast(transform.position, new Vector2(0, -1));

        disanceToFloor = hit.distance;
        if (!grounded && disanceToFloor < height * 0.6f)
            animator.Play("PlayerMove");
        grounded = disanceToFloor < height * 0.6f;

    }

    private void CheckJump()
    {
        bool jump = Input.GetButtonDown("Jump");
        if (jump && (DateTime.Now - lastJump).TotalSeconds > jumpTime)
        {

            lastJump = DateTime.Now;
            if (grounded)
            {
                body.AddForce(force: new Vector2(0, JumpForce), ForceMode2D.Impulse);
                animator.Play("Jump");
            }
            else
            {
                if (isFlying)
                {
                    isFlying = false;
                    body.gravityScale = 1;
                    animator.Play("PlayerFaling");

                }
                else
                {
                    isFlying = true;
                    body.gravityScale = 0;
                    body.velocity = Vector3.zero;
                    body.angularVelocity = 0;
                    animator.Play("PlayerHover");
                }
            }
        }
    }


    private void CheckFire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Instantiate(shotPrefab,
                new Vector2(transform.position.x - shootOffsetX, transform.position.y + shootOffsetY),
                Quaternion.identity);
            if(grounded)
            {
                animator.Play("PlayerAtacks");
                playerCanMove = false;
            }
            else
            {
                animator.Play("PlayerAtacksAir");
                playerCanMove = false;
            }
        }
    }

    private void CheckMoved()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        transform.Translate(horizontal * speed * Time.deltaTime, 0, 0);
        
        if (horizontal != 0)
        {
            if (isFlying)
                animator.Play("PlayerFly");
            else
                animator.Play("PlayerWalk");
        }
        else
        {
            if (isFlying)
                animator.Play("PlayerHover");
            else
                animator.Play("PlayerMove");

        }
        if (horizontal < 0)
        {
            lookingLeft = true;
            FlipModel();
        }
        else if (horizontal > 0)
        {
            lookingLeft = false;
            FlipModel();
        }
        
    }
    void Update()
    {
        
        if (playerCanMove && !playerDying)
        {
            CheckMoved();
            checkGround();
            CheckJump();
            CheckFire();
        }
    }

    public void hited(Transform other)
    {
        if (!playerDying)
        {
            animator.Play("PlayerHited");

            FindObjectOfType<GameController>().playerHit();
            playerCanMove = false;
            //push back player
            float magnitude = 25;
            Vector2 force = other.transform.position - transform.position;
            force.Normalize();
            body.AddForce(-force * magnitude);
        }
    }


    public void unlockPlayer()
    {
        if (!playerDying)
        {
            playerCanMove = true;
            //cancel push back
            body.velocity = Vector3.zero;
            body.angularVelocity = 0;
        }
    }

    public void playerDies()
    {
        playerDying = true;
        animator.Play("PlayerDies");
        playerCanMove = false;
    }
    public void playeDead()
    {
        StartCoroutine(FindObjectOfType<GameController>().changeToWelcome());
    }

}
