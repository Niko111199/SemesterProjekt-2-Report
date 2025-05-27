//dette script g�r ind og kontrollere om brugere har tilsluttet at kunne bruge GPS p� deres telefon
//ydeligere for at gantere lovligt brug af GPS, g�r scriptet ogs� at appen sp�rger om tilladelse
// vis begge disse ting er godkendt af brugeren, s� t�nderer vi for Lokations tracking p� Telefonen


using System.Collections;
using UnityEngine;
using UnityEngine.Android;

public class AccesLocationPermision : MonoBehaviour
{
    public IEnumerator Start()
    {


#if UNITY_ANDROID

            if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
            {
                Permission.RequestUserPermission(Permission.FineLocation);
                yield return new WaitForSeconds(1f);

                
                if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
                {
                    Application.Quit();
                    yield break;
                }
            }
#endif


        // Tester om spilleren f�r har givet tilladelse til locations data
        if (!Input.location.isEnabledByUser)
        {
            Application.Quit();
            yield break;
        }

        Input.location.Start();
    }
    void OnApplicationQuit()
    {
        Input.location.Stop();
    }
}

//Kode skrevet af Nikolaj Br�mer
//Valideret af: TODO ikke valideret endnu

