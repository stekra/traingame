using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using Yarn.Unity;

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
    DialogueRunner dialogueRunner;

    void Start()
    {
        dialogueRunner = FindObjectOfType<DialogueRunner>();
        videoTexture = GetComponent<RawImage>();
        videoPlayer = GetComponent<VideoPlayer>();
        videoPlayer.clip = clips[clipIndex];
        currentFadeColor = new Color(1, 1, 1, 0);
    }

    void Update()
    {
        // Check fading in condition
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

        // Fade out all the way after being active
        if (fadingOut && fade <= 0)
        {
            fadingOut = false;
            train.speedFrozen = false;
            videoPlayer.clip = clips[clipIndex];
        }

        // Set the alpha of the texture
        currentFadeColor.a = Mathf.InverseLerp(2, fadeDuration, fade);
        videoTexture.color = currentFadeColor;

        // Activate!
        if (fade >= fadeDuration && !active && !fadingOut)
        {
            active = true;
            train.speedFrozen = true;

            dialogueRunner.StartDialogue(clipIndex.ToString());
        }
    }

    bool inSpeedRange()
    {
        return Mathf.Abs(train.speed) >= speedRange.x && Mathf.Abs(train.speed) <= speedRange.y;
    }

    public void stopVideo()
    {
        if (active)
        {
            active = false;
            clipIndex += 1;

            fadingOut = true;
        }
    }

    [YarnCommand("quit")]
    public void Quit()
    {
        Application.Quit();
    }
}
