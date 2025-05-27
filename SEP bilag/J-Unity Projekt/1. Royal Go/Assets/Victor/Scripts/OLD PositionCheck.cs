using NUnit.Framework;
using UnityEngine;

/*
 * Scriptet er ekslusivt udregning og udnytter derfor ikke monobehaviour, udefra kan withinRange() kaldes som vil output enten true eller false afhængig om de givne parametre er tætte nok.
 * playerPos og targetPos skal være henholdsvis latitude og longitude i grader, spillerens lat og long kan findes ved bruges af WorldPos klassen.
 * For formlerne for længden af en grad er følgende kilde brugt: https://www.movable-type.co.uk/scripts/latlong.html
 */
public class OLDPositionCheck
{
    [SerializeField] private Locations locationManager;

    private bool withinRange(Vector2 playerPos, Vector2 targetPos, float range)
    { 
        return distanceBetweenPoints(playerPos, targetPos) <= range;

    }

    //ændring lavet til at inkludere indbygget rader til raddianer, for mere læslig kode
    private float distanceBetweenPoints(Vector2 playerPos, Vector2 targetPos)
    {
        // Radius of earth in meters
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
    }

    public void SetTarget()
    {
        int targetIndex = -1;

        for (int i = 0; i < locationManager.locations.Count;  i++)
        {
            if (targetIndex < 0)
                targetIndex = i;
            //else if (distanceBetweenPoints())
        }
    }

    public bool IsTargetInRange()
    {
        return true;
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
