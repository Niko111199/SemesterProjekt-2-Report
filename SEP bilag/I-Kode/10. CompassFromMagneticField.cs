//TODO mangler validering


// kompas system baseret på magnetfelt, og denne guide: https://digilent.com/blog/how-to-convert-magnetometer-data-into-compass-heading/

using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements.Experimental;

public class CompassFromMagneticField: IGetHeading
{


    public CompassFromMagneticField()
    {
        if (MagneticFieldSensor.current == null)
        {
            //("MagneticFieldSensor is not available on this device.");
        } 

        else
        {
            InputSystem.EnableDevice(MagneticFieldSensor.current);
        }

        
    }



    public float GetHeading()
    {
       
        return (float) Heading(MagneticFieldSensor.current.magneticField.ReadValue());

    }

    public bool HeadingActive()
    {
        if (MagneticFieldSensor.current == null || !MagneticFieldSensor.current.enabled)
        {
            return false;
        }

        else if (MagneticFieldSensor.current != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private double Heading(Vector3 magneticField)
    {
        if (magneticField.x == 0)
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

        double headingRaw = 
            Math.Atan2(
                MictoTeslaToGauss(magneticField.x * -1),
                MictoTeslaToGauss(magneticField.y)
                ) * (180 / Math.PI);

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



}


//Skrevet af: Peter
//Valideret af: 