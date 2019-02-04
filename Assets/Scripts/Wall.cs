using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour // об нее будут стукаться поднимающиеся вверх крюки и уничтожаться. Навесить на пустой коллайдер, растянуть его вдоль уровня
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Hook") || other.CompareTag("Garbage"))
        {
            Destroy(other.gameObject);
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Garbage")
            Destroy(collision.gameObject);
    }
}
