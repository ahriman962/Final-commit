using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.AI;

public class MainMenu : MonoBehaviour
{
    AudioSource audioSource;
    public Text buttonText;
    
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Main");
    }

    public void SoundToggle()
    {
        audioSource.enabled = !audioSource.enabled;
        if (audioSource.enabled)
        {
            buttonText.text = "SOUND <color=green>ON</color>";
        } else if (!audioSource.enabled)
        {
            buttonText.text = "SOUND <color=red>OFF</color>";
        }
    }

    public void Exit()
    {
        Application.Quit();
    }
}
