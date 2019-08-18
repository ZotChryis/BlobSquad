using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void OnPlayClicked()
    {
        SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
    }

    public void AnimationExit()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
}
