using UnityEngine;

public class SoundEffectManager : MonoBehaviour
{
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySoundEffect(AudioClip soundEffect)
    {
        audioSource.PlayOneShot(soundEffect);
    }
}
