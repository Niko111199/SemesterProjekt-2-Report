using UnityEngine;
/*
 * returnerer spillerens nuværende retning, getPlayerDirectionInDegrees(), hvor 0 er nord og 360 grader med uret.
 * Skal kaldes udefra, nok i forbindelse med DirectionCheck klassen i forbindelse med instantiering af objekter.
 */
public class Compas : MonoBehaviour
{
    public float getPlayerDirectionInDegrees() //Returns the players direction with 0 degrees being north and then going clockwise.
    {
        if (Input.location.status == LocationServiceStatus.Running)
            return Input.compass.magneticHeading;
        else
        {
            Debug.Log("Tried to get player direction in degrees, but location service is not running!");
            //return 0; //Don't know what to return if service fails, any input would be nice here.
            return -1; // vi får den til at retunere noget der ikke kan være en compas retning, da vis vu retunere 0
                       // ville det kunne aflæses som at vende mod nord, så for nem fejlsøgning retunere vi -1
        }
    }
}

/*
  * Valideringsstatus: valideret af Nikolaj Bræmer
  * Skrevet af: Victor
*/