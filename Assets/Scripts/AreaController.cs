using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaController : MonoBehaviour
{
    public GameObject havanAreaPlane;
    public GameObject havanAreaObject; // The object you want to activate/deactivate
    public GameObject shoeAreaPlane;
    public GameObject shoeAreaObject;
    public float activationTime = 5f; // Time in seconds to activate/deactivate the object

    private bool isActivated = false;

    // Method to toggle the activation state of the area object
    public void ActiveHavanAreaObject()
    {
        if (!isActivated)
        {
            havanAreaPlane.SetActive(false);
            havanAreaObject.SetActive(true);
        }
    }

    public void ActiveShoeAreaObject()
    {
        if (!isActivated)
        {
            shoeAreaPlane.SetActive(false);
            shoeAreaObject.SetActive(true);
        }
    }
}
