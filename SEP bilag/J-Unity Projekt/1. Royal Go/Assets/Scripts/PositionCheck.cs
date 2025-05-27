using System;
using UnityEngine;

/*
 * Scriptet er ekslusivt udregning og udnytter derfor ikke monobehaviour, udefra kan withinRange() kaldes som vil output enten true eller false afhængig om de givne parametre er tætte nok.
 * playerPos og targetPos skal være henholdsvis latitude og longitude i grader, spillerens lat og long kan findes ved bruges af WorldPos klassen.
 * For formlerne for længden af en grad er følgende kilde brugt: https://www.movable-type.co.uk/scripts/latlong.html
 */
public class PositionCheck
{
    //public bool withinRange(Vector2 playerPos, Vector2 targetPos, float range)
    //{ 
    //    return distanceBetweenPoints(playerPos, targetPos) <= range;

    //}

    //ændring lavet til at inkludere indbygget rader til raddianer, for mere læslig kode


    // bruges externt der sker noget fuckery hvis vi bruger externe locations
     public float distanceBetweenPoints(Vector2 playerPos, Locations locations)
    {
        return distanceBetweenPoints(playerPos, getTargetPosition(locations));
    }


    public float distanceBetweenPoints(Vector2 playerPos, Vector2 targetPos)
    {
        float R = 6371000f;

        //float deltaLat = DegreesToRadians(targetPos.x - playerPos.x);
        // float deltaLon = DegreesToRadians(targetPos.y - playerPos.y);

        float deltaLat = (targetPos.x - playerPos.x) * Mathf.Deg2Rad;
        float deltaLon = (targetPos.y - playerPos.y) * Mathf.Deg2Rad;

        float a = Mathf.Sin(deltaLat / 2) * Mathf.Sin(deltaLat / 2) +
                  //Mathf.Cos(DegreesToRadians(playerPos.x)) * Mathf.Cos(DegreesToRadians(targetPos.x)) *
                  Mathf.Cos(playerPos.x * Mathf.Deg2Rad) * Mathf.Cos(targetPos.x * Mathf.Deg2Rad) *
                  Mathf.Sin(deltaLon / 2) * Mathf.Sin(deltaLon / 2);

        float c = 2 * Mathf.Atan2(Mathf.Sqrt(a), Mathf.Sqrt(1 - a));

        // Returns distance in meters
        return R * c;


        // changed to floats using copilot but it didn't change anything
        // Radius of earth in meters
        //double deg2Rad = Math.PI / 180.0;
        //double deltaLat = (targetPos.x - playerPos.x) * deg2Rad;
        //double deltaLon = (targetPos.y - playerPos.y) * deg2Rad;

        //double a = Math.Sin(deltaLat / 2) * Math.Sin(deltaLat / 2) +
        //           Math.Cos(playerPos.x * deg2Rad) * Math.Cos(targetPos.x * deg2Rad) *
        //           Math.Sin(deltaLon / 2) * Math.Sin(deltaLon / 2);

        //double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

        //return R * c;

    }

    /*public void SetTarget()
    {
        int targetIndex = -1;

        for (int i = 0; i < locationManager.locations.Count; i++)
        {
            if (targetIndex < 0)
                targetIndex = i;
            //Nuværende index af location listens afstand til spiller er mindre end valgte index.
            else if (distanceBetweenPoints(new Vector2(Input.location.lastData.latitude, Input.location.lastData.longitude),
                                           new Vector2(locationManager.locations[i].latitude, locationManager.locations[i].longitude)) <
                     distanceBetweenPoints(new Vector2(Input.location.lastData.latitude, Input.location.lastData.longitude),
                                           new Vector2(locationManager.locations[targetIndex].latitude, locationManager.locations[targetIndex].longitude)))
            {
                targetIndex = i;
            }
        }

        if (locationManager.currentTarget != targetIndex)
            locationManager.currentTarget = targetIndex;
    }*/

    private Vector2 getTargetPosition(Locations location)
    {
        return new Vector2(location.locations[location.currentTarget].latitude,
                           location.locations[location.currentTarget].longitude);
    }

    public bool IsTargetInRange(float range, Vector2 playerPos, Locations locations)
    {
        return distanceBetweenPoints(playerPos, getTargetPosition(locations)) <= range;
    }

    /*private float DegreesToRadians(float degrees)
    {
        return degrees * Mathf.PI / 180.0f;
    }

    private float RadiansToDegrees(float radians)
    {
        return radians * 180.0f / Mathf.PI;
    }
    */

}
/*
  * Valideringsstatus: Valdideret af Nikolaj Bræmer
  * Skrevet af: Victor
*/
