using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

public class ARObjectSpawnTests
{





    //[UnityTest]
    //public IEnumerator SkattejagtSceneRunsAndTriggers()
    //{

    //    // først startes test systemerne, da disse er singletons, og afhængig af mobil indput

    //    // start gps med egne koordinater
    //    GPSSystem gps = GPSSystem.GetInstance();
    //    gps.SetGPSToTesting(11, 11, 11, 111111); // der testes kun for om den kan instatiere, ikke for funktion

    //    // start kompas i vilkårlig retning
    //    CompasSystem compas = CompasSystem.GetInstance();
    //    compas.SetCompasForTestMode(10); // antal grader bør være irrelevant så længe de er mellem 0 og 360;


    //    GameObject skattejagtScene = Object.Instantiate(Resources.Load<GameObject>("SkattejagtPrefab"));

    //    WorldSpawnController worldSpawnController = skattejagtScene.GetComponentInChildren<WorldSpawnController>();
    //    JuvelController juvelController = worldSpawnController.GetJuvelController();

    //    juvelController.setDebugSpawnTime = true;

    //    bool hasTriggerBeenOff = false;

    //    JuvelController.OnJuvelSpawn += () => hasTriggerBeenOff = true;

    //    yield return new WaitForSeconds(2);

    //    Assert.IsTrue(hasTriggerBeenOff);

    //    // JuvelProdukt juvelProdukt = GameObject.FindAnyObjectByType<JuvelProdukt>();
    //    // Assert.NotNull(juvelProdukt);
    //}


