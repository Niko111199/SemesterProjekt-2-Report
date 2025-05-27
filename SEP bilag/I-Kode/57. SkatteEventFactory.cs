// factory for SkatteEvents. her sættes skatteEventproduktet på skatteEvent prefabbet
// og prefabet instantieres i scenen. denne benytter en object pool (på 1) som den tænker og slukker alt efter behov

using UnityEngine;


public class SkatteEventFactory : WorldSpawnFactory
{
    [SerializeField] GameObject SkatteEventprefab;

    GameObject kroneTheOneAndOnly;
    SkatteEventProdukt skatteKorneProdukt;

    private void Start()
    {
        kroneTheOneAndOnly = Instantiate(SkatteEventprefab);
        kroneTheOneAndOnly.GetComponent<ICanSpawnInARWorld>().Initialize(); // her findes og opsættes produktet

        skatteKorneProdukt = kroneTheOneAndOnly.GetComponent<SkatteEventProdukt>(); // her hentes det specifikke produkt

        kroneTheOneAndOnly.SetActive(false);
    }

    public override GameObject GetProduct(Vector3 location, Quaternion rotation)
    {
        kroneTheOneAndOnly.transform.position = location;
        kroneTheOneAndOnly.transform.rotation = rotation;

        kroneTheOneAndOnly.SetActive(true);

        //NavigationSystem.GetInstance().RemoveAndSetNewTarget();

        return kroneTheOneAndOnly;
    }
}
// skrevet af: Peter
// valideret af: Victor