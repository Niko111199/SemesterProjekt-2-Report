//dette script går ind og læser telefonens nuværnde lat/long lokation


using UnityEngine;
using TMPro;
using UnityEngine.Android;
//using Unity.Mathematics; - Bliver den her package brugt?

public class GetCurrentPos : MonoBehaviour
{
    [SerializeField] private TMP_Text displayLatitude;
    [SerializeField] private TMP_Text displayLongitude;
    [SerializeField] private TMP_Text test;

    void Update()
    {
        float currentlatitude = Input.location.lastData.latitude;
        float currenLongitude = Input.location.lastData.longitude;

        displayLatitude.text = "Latitude " + currentlatitude.ToString();
        displayLongitude.text = "Longitude " + currenLongitude.ToString();

        test.text ="Is Home?: " + ishome(currentlatitude, currenLongitude).ToString();
    }

    public bool ishome(float currentlatitude, float currentlongititude)
    {
         float targetLatitude = 56.16444f; 
         float targetLongitude = 8.918716f; 
         float tolerance = 50.0f; //(50 meter)

        float distance = Haversine(currentlatitude, currentlongititude, targetLatitude, targetLongitude);

        if (distance <= tolerance)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private float Haversine(float lat1, float lon1, float lat2, float lon2)
    {
        float R = 6371f; // Radius of the Earth in kilometers
        float dLat = (lat2 - lat1) * Mathf.Deg2Rad;  
        float dLon = (lon2 - lon1) * Mathf.Deg2Rad;  

        float a = Mathf.Sin(dLat / 2) * Mathf.Sin(dLat / 2) +
                  Mathf.Cos(lat1 * Mathf.Deg2Rad) * Mathf.Cos(lat2 * Mathf.Deg2Rad) *
                  Mathf.Sin(dLon / 2) * Mathf.Sin(dLon / 2);
        float c = 2 * Mathf.Atan2(Mathf.Sqrt(a), Mathf.Sqrt(1 - a));

        return R * c * 1000f; // Returnerer afstand i meter
    }

}
//Koden er fin, men vi skal vist stadig lige have 100% styr på arkitekturen af hele unity projektet, da jeg føler vi bevæger os ud af SOLID her. 


//Kode skrevet af Nikolaj Bræmer
//Valideret af: Victor