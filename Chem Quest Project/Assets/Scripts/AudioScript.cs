using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioScript : MonoBehaviour
{
    AudioSource AuS;

    public AudioClip[] AuC;

    // Start is called before the first frame update
    void Start()
    {
        AuS = GetComponent<AudioSource>();
    }

    public void PlayAudio(int value)
    {
        AuS.PlayOneShot(AuC[value]);
    }
}
