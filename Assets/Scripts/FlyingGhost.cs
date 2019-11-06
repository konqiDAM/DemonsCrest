using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingGhost : MonoBehaviour
{
    [SerializeField]  float speed = 4f;
    [SerializeField] float idleSpeed = 1f;
    private Vector2 newPosition;
    private Transform player;
    private Vector2 originalPosition;
    private int range;
    private float newX;
    void Start()
    {
        player = FindObjectOfType<Player>().transform;
        originalPosition = transform.position;
        range = 4;
        newX = (originalPosition.x + Random.Range(-range, range));
    }

    void Update()
    {
        if (Vector2.Distance(transform.position, new Vector2(originalPosition.x + newX, transform.position.y)) < 1)
        {
            newX = (originalPosition.x + Random.Range(-range, range));
        }
        if (Vector2.Distance(this.transform.position, player.position) < 3)
        {
            newPosition = Vector2.MoveTowards(transform.position,
                new Vector2(player.position.x, player.position.y),
                speed * Time.deltaTime);
        }
        else
        {
            newPosition = Vector2.MoveTowards(transform.position,
                new Vector2(originalPosition.x + newX, originalPosition.y),
                idleSpeed * Time.deltaTime);
        }

        if (newPosition.x < transform.position.x)
            GetComponent<SpriteRenderer>().flipX = false;
        else if (newPosition.x > transform.position.x)
            GetComponent<SpriteRenderer>().flipX = true;

        transform.position = newPosition;

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            FindObjectOfType<Player>().hited(this.transform);

        }
        Destroy(gameObject, 0.2f);
    }
}
