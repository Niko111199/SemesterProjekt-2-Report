// detetere om noget er blevet trykket på i ar scenen

using UnityEngine;
using UnityEngine.InputSystem;

public class ARPressDetector : MonoBehaviour
{
    private string targetTag;

    private IPressHandler handler;

    public void SetUpDetector (IPressHandler pressHandler, string targetTag)
    {
        handler = pressHandler;
        this.targetTag = targetTag;
    }

    private void Update()
    {
        // TODO update for input instead of touchscreen
        if (Touchscreen.current.primaryTouch.press.wasPressedThisFrame)
        {
            DetectPress();
        }
    }
    private void DetectPress()
    {

        Ray ray = Camera.main.ScreenPointToRay(Touchscreen.current.primaryTouch.position.ReadValue());
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if (hit.collider.CompareTag(targetTag))
            {
                handler.OnPressed();
                //gold.Instance.AddGold(goldValue);
                //_gold.AddGold(goldValue);
                //Destroy(gameObject);

                //Lidt ærgeligt at skulle køre en fail-safe if statement i et update loop hvor det kan tjekkes i en start funktion.
                /*if (gold.Instance != null) 
                {
                        gold.Instance.AddGold(10);
                }*/

            }
        }
    }
}

// skrevet af: Nikolaj og Peter
// valideret af: Victor