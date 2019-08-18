using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void OnPlayClicked()
    {
        SceneManager.LoadScene("Easy", LoadSceneMode.Single);
    }

    public void AnimationExit()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
}
