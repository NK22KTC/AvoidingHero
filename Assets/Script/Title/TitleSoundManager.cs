using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class TitleSoundManager : MonoBehaviour
{
    [SerializeField]
    AudioSource bgmSource;

    [SerializeField]
    AudioClip[] clips;

    bool clip1_Playing;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (clip1_Playing && !bgmSource.isPlaying)
        {
            clip1_Playing = false;
            SoundSetting(2, true);
        }
    }

    public void ChangeSound(GameObject panel)
    {
        if (panel.name != "SkillStorePanel") return;

        if(bgmSource.clip.name == "TitleBGM")
        {
            clip1_Playing = true;
            SoundSetting(1, false);
        }
        else
        {
            SoundSetting(0, true);
        }
    }

    void SoundSetting(int clipNum, bool isLoop)
    {
        bgmSource.clip = clips[clipNum];
        bgmSource.Play();
        bgmSource.loop = isLoop;
    }
}
