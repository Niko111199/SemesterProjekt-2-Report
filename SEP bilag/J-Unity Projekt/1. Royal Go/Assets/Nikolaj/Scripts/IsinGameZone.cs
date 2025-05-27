// dette script er tænkt til at finde det midterste punkt af alle locationers, i listenen af
// locationer, dette er tænkt til at kunne bruges til at danne et "spil område" hvor fra 
// det kan sammenlignes om man er iden for en givende radius

using System.Collections.Generic;
using UnityEngine;

public class LocationCenterFinder : MonoBehaviour
{
    public Locations locationsHolder;

    public Vector2 FindCenter()
    {
        if (locationsHolder.locations == null || locationsHolder.locations.Count == 0)
        {
            return Vector2.zero;
        }

        double totalLatitude = 0;
        double totalLongitude = 0;

        foreach (var loc in locationsHolder.locations)
        {
            totalLatitude += loc.latitude;
            totalLongitude += loc.longitude;
        }

        double centerLatitude = totalLatitude / locationsHolder.locations.Count;
        double centerLongitude = totalLongitude / locationsHolder.locations.Count;

        return new Vector2((float)centerLatitude, (float)centerLongitude);
    }
}


//valideret af: ikke valideret
//skrevet af: Nikolaj Bræmer