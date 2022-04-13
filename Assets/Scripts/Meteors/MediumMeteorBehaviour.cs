using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MediumMeteorBehaviour : MonoBehaviour
{
    // this script is made for medium size meteors that are created by destroying of big meteors
    public GameObject player;
    public GameObject meteorit;
    public GameObject smallMeteor;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // if this medium meteor is hit by your projectile, four small meteors will be spwned and this meteor will be destroyed
        if (collision.gameObject.CompareTag("OnCollision_DestroyOther"))
        {
            for (int i = 0; i < 4; i++)
            {
                Instantiate(smallMeteor, transform.position, Quaternion.identity);
            }
            Destroy(meteorit);
            Destroy(collision.gameObject);
            // function from script CharController will be called to ++score
            player.GetComponent<CharController>().MediumMeteorIsHit();
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
