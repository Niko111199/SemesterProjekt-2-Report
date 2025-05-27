// Dette script er til at placere objeckter i AR scenen.
// når funktionen SpawnObjectInARAsync er kaldt vil en coroutine forsøge at finde et plan at spawne objektet på indtil det lykkes

using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
using UnityEngine.XR.ARFoundation; 
using UnityEngine.XR.ARSubsystems;
using TMPro;
using System.Threading.Tasks;




public class WorldElementSpawner : MonoBehaviour

{
    private static WorldElementSpawner Instance;

    //Lige lidt struktur da dette begynder at blive et små-stort script.
    //[Header("Wait times")]
    // public Vector2 waitTimeRange;
    public int retryTimer;

    [Header("Object references")]
    public ARRaycastManager raycastManager;
    public Transform player;

    //[Header("Debugging")]
    //public TMP_Text debugText;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public static WorldElementSpawner GetInstance ()
    {
      
        if (Instance == null)
        {
            GameObject gobj = new GameObject();
            gobj.name = "worldElementSpawner";
            gobj.AddComponent<WorldElementSpawner>();
            Instantiate(gobj);

        }

        return Instance;
    }

 

    // påbegynder spawning routinen og retunere true når den er udført
    public async Task<bool> SpawnObjectInARAsync (WorldSpawnFactory factory)
    {
        
        TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool> ();
        
        print("co routine started2");
        StartCoroutine (SpawnRoutine(factory, tcs));
        await tcs.Task;
        //debugText.text = "spawning afsluttet";
        return true;
    }


    // spawn routine that checks if there is a plane to spawn on, and when it finds one, it spawns the object and finish the task.
    private IEnumerator SpawnRoutine(WorldSpawnFactory factory, TaskCompletionSource<bool> isFinished)
    {
        print("co routine started");

        Vector3 spawnPosition = player.position + Random.onUnitSphere * 1.5f;
        spawnPosition.y = player.position.y;

        bool spawned = false;

        // as long as the item has not spawned this coroutine will contrinue to run
        while (!spawned)
        {
            // check if there is a plane to spawn on.
            List<ARRaycastHit> hits = new List<ARRaycastHit>();
            if (raycastManager.Raycast(spawnPosition, hits, TrackableType.Planes))
            {
         
                Pose hitPose = hits[0].pose;

                //Instantiate(this.objectToSpawn, hitPose.position, hitPose.rotation);
                GameObject product = factory.GetProduct(hitPose.position, hitPose.rotation);


                print("spawned and should exit loop");
                spawned = true;
            }

            // if there is no plane to spawn on, wait for 1 second and try again.
            else
            {
                //debugText.text = "no plane found";
                
            }
            yield return new WaitForSeconds(1);
        }

        isFinished.SetResult(true);



        // sat så spawn routine starter sin egen, 
        // havde det været sat som en variable kunne den risikere at bliver overskrevet ved et senere kald




        //while (true)
        //{
        //    debug.text = "spawn status: starter spawn routine";

        //    /*
        //    //deleted stuff below:
        //    //Ændret for mindre hardcoding
        //    //int initialWaitTime = Mathf.RoundToInt(Random.Range(waitTimeRange.x, waitTimeRange.y));
        //    //int initialWaitTime = Random.Range(420, 720); // Vent 7-12 minutter før første spawn

        //    //yield return new WaitForSeconds(initialWaitTime);

        //    */


        //    bool spawned = false;
        //    while (!spawned) 
        //    {
        //            debug.text = "spawn status: prøver at spawne på plane nu";
        //        spawned = TrySpawnObject(objToSpawn);
        //        if (!spawned)
        //        {
        //            debug.text = "spawn status: Kunne ikke finde en plane";

        //            //Ændret for mindre hardcoding
        //            yield return new WaitForSeconds(retryTimer);
        //            //yield return new WaitForSeconds(5f); 
        //        }
        //    }

        //    /* Jeg er ikke h elt 100 på enumerators men gør dette ikke at der vil være et dobbelt delay? Vi har jo allerede defineret en lignende waittime i starten af while loopet?
        //    int waitTime = Random.Range(420, 720);  // 7-12 minutter
        //    yield return new WaitForSeconds(waitTime);
        //    */
        //}
    }



    //bool TrySpawnObject(GameObject objToSpawn)
    //{
    //    Vector3 spawnPosition = player.position + Random.onUnitSphere * 1.5f;
    //    spawnPosition.y = player.position.y;

    //    List<ARRaycastHit> hits = new List<ARRaycastHit>();
    //    if (raycastManager.Raycast(spawnPosition, hits, TrackableType.Planes))
    //    {
    //        Pose hitPose = hits[0].pose;
    //        Instantiate(objectToSpawn, hitPose.position, hitPose.rotation);
    //        debug.text = "spawn status: Objekt spawnet!";
    //        return true;
    //    }
    //    return false;
    //}
}

//kode skrevet af: Nikolaj Bræmer
//valideret af: Victor
