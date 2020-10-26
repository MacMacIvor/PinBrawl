using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sounds : MonoBehaviour
{
    public AudioSource background;
    AudioSource m_MyAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        background.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
