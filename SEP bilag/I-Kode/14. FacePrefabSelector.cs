//dette script har hensigt at hente alle facefiltre fra listen af k�bte facefiltre, disse
//bliver s� igenem scriptet her givet til ARFaceManager, som s� placerer dem p� ansigtet

using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class FacePrefabSelector : MonoBehaviour
{
    public ARFaceManager arFaceManager;
    private List<GameObject> facePrefabslist;
    private GameObject currentFacePrefab;
    private ARFace currentFace;
    private int currentPrefabIndex = 0;

    void Start()
    {
        if (arFaceManager == null)
        {
            arFaceManager = FindFirstObjectByType<ARFaceManager>();
        }

        if (ARFaceList.Instance != null)
        {
            facePrefabslist = ARFaceList.Instance.GetFacePrefabs();
        }
        else
        {
            Debug.LogError("ARFaceList.Instance is null. Ensure ARFaceList is present in the scene.");
        }

        SelectFacePrefab(0);
    }

    void Update()
    {
        if (arFaceManager.subsystem != null && arFaceManager.trackables.count > 0)
        {
            foreach (var face in arFaceManager.trackables)
            {
                currentFace = face;
                break;
            }
        }
    }

    public void SelectFacePrefab(int index)
    {
        if (index < 0 || index >= facePrefabslist.Count) return;

        if (currentFacePrefab != null)
        {
            Destroy(currentFacePrefab);
        }

        currentPrefabIndex = index;
        currentFacePrefab = Instantiate(facePrefabslist[index]);

        if (currentFace != null)
        {
            currentFacePrefab.transform.position = currentFace.transform.position;
            currentFacePrefab.transform.rotation = currentFace.transform.rotation;
        }

        currentFacePrefab.transform.SetParent(currentFace.transform);
    }

    public void SwitchToNextPrefab()
    {
        int nextIndex = (currentPrefabIndex + 1) % facePrefabslist.Count;
        SelectFacePrefab(nextIndex);
    }

    public void SwitchTopreviousPrefab()
    {
        int nextIndex = (currentPrefabIndex - 1) % facePrefabslist.Count;
        SelectFacePrefab(nextIndex);
    }
}
//skrevet af Nikolaj Br�mer
//valideret af: TODO ikke valideret