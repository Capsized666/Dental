using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoPlayerBeh : MonoBehaviour
{
    public VideoPlayer  currentPlayer;
    public RawImage     currentScreen;
    public AudioSource  currentAudio;

    //private bool isStart;

    void Awake()
    {
        currentPlayer = GetComponent<VideoPlayer>();
        currentScreen = GetComponent<RawImage>();
        currentAudio =  GetComponent<AudioSource>();


        StopVideo();
    }
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        //if (!currentPlayer.isPlaying)
        //{
        //    StopVideo();
        //}
    }


    public void PlayVideo() {
        //currentPlayer.Prepare();
        currentPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;
        currentPlayer.SetTargetAudioSource(0, currentAudio);
         

        showVideo();
        currentPlayer.Play();
        //isStart = true;
        //print("play");
        
    }
           
    public void LoadVideo(VideoClip vc) {
        currentPlayer.clip = vc;
    }
           
    public void showVideo() {
        currentScreen.enabled = true;
    }
    
    public void hideVideo() {
        currentScreen.enabled = false;
    }

    public void StopVideo()
    {
        currentPlayer.Stop();
        hideVideo();
    }

    public bool isPlaing()
    {
        return currentPlayer.isPlaying;
    }


}