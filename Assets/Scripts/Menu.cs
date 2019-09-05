using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public bool isGameOver = false;
    public bool isVictory = false;
    public bool isMain = false;

    public void Start()
    {
        SoundManager soundMgr = SoundManager.Get();
        if (isGameOver)
        {
            soundMgr.PlaySoundEffect(SoundManager.SoundEffect.GameOver);
        }
        if (isVictory)
        {
            soundMgr.PlaySoundEffect(SoundManager.SoundEffect.Victory);
        }
    }

    public void OnPlayClicked()
    {
        SceneManager.LoadScene("Easy", LoadSceneMode.Single);
    }

    public void AnimationExit()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
}
