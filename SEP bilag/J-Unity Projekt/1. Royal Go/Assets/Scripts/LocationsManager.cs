// dette script er til at holde styr på de forskellige locationer, som kan gåes til i spillet
// det er hensigten at man nemt kan tilføje flere til listen

//TODO skriv lat/long ind for dem der er i listen
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Location
{
    public string name;
    public float latitude;
    public float longitude;

    public Location(string name, float latitude, float longitude) // added constructor
    {
        this.name = name;
        this.latitude = latitude;
        this.longitude = longitude;
    }
}



[System.Serializable]
public class Locations
{
    public List<Location> locations = new List<Location>();
    public int currentTarget = -1;
}

// ikke optimalt, men det eneste mono
public class LocationsWithoutMonoBehaviourForTests
{
    public List<Location> locations = new List<Location>();
    public int currentTarget = -1;
}



//valideret af: Victor og Peter
//skrevet af: Nikolaj Bræmer