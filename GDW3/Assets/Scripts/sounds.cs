using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sounds : MonoBehaviour
{
    //What is a sound manager?
    //Is static so has instance
    //Has a play pause stop
    //That's it?

    public AudioSource firstSong;
    public AudioSource temp;
    static float pitchBackGround = 1;
    static float volumeBackGround = 1;
    static bool isMutedSoundEffects = false;
    static bool isMutedBackground = false;

    List<AudioSource> backgroundPlaying;

    

    public void startBackgroundSong(string name)
    {
        if (backgroundPlaying.Count != 0)
        {
            backgroundPlaying.Clear();
        }
        switch (name)
        {
            case "firstSong":
                backgroundPlaying.Add(firstSong);
                break;
            case "temp":
                backgroundPlaying.Add(temp);
                break;
        }
        backgroundPlaying[0].Play();
        if (isMutedBackground == true)
        {
            backgroundPlaying[0].mute = true;
        }
    }

    public void pauseBackgroundSong()
    {
        if (backgroundPlaying.Count != 0)
        {
            backgroundPlaying[0].Pause();
        }
        else
        {
            Debug.LogError("NO SONG TO PAUSE IN LIST BACKGROUND!!!!");
        }
    }

    public void unPauseBackgroundSong()
    {
        if (backgroundPlaying.Count != 0)
        {
            backgroundPlaying[0].UnPause();
        }
        else
        {
            Debug.LogError("NO SONG TO PAUSE IN LIST BACKGROUND!!!!");
        }
    }

    public void stopBackGroundMusic()
    {
        if (backgroundPlaying.Count != 0)
        {
            backgroundPlaying[0].Stop();
        }
        else
        {
            Debug.LogError("NO SONG TO PAUSE IN LIST BACKGROUND!!!!");
        }
    }

    public void muteBackGround(bool onOff)
    {
        switch (onOff)
        {
            case true:
                isMutedBackground = true;
                if (backgroundPlaying.Count != 0)
                {
                    backgroundPlaying[0].mute = true;
                }
                else
                {
                    Debug.LogError("NO SONG TO PAUSE IN LIST BACKGROUND!!!!");
                }
                break;
            case false:
                isMutedBackground = false;
                if (backgroundPlaying.Count != 0)
                {
                    backgroundPlaying[0].mute = false;
                }
                else
                {
                    Debug.LogError("NO SONG TO PAUSE IN LIST BACKGROUND!!!!");
                }
                break;
        }
    }

    public void muteSoundEffects(bool onOff)
    {
        switch (onOff)
        {
            case true:
                isMutedSoundEffects = true;
                break;
            case false:
                isMutedSoundEffects = false;
                break;
        }
    }
    public void playSoundEffect(string name, float volume = 1.0f, float pitch = 1.0f)
    {
        switch (name)
        {
            case "need effects":
                if (isMutedSoundEffects == false)
                {
                    //VariableName.play();
                    //VariableName.volume = volume;
                    //VariableName.pitch = pitch;
                }
                break;
        }

    }


    // Start is called before the first frame update
    void Start()
    {
        //firstSong.Play();
        //temp.Play();
        ////background2.volume = 0.1f;
        //firstSong.volume = 2.0f;
        //temp.pitch = 10.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (backgroundPlaying.Count != 0)
        {
            backgroundPlaying[0].pitch = pitchBackGround;
            backgroundPlaying[0].volume = volumeBackGround;
        }

        //i++;
        //if (i == 1000)
        //{
        //    
        //    i = 0;
        //}
    }
}
