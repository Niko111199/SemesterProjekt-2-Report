// bruges til at time og trigger metoder i både monobehavour og almindelig.

using System;
using System.Threading.Tasks;
using UnityEngine;



public class Timer
{

    public bool loop;

    public delegate void timerTriggerEvent();

    int millisecondsRunning;
    timerTriggerEvent triggeredEvent;

    

    public Timer (bool loop)
    {
        this.loop = loop;
        millisecondsRunning = 1000; // 1 second
    }



    public async Task StartTimer(int millisecondsToWait, timerTriggerEvent eventToTrigger)
    {
        millisecondsRunning = millisecondsToWait;
        triggeredEvent = eventToTrigger;

        await RunTimer();

    }

    private async Task RunTimer ()
    {
        bool looping = true;
        while (looping == true)
        {

            // Wait for 3 seconds
            await Task.Delay(millisecondsRunning);
            triggeredEvent?.Invoke();

            looping = loop; // if loop is changed outside, then the while loop wil exit
            
        }

       
    }

    public void StopLoop ()
    {
        loop = false;
    }


}
// skrevet af: Peter
// valideret af: Victor