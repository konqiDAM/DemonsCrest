using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShot : MonoBehaviour
{
    [SerializeField] private float shootSpped = 300;
    [SerializeField] Transform player;
    [SerializeField] Transform explosionPrefab;
    [SerializeField] Transform coinPrefab;

    void Start()
    {
        if (FindObjectOfType<Player>().GetComponent<SpriteRenderer>().flipX)
        {
            shootSpped = -shootSpped;
            GetComponent<SpriteRenderer>().flipX = true;
        }
        GetComponent<Rigidbody2D>().velocity = new Vector2(shootSpped * Time.deltaTime, 0);

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > 50)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy" || other.tag == "Shot")
        {
            Transform explo = Instantiate(explosionPrefab, other.transform.position, Quaternion.identity);
            if(other.name.StartsWith("AxeDemon"))
            {
                Instantiate(coinPrefab, other.transform.position, Quaternion.identity);
            }
            Destroy(explo.gameObject, 0.2f);
            Destroy(other.gameObject,0.2f);
            GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
            Destroy(gameObject);
        }
        if (other.tag == "Limit")
            Destroy(gameObject);
    }
}
