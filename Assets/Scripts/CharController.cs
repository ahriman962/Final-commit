using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.AI;

public class CharController : MonoBehaviour
{
    // this script handles controlling of the player ship, main attributes, sounds, etc.
    // I made that level gets higher when you reach certain score in game. It is handled in LateUpdate
    // mostly I followed your rules, except of reaching higher levels. In my script, reaching higher levels depends on your score
    // if you reach 1000 score, you enter LEVEL2. If you reach 4000 score, you enter LEVEL3
    public int actualLevel;
    bool levelHandler = false;
    bool levelHandler1 = false;
    bool deadHandler = false;

    RawScore handlerJson;

    [Header("UI SETTINGS")]
    public Text infoT;
    public Text scoreT;
    public Text lifeT;
    public Text levelT;
    public GameObject deathScreen;
    public GameObject mainCanvas;

    [Header("PLAYER SETTINGS")]
    public GameObject leftShoot;
    public GameObject rightShoot;
    public GameObject yourFires;
    public float horizontal;
    public float vertical;
    public int speed;
    public int rotSpeed;
    public int lifes;
    public int score;
    
    [Header("HIT SETTINGS")]
    public int numberOfHits;

    [Header("AUDIO SETTINGS")]
    AudioSource audio;
    public GameObject projectileSound;
    public GameObject crashSound;
    public AudioClip meteorSound;
    public AudioClip enemySound;

    void Start()
    {
        handlerJson = new RawScore();
        actualLevel = 1;
        levelT.text = "1";
        numberOfHits = 0;
        audio = GetComponent<AudioSource>();
        speed = 3;
        rotSpeed = 500;
        lifes = 3;
        score = 0;
    }

