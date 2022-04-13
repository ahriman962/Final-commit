using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.AI;

public class CanvasSet : MonoBehaviour
{
    public Text finalScoreT;
    public int finalScore;

    RawScore handler;
    public GameObject highScores;

    private void Start()
    {
        handler = new RawScore();
    }
    public void PlayAgain()
    {
        SceneManager.LoadScene("Main");
    }

    public void ShowMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
