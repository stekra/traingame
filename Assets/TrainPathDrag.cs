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

            if (Physics.Raycast(ray, out hit, 100, trainMask))
            {
                print("clicked train!");
                dragging = true;
            }
        }

        if (dragging)
        {
            if (Input.GetMouseButton(0) == false)
            {
                print("done draggin");
                dragging = false;
            }
            else
            {
                print("draggin");
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 100, groundMask))
                {
                    Debug.DrawLine(transform.position, hit.point, Color.black);
                }
            }
        }
    }
}
