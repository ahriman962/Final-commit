using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.AI;

public class HighScores : MonoBehaviour
{

    RawScore handler;

    // start of the game will load score from JSON - score from your last game
    void Start()
    {
        handler = new RawScore();
       
        handler = JsonUtility.FromJson<RawScore>(File.ReadAllText("Assets/GameData/scores.json"));
        foreach (string item in handler.rawScore)
        {
            GetComponent<Text>().text += item;
        }
    }

}
