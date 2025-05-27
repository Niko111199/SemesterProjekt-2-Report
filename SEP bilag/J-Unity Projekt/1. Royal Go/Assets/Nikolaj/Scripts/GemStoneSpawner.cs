// dette script har hensigt at spawne Juvler rundt om spilleren efter en givende
//mængde tid, scriptet, bliver ved at prøve at finde et plain at sætte den på
// og når det er lykkes så så starter uret forfra med hvornår den næste skal
//skal komme

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using TMPro;

public class GemStoneSpawner : MonoBehaviour

{
    public static GemStoneSpawner Instance;

    //Lige lidt struktur da dette begynder at blive et små-stort script.
    [Header("Wait times")]
    public Vector2 waitTimeRange;
    public int retryTimer;

    [Header("Object references")]
    public GameObject objectToSpawn;
    public ARRaycastManager raycastManager;
    public Transform player;

    [Header("Debugging")]
    public TMP_Text debug;
    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        else
        {
            Destroy(this);
        }

        //StartCoroutine(SpawnRoutine()); 
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            debug.text = "spawn status: starter spawn routine";

            //Ændret for mindre hardcoding
            //int initialWaitTime = Mathf.RoundToInt(Random.Range(waitTimeRange.x, waitTimeRange.y));
            //int initialWaitTime = Random.Range(420, 720); // Vent 7-12 minutter før første spawn

            //yield return new WaitForSeconds(initialWaitTime);

            bool spawned = false;
            while (!spawned) 
            {
                    debug.text = "spawn status: prøver at spawne på plane nu";
                spawned = TrySpawnObject();
                if (!spawned)
                {
                    debug.text = "spawn status: Kunne ikke finde en plane";

                    //Ændret for mindre hardcoding
                    yield return new WaitForSeconds(retryTimer);
                    //yield return new WaitForSeconds(5f); 
                }
            }

            /* Jeg er ikke h elt 100 på enumerators men gør dette ikke at der vil være et dobbelt delay? Vi har jo allerede defineret en lignende waittime i starten af while loopet?
            int waitTime = Random.Range(420, 720);  // 7-12 minutter
            yield return new WaitForSeconds(waitTime);
            */
        }
    }

    bool TrySpawnObject()
    {
        Vector3 spawnPosition = player.position + Random.onUnitSphere * 1.5f;
        spawnPosition.y = player.position.y;

        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        if (raycastManager.Raycast(spawnPosition, hits, TrackableType.Planes))
        {
            Pose hitPose = hits[0].pose;
            Instantiate(objectToSpawn, hitPose.position, hitPose.rotation);
            debug.text = "spawn status: Objekt spawnet!";
            return true;
        }
        return false;
    }
}

//kode skrevet af: Nikolaj Bræmer
//valideret af: Victor
