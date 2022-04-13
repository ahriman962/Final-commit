using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.AI;

public class Meteor : MonoBehaviour
{
    // this script handles Meteor spawning over period of time
    public GameObject player;
    public GameObject meteorit;
    float timeToSpawn;
    float speed;

    private void Start()
    {
        StartCoroutine(SpawningMeteor());
    }

    private void Update()
    {
        // if you loose your lives, destroy this object and all coroutines will be stopped
        if (GameObject.FindGameObjectWithTag("Player"))
        {
            if (player.GetComponent<CharController>().lifes <= 0)
            {
                StopAllCoroutines();
            }
        }
        else
            return;
    }

    // coroutine for spawning meteors considering actual level
    // the higher level is, the lesser time will be yield in coroutine between spawning
    // the higher level is, the higher is speed of meteors and gravity scale is a little bit increased
    IEnumerator SpawningMeteor()
    {
        while (true)
        {
            if (player.GetComponent<CharController>().actualLevel == 1)
            {
                speed = Random.Range(30, 80);
                timeToSpawn = Random.Range(1f, 1.5f);
            }
            else if (player.GetComponent<CharController>().actualLevel == 2)
            {
                speed = Random.Range(70, 80);
                timeToSpawn = Random.Range(0.3f, 0.6f);
                meteorit.GetComponent<Rigidbody2D>().gravityScale = 0.1f;
            }
            else if (player.GetComponent<CharController>().actualLevel == 3)
            {
                speed = Random.Range(80, 100);
                timeToSpawn = Random.Range(0.3f, 0.4f);
                meteorit.GetComponent<Rigidbody2D>().gravityScale = 0.1f;
            }

            yield return new WaitForSeconds(timeToSpawn);

            // setting of random positions on four sides of main screen - up, down, left, right
            Vector2 randPosition = new Vector2(-9.6f, Random.Range(-5, 5));
            Vector2 randPosition1 = new Vector2(9.6f, Random.Range(-5, 5));
            Vector2 randPosition2 = new Vector2(Random.Range(-10f, 10f), 5.73f);
            Vector2 randPosition3 = new Vector2(Random.Range(-10f, 10f), -5.73f);

            int probability = Random.Range(1, 100);
            // probabilty of meteor spawning from the four sides of main screen
            if (probability < 40)
            {
                /* meteor is spawned, random rotation is set and AddForce will force the meteor in random speed that is set considering
                   your actual level */
                GameObject spawned = Instantiate(meteorit, randPosition, Quaternion.identity);
                spawned.GetComponent<Rigidbody2D>().rotation = Random.Range(0f, 360f);
                spawned.GetComponent<Rigidbody2D>().AddForce(spawned.transform.right * speed + (Vector3)Random.insideUnitCircle);
            }
            else if (probability > 40 && probability < 60)
            {
                GameObject spawned = Instantiate(meteorit, randPosition1, Quaternion.identity);
                spawned.GetComponent<Rigidbody2D>().rotation = Random.Range(0f, 360f);
                spawned.GetComponent<Rigidbody2D>().AddForce(-spawned.transform.right * speed + (Vector3)Random.insideUnitCircle);
            }
            else if (probability > 60 && probability < 80)
            {
                GameObject spawned = Instantiate(meteorit, randPosition2, Quaternion.identity);
                spawned.GetComponent<Rigidbody2D>().rotation = Random.Range(0f, 360f);
                spawned.GetComponent<Rigidbody2D>().AddForce(-spawned.transform.up * speed + (Vector3)Random.insideUnitCircle);
            }
            else
            {
                GameObject spawned = Instantiate(meteorit, randPosition3, Quaternion.identity);
                spawned.GetComponent<Rigidbody2D>().rotation = Random.Range(0f, 360f);
                spawned.GetComponent<Rigidbody2D>().AddForce(spawned.transform.up * speed + (Vector3)Random.insideUnitCircle);
            }
        }
    }

    
}
