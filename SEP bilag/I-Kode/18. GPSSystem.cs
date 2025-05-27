using UnityEngine;
/*
 * Simpelt script grundet single responsibility, har en funktion som returnerer spillerens nuværende position, getPlayerLatAndLon(). 
 * Skal kaldes udefra, i forbindelse med NavigationsSystem klassen i forbindelse med instantiering af objekter.
 */
public class GPSSystem
    
{
    private static GPSSystem Instance;

    // for testing purpose
    private bool testMode;
    private GPSData GPSTestData;

    private GPSSystem ()
    {
    }

    public static GPSSystem GetInstance()
    {
        if (Instance == null)
        {
            Instance = new GPSSystem();
        }
        return Instance;
    }

    public void SetGPSToTesting (float latitude, float longitude, float horizontalAccuracy, double timestamp)
    {
        testMode = true;
        GPSTestData = new GPSData(latitude, longitude, horizontalAccuracy, timestamp);
        
    }

    public GPSData GetGPSData() //Returns the players GPSData
    {
        if (testMode)
        {
            return GPSTestData;
        }

        if (Input.location.status == LocationServiceStatus.Running)
            return new GPSData(Input.location.lastData.latitude, Input.location.lastData.longitude, Input.location.lastData.horizontalAccuracy, Input.location.lastData.timestamp);
        else
        {
            Debug.Log("Tried to get player position, but location service is not running! returnung 1111");
            return new GPSData(56.303241f, 9.340671f, 1, 1);
        }
    }
}


public class GPSData
{
    public float Longitude { get; private set; }
    public float Latitude { get; private set; }
    public float HorizontalAccuracy { get; private set; }
    public double Timestamp { get; private set; }

    public GPSData(float latitude, float longitude, float horizontalAccuracy, double timestamp)
    {
        Longitude = longitude;
        Latitude = latitude;
        HorizontalAccuracy = horizontalAccuracy;
        Timestamp = timestamp;
    }
}


/*
  * Valideringsstatus: valideret af Nikolaj
  * Skrevet af: Victor
*/