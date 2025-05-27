using UnityEngine;

public class ClearPlayerPrefs : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }


}
