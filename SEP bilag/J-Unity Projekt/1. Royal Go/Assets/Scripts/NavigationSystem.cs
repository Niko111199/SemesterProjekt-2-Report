
/*
 * GOD-klassen for indsamling og opbevaring af geolokations-data. Scriptet er ansvarligt for at initialisere alle scripts der indsamler data fra Input systemet, samt de som udregner på det.
 * Data gæmmes i et public Event system under strukturen "NavigationSystemData" som alle klasser kan abonnere til
 *  OBS. Dette script er ansvarligt for at sætte nye targets for spilleren, ved fuldændt minigame eller instantiering af kongekrone bør RemoveAndSetNewTarget() kaldes.
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using System.Threading.Tasks;


public class NavigationSystem //: //IHasTimer
{
    private float fieldOfView;
    private float spawnRange;
    private Locations locations;

    private GPSSystem gpsSystem;
    private CompasSystem compasSystem;

    private GPSData latestLocationData;
    private float compasDirection;

    private DirectionCheck directionCheck;
    private PositionCheck positionCheck;

    public Timer timer {get; set; }
    private int refreshRateForGPS = 300; // miliseconds

    public void SetUpNavigationsSystem (float fieldOfView, float spawnRange, Locations locationManager)
    {
        this.fieldOfView = fieldOfView;
        this.spawnRange = spawnRange;
        this.locations = locationManager;
        Init();
    }

    public delegate void NavigationSystemEvent(NavigationSystemData data);
    public static event NavigationSystemEvent OnNavigationSystemEvent;


    public static NavigationSystem Instance { get; private set; }


    private NavigationSystem ()
    {
        //Init();
    }

    //private void Awake()
    //{
    //    if (Instance != null && Instance != this)
    //    {
    //        Destroy(this);
    //    }
        
    //    Instance = this;
    //}

    //private void Start()
    //{
    //    Init();
    //}

    public bool RemoveAndSetNewTarget()
    {
        if (locations.currentTarget !< 0)
            RemoveLocation(locations.currentTarget);

        if (locations.locations.Count <= 0)
        {
            Debug.Log("Tried to set new target location but all locations have been visited...");
            return false;
        }
        else if (locations.locations.Count == 1)
        {
            locations.currentTarget = 0;
            return true;
        }

        latestLocationData = gpsSystem.GetGPSData();
        int targetindex = 0;


        for (int i = 1; i < locations.locations.Count; i++)
        {
            if (positionCheck.distanceBetweenPoints(new Vector2(latestLocationData.Latitude, latestLocationData.Longitude),
                                                    new Vector2(locations.locations[i].latitude, locations.locations[i].longitude)) <
                positionCheck.distanceBetweenPoints(new Vector2(latestLocationData.Latitude, latestLocationData.Longitude),
                                                    new Vector2(locations.locations[targetindex].latitude, locations.locations[targetindex].longitude)))
            {
                targetindex = i;
            }
        }

        locations.currentTarget = targetindex;
        return true;
    }

    private void RemoveLocation(int index)
    {
        locations.locations.RemoveAt(index);
    }

    public static NavigationSystem GetInstance() 
    { 
        if (Instance == null)
        {
            Instance = new NavigationSystem();
        }

        return Instance;

    }
    private void Init()
    {
        //RemoveAndSetNewTarget(); flyttet da den kører for tidligt, gpsSystem er ikke endnu instantieret

        gpsSystem = GPSSystem.GetInstance();
        //latestLocationData = gpsSystem.GetGPSData();

        compasSystem = CompasSystem.GetInstance();
        //compasDirection = compasSystem.getPlayerDirectionInDegrees();

        directionCheck = new DirectionCheck();
        positionCheck = new PositionCheck();

        // sætter up alt data i første run i første run
        //OnTimerTrigger();


        // start timer for refresh rate af gps system
        timer = new Timer(true);


        //_ = timer.StartTimer(refreshRateForGPS,  () => OnTimerTrigger());

        //StartCoroutine( RefreshTimer());
        _ = RefreshTimer();

        
        //RemoveAndSetNewTarget();
    }



    private async Task RefreshTimer ()
    {
        while (true)
        {

            await Task.Delay(300);
            OnTrigger();
        }

    }

    //// event der bliver trs
    //public Timer OnTimerTrigger() { return null; }


    public void TriggerForTest ()
    {
        OnTrigger();
    }
    private void OnTrigger () 
    {
        //Alt herunder er ansvarligt for opdatering af NavigationSystemData. 

        latestLocationData = gpsSystem.GetGPSData();

        compasDirection = compasSystem.getPlayerDirectionInDegrees();

        bool isWithinFieldOfView = directionCheck.isWithinFieldOfView(compasDirection, 
                                                                      new Vector2(latestLocationData.Latitude, latestLocationData.Longitude),
                                                                      fieldOfView, locations);

        bool isWithinRange = positionCheck.IsTargetInRange(
            spawnRange, 
            new Vector2(latestLocationData.Latitude,latestLocationData.Longitude),
            locations);

     

        float distanceToTarget = positionCheck.distanceBetweenPoints(
            // player pos
           new Vector2(
               latestLocationData.Latitude, 
               latestLocationData.Longitude),
           // target pos
           locations) ;



        double targetDirection = directionCheck.getTargetDirection(
                                                    new Vector2(
                                                        latestLocationData.Latitude,
                                                        latestLocationData.Longitude),
                                                    locations);



        //TODO throw exception
        if (latestLocationData != null)
        {
            //Overrider det nuværende datasæt med et nyt. 
            InvokeNavigationSystemEvent(new NavigationSystemData(

                latestLocationData.HorizontalAccuracy,
                latestLocationData.Latitude,
                latestLocationData.Longitude,
                latestLocationData.Timestamp,
                isWithinRange,
                isWithinFieldOfView,
                compasDirection,
                targetDirection,
                distanceToTarget
            ));
        }
        
    }



    private void InvokeNavigationSystemEvent(NavigationSystemData data)
    {
        OnNavigationSystemEvent?.Invoke(data);
    }

    public void TestSetTestLocations (Locations locations)
    {
        this.locations = locations;
    }

}

/*
 * Skrevet af: Peter og Victor
 * Valideringsstatus: Ikke valideret
 * TODO: Mangler validering (fra anden end Victor eller Peter).
 */