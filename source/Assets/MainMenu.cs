using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public AudioManager am;
    public void PlayGame()
    {
        am.Play("ButtonClick");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }
    public void QuitGame()
    {
        am.Play("ButtonClick");
        Debug.Log("Quit");
        Application.Quit();
    }
}
