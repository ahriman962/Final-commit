using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleShipBehaviour : MonoBehaviour
{
    // this script handles behaviour of simple enemies
    public GameObject player;
    public GameObject shotPos;
    public GameObject projectile;

    bool deadHandler = true;

    void Start()
    {
        StartCoroutine(Move());
        StartCoroutine(Fire());
    }

    private void Update()
    {
        // if you loose your lives, this object will be destroyed and all coroutines will be stopped
        if (player.GetComponent<CharController>().lifes <= 0 && deadHandler)
        {
            Destroy(this.gameObject);
            StopAllCoroutines();
            deadHandler = false;
        }
    }

    // coroutine for firing
    public IEnumerator Fire()
    {
        while (true)
        {
            // simple enemy fires projectile every 0.5sec
            // his projectile is spawned on "shotPos" position
            yield return new WaitForSeconds(0.5f);
            Instantiate(projectile, shotPos.transform.position, shotPos.transform.rotation);
        }
        
    }

    // simple enemy can randomly move. This coroutine will ensure the moving
    public IEnumerator Move()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(1f, 1.5f));

            // random X and Y
            float randomX = Random.Range(-5, 5);
            float randomY = Random.Range(-5, 5);

            // vector variable to handle the random axis
            Vector3 destination = new Vector3(randomX, randomY);
            float inputMagn = destination.magnitude;
            destination.Normalize();

            // force the simple enemy on random destination multiplying by 5 (speed)
            gameObject.GetComponent<Rigidbody2D>().AddForce(destination * 5 * inputMagn);

            // rotate to the destination vector point
            Quaternion toRotate = Quaternion.LookRotation(Vector3.forward, destination);
            gameObject.transform.rotation = Quaternion.RotateTowards(gameObject.transform.rotation, toRotate, 5000 * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("OnCollision_DestroyOther"))
        {
            Destroy(collision.gameObject);

            // if this simple enemy is hit by you projectile, the function will be called to ++score
            player.GetComponent<CharController>().SimpleEnemyIsHit();
        }
        if (collision.gameObject.CompareTag("TriggerDestroy"))
        {
            Destroy(this.gameObject);
        }
    }
}
