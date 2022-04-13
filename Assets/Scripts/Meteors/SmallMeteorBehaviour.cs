using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallMeteorBehaviour : MonoBehaviour
{
    public GameObject player;
    public GameObject meteorit;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // if small meteor is hit by your projectile, it will be destroyed
        if (collision.gameObject.CompareTag("OnCollision_DestroyOther"))
        {
            Destroy(meteorit);
            Destroy(collision.gameObject);
            // function from script CharController will be called to ++score
            player.GetComponent<CharController>().SmallMeteorIsHit();
        }
    }
    // tag TriggerDestroy is a game object presented on the scene to prevent hierarchy overflow
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("TriggerDestroy"))
        {
            Destroy(meteorit);
        }
    }
}
