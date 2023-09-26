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
