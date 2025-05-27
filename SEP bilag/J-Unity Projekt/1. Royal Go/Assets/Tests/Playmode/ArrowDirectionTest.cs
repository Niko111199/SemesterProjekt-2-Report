using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class ArrowDirectionTest
{
    [UnityTest]
    public IEnumerator testPlayerDirection()
    {
        CompasSystem compas = CompasSystem.GetInstance();

        GameObject skattejagtScene = Object.Instantiate(Resources.Load<GameObject>("SkattejagtPrefab"));

        RectTransform playerArrowTransform = GameObject.Find("DirectionArrow").GetComponent<RectTransform>();

        Assert.IsNotNull(playerArrowTransform);

        bool triggered = false;

        NavigationSystemData navData = new NavigationSystemData();
        NavigationSystem.OnNavigationSystemEvent += data => navData = data;

        NavigationSystem.OnNavigationSystemEvent += data => triggered = true;

        int count = 0;

        while (count < 20)
        {
            float r = Random.Range(0.0f, 359.0f);
            compas.SetCompasForTestMode(r);
            count++;

            yield return new WaitUntil(() => triggered);
            triggered = false;

            yield return new WaitForEndOfFrame();
            Assert.AreEqual(r, playerArrowTransform.rotation.eulerAngles.z, 3);
        }

        //Edge cases
        //Min
        compas.SetCompasForTestMode(0);

        yield return new WaitUntil(() => triggered);
        triggered = false;

        yield return new WaitForEndOfFrame();

        Assert.AreEqual(0.0f, playerArrowTransform.rotation.eulerAngles.z, 3);
        //Max
        compas.SetCompasForTestMode(359);

        yield return new WaitUntil(() => triggered);
        triggered = false;

        yield return new WaitForEndOfFrame();

        Assert.AreEqual(359.0f, playerArrowTransform.rotation.eulerAngles.z, 3);

        yield return null;
    }

    [UnityTest]
    public IEnumerator testTargetDirection()
    {
        GPSSystem gps = GPSSystem.GetInstance();
        gps.SetGPSToTesting(33, 33, 33, 333333);

        GameObject skattejagtScene = Object.Instantiate(Resources.Load<GameObject>("SkattejagtPrefab"));

        Locations locations = skattejagtScene.GetComponent<LocationManager>().locations;
        List<Location> newLocations = new List<Location>();
        newLocations.Add(new Location("testTarget", 33, 33));
        locations.locations = newLocations;

        RectTransform targetArrowTransform = GameObject.Find("PointNext").GetComponent<RectTransform>();

        Assert.IsNotNull(targetArrowTransform);

        bool triggered = false;

        NavigationSystemData navData = new NavigationSystemData();
        NavigationSystem.OnNavigationSystemEvent += data => navData = data;

        NavigationSystem.OnNavigationSystemEvent += data => triggered = true;

        locations.locations[0].latitude = 32;
        yield return new WaitUntil(() => triggered);
        triggered = false;

        //Tjek syd
        Assert.AreEqual(180, Mathf.RoundToInt(targetArrowTransform.rotation.eulerAngles.z));
        locations.locations[0].latitude = 33;
        locations.locations[0].longitude = 34;

        yield return new WaitUntil(() => triggered);
        triggered = false;
        //Tjek øst
        Assert.AreEqual(90, Mathf.RoundToInt(targetArrowTransform.rotation.eulerAngles.z));
        locations.locations[0].latitude = 33;
        locations.locations[0].longitude = 32;

        yield return new WaitUntil(() => triggered);
        triggered = false;
        //Tjek vest
        Assert.AreEqual(270, Mathf.RoundToInt(targetArrowTransform.rotation.eulerAngles.z));
        locations.locations[0].latitude = 34;
        locations.locations[0].longitude = 33;

        yield return new WaitUntil(() => triggered);
        triggered = false;
        //Tjek nord
        Assert.AreEqual(0, Mathf.RoundToInt(targetArrowTransform.rotation.eulerAngles.z));





        yield return null;
    }
}
