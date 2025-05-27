using System;
using UnityEngine;
using static NavigationSystem;
/*
 * returnerer spillerens nuværende retning, getPlayerDirectionInDegrees(), hvor 0 er nord og 360 grader med uret.
 * Skal kaldes udefra, nok i forbindelse med DirectionCheck klassen i forbindelse med instantiering af objekter.
 */




public class CompasSystem
{
    //test purpose
    private bool testMode;
    private float testHeading;


    private static CompasSystem Instance;

    private IGetHeading compass;

    private CompasSystem ()
    {
        compass = new CompassFromMagneticField();
    }

    public static CompasSystem GetInstance ()
    {
        if (Instance == null)
        {
            Instance = new CompasSystem();
        }
        return Instance;
    }

    public void SetCompasForTestMode (float heading)
    {
        testHeading = heading;
        testMode = true;

    }

    public float getPlayerDirectionInDegrees() //Returns the players direction with 0 degrees being north and then going clockwise.
    {
        if (testMode)
        {
            if (testHeading > 360 || testHeading  < 0)
            {
                testHeading = -1;
            }

            return testHeading;
        }


        if (compass != null && compass.HeadingActive()) return compass.GetHeading();


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