using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmartShip : MonoBehaviour
{
    // this script handles spawning smart enemies over period of time
    public GameObject player;
    public GameObject ship;
    void Start()
    {
        StartCoroutine(Pending());
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

    // coroutine for waiting for the player to reach LEVEL 3
    IEnumerator Pending()
    {
        while (true)
        {
            // if you reach LEVEL 3, coroutine for spawning will be called and this coroutine will be stopped
            if (player.GetComponent<CharController>().actualLevel == 3)
            {
                StartCoroutine(CreateSmartShip());
                yield break;
                
            }
            yield return new WaitForFixedUpdate();
        }
    }

    IEnumerator CreateSmartShip()
    {
        while (true)
        {
            // random time between spawning smart enemies on random position on the scene
            yield return new WaitForSeconds(Random.Range(4f, 8f));
            Vector2 spawnPosition = new Vector2(Random.Range(-9, 9), Random.Range(-5, 5));
            Instantiate(ship, spawnPosition, Quaternion.identity);
        }
    }
    
}
