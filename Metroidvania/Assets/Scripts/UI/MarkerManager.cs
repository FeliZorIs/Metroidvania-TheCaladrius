using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerManager : MonoBehaviour {

    public static MarkerManager Instance { get; set; }
    public RectTransform player_icon;
    public string areaName;

    public List<Marker> markers;

    void Start()
    {
        if (Instance == null)
            Instance = this;
    }

    void Update()
    {        //update player icon in real time
        if (player_icon.gameObject.activeInHierarchy == false)
            return;
        else
            playerPosOnMap(areaName);
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
        //try to set it by name
        markers[1].gameObject.SetActive(true);
    }

    public void setMark3()
    {
        foreach (Marker mark in markers)
        {
            mark.gameObject.SetActive(false);
        }
        //try to set it by name
        markers[2].gameObject.SetActive(true);
    }

    public void setMark4()
    {
        foreach (Marker mark in markers)
        {
            mark.gameObject.SetActive(false);
        }
        //try to set it by name
        markers[3].gameObject.SetActive(true);
    }

    //gets the name of the player's current area  and then sets their icon accordingly (from Player.cs, from AreaPasser.cs)
    public void playerPosOnMap(string areaName)
    {
        if (areaName == "Player In Area1") //Area 1
        {
            player_icon.position = new Vector3(623, 384, 0);
            if(markers[0].isActiveAndEnabled == true)
            { 
                markers[0].gameObject.SetActive(false);
            }
        }

        if (areaName == "Player In Area2") //Area 2
        {
            player_icon.position = new Vector3(743, 384, 0);
            if(markers[1].isActiveAndEnabled == true)
            { 
                markers[1].gameObject.SetActive(false);
            }
        }

        if (areaName == "Player In Area3") //Area 3
        {
            player_icon.position = new Vector3(863, 384, 0);
            if(markers[2].isActiveAndEnabled == true)
            { 
                markers[2].gameObject.SetActive(false);
            }
        }

        if (areaName == "Player In Area4") //Area 4
        {
            player_icon.position = new Vector3(743, 504, 0);
            if (markers[3].isActiveAndEnabled == true)
            {
                markers[3].gameObject.SetActive(false);
            }
        }
    }
}
