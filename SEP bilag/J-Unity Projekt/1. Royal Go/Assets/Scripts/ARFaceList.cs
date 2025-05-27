//dette er et script med hensigt at havde styr p� alle facefilteres som brugeren har k�bt, det bliver s� brugt
//af et andet script til at placere p� brugeren

using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class ARFaceList : MonoBehaviour
{
    public static ARFaceList Instance;

    [SerializeField] private List<GameObject> allAvailablePrefabs;
    [SerializeField] private List<GameObject> facePrefabs = new List<GameObject>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadFacePrefabs();
    }

    public List<GameObject> GetFacePrefabs()
    {
        return facePrefabs;
    }

    public void AddFacePrefab(GameObject prefab)
    {
        if (facePrefabs != null && prefab != null && !facePrefabs.Contains(prefab))
        {
            facePrefabs.Add(prefab);
            SaveFacePrefabs();
        }
    }

    public void RemoveFacePrefab(GameObject prefab)
    {
        if (facePrefabs != null && prefab != null)
        {
            facePrefabs.Remove(prefab);
            SaveFacePrefabs();
        }
    }

    private void SaveFacePrefabs()
    {
        List<string> prefabNames = facePrefabs.Select(p => p.name).ToList();
        string json = JsonUtility.ToJson(new StringListWrapper(prefabNames));
        PlayerPrefs.SetString("FacePrefabs", json);
        PlayerPrefs.Save();
    }

    private void LoadFacePrefabs()
    {
        if (PlayerPrefs.HasKey("FacePrefabs"))
        {
            string json = PlayerPrefs.GetString("FacePrefabs");
            StringListWrapper wrapper = JsonUtility.FromJson<StringListWrapper>(json);

            facePrefabs.Clear();
            foreach (string name in wrapper.items)
            {
                GameObject prefab = allAvailablePrefabs.Find(p => p.name == name);
                if (prefab != null)
                {
                    facePrefabs.Add(prefab);
                }
            }
        }
    }

    [System.Serializable]
    private class StringListWrapper
    {
        public List<string> items;

        public StringListWrapper(List<string> items)
        {
            this.items = items;
        }
    }
}


//Skrevet af Nikolaj Br�mer
//valideret af: TODO ikke valideret