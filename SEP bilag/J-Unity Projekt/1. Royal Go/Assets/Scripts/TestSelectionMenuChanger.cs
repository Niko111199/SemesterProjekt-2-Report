// skifter mellem debug scener

using System.Collections.Generic;
using UnityEngine;

public class TestSelectionMenuChanger : MonoBehaviour
{

    [SerializeField] GameObject CanvasMenu;
    [SerializeField] GameObject GPSTtest;
    [SerializeField] GameObject Skattejagt;
    [SerializeField] GameObject testKrone;


    private List<GameObject> ObjectsInScene;


    private void Start()
    {
        ObjectsInScene = new List<GameObject>();

        ObjectsInScene.Add(GPSTtest);
        ObjectsInScene.Add(Skattejagt);
        ObjectsInScene.Add(CanvasMenu);
        ObjectsInScene.Add(testKrone);

        print("trykket");
    }

    private void TurnOffAll ()
    {
        foreach (GameObject obj in ObjectsInScene)
        {
            obj.SetActive  (false);
        }
    }


    public void StartMenu()
    {
        TurnOffAll();
        CanvasMenu.SetActive(true);
    }



    public void ShowGPSTest ()
    {
        TurnOffAll();
        GPSTtest.SetActive(true);
    }

    public void ShowSkattejagt()
    {
        TurnOffAll();

        Skattejagt.SetActive(true);

        WorldSpawnController spwnController = Skattejagt.GetComponentInChildren<WorldSpawnController>();
        JuvelController juvelController = spwnController.GetJuvelController();
        juvelController.setDebugSpawnTime = true;

    }

    public void ShowTestKrone()
    {
        TurnOffAll();

        Skattejagt.SetActive(true);

        WorldSpawnController spwnController = Skattejagt.GetComponentInChildren<WorldSpawnController>();
        JuvelController juvelController = spwnController.GetJuvelController();
        juvelController.setDebugSpawnTime = true;

    
        testKrone.SetActive(true);
    }

}

// skrevet af: Peter
// Valideret af: Victor