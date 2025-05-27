//dette script går ind og kontrollere om brugere har tilsluttet at kunne bruge GPS på deres telefon
//ydeligere for at gantere lovligt brug af GPS, gør scriptet også at appen spørger om tilladelse
// vis begge disse ting er godkendt af brugeren, så tænderer vi for Lokations tracking på Telefonen


using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Android;

public class AccesLocationPermision : MonoBehaviour
{

    [SerializeField] private TMP_Text status;
 public IEnumerator Start()
 {


#if UNITY_ANDROID
        status.text = "Status: tjekker om brugere har givet adgang til GPS";

            if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
            {
                status.text = "Status: anmoder om tilladelse til GPS";
                Permission.RequestUserPermission(Permission.FineLocation);
                yield return new WaitForSeconds(1f);

                
                if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
                {
                     status.text = "Status: Tilladelse til GPS blev nægtet.";
                     yield break;
                }
            }
#endif

//Lidt ærgeligt at vi ikke har noget fejlbesked for apple brugere..Jeg kan se der er et device tag der hedder "UNITY_IOS" for apple enheder men er ikke sikker på hvordan vi kan output en fejlbesked.


        if (!Input.location.isEnabledByUser)
            {
                status.text = "Status: Brugeren har ikke aktiveret GPS.";
                yield break;
            }

            status.text = "Status: GPS er klar og køre";
            Input.location.Start();

        void OnApplicationQuit()
        {
            Input.location.Stop();
        }
    }
}

//Kode skrevet af Nikolaj Bræmer
//Valideret af: Victor

