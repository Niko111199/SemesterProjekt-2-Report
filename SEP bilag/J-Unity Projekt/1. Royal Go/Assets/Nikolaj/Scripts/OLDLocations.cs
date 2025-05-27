// dette script er til at holde styr på de forskellige locationer, som kan gåes til i spillet
// det er hensigten at man nemt kan tilføje flere til listen

using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class OldLocation
{
    public string name;
    public double latitude;
    public double longitude;
}
public class OldLocations //: MonoBehaviour, ikke nødvendigt for dette script
{
    public List<Location> locations = new List<Location>();
    public int currentTarget;
}

//valideret af: Victor og Peter
//skrevet af: Nikolaj Bræmer