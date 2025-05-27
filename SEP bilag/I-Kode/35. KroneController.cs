// styrer hvorn�r kroner skal spawne, og hvordan deres opf�rsel skal v�re

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
            NavigationSystem.GetInstance().RemoveAndSetNewTarget(); // s�t n�ste m�l f�r spawn n�r og lave for mange p� denne plads
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