using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TrainPathFollower))]
public class TrainPathDrag : MonoBehaviour
{
    TrainPathFollower pathFollower;

    bool dragging = false;
    int trainMask = 1 << 6;
    int groundMask = 1 << 7;

    public float maxDragDistance = 5;

    void Start()
    {
        pathFollower = GetComponent<TrainPathFollower>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 40, trainMask))
            {
                dragging = true;
            }
        }

        if (dragging)
        {
            if (Input.GetMouseButton(0) == false)
            {
                dragging = false;
            }
            else
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 40, groundMask))
                {
                    Vector3 dragDir = (hit.point - transform.position).normalized;
                    Vector3 trainDir = pathFollower.GetDirection();
                    float magnitude = Mathf.InverseLerp(0, maxDragDistance, (hit.point - transform.position).magnitude);

                    pathFollower.speed = pathFollower.maxSpeed * magnitude * Vector3.Dot(dragDir, trainDir);
                }
            }
        }
    }
}
