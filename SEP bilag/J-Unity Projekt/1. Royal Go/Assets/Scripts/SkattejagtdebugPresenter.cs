using UnityEngine;
using TMPro;


public class SkattejagtdebugPresenter : MonoBehaviour
{


    [SerializeField] TMP_Text compas; 
    [SerializeField] TMP_Text koordinates;

    private int signal;
    [SerializeField] TMP_Text signalCounter; 
    [SerializeField] TMP_Text distanceToTarget; 





    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        NavigationSystem.GetInstance();
        NavigationSystem.OnNavigationSystemEvent += UpdateInfo;
        signal = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void UpdateInfo (NavigationSystemData data)
    {
        signal++;
        compas.text = data.headingDirection.ToString();
        koordinates.text = "lat: " +data.latitude.ToString() + ". long: " + data.longitude.ToString();
        signalCounter.text = signal.ToString();
        distanceToTarget.text = "distande:  " + data.distanceToTarget.ToString();
    }
}
