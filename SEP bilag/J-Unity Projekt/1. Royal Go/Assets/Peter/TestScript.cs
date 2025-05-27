using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using System.Collections;
using System;

public class TestScript : MonoBehaviour
{
    [SerializeField] TMP_Text compas;
    [SerializeField] TMP_Text heading;

    public float tolerance = 1;


    private void Start()
    {
        if (MagneticFieldSensor.current != null)
        {
            InputSystem.EnableDevice(MagneticFieldSensor.current);
            InputSystem.EnableDevice(AttitudeSensor.current);
        }
       
       

        StartCoroutine(Readings());
    }


    
    private IEnumerator Readings ()
    {

        while (true)
        {
            if (MagneticFieldSensor.current == null)
            {
                compas.text = "magnetic is null";
            }

            else if (!MagneticFieldSensor.current.enabled)
            {
                compas.text = "magnetic is not enabled";
            }

            else if (MagneticFieldSensor.current != null)
            {
                Vector3 magneticField = MagneticFieldSensor.current.magneticField.ReadValue();
                compas.text = $"Magnetic Field: X={magneticField.x:F2}, Y={magneticField.y:F2}, Z={magneticField.z:F2}";

                heading.text = Heading(magneticField).ToString();


            }
            else
            {
                compas.text = "Magnetic Sensor not available.";
            }
            yield return new WaitForSeconds(0.25f);
        }
    }





    private double Heading(Vector3 magneticField)
    {
        if (magneticField.x == 0 )
        {
            if (magneticField.y < 0)
            {
                return 90;
            }
            else
            {
                return 0;
            }
        }

        double headingRaw = Math.Atan2(MictoTeslaToGauss(magneticField.x), MictoTeslaToGauss(magneticField.y)) * (180 / Math.PI);

        if (headingRaw < 0)
        {
            return headingRaw + 360;
        } 
        else if (headingRaw > 360)
        {
            return headingRaw - 360;
        }
        return headingRaw;

    }


    private float MictoTeslaToGauss(float microTesla)
    {
        return microTesla / 100;
    }

    private bool IsPointingNorth (Vector3 magneticField)
    {
        // x er nord / syd aksen
        // y er øst / vest aksen 
        // z er op / ned aksen


        float nordSydAkse = magneticField.x;
        float eastWest = magneticField.y;
        float UpDown = magneticField.z;


        // hvis x er 0, og y og z er mindre end 0 (eller bare negative tal) pejer tilefonen mod nord
        return (AxisIsWithinTolerance(nordSydAkse) && eastWest < 0 && UpDown < 0); // telfon pegner mod nord


    }

    private bool AxisIsWithinTolerance (float amount)
    {
        return amount < 0 + tolerance && amount > 0 - tolerance;
    }






    private void SetAttitudeToNorth ()
    {
        Quaternion currentOrientation = AttitudeSensor.current.attitude.ReadValue();

        Quaternion northOrientation = this.gameObject.transform.rotation;


    


    }


}

