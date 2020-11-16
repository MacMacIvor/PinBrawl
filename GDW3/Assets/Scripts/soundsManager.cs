using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundsManager : MonoBehaviour
{
    //What is a sound manager?
    //Is static so has instance
    //Has a play pause stop
    //That's it?

    public static soundsManager soundsSingleton = null;

    public AudioSource firstSong;
    public AudioSource[] temp;


    static float pitchBackGround = 1;
    static float volumeBackGround = 1;
    static bool isMutedSoundEffects = false;
    static bool isMutedBackground = false;

    AudioSource backgroundPlaying;



    public void Awake()
    {
        if (soundsSingleton == null)
        {
            soundsSingleton = this;
            return;
        }
        Destroy(this);
    }

    public void startBackgroundSong(string name)
    {
        
        switch (name)
        {
            case "firstSong":
                backgroundPlaying = (firstSong);
                break;
        }
        backgroundPlaying.Play();
        if (isMutedBackground == true)
        {
            backgroundPlaying.mute = true;
        }
    }

    public void pauseBackgroundSong()
    {
       
        backgroundPlaying.Pause();
       
    }

    public void unPauseBackgroundSong()
    {
        backgroundPlaying.UnPause();
        
    }

    public void stopBackGroundMusic()
    {
        backgroundPlaying.Stop();
        
    }

    public void muteBackGround(bool onOff)
    {
        switch (onOff)
        {
            case true:
                isMutedBackground = true;
                backgroundPlaying.mute = true;
                break;
            case false:
                isMutedBackground = false;
                backgroundPlaying.mute = false;
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
            case "dronecrash_by_metal_wall":
                //backgroundPlaying = (temp[0]);
                if (isMutedSoundEffects == false)
                {
                    temp[0].Play();
                    temp[0].volume = volume;
                    temp[0].pitch = pitch;
                }
                break;
            case "laser_cannon":
                if (isMutedSoundEffects == false)
                {
                    temp[1].Play();
                    temp[1].volume = volume;
                    temp[1].pitch = pitch;
                }
                break;
            case "laser_pistol":
                if (isMutedSoundEffects == false)
                {
                    temp[2].Play();
                    temp[2].volume = volume;
                    temp[2].pitch = pitch;
                }
                break;

        }

    }

    public void changePitch(float newPitch)
    {
        muteBackGround(false);
        pitchBackGround = newPitch;
        backgroundPlaying.pitch = pitchBackGround;
    }

    public void changeVolume(float newVolume)
    {
        muteBackGround(false);
        volumeBackGround = newVolume;
        backgroundPlaying.volume = volumeBackGround;
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
        //if (backgroundPlaying.Count != 0)
        //{
        //    backgroundPlaying[0].pitch = pitchBackGround;
        //    backgroundPlaying[0].volume = volumeBackGround;
        //}

        //i++;
        //if (i == 1000)
        //{
        //    
        //    i = 0;
        //}
    }
}
