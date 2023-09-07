using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundControl : MonoBehaviour
{
    public static SoundControl sndMan;
    private AudioSource audiosrc;
    private AudioClip[] hornsounds;
    private int randomSounds;
    // Start is called before the first frame update
    void Start()
    {
        sndMan = this;
        audiosrc = GetComponent <AudioSource>();

        hornsounds = Resources.LoadAll<AudioClip>("HornSounds");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playeButtonSounds()
    {
        randomSounds = Random.Range(0, 4);
        audiosrc.PlayOneShot(hornsounds[randomSounds]);
    }
}
