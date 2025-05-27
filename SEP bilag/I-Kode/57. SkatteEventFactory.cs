// factory for SkatteEvents. her s�ttes skatteEventproduktet p� skatteEvent prefabbet
// og prefabet instantieres i scenen. denne benytter en object pool (p� 1) som den t�nker og slukker alt efter behov

using UnityEngine;


public class SkatteEventFactory : WorldSpawnFactory
{
    [SerializeField] GameObject SkatteEventprefab;

    GameObject kroneTheOneAndOnly;
    SkatteEventProdukt skatteKorneProdukt;

    private void Start()
    {
        kroneTheOneAndOnly = Instantiate(SkatteEventprefab);
        kroneTheOneAndOnly.GetComponent<ICanSpawnInARWorld>().Initialize(); // her findes og ops�ttes produktet

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