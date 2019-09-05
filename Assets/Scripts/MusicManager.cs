using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private static MusicManager m_instance;
    public static MusicManager Get()
    {
        return m_instance;
    }

    [SerializeField]
    private AudioSource source;

    public enum Music
    {
        MainMenu,
        Gameplay,

        End, // Used to denote end of enum
    }

    void Start()
    {
        m_instance = this;
        source.Play();
    }
}
