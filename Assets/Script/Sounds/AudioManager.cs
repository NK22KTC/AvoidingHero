using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    AudioSource bgm;

    internal bool gameStarted = false;

    // Update is called once per frame
    void Update()
    {
        if (gameStarted && !bgm.isPlaying)
        {
            bgm.Play();
        }
    }
}
