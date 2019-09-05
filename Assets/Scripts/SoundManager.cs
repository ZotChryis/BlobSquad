using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager m_instance;
    public static SoundManager Get()
    {
        return m_instance;
    }

    void Start()
    {
        m_instance = this;
    }

    public void PlaySoundEffect()
    {
        
    }
}
