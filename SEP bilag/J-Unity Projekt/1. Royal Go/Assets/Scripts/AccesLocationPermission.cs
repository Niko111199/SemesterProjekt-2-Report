//dette script går ind og kontrollere om brugere har tilsluttet at kunne bruge GPS på deres telefon
//ydeligere for at gantere lovligt brug af GPS, gør scriptet også at appen spørger om tilladelse
// vis begge disse ting er godkendt af brugeren, så tænderer vi for Lokations tracking på Telefonen


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


        // Tester om spilleren før har givet tilladelse til locations data
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

//Kode skrevet af Nikolaj Bræmer
//Valideret af: TODO ikke valideret endnu

