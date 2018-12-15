using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class MarkerManager : MonoBehaviour {

    public static MarkerManager Instance { get; set; }
    public RectTransform player_icon;
    public string areaName;

    GameObject ActivateThis;

    public List<Marker> markers;

    void Start()
    {
        if (Instance == null)
            Instance = this;
    }

    void Update()
    {
        //update player icon in real time
        if (player_icon.gameObject.activeInHierarchy == false)
            return;
        else
            playerPosOnMap(areaName);
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

    public void setMark5()
    {
        foreach (Marker mark in markers)
        {
            mark.gameObject.SetActive(false);
        }
        //try to set it by name
        markers[4].gameObject.SetActive(true);
    }
    public void setMark6()
    {
        foreach (Marker mark in markers)
        {
            mark.gameObject.SetActive(false);
        }
        //try to set it by name
        markers[5].gameObject.SetActive(true);
    }
    public void setMark7()
    {
        foreach (Marker mark in markers)
        {
            mark.gameObject.SetActive(false);
        }
        //try to set it by name
        markers[6].gameObject.SetActive(true);
    }
    public void setMark8()
    {
        foreach (Marker mark in markers)
        {
            mark.gameObject.SetActive(false);
        }
        //try to set it by name
        markers[7].gameObject.SetActive(true);
    }

    public void setMark9()
    {
        foreach (Marker mark in markers)
        {
            mark.gameObject.SetActive(false);
        }
        //try to set it by name
        markers[8].gameObject.SetActive(true);
    }

    public void setMark10()
    {
        foreach (Marker mark in markers)
        {
            mark.gameObject.SetActive(false);
        }
        //try to set it by name
        markers[9].gameObject.SetActive(true);
    }


    //gets the name of the player's current area  and then sets their icon accordingly (from Player.cs, from AreaPasser.cs)
    public void playerPosOnMap(string areaName)
    {
        if (areaName == "Player In Area1") //Area 1
        {
            player_icon.position = new Vector3(533, 444, 0);
            if(markers[0].isActiveAndEnabled == true)
            { 
                markers[0].gameObject.SetActive(false);
            }
        }

        if (areaName == "Player In Area2") //Area 2
        {
            player_icon.position = new Vector3(653, 444, 0);
            if(markers[1].isActiveAndEnabled == true)
            { 
                markers[1].gameObject.SetActive(false);
            }
        }

        if (areaName == "Player In Area3") //Area 3
        {
            player_icon.position = new Vector3(773, 444, 0);
            if(markers[2].isActiveAndEnabled == true)
            { 
                markers[2].gameObject.SetActive(false);
            }
        }

        if (areaName == "Player In Area4") //Area 4
        {
            player_icon.position = new Vector3(773, 324, 0);
            if (markers[3].isActiveAndEnabled == true)
            {
                markers[3].gameObject.SetActive(false);
            }
        }

        if (areaName == "Player In Area5") //Area 5
        {
            player_icon.position = new Vector3(773, 204, 0);
            if (markers[4].isActiveAndEnabled == true)
            {
                markers[4].gameObject.SetActive(false);
            }
        }

        if (areaName == "Player In Area6") //Area 6
        {
            player_icon.position = new Vector3(893, 264, 0);
            if (markers[5].isActiveAndEnabled == true)
            {
                markers[5].gameObject.SetActive(false);
            }
        }

        if (areaName == "Player In Area7") //Area 7
        {
            player_icon.position = new Vector3(893, 444, 0);
            if (markers[6].isActiveAndEnabled == true)
            {
                markers[6].gameObject.SetActive(false);
            }
        }

        if (areaName == "Player In Area8") //Area 8
        {
            player_icon.position = new Vector3(1013, 444, 0);
            if (markers[7].isActiveAndEnabled == true)
            {
                markers[7].gameObject.SetActive(false);
            }
        }

        if (areaName == "Player In Area9") //Area 9
        {
            player_icon.position = new Vector3(950, 564, 0);
            if (markers[8].isActiveAndEnabled == true)
            {
                markers[8].gameObject.SetActive(false);
            }
        }

        if (areaName == "Player In Area10") //Area 10
        {
            player_icon.position = new Vector3(773, 564, 0);
            if (markers[9].isActiveAndEnabled == true)
            {
                markers[9].gameObject.SetActive(false);
            }
        }
    }
}
