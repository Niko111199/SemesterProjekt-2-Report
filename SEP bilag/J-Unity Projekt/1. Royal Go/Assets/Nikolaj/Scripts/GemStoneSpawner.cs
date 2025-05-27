// dette script har hensigt at spawne Juvler rundt om spilleren efter en givende
//m�ngde tid, scriptet, bliver ved at pr�ve at finde et plain at s�tte den p�
// og n�r det er lykkes s� s� starter uret forfra med hvorn�r den n�ste skal
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

    //Lige lidt struktur da dette begynder at blive et sm�-stort script.
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

            //�ndret for mindre hardcoding
            //int initialWaitTime = Mathf.RoundToInt(Random.Range(waitTimeRange.x, waitTimeRange.y));
            //int initialWaitTime = Random.Range(420, 720); // Vent 7-12 minutter f�r f�rste spawn

            //yield return new WaitForSeconds(initialWaitTime);

            bool spawned = false;
            while (!spawned) 
            {
                    debug.text = "spawn status: pr�ver at spawne p� plane nu";
                spawned = TrySpawnObject();
                if (!spawned)
                {
                    debug.text = "spawn status: Kunne ikke finde en plane";

                    //�ndret for mindre hardcoding
                    yield return new WaitForSeconds(retryTimer);
                    //yield return new WaitForSeconds(5f); 
                }
            }

            /* Jeg er ikke h elt 100 p� enumerators men g�r dette ikke at der vil v�re et dobbelt delay? Vi har jo allerede defineret en lignende waittime i starten af while loopet?
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

//kode skrevet af: Nikolaj Br�mer
//valideret af: Victor
