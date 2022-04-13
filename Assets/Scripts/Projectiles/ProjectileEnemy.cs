using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileEnemy : MonoBehaviour
{
    // settings of enemy projectile

    Rigidbody2D rb;
    AudioSource audio;
    public AudioClip projectileSound;

    // velocity of the projectile
    void Start()
    {
        audio = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * 50;
        StartCoroutine(DestroyOverTime());
    }

    // sometimes the projectiles can wander anywhere, so they have to be destroyed to prevent hierarchy overflow
    IEnumerator DestroyOverTime()
    {
        yield return new WaitForSeconds(0.3f);
        Destroy(gameObject);
    }

    public void PlaySound()
    {
        audio.PlayOneShot(projectileSound, 0.5f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("TriggerDestroy"))
        {
            Destroy(gameObject);
        }


    }
    
}
