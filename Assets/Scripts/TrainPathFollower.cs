﻿using UnityEngine;
using PathCreation;

public class TrainPathFollower : MonoBehaviour
{
    public PathCreator pathCreator;
    [HideInInspector]
    public float speed = 0;
    public float maxSpeed = 5;
    public float speedLerpFalloff = 0.02f;
    float wagonOffset;
    float distanceTravelled;
    [HideInInspector]
    public bool speedFrozen = false;
    public AudioSource rollingAudio;

    void Start()
    {
        if (pathCreator != null)
        {
            pathCreator.pathUpdated += OnPathChanged;
        }
        wagonOffset = Vector3.Distance(transform.GetChild(0).position, transform.GetChild(1).position);
    }

    void Update()
    {
        if (pathCreator != null)
        {
            distanceTravelled += speed * Time.deltaTime;
            transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled);
            transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled);

            for (int i = 0; i < transform.childCount; i++)
            {
                float wagonDistance = distanceTravelled - wagonOffset * i;

                transform.GetChild(i).position = pathCreator.path.GetPointAtDistance(wagonDistance);
                transform.GetChild(i).rotation = pathCreator.path.GetRotationAtDistance(wagonDistance);
            }
        }

        if (!speedFrozen)
        {
            speed = Mathf.Lerp(speed, 0, speedLerpFalloff);
            float speed01 = Mathf.InverseLerp(0, maxSpeed, Mathf.Abs(speed));
            rollingAudio.volume = speed01 / 2;
            rollingAudio.pitch = 1 + speed01 * 0.3f;
        }
    }

    void OnPathChanged()
    {
        distanceTravelled = pathCreator.path.GetClosestDistanceAlongPath(transform.position);
    }

    public Vector3 GetDirection()
    {
        return pathCreator.path.GetDirectionAtDistance(distanceTravelled);
    }
}
