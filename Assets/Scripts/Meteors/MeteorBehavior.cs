using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorBehavior : MonoBehaviour
{
    public GameObject player;
    public GameObject meteorit;
    public GameObject mediumMeteor;
   

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // if this big meteor is hit by your projectile, two medium meteors will be spwned and this meteor will be destroyed
        if (collision.gameObject.CompareTag("OnCollision_DestroyOther"))
        {
            GameObject spawned = Instantiate(mediumMeteor, transform.position, Quaternion.identity);
            GameObject spawned1 = Instantiate(mediumMeteor, transform.position, Quaternion.identity);
            spawned.GetComponent<Rigidbody2D>().AddForce(transform.up * 30);
            spawned1.GetComponent<Rigidbody2D>().AddForce(transform.up * 30);
            Destroy(meteorit);
            Destroy(collision.gameObject);
            // function from script CharController will be called to ++score
            player.GetComponent<CharController>().BigMeteorIsHit();
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
