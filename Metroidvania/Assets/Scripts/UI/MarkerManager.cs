using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerManager : MonoBehaviour {

    public static MarkerManager Instance { get; set; }

    public List<Marker> markers;

    void Start()
    {
        if (Instance == null)
            Instance = this;
    }

    public void RegisterMarker(Marker marker)
    {
        markers.Add(marker);
    }

    //disables all visible markers
    public void TurnOffMarks()
    {
        foreach (Marker mark in markers)
        {
            mark.gameObject.SetActive(false);
        }
    }

    //allows the marker for the first area to be set and delete the marks in each area.
    public void setMark1()
    {
        foreach (Marker mark in markers)
        {
            mark.gameObject.SetActive(false);
        }
        markers[0].gameObject.SetActive(true);
    }

    //set the second area marker and turn off the rest
    public void setMark2()
    {
        foreach (Marker mark in markers)
        {
            mark.gameObject.SetActive(false);
        }
        markers[1].gameObject.SetActive(true);
    }

    /*
    void Start()
    {
        area1_cm.SetActive(false);
        area2_cm.SetActive(false);
    }

    public void MarkArea1()
    {
        if (area1Active == false)
        {
            area1_cm.SetActive(true);
            area1Active = true;
            activeStack.Push(area1_cm);
        }
        else
        {
            area1_cm.SetActive(false);
            area1Active = false;
            activeStack.Pop();
        }
    }

    public void MarkArea2()
    {
        if (area2Active == false)
        {
            area2_cm.SetActive(true);
            area2Active = true;
            activeStack.Push(area1_cm);
        }
        else
        {
            area2_cm.SetActive(false);
            area2Active = false;
            activeStack.Pop();
        }
    }*/
}
