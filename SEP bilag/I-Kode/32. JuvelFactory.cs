// Factory der instantiere juveler i AR scenen
// juvelen loades som en prefab, og skal have "JuvelProdukt" scriptet på.
// kan fungere med alle objeckter der har interfaced "ICanSpawnInARWorld"

using UnityEngine;

public class JuvelFactory : WorldSpawnFactory
{

    [SerializeField] GameObject juvelPrefab;

    public override GameObject GetProduct(Vector3 location, Quaternion rotation)
    {
        GameObject juvelGOB = Instantiate(juvelPrefab, location, rotation);
        juvelGOB.GetComponent<ICanSpawnInARWorld>().Initialize(); // starter JuvelProduktet op.

        return juvelGOB;
    }


}

// skrevet af: Peter
// valideret af: Victor
