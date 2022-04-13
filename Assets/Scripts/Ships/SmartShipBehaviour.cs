using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmartShipBehaviour : MonoBehaviour
{
    // this script handles behaviour of smart enemies
    public GameObject player;
    public GameObject shotPos;
    public GameObject projectile;

    bool deadHandler = true;
    void Start()
    {
        StartCoroutine(Fire());
    }


    void Update()
    {
        // this code handles persistent rotating of smart ship towards player

        Vector3 direction = (player.transform.position - transform.position).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        int offset = 260;
        transform.rotation = Quaternion.Euler(Vector3.forward * (angle + offset));

        // if you loose your lives, this object will be destroyed and all coroutines will be stopped
        if (player.GetComponent<CharController>().lifes <= 0 && deadHandler)
        {
            Destroy(this.gameObject);
            StopAllCoroutines();
            deadHandler = false;
        }
    }

    public IEnumerator Fire()
    {
        while (true)
        {
            // smart enemy fires projectile every 0.5sec
            // his projectile is spawned on "shotPos" position
            yield return new WaitForSeconds(0.5f);
            Instantiate(projectile, shotPos.transform.position, shotPos.transform.rotation);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("OnCollision_DestroyOther"))
        {
            Destroy(collision.gameObject);
            player.GetComponent<CharController>().SmartEnemyIsHit();
        }
        if (collision.gameObject.CompareTag("TriggerDestroy"))
        {
            Destroy(gameObject);
        }
    }
}
