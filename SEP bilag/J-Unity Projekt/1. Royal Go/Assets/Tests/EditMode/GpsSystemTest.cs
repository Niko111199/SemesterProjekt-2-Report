using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;



public class GpsSystemTest
{

    // Test forbindelse til compas
    [Test]
    public void CompasDirectionIsNotNull()
    {
        CompasSystem compasSys = CompasSystem.GetInstance();

        Assert.IsNotNull(compasSys.getPlayerDirectionInDegrees());
    }

    [Test]
    public void CompasShowsFailsWithNoAndroid ()
    {
        CompasSystem compasSys = CompasSystem.GetInstance();

        Assert.AreEqual(compasSys.getPlayerDirectionInDegrees(), -1);
    }

    [Test]
    public void CompasSetsTestmode()
    {
        CompasSystem compasSys = CompasSystem.GetInstance();

        compasSys.SetCompasForTestMode(0);
        Assert.AreEqual(compasSys.getPlayerDirectionInDegrees(), 0);        
        
        compasSys.SetCompasForTestMode(300);
        Assert.AreEqual(compasSys.getPlayerDirectionInDegrees(), 300);        
    }

    [Test]
    public void CompasSetsTestmodeOverflow()
    {
        CompasSystem compasSys = CompasSystem.GetInstance();

        compasSys.SetCompasForTestMode(361);
        Assert.AreEqual(compasSys.getPlayerDirectionInDegrees(), -1);
    }





    [Test]
    public void NavigationSystemCalculationsLookingAtAndInRange ()
    {

        float testLatEvent = 56.456432f;
        float testLongEvent = 9.407137f;

        float testLatPlayerWrongPos = 11.111111f;
        float testLongPlayerWrongPos = 22.222222f;

        float testLatPlayerCorrectPos = 56.456425f;
        float testLongPlayerCorrectPos = 9.407137f;

        float compasWrongDirection = 90;
        float compasCorrectDirection = 180;

        float horizontalAccuracy = 10;
        double timestamp = 9999;

        float fieldOfView = 20;
        float spawnRange = 10;



        Locations locations = new Locations();
        locations.locations.Clear();
        locations.locations.Add(new Location("test1", testLatEvent, testLongEvent));
        locations.currentTarget = 0;

        CompasSystem compasSys = CompasSystem.GetInstance();
        compasSys.SetCompasForTestMode(compasCorrectDirection);

        GPSSystem gpsSys = GPSSystem.GetInstance();
        gpsSys.SetGPSToTesting(testLatPlayerCorrectPos, testLongPlayerCorrectPos, horizontalAccuracy, timestamp);

        NavigationSystemData navData = new NavigationSystemData();

        NavigationSystem navSys = NavigationSystem.GetInstance();
        navSys.SetUpNavigationsSystem(fieldOfView, spawnRange, locations);
        NavigationSystem.OnNavigationSystemEvent += data => navData = data;

        navSys.TriggerForTest();

        Assert.AreEqual(compasCorrectDirection, navData.headingDirection);
        Assert.AreEqual(testLatPlayerCorrectPos, navData.latitude);
        Assert.AreEqual(testLongPlayerCorrectPos, navData.longitude);
        Assert.AreEqual(horizontalAccuracy, navData.horizontalAccuracy);
        Assert.AreEqual(timestamp, navData.timestamp);
        Assert.AreEqual(true, navData.isLookingTowardsEvent);
        Assert.AreEqual(true, navData.isInRangeOfEvent);
    }


    [Test]
    public void NavigationSystemCalculationsLookingAtButOutOfRange()
    {

        float testLatEvent = 56.456432f;
        float testLongEvent = 9.407137f;

        float testLatPlayerWrongPos = 56.0f;
        float testLongPlayerWrongPos = 9.407137f;

        float testLatPlayerCorrectPos = 56.456425f;
        float testLongPlayerCorrectPos = 9.407137f;

        float compasWrongDirection = 90;
        float compasCorrectDirection = 180;

        float horizontalAccuracy = 10;
        double timestamp = 9999;

        float fieldOfView = 20;
        float spawnRange = 10;



        Locations locations = new Locations();
        locations.locations.Clear();
        locations.locations.Add(new Location("test1", testLatEvent, testLongEvent));
        locations.currentTarget = 0;

        CompasSystem compasSys = CompasSystem.GetInstance();
        compasSys.SetCompasForTestMode(compasCorrectDirection);

        GPSSystem gpsSys = GPSSystem.GetInstance();
        gpsSys.SetGPSToTesting(testLatPlayerWrongPos, testLongPlayerWrongPos, horizontalAccuracy, timestamp);

        NavigationSystemData navData = new NavigationSystemData();

        NavigationSystem navSys = NavigationSystem.GetInstance();
        navSys.SetUpNavigationsSystem(fieldOfView, spawnRange, locations);
        NavigationSystem.OnNavigationSystemEvent += data => navData = data;

        navSys.TriggerForTest();

        Assert.AreEqual(compasCorrectDirection, navData.headingDirection);
        Assert.AreEqual(horizontalAccuracy, navData.horizontalAccuracy);
        Assert.AreEqual(timestamp, navData.timestamp);
        Assert.AreEqual(true, navData.isLookingTowardsEvent);
        Assert.AreEqual(false, navData.isInRangeOfEvent);
    }


