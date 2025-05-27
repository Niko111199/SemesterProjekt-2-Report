using UnityEngine;
using UnityEngine.UI;

public class DebugCrownTestControllerForDebug : MonoBehaviour
{
    [SerializeField] SkatteEventFactory factory;




    public void SpawnKrone ()
    {
        factory.GetProduct(new Vector3(-1, 0, 0), new Quaternion());
    }



}
