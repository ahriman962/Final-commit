using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleShip : MonoBehaviour
{
    // this script handles spawning simple enemies over period of time
    public GameObject ship;
    public GameObject player;

    void Start()
    {
        StartCoroutine(CreateShip());
        
    }
    private void Update()
    {
        // if you loose your lives, all coroutines will be stopped
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

    // coroutine for spawning simple enemies
    IEnumerator CreateShip()
    {
        while (true)
        {
            // random time between spawning
            yield return new WaitForSeconds(Random.Range(3f, 4f));

            // spawn position is also random on the scene
            Vector2 spawnPosition = new Vector2(Random.Range(-9, 9), Random.Range(-5, 5));
            Instantiate(ship, spawnPosition, Quaternion.identity);

            // if you reach LEVEL 3, simple enemies will not be spawned anymore
            if (player.GetComponent<CharController>().actualLevel == 3)
            {
                yield break;
            }
        }
    }
}
