// abstract klasse for factories der skal bruges til at spawne elementer i AR world.
// functionen GetProduct er drivkraften for instantieringen.

using UnityEngine;

public abstract class WorldSpawnFactory: MonoBehaviour
{

    public abstract GameObject GetProduct(Vector3 location, Quaternion rotation);



}

// skrevet af: Peter
// valideret af: TODO valider