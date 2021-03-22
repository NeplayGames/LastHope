using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    [SerializeField] AudioClip buttonClick;
    [SerializeField] AudioSource audioSource;
   // public bool playSound = true;
    [SerializeField] GameObject environment;
    [SerializeField] Image image;
    [SerializeField] Sprite noSoundSprite;
    [SerializeField] Sprite soundSprite;
    [SerializeField] bool GameMode = false;
  public void ButtonClick()
    {
        if (soundPause) return;
        audioSource.clip = buttonClick;
        audioSource.Play();
    }
    public bool soundPause = false;
    public void PauseSound()
    {
        soundPause = !soundPause;
        image.sprite = soundPause ?   noSoundSprite :soundSprite;
        if (!GameMode) return;
        if (soundPause)
        {
            environment.SetActive(false);
        }
        else
        {
            environment.SetActive(true);

        }
    }
}
