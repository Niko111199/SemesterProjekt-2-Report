// håndrete hvornår juveler skal spawn, og giver et signal videre når det er tid

using UnityEngine;
using System;

public class JuvelController :  ISubscribeToNavigationData, IDisposable, IHasTimer
{
    public Timer timer { get; set; }

    public Vector2 spawnInterval;
    public bool setDebugSpawnTime = false;

    private bool timerIsRepeating = true;



    public delegate void JuvelSpawnDelegate();
    public static event JuvelSpawnDelegate OnJuvelSpawn;

    public JuvelController ()
    {
        timer = new Timer(false); // ikke gentag da tiden er forskellig hver runde
        

        SubscribeToNavigationData();

        int toMinutes = 60000;

        spawnInterval.x = 10 * toMinutes;
        spawnInterval.y = 12 * toMinutes;


    }



    public void StartSpawnTimer ()
    {

        // if debug mode is activated
        // spawn time is between 0.5 sec and 1 secund
        if (setDebugSpawnTime)
        {
            spawnInterval.x = 500;
            spawnInterval.y = 1000;
        }

        timerIsRepeating = true;
        int initialWaitTime = Mathf.RoundToInt(UnityEngine.Random.Range(spawnInterval.x, spawnInterval.y));

        // ikke sikker på om dette er lavet korrekt
        //TODO hør Søren om dette
        _ = timer.StartTimer(initialWaitTime, () => OnTimerTrigger());
    }

    private void RepeatTimer ()
    {
        if (timerIsRepeating)
        {
            int initialWaitTime = Mathf.RoundToInt(UnityEngine.Random.Range(spawnInterval.x, spawnInterval.y));

            // ikke sikker på om dette er lavet korrekt
            //TODO hør Søren om dette
            _ = timer.StartTimer(initialWaitTime, () => OnTimerTrigger());
        }
    }

    public void StopSpawnTimer ()
    {
        timerIsRepeating = false;
    }

    public void Dispose ()
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

    // TODO impelement spawn logic here om man er indenfor område eller udenfor
    public void OnNavigationDataUpdate(NavigationSystemData navData)
    {
        // TODO throw new System.NotImplementedException();
    }


    public void EmitJuvelEvent ()
    {
        OnJuvelSpawn?.Invoke();
    }


    // tror den her har rimelig dårlig kode stil? den kalder sig selv inde i sig!!
    //TODO hør søren om dette
    public Timer OnTimerTrigger()
    {
        EmitJuvelEvent();
        int initialWaitTime = Mathf.RoundToInt(UnityEngine.Random.Range(spawnInterval.x, spawnInterval.y));

        RepeatTimer();
        return null;
    }

}




// skrevet af: Peter
// valideret af: Victor


