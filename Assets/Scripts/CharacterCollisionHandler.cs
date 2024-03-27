using UnityEngine;

public class CharacterCollisionHandler : MonoBehaviour
{
    public AreaController areaController;
    private float timeInsideArea = 0f;
    private bool insideArea = false;


    private void Start()
    {
        //areaController = FindObjectOfType<AreaController>();
    }

    private void Update()
    {
        if (insideArea)
        {
            timeInsideArea += Time.deltaTime;
            if (timeInsideArea >= areaController.activationTime)
            {
                
                areaController.ActiveHavanAreaObject();
                timeInsideArea = 0f;
            }
        }
        else
        {
            timeInsideArea = 0f;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("HavanArea"))
        {
            insideArea = true;
        }
        else if (other.CompareTag("ShoeArea"))
        {
            insideArea = true;
        }
    }
    private void OnTriggerExit(Collider other){
            if (other.CompareTag("HavanArea"))
            {
                insideArea = false;
            }
            else if (other.CompareTag("ShoeArea"))
            {
                insideArea = false;
            }
    }
}
