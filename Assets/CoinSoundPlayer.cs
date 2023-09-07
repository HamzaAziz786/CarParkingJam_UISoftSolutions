using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSoundPlayer : MonoBehaviour
{
    public AudioSource Src;
    public AudioClip clip;

    public void PlaySound()
    {
        Src.PlayOneShot(clip);
    }
}