    void Update()
    {
        // these lines repeat themselves every frame. It is important due to keyboard control of the player ship 
        if (deadHandler == false)
        {
            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");

            Vector2 moveDir = new Vector2(horizontal, vertical);
            float inputMagn = moveDir.magnitude;
            moveDir.Normalize();
            transform.Translate(moveDir * speed * inputMagn * Time.deltaTime, Space.World);
            Quaternion toRotate = Quaternion.LookRotation(Vector3.forward, moveDir);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotate, rotSpeed * Time.deltaTime);

            // if space button is hit, coroutine Fire starts
            if (Input.GetKeyDown(KeyCode.Space))
            {
                projectileSound.GetComponent<AudioSource>().Play();
                StartCoroutine(Fire());
            }
        }
        else
            return;

    }
    private void LateUpdate()
    {
        // updating score UI text
        scoreT.text = "" + score;

        // switching level when you reach defined score
        if (score > 999 && score <= 2999 && levelHandler == false)
        {
            infoT.text = "LEVEL 2";
            levelT.text = "2";
            actualLevel = 2;
            infoT.GetComponent<Animator>().Play("FadeAnim", -1, 0);
            levelHandler = true;
        }
        if (score >= 4000 && levelHandler1 == false)
        {
            infoT.text = "LEVEL 3";
            levelT.text = "3";
            actualLevel = 3;
            infoT.GetComponent<Animator>().Play("FadeAnim", -1, 0);
            levelHandler1 = true;
        }

        // if you loose you lives, final score UI text is set
        // score you have earned, will be saved. Also random score from fake players will be saved
        // once you start your game again, you can see your score in HIGH SCORES
        // function PlayerDies() will be called
        if (lifes <= 0 && deadHandler == false)
        {
            mainCanvas.GetComponent<CanvasSet>().finalScoreT.text = "" + score;
            mainCanvas.GetComponent<CanvasSet>().finalScore = score;
            handlerJson.rawScore.Add("YOUR SCORE                   " + score + "\n");
            handlerJson.rawScore.Add("Marco                            " + Random.Range(840, 7800) + "\n");
            handlerJson.rawScore.Add("David                               " +Random.Range(840, 7800) + "\n");
            handlerJson.rawScore.Add("Antonio                          " + Random.Range(840, 7800) + "\n");
            handlerJson.rawScore.Add("Onur                                " + Random.Range(840, 7800) + "\n");
            handlerJson.rawScore.Add("Dumbo                            " + Random.Range(840, 7800) + "\n");
            handlerJson.rawScore.Add("Lame                               " + Random.Range(840, 7800) + "\n");
            handlerJson.rawScore.Add("Thumb                            " + Random.Range(840, 7800) + "\n");
            handlerJson.rawScore.Add("Draco                              " + Random.Range(840, 7800) + "\n");
            
            string json = JsonUtility.ToJson(handlerJson);
            File.WriteAllText("Assets/GameData/scores.json", json);

            Destroy(this.gameObject);
            PlayerDies();
            deadHandler = true;
        }

        /* I set this variable to 0 every frame, because I want it to increase only once you hit an enemy,
           then your life decrease by 1, so the variable must be set again to 0 */
        numberOfHits = 0;
    }

    // is you are hit, life will be decreased and will be showed on UI text
    public void PlayerIsHit()
    {
        crashSound.GetComponent<AudioSource>().Play();
        numberOfHits++;
        lifes -= numberOfHits;
        lifeT.text = "" + lifes;
    }

    // if you hit big meteor
    public void BigMeteorIsHit()
    {
        audio.PlayOneShot(meteorSound, 0.5f);
        switch (actualLevel)
        {
            case 1:
                score += 20;
                break;
            case 2:
                score += 30;
                break;
            case 3:
                score += 40;
                break;
        }
    }

    // if you hit medium meteor
    public void MediumMeteorIsHit()
    {
        audio.PlayOneShot(meteorSound, 0.5f);
        switch (actualLevel)
        {
            case 1:
                score += 15;
                break;
            case 2:
                score += 25;
                break;
            case 3:
                score += 35;
                break;
        }
    }

    // if you hit small meteor
    public void SmallMeteorIsHit()
    {
        audio.PlayOneShot(meteorSound, 0.5f);
        switch (actualLevel)
        {
            case 1:
                score += 10;
                break;
            case 2:
                score += 20;
                break;
            case 3:
                score += 30;
                break;
        }
    }

    // if you hit simple enemy(in Level 1 and 2)
    public void SimpleEnemyIsHit()
    {
        audio.PlayOneShot(enemySound, 0.5f);
        switch (actualLevel)
        {
            case 1:
                score += 40;
                break;
            case 2:
                score += 45;
                break;
        }
    }

    // if you hit smart enemy (in Level 3)
    public void SmartEnemyIsHit()
    {
        audio.PlayOneShot(enemySound, 0.5f);
        score += 60;
    }

    // if you die, UI screen will be showed and all coroutines stop
    public void PlayerDies()
    {
        crashSound.GetComponent<AudioSource>().Play();
        StopAllCoroutines();
        deathScreen.SetActive(true);
        Debug.Log("you died");
    }

    // coroutine for firing of projectiles, delay between those projectiles is 0.1sec
    // I added this delay because if an object is hit by two projectiles at once, score will be increased twice
    IEnumerator Fire()
    {
        Instantiate(yourFires, leftShoot.transform.position , leftShoot.transform.rotation);
        yield return new WaitForSeconds(0.1f);
        Instantiate(yourFires, rightShoot.transform.position, rightShoot.transform.rotation);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.transform.CompareTag("SimpleShip")) {
            PlayerIsHit();
        }
        if (collision.transform.CompareTag("SmartShip"))
        {
            PlayerIsHit();
        }
        if (collision.transform.CompareTag("OnCollision_DestroyPlayer"))
        {
            PlayerIsHit();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("OnCollision_Death"))
        {
            Destroy(collision.gameObject);
            PlayerIsHit();
        }
        if (collision.transform.CompareTag("MediumMeteor"))
        {
            Destroy(collision.gameObject);
            PlayerIsHit();
        }
        if (collision.transform.CompareTag("SmallMeteor"))
        {
            Destroy(collision.gameObject);
            PlayerIsHit();
        }
    }
}
