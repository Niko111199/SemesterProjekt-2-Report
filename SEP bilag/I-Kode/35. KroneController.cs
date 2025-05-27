// styrer hvornår kroner skal spawne, og hvordan deres opførsel skal være

using System;
using UnityEngine;

public class KroneController : IDisposable, ISubscribeToNavigationData
{
    private ISpawnKrone spawnMethod;
    public KroneController (ISpawnKrone spawnMethod)
    {
        this.spawnMethod = spawnMethod;
        SubscribeToNavigationData();
    }

    public void Dispose()
    {
        UnsubscribeToNavigationData();
    }
    public void SubscribeToNavigationData()
    {
        NavigationSystem.OnNavigationSystemEvent += OnNavigationDataUpdate;
    }

    public void UnsubscribeToNavigationData()
    {
        NavigationSystem.OnNavigationSystemEvent -= OnNavigationDataUpdate;
    }
    public void OnNavigationDataUpdate(NavigationSystemData navData)
    {
        /*if (IsReadyToSpawn(navData.isInRangeOfEvent, navData.isLookingTowardsEvent))
        {
            Spawn();
        }*/

        if (navData.isInRangeOfEvent && navData.isLookingTowardsEvent)
        { 
            NavigationSystem.GetInstance().RemoveAndSetNewTarget(); // sæt næste mål før spawn når og lave for mange på denne plads
            Spawn();
        }
    }

    /* Virker lidt overkompliceret at lave en metode til dette tjek.
    private bool IsReadyToSpawn (bool inRange, bool lookingAtDirection)
    {
        return inRange && lookingAtDirection ? true : false;
    }*/


    private void Spawn ()
    {
        spawnMethod.SpawnKrone();
    }
    


}
// skrevet af: Peter
// valideret af: Victor