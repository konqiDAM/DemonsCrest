using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPick : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            FindObjectOfType<GameController>().pickHealth();
            Destroy(gameObject);
        }
    }
}