    [Test]
    public void NavigationSystemCalculationsNotLookingAtButInRange()
    {

        float testLatEvent = 56.456432f;
        float testLongEvent = 9.407137f;

        float testLatPlayerWrongPos = 56.0f;
        float testLongPlayerWrongPos = 9.407137f;

        float testLatPlayerCorrectPos = 56.456425f;
        float testLongPlayerCorrectPos = 9.407137f;

        float compasWrongDirection = 90;
        float compasCorrectDirection = 180;

        float horizontalAccuracy = 10;
        double timestamp = 9999;

        float fieldOfView = 20;
        float spawnRange = 10;



        Locations locations = new Locations();
        locations.locations.Add(new Location("test1", testLatEvent, testLongEvent));
        locations.currentTarget = 0;

        CompasSystem compasSys = CompasSystem.GetInstance();
        compasSys.SetCompasForTestMode(compasWrongDirection);

        GPSSystem gpsSys = GPSSystem.GetInstance();
        gpsSys.SetGPSToTesting(testLatPlayerCorrectPos, testLongPlayerCorrectPos, horizontalAccuracy, timestamp);

        NavigationSystemData navData = new NavigationSystemData();

        NavigationSystem navSys = NavigationSystem.GetInstance();
        navSys.SetUpNavigationsSystem(fieldOfView, spawnRange, locations);
        NavigationSystem.OnNavigationSystemEvent += data => navData = data;

        navSys.TriggerForTest();

        Assert.AreEqual(compasWrongDirection, navData.headingDirection);
        Assert.AreEqual(horizontalAccuracy, navData.horizontalAccuracy);
        Assert.AreEqual(timestamp, navData.timestamp);
        Assert.AreEqual(false, navData.isLookingTowardsEvent);
        Assert.AreEqual(true, navData.isInRangeOfEvent);
    }



    [Test]
    public void NavigationSystemCalculationsNotLookingAtAndOutOfRange()
    {

        float testLatEvent = 56.456432f;
        float testLongEvent = 9.407137f;

        float testLatPlayerWrongPos = 56.0f;
        float testLongPlayerWrongPos = 9.407137f;

        float testLatPlayerCorrectPos = 56.456425f;
        float testLongPlayerCorrectPos = 9.407137f;

        float compasWrongDirection = 90;
        float compasCorrectDirection = 180;

        float horizontalAccuracy = 10;
        double timestamp = 9999;

        float fieldOfView = 20;
        float spawnRange = 10;



        Locations locations = new Locations();
        locations.locations.Add(new Location("test1", testLatEvent, testLongEvent));
        locations.currentTarget = 0;

        CompasSystem compasSys = CompasSystem.GetInstance();
        compasSys.SetCompasForTestMode(compasWrongDirection);

        GPSSystem gpsSys = GPSSystem.GetInstance();
        gpsSys.SetGPSToTesting(testLatPlayerWrongPos, testLongPlayerWrongPos, horizontalAccuracy, timestamp);

        NavigationSystemData navData = new NavigationSystemData();

        NavigationSystem navSys = NavigationSystem.GetInstance();
        navSys.SetUpNavigationsSystem(fieldOfView, spawnRange, locations);
        NavigationSystem.OnNavigationSystemEvent += data => navData = data;

        navSys.TriggerForTest();

        Assert.AreEqual(horizontalAccuracy, navData.horizontalAccuracy);
        Assert.AreEqual(timestamp, navData.timestamp);
        Assert.AreEqual(false, navData.isLookingTowardsEvent);
        Assert.AreEqual(false, navData.isInRangeOfEvent);
    }

    [Test]
    public void JuvelControllerIsNotUsingDebugSpawnTime()
    {

        JuvelController jc = new JuvelController();

        // check if min spawn interval is higher than 9 minutes
        Assert.Greater(jc.spawnInterval.x, 9 * 60000);
    }






}

