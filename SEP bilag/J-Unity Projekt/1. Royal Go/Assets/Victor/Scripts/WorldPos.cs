using UnityEngine;
/*
 * Simpelt script grundet single responsibility, har en funktion som returnerer spillerens nuværende position, getPlayerLatAndLon(). 
 * Skal kaldes udefra, nok i forbindelse med PositionCheck klassen i forbindelse med instantiering af objekter.
 */
public class WorldPos : MonoBehaviour
{
    public Vector2 getPlayerLatAndLon() //Returns the players position in lat and lon degrees.
    {
        if (Input.location.status == LocationServiceStatus.Running)
            return new Vector2(Input.location.lastData.latitude, Input.location.lastData.longitude);
        else
        {
            Debug.Log("Tried to get player position, but location service is not running!");
            return new Vector2(0,0); //Don't know what to return if service fails, any input would be nice here.
            //at den bare retunere (0,0) er fint, da hele systemet i sig selv ikke ville virke vis location ikke er accepeteret af brugeren
        }
    }
}

/*
  * Valideringsstatus: valideret af Nikolaj
  * Skrevet af: Victor
*/