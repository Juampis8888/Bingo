using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public AudioSource ThemeBingo;

    public bool IsActive = true;

    public Image Image;

    public Sprite ActiveSprite;

    public Sprite DesActiveSprite;
    
    public void Active()
    {
        IsActive = !IsActive;

        if (IsActive)
        {
            ThemeBingo.Play();
            Image.sprite = ActiveSprite;
        }
        else if (!IsActive)
        {
            ThemeBingo.Pause();
            Image.sprite = DesActiveSprite;
        }
    }
}
