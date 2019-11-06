using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class AxeDemon : MonoBehaviour
{
    [SerializeField] Transform axe;
    [SerializeField] float shootSpeed;
    [SerializeField] float JumpForce = 5;
    private SpriteRenderer render;
    private Player player;
    private Rigidbody2D body;
    private Animator animator;

    void Start()
    {
        render = GetComponent<SpriteRenderer>();
        StartCoroutine(Shoot());
        player = FindObjectOfType<Player>();
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        StartCoroutine(jump());
    }

    void Update()
    {
        if (player.transform.position.x > transform.position.x)
        {
            render.flipX = true;
        }
        else
            render.flipX = false;
    }



    IEnumerator Shoot()
    {

        float pause = UnityEngine.Random.Range(2f, 3f);
        yield return new WaitForSeconds(pause);
        animator.Play("DemonShoots");

        Transform shoot = Instantiate(axe,
            transform.position,
            Quaternion.identity);
        int dir = render.flipX ? 1 : -1;
        shoot.gameObject.GetComponent<Rigidbody2D>().velocity =
            new Vector2(shootSpeed * Time.deltaTime * dir, 0);

        StartCoroutine(Shoot());
    }


    IEnumerator jump()
    {
        float pause = UnityEngine.Random.Range(3f, 6f);
        yield return new WaitForSeconds(pause);

        body.AddForce(force: new Vector2(0, JumpForce), ForceMode2D.Impulse);
        animator.Play("DemonJump");
        StartCoroutine(jump());
    }

    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            FindObjectOfType<Player>().hited(this.transform);

        }
    }
}