    [UnityTest]
    public IEnumerator SkatteJagtSceneTest()
    {

        float testLatEvent = 56.456432f;
        float testLongEvent = 9.407137f;  
        
        float testLatPlayerWrongPos = 11.111111f;
        float testLongPlayerWrongPos = 22.222222f;

        float testLatPlayerCorrectPos = 56.456427f;
        float testLongPlayerCorrectPos = 9.407137f;

        float compasWrongDirection = 90;
        float compasCorrectDirection = 180;

        float horizontalAccuracy = 10;
        double timestamp = 9999;

        // først startes test systemerne, da disse er singletons, og afhængig af mobil indput

        // start gps med egne koordinater
        GPSSystem gps = GPSSystem.GetInstance();
        gps.SetGPSToTesting(testLatPlayerWrongPos, testLongPlayerWrongPos, horizontalAccuracy, timestamp); // der testes kun for om den kan instatiere, ikke for funktion

        // start kompas i vilkårlig retning
        CompasSystem compas = CompasSystem.GetInstance();
        compas.SetCompasForTestMode(compasWrongDirection);


        SceneManager.LoadScene("SkattejagtScene");

        yield return null;

        GameObject skattejagtScene = SkattejagtInitializer.Instance.gameObject;

        WorldSpawnController wsc = skattejagtScene.GetComponentInChildren<WorldSpawnController>();
        JuvelController juvelController = wsc.GetJuvelController();
        juvelController.setDebugSpawnTime = true;
        juvelController.StartSpawnTimer();


        bool navSysTriggered = false;

        NavigationSystemData navData = new NavigationSystemData();

        NavigationSystem.OnNavigationSystemEvent += data => navData = data;
        NavigationSystem.OnNavigationSystemEvent += data => navSysTriggered = true;

        bool triggerJuvel = false;
        JuvelController.OnJuvelSpawn += () => triggerJuvel = true;

        yield return new WaitUntil(() => triggerJuvel);
        triggerJuvel = false;

        //Assert.AreEqual(navData.latitude, 22);
        Assert.IsNotNull(navData);


        //
        // test af juvel spawn
        //


        // sæt spawn hastigheden for juvler til debug mode


        // instantier test miljø
        GameObject simulatedScene = Object.Instantiate(Resources.Load<GameObject>("Simulation Environment"));

        // find simuleret camera
        GameObject simulatedCamera = GameObject.Find("SimulationCamera");

        // vent en frame for lige at give luft
        yield return null;

        // sæt udgangspunkt for simuleret camera
        simulatedCamera.transform.rotation = Quaternion.Euler(0, 0, 0); // Initial rotation

        float duration = 1f; // Duration in seconds
        float elapsedTime = 0f;

        // roter cameraet ned mod jorden for at finde et plan at spawne på
        while (elapsedTime < duration)
        {
            // AI genereret kode begynder Github Copilot*
            float angle = Mathf.Lerp(0, 70, elapsedTime / duration); // Interpolate angle
            simulatedCamera.transform.rotation = Quaternion.Euler(angle, 0, 0); // Apply rotation
            elapsedTime += Time.deltaTime; // Increment elapsed time

            yield return null; // Wait for the next frame
                               // AI generent kode slut
        }

        // vi sætter den endelige roation for en sikkerheds skyld
        simulatedCamera.transform.rotation = Quaternion.Euler(70, 0, 0); // Final rotation


        // vent i 2 sekunder for juvelen til at spawne
        yield return new WaitForSeconds(2);

        // check scenen om der er nogle juveler
        JuvelProdukt juvelProdukt = GameObject.FindAnyObjectByType<JuvelProdukt>();

        Assert.IsNotNull(juvelProdukt);


        // sæt juvel spawn hastighed til almindelig igen så scenne ikke oversvømmes 
        juvelController.setDebugSpawnTime = false;


        ///
        // Test af guld og score
        ////


        Assert.AreEqual(0, GoldManager.GetInstance().GetGoldAmount());

        GameObject juvelGoldTest = Object.Instantiate<GameObject>(Resources.Load<GameObject>("KronjuvelPrefab"));

        JuvelProdukt juvelProduktGold1 = juvelGoldTest.GetComponent<JuvelProdukt>();
        juvelProdukt.Initialize();
        juvelProdukt.OnPressed();

        int juvelAmountGoldIncrease = juvelProduktGold1.goldAmount;

        GoldManager goldManager = GoldManager.GetInstance();

        Assert.AreEqual(goldManager.GetGoldAmount(), juvelAmountGoldIncrease);

        yield return null;


        GameObject juvel2 = Object.Instantiate<GameObject>(Resources.Load<GameObject>("KronjuvelPrefab"));

        JuvelProdukt juvelProduktGold2 = juvel2.GetComponent<JuvelProdukt>();
        juvelProduktGold2.Initialize();
        juvelProduktGold2.OnPressed();


        Assert.AreEqual(goldManager.GetGoldAmount(), juvelAmountGoldIncrease * 2);



        //
        // test af skatteEvent
        //

        //override eventuelle locations med kendte locationer, en burde være nok
        Locations locations = skattejagtScene.GetComponent<LocationManager>().locations;
        locations.locations.Clear();
        locations.locations.Add(new Location("test1", testLatEvent, testLongEvent));


        // vent en frame for lige at give luft
        yield return null;
  
        // vent til navigationsystem trigger
        yield return new WaitUntil(() => navSysTriggered);
        navSysTriggered = false;

        // der burde ikke være spawnet noget da vi er langt fra location, og ikke kigger i dens retning
        SkatteEventProdukt SkatteEventProdukt = GameObject.FindAnyObjectByType<SkatteEventProdukt>();
        Assert.AreEqual(false, navData.isInRangeOfEvent);
        Assert.AreEqual(false, navData.isLookingTowardsEvent);
        Assert.IsNull(SkatteEventProdukt);

        // sæt location til at være tæt på
        gps.SetGPSToTesting(testLatPlayerCorrectPos, testLongPlayerCorrectPos, 100, 10000);

        // vent på at navigation system event triggers
        yield return new WaitUntil(() => navSysTriggered);
        Assert.AreEqual(true, navData.isInRangeOfEvent);
        Assert.AreEqual(false, navData.isLookingTowardsEvent);
        navSysTriggered = false;

        
        SkatteEventProdukt = GameObject.FindAnyObjectByType<SkatteEventProdukt>();
        // test bør være null da retningen der kigges ikke er korrekt
        Assert.IsNull(SkatteEventProdukt);



        // sæt retningen for kompasset til at pege mod eventet
        compas.SetCompasForTestMode(compasCorrectDirection);

        yield return new WaitUntil(() => navSysTriggered);
        Assert.AreEqual(true, navData.isInRangeOfEvent);
        Assert.AreEqual(true, navData.isLookingTowardsEvent);
        navSysTriggered = false;

        SkatteEventProdukt = GameObject.FindAnyObjectByType<SkatteEventProdukt>();
        Assert.IsNotNull(SkatteEventProdukt);

    }


   


}
