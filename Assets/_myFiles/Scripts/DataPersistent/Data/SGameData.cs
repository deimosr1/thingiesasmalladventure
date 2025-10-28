using UnityEngine;
using System.Collections.Generic;
using System.Collections;

[System.Serializable]

public class SGameData
{
    public List<SItemProfile> ItemData = new List<SItemProfile>();
    public List<SItemProfile> ItemPickedData = new List<SItemProfile>();
    public List<SItemProfile> ToolData = new List<SItemProfile>();
    public ECurrentLevel LastLevel;
    public int PeopleHelped;


    public SGameData()
    {
        LastLevel = ECurrentLevel.Town;
        ItemData = new List<SItemProfile>();
        ToolData = new List<SItemProfile>();
        ItemPickedData = new List<SItemProfile>();
        PeopleHelped = 0;
    }
}
