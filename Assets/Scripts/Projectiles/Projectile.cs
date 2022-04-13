using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.AI;

public class Projectile : MonoBehaviour
{
    // settings of YOUR projectile. Velocity of this rigidbody
    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * 50;
        StartCoroutine(DestroyOverTime());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("TriggerDestroy"))
        {
            Destroy(gameObject);
        }
       
        if (collision.transform.CompareTag("SimpleShip"))
        {
            Destroy(collision.gameObject);
        }
        if (collision.transform.CompareTag("SmartShip"))
        {
            Destroy(collision.gameObject);
        }
    }

    // sometimes the projectiles can wander anywhere, so they have to be destroyed to prevent hierarchy overflow
    IEnumerator DestroyOverTime()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
