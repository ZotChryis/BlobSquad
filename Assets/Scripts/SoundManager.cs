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

    [SerializeField]
    private AudioSource source;

    [SerializeField]
    private List<AudioClip> sounds;

    [SerializeField]
    private List<AudioClip> songs;

    public enum SoundEffect
    {
        AttackArrow,
        AttackSword,
        AttackChakram,
        Death,
        Recruit,
        GameOver,
        Victory,

        End, // Used to denote end of enum
    }

    public enum Music
    {
        MainMenu,
        Gameplay,

        End, // Used to denote end of enum
    }

    void Start()
    {
        m_instance = this;
    }

    public void PlaySoundEffect(SoundEffect sound)
    {
        // Invalid option
        if (sound < 0 || sound >= SoundEffect.End)
        {
            return;
        }

        source.PlayOneShot(sounds[(int)sound], 0.5f);
    }
}
