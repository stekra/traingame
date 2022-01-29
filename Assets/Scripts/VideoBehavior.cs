using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoBehavior : MonoBehaviour
{
    public VideoClip[] clips;
    int clipIndex = 0;
    public Vector2 speedRange;
    public TrainPathFollower train;

    public float fadeDuration;
    float fade = 0f;
    Color currentFadeColor;
    bool fadingOut = false;

    bool active = false;

    RawImage videoTexture;
    VideoPlayer videoPlayer;

    void Start()
    {
        videoTexture = GetComponent<RawImage>();
        videoPlayer = GetComponent<VideoPlayer>();
        videoPlayer.clip = clips[clipIndex];
        currentFadeColor = new Color(1, 1, 1, 0);
    }

    void Update()
    {
        if (!active)
        {
            if (inSpeedRange() && !fadingOut)
            {
                fade += Time.deltaTime;
            }
            else
            {
                fade = fade >= 0 ? fade - Time.deltaTime : 0;
            }
        }

        if (fadingOut && fade <= 0)
        {
            fadingOut = false;
            train.speedFrozen = false;
            videoPlayer.clip = clips[clipIndex];
        }

        currentFadeColor.a = Mathf.InverseLerp(2, fadeDuration, fade);
        videoTexture.color = currentFadeColor;

        if (fade >= fadeDuration && !fadingOut)
        {
            active = true;
            train.speedFrozen = true;
        }
    }

    bool inSpeedRange()
    {
        return Mathf.Abs(train.speed) >= speedRange.x && Mathf.Abs(train.speed) <= speedRange.y;
    }

    // [YarnCommand("stopVideo")]
    public void stopVideo()
    {
        active = false;
        clipIndex += 1;
        if (clipIndex >= clips.Length)
        {
            print("The End!");
        }
        else
        {
            fadingOut = true;
        }
    }
}
