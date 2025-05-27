// Controllere hvad der skal spawne i AR verden. 
// Det er her andre controllere startes og modtager beskeder fra: fx juvel og skatte event controlleren
// Denne klasse fungere meget lig med presenteren i et MVP mønster.


using UnityEngine;
using System;

public class WorldSpawnController : MonoBehaviour,  IKronJuvelListener, ISpawnKrone
{
    private static WorldSpawnController Instance;

    private NavigationSystem navSystem;
    
    private WorldElementSpawner worldSpawner;

    [SerializeField] JuvelFactory juvelFactory;
    private JuvelProdukt juvelPrefab;

    [SerializeField] SkatteEventFactory skatteEventFactory;


    JuvelController juvelController;
    KroneController kroneController;
    

    private void Awake()
    {
        juvelController = new JuvelController();
        kroneController = new KroneController(this);

        navSystem = NavigationSystem.Instance;
    }

    private void Start()
    {
        juvelController.StartSpawnTimer();
        SubscribeToJuvelEvent();
        worldSpawner = WorldElementSpawner.GetInstance();


    }

    private void OnDestroy()
    {
        UnSubscribeToJuvelEvent();
    }

    public JuvelController GetJuvelController ()
    {
        return juvelController;
    }

    public static WorldSpawnController GetInstance ()
    {
        if (Instance == null)
        {
            Instance = new WorldSpawnController();
        }

        return Instance;
    }

    public void SubscribeToJuvelEvent()
    {
        JuvelController.OnJuvelSpawn += OnJuvelEvent;
    }

    public void UnSubscribeToJuvelEvent()
    {
        JuvelController.OnJuvelSpawn -= OnJuvelEvent;
    }


    private bool juvelIsWaitingToSpawn = false; // too make sure there is only one spawn at the time
    public async void OnJuvelEvent()
    {

        if (juvelIsWaitingToSpawn)
        {

        }
        else
        {
            juvelIsWaitingToSpawn = true;
            await worldSpawner.SpawnObjectInARAsync(juvelFactory);
            juvelIsWaitingToSpawn = false;

        }

    }


    public void SpawnKrone()
    {
        _= worldSpawner.SpawnObjectInARAsync(skatteEventFactory);
    }


}

// skrevet af: Peter
// valideret af: Victor
