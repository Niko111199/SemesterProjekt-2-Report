using UnityEngine;
using UnityEngine.InputSystem;

public class KingCrownStartGame : MonoBehaviour
{
   // private MiniGameManger games;

    private void Start()
    {
       // games = FindFirstObjectByType<MiniGameManger>();
    }

    private void Update()
    {
        //if (Touchscreen.current.primaryTouch.press.wasPressedThisFrame)
        //{
        //    Detectpress();
        //}
    }

    //private void Detectpress()
    //{
    //    Ray ray = Camera.main.ScreenPointToRay(Touchscreen.current.primaryTouch.position.ReadValue());
    //    RaycastHit hit;

    //    if (Physics.Raycast(ray, out hit, Mathf.Infinity))
    //    {
    //        if (hit.collider.CompareTag("KingCrown"))
    //        {
    //            games.startMingame();
    //            Destroy(gameObject);
    //        }
    //    }
    //}
}
