using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    private static ScoreManager m_instance;
    public static ScoreManager Get()
    {
        return m_instance;
    }

    [SerializeField]
    private Text text;

    void Start()
    {
        m_instance = this;

        // Game scene doesnt have this text
        if (text == null)
        {
            return;
        }

        int clearTime = PlayerPrefs.GetInt("ClearTime", -1);
        if (clearTime != -1)
        {
            text.text = "Fastest Clear: " + clearTime.ToString() + "s";
        }
        else
        {
            text.text = "Fastest Clear: UNAVAILABLE";
        }
    }

    public void TrySetBestTime(int time)
    {
        int clearTime = PlayerPrefs.GetInt("ClearTime", -1);
        if (clearTime == -1 || clearTime > time)
        {
            PlayerPrefs.SetInt("ClearTime", time);
        }
    }
}
