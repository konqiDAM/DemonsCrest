using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyGhost : MonoBehaviour
{
    [SerializeField] private float speedX = 0.3f;

    private Vector2 newPostion;
    private bool isSpawning;
    private int lifeSpan;
    private Animator anim;
    void Start()
    {
        lifeSpan = (int)UnityEngine.Random.Range(10f, 20f); ;
        isSpawning = true;
        anim = GetComponent<Animator>();
        //anim.Play("GhostSpawning");

    }

    public void Spawned()
    {
        isSpawning = false;
        Destroy(transform.GetChild(0).gameObject);
        anim.Play("Ghost");

        StartCoroutine(Despawn());
    }

    private void DespawnGhost()
    {
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isSpawning)
        {
            newPostion = Vector2.MoveTowards(transform.position,
                new Vector2(FindObjectOfType<Player>().transform.position.x, transform.position.y),
                speedX * Time.deltaTime);
            if (newPostion.x < transform.position.x)
                GetComponent<SpriteRenderer>().flipX = false;
            else if (newPostion.x > transform.position.x)
                GetComponent<SpriteRenderer>().flipX = true;

            transform.position = newPostion;
        }
    }

    IEnumerator Despawn()
    {
        yield return new WaitForSecondsRealtime(lifeSpan);
        anim.Play("GhostDespawn");
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" )
        {
            FindObjectOfType<Player>().hited(this.transform);

        }
    }

}
