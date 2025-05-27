//dette script er lavet til at få brugeren, kan tage et billede direkte i appen, og så bilver billedet gemt, i brugerens
// kammerarulle, ydeligere så fjerne den UI som er på SKermen, således du får et billede rent billede uden UI

using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

#if UNITY_ANDROID
using UnityEngine.Android;
using UnityEngine.UI;
#endif

public class SelfieCamButton : MonoBehaviour
{
    public List<GameObject> buttonsToHide;

    public void takePhoto()
    {
        StartCoroutine(TakePhoto());
    }

    IEnumerator TakePhoto()
    {
        if(buttonsToHide != null)
        {
            foreach (GameObject buttonToHide in buttonsToHide)
            {
                if (buttonToHide != null)
                {
                    buttonToHide.GetComponent<Image>().color = new Color(buttonToHide.GetComponent<Image>().color.r, buttonToHide.GetComponent<Image>().color.g, buttonToHide.GetComponent<Image>().color.b, 0);
                }
            }
        }

        yield return new WaitForEndOfFrame();

        
        Texture2D photo = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        photo.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        photo.Apply();

        byte[] bytes = photo.EncodeToPNG();
        string fileName = "selfie_" + DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + ".png";

#if UNITY_ANDROID
        
        string directoryPath = "/storage/emulated/0/DCIM/Camera";
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        string filePath = Path.Combine(directoryPath, fileName);
        File.WriteAllBytes(filePath, bytes);

        
        using (AndroidJavaClass mediaScanner = new AndroidJavaClass("android.media.MediaScannerConnection"))
        using (AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        {
            AndroidJavaObject context = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            mediaScanner.CallStatic("scanFile", context, new string[] { filePath }, null, null);

            
            AndroidJavaClass toastClass = new AndroidJavaClass("android.widget.Toast");
            context.Call("runOnUiThread", new AndroidJavaRunnable(() =>
            {
                AndroidJavaObject toast = toastClass.CallStatic<AndroidJavaObject>("makeText", context, "Billede gemt i Galleri", toastClass.GetStatic<int>("LENGTH_SHORT"));
                toast.Call("show");
            }));
        }
#else
        string filePath = Path.Combine(Application.persistentDataPath, fileName);
        File.WriteAllBytes(filePath, bytes);
        Debug.Log("Gemte billede: " + filePath);
#endif

        if (buttonsToHide != null)
        {
            foreach (GameObject buttonToHide in buttonsToHide)
            {
                if (buttonToHide != null)
                {
                    buttonToHide.GetComponent<Image>().color = new Color(buttonToHide.GetComponent<Image>().color.r, buttonToHide.GetComponent<Image>().color.g, buttonToHide.GetComponent<Image>().color.b, 255);
                }
            }
        }

        Destroy(photo);
    }
}

//skrevet af Nikolaj Bræmer:
//valideret af: TODO: ikke valideret