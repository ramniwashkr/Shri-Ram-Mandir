using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject character;
    public ShoeDepositStoreHandler shoeDepositStoreHandler; // Reference to the ShoeDepositStoreHandler object
    public GameObject FlowerPrefab;
    public GameObject ShoePrefab;
    public List<Devotee> devoteeList;
    private List<Vector3> positionList = new List<Vector3>();
    private List<Vector3> ShoepositionList = new List<Vector3>();
    public List<GameObject> shoeQueue = new List<GameObject>();

    int maxInPlane = 60; // Maximum number of flowers in a 2D plane
    int countInPlane = 0; // Counter for flowers in the current 2D plane
    int planeCount = 0; // Counter for the number of planes
    int zAxisFlowerPlate = 0,xAxisCount=0;
    public static GameController Instance { get; private set; }

    // Other variables and methods

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            //Debug.LogWarning("Duplicate GameController instance found. Destroying the duplicate.");
            Destroy(gameObject);
        }
    }
    public void generateFlowers()
    {



            for (int x = 0; x < 3; x++) // iterate over x-axis
            {
                float yPos = 0.35f + (planeCount * 0.2f); // Adjust y-position based on planeCount
                GameObject newDevoteeObject = Instantiate(FlowerPrefab, new Vector3((0.7f * xAxisCount) + 56.5f + (x * 0.2f), yPos, 72.5f - (zAxisFlowerPlate * 0.2f)), Quaternion.identity);
                newDevoteeObject.SetActive(true);
                Devotee newDevotee = newDevoteeObject.GetComponent<Devotee>();

                if (newDevotee != null)
                {
                    countInPlane++;

                    // If the current 2D plane is full, increment planeCount and reset countInPlane
                    if (countInPlane >= maxInPlane)
                    {
                        planeCount++;
                        countInPlane = 0;
                        zAxisFlowerPlate = 0;
                        xAxisCount = 0;
                    }

                    var pos = new Vector3((0.7f * xAxisCount) + 56.5f + (x * 0.2f), yPos, 72.5f - (0.2f * zAxisFlowerPlate));
                    newDevotee.MoveTo(pos);
                    positionList.Add(pos);
                    }
                else
                {
                    Debug.LogError("Devotee component not found on instantiated GameObject!");
                }
            }
        xAxisCount++;
        if (xAxisCount == 2) {
            zAxisFlowerPlate++;
            xAxisCount = 0;
        }
    }
    public void generateShoes()
    {
        GameObject newShoeObject = Instantiate(ShoePrefab, new Vector3(59.42f, 1.9f, 72.11f), Quaternion.identity);
        newShoeObject.SetActive(true);
        Devotee newDevotee = newShoeObject.GetComponent<Devotee>();
        if (newDevotee != null)
        {
            var pos = new Vector3(59.42f, 1.9f, 72.11f);
            newDevotee.MoveTo(pos);
            ShoepositionList.Add(pos);
            shoeQueue.Add(newShoeObject); 
        }
        else
        {
            Debug.LogError("Shoe component not found on instantiated GameObject!");
        }
    }
    public void TransferSleeper()
    {
        // Access the ShoeDepositStoreHandler
        if (shoeDepositStoreHandler != null)
        {
            // Check if there are any devotees in the waiting queue
            if (shoeDepositStoreHandler.waitingQueue.devoteeList.Count > 0)
            {
                // Get the first devotee from the waiting queue
                Devotee firstDevotee = shoeDepositStoreHandler.waitingQueue.GetFirstInQueue();

                // Find the "Sleeper" object under the "Devotee"
                GameObject sleeper = firstDevotee.transform.Find("Sleeper").gameObject;

                // Check if the "Sleeper" object exists
                if (sleeper != null)
                {
                    sleeper.SetActive(false);
                    //Debug.Log("Generating Flowers");
                    generateFlowers();
                    generateShoes();
                }
            }
            else
            {
                Debug.LogWarning("No devotees in the waiting queue.");
            }
        }
        else
        {
            Debug.LogWarning("ShoeDepositStoreHandler reference is not set.");
        }
    }
}
