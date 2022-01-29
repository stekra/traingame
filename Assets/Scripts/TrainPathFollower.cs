using UnityEngine;
using PathCreation;

public class TrainPathFollower : MonoBehaviour
{
    public PathCreator pathCreator;
    public float speed = 5;
    float wagonOffset;
    float distanceTravelled;

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
    }

    void OnPathChanged()
    {
        distanceTravelled = pathCreator.path.GetClosestDistanceAlongPath(transform.position);
    }
}
