using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoBehavior : MonoBehaviour
{
    public VideoClip[] clips;
    public Vector2 speedRange;
    public TrainPathFollower train;

    public float fadeDuration;
    float fade;
    Color currentFadeColor;

    bool active = false;

    RawImage videoTexture;
    VideoPlayer videoPlayer;

    void Start()
    {
        videoTexture = GetComponent<RawImage>();
        videoPlayer = GetComponent<VideoPlayer>();
        currentFadeColor = new Color(1, 1, 1, 0);
    }

    void Update()
    {
        // print(Mathf.Abs(train.speed));
        if (!active)
        {
            if (inSpeedRange())
            {
                fade += Time.deltaTime;
            }
            else
            {
                fade = fade >= 0 ? fade - Time.deltaTime : 0;
            }
        }

        currentFadeColor.a = Mathf.InverseLerp(2, fadeDuration, fade);
        videoTexture.color = currentFadeColor;

        if (fade >= fadeDuration)
        {
            active = true;
        }
    }

    bool inSpeedRange()
    {
        return Mathf.Abs(train.speed) >= speedRange.x && Mathf.Abs(train.speed) <= speedRange.y;
    }
}