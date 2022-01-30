using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationLogic : MonoBehaviour
{
    public bool active = false;
    List<StationLogic> otherStations;

    public GameObject activeVisuals;
    public GameObject inactiveVisuals;

    void Start()
    {
        otherStations = new List<StationLogic>();
        otherStations.AddRange(FindObjectsOfType<StationLogic>());
        otherStations.Remove(this);

        SetVisuals();
    }

    void OnTriggerEnter(Collider other)
    {
        if (active)
        {
            active = false;
            SetVisuals();
            StartCoroutine(ChooseNewStation());
        }

        print("yo");
    }

    public void SetVisuals()
    {
        activeVisuals.SetActive(active);
        inactiveVisuals.SetActive(!active);
    }

    IEnumerator ChooseNewStation()
    {
        yield return new WaitForSeconds(2);
        int choice = Random.Range(0, otherStations.Count);
        otherStations[choice].active = true;
        otherStations[choice].SetVisuals();
    }
}
