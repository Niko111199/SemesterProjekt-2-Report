using System;
using UnityEngine;
/*
 * Scriptet er ekslusivt udregning og udnytter derfor ikke monobehaviour, udefra kan isWithinLookRange() kaldes som vil output enten true eller false afhængig om de givne parametre viser om spilleren kigger tæt nok i den rigtige retning af objektet.
 * playerPos og targetPos skal være henholdsvis latitude og longitude i grader, spillerens lat og long kan findes ved bruges af WorldPos klassen, playerDirection kan fås fra Compas klassen og fieldOfView definerer i grader synsfeltet af søgningen.
 * For formlerne for bearing er følgende kilde brugt: https://www.movable-type.co.uk/scripts/latlong.html
 */
public class DirectionCheck
{
    //ændring lavet til at inkludere indbygget rader til raddianer, for mere læslig kode
 
    public bool isWithinFieldOfView(float playerDirection, Vector2 playerPos, float fieldOfView, Locations locations)
    {
        double bearing = CalculateBearing(playerPos, locations);

        return bearing <= Mathf.Clamp(playerDirection + (fieldOfView / 2), 0, 360) 
            // || 
            &&
               bearing >= Mathf.Clamp(playerDirection - (fieldOfView / 2), 0, 360);

    } 

    public double getTargetDirection(Vector2 playerPos, Locations locations)
    {
        return CalculateBearing(playerPos, locations);
    }


    private Vector2 getTargetPosition(Locations location)
    {
        return new Vector2(location.locations[location.currentTarget].latitude,
                           location.locations[location.currentTarget].longitude);
    }

    private double CalculateBearing(Vector2 playerPos, Locations locations)
    {

        ////Converts lat and lon to radians for calculation:
        ////playerPos = new Vector2(DegreesToRadians(playerPos.x),DegreesToRadians(playerPos.y));
        ////targetPos = new Vector2(DegreesToRadians(targetPos.x),DegreesToRadians(targetPos.y));

        //Vector2 target = getTargetPosition(locations);

        //playerPos = new Vector2(playerPos.x * Mathf.Deg2Rad, playerPos.y * Mathf.Deg2Rad);
        //Vector2 targetPos = new Vector2(target.x * Mathf.Deg2Rad, target.y * Mathf.Deg2Rad);

        //float y = Mathf.Sin(targetPos.y - playerPos.y) * Mathf.Cos(targetPos.x);
        //float x = Mathf.Cos(playerPos.x) * Mathf.Sin(targetPos.x) -
        //                   Mathf.Sin(playerPos.x) * Mathf.Cos(playerPos.y) * Mathf.Cos(targetPos.y - playerPos.y);
        //float teta = Mathf.Atan2(y, x);

        ////Convert to degrees on a compas:
        //float d = (teta * Mathf.Rad2Deg + 360) % 360;

        //return d;
    

        Vector2 target = getTargetPosition(locations);

        double playerLat = playerPos.x * Math.PI / 180.0; // Convert to radians
        double playerLon = playerPos.y * Math.PI / 180.0;
        double targetLat = target.x * Math.PI / 180.0;
        double targetLon = target.y * Math.PI / 180.0;

        double y = Math.Sin(targetLon - playerLon) * Math.Cos(targetLat);
        double x = Math.Cos(playerLat) * Math.Sin(targetLat) -
                   Math.Sin(playerLat) * Math.Cos(targetLat) * Math.Cos(targetLon - playerLon);
        double theta = Math.Atan2(y, x);

        // Convert to degrees on a compass:
        double bearing = (theta * 180.0 / Math.PI + 360) % 360;

        return bearing;
    }


   /* private float DegreesToRadians(float degrees)
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
  * Valideringsstatus: Valideret af Nikolaj Bræmer
  * Skrevet af: Victor
*/