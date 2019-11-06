using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAxe : MonoBehaviour
{
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
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
