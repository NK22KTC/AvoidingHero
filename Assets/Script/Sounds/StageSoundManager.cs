using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking.Types;

public class StageSoundManager : MonoBehaviour
{
    GameFlow gameFlow;

    [SerializeField]
    AudioSource source;

    [SerializeField]
    AudioClip clip;

    void FixedUpdate()
    {
        if (!source.isPlaying)
        {
            source.PlayOneShot(clip);
        }
    }
}
