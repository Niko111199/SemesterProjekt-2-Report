//dette script har til opgave at se om kronjuvlere bliver trykket på
//såfremt at det gør det, så bliver det tilføjet guld til spilleren
//og opbjeket forsvinde igne

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class JuvelOnPress : MonoBehaviour
{
    public string targetTag = "Interactable";
    public int goldValue; //For nemere balancering down the line.
    private Gold _gold; // added gold instance

    private void Update()
    {
        // TODO update for input instead of touchscreen
        if (Touchscreen.current.primaryTouch.press.wasPressedThisFrame)
        {
            PerformRaycast();
        }
    }

    private void Start()
    {
        _gold = Gold.GetInstance();
        //if (gold.Instance == null)
        if (_gold == null)
        {
            Debug.Log("Prøvede at spawne juvel, men guld singleton kører ikke, sletter juvel...");
            Destroy(gameObject);
        }
    }

    void PerformRaycast()
    {
        Ray ray = Camera.main.ScreenPointToRay(Touchscreen.current.primaryTouch.position.ReadValue());
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if (hit.collider.CompareTag(targetTag))
            {
                //gold.Instance.AddGold(goldValue);
                _gold.AddGold(goldValue);
                Destroy(gameObject);

                //Lidt ærgeligt at skulle køre en fail-safe if statement i et update loop hvor det kan tjekkes i en start funktion.
                /*if (gold.Instance != null) 
                {
                        gold.Instance.AddGold(10);
                }*/
            }
        }
    }
}

//valideret af: Victor
//skrevet af: Nikolaj Bræmer
