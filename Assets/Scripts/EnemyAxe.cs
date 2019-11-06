using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAxe : MonoBehaviour
{
    private float lifeSpan;
    void Start()
    {
        lifeSpan = Random.Range(0, 3);
        StartCoroutine(applyGravity());
    }

    IEnumerator applyGravity()
    {
        yield return new WaitForSecondsRealtime(lifeSpan);
        GetComponent<Rigidbody2D>().gravityScale = 1;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag != "Enemy")
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            if (other.tag == "Player")
            {
                FindObjectOfType<Player>().hited(this.transform);
            }
            Destroy(gameObject, 0.1f);
        }
    }
}
