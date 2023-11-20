using UnityEngine;
using UnityEngine.UI;

public class MusicSettings : MonoBehaviour
{
    private int i = 1;
    [SerializeField] private AudioClip idleMusic;
    [SerializeField] private Sprite imageMuted, imagePlaying;
    [SerializeField] private Image musicSttingsButton;
    private AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        // Set up the idle music
        audioSource.clip = idleMusic;
        audioSource.loop = true; // Loop the music infinitely
        audioSource.Play();
    }

    public void ToggleMusic()
    {
        audioSource.mute = !audioSource.mute;
        if (i == 1) musicSttingsButton.sprite = imageMuted;
        else musicSttingsButton.sprite = imagePlaying;
        i *= -1;
    }



}
