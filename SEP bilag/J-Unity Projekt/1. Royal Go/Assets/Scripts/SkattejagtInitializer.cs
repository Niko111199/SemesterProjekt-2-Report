using UnityEngine;

public class SkattejagtInitializer : MonoBehaviour
{
    [SerializeField] LocationManager locationsManager;



    private Locations locations;

    public static SkattejagtInitializer Instance { get; private set; } // bør primært bruges til at kalde scenen i test cases

    private NavigationSystem navSys;
    private void Awake()
    {

        locations = locationsManager.locations;

        navSys = NavigationSystem.GetInstance();
        navSys.SetUpNavigationsSystem(20,5, locations);
        Instance = this;
    }
     

}
