// dette script er til at holde styr p� de forskellige locationer, som kan g�es til i spillet
// det er hensigten at man nemt kan tilf�je flere til listen

using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class OldLocation
{
    public string name;
    public double latitude;
    public double longitude;
}
public class OldLocations //: MonoBehaviour, ikke n�dvendigt for dette script
{
    public List<Location> locations = new List<Location>();
    public int currentTarget;
}

//valideret af: Victor og Peter
//skrevet af: Nikolaj Br�mer