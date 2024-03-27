using _Joystick.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShoeDepositStoreHandler : MonoBehaviour
{
    private ShoeAlmirahManager almirahManager;
    public GameObject EntryPlane;
    public GameObject maleDevoteeEntryQueue;
    public ShoeDepositStore_WaitingQueue waitingQueue;
    public GameObject ShoeAlmirahManager;
    List<Vector3> waitingQueuePositionList = new List<Vector3>();

    void Start()
    {
        var EntryPos = EntryPlane.transform.position;
        Vector3 firstPosition = EntryPos;
        firstPosition.x = firstPosition.x - 3;
        float positionSize = 1f;
        for (int i = 0; i < 5; i++)
        {
            waitingQueuePositionList.Add(firstPosition + new Vector3(-1, 0, 0) * positionSize * i);
        }

        waitingQueue = new ShoeDepositStore_WaitingQueue(waitingQueuePositionList);
        almirahManager = new ShoeAlmirahManager(4);
    }

    public void EntryUpdate()
    {
        if (waitingQueue.CanAddDevotee())
        {
            var devoteeSpawner = maleDevoteeEntryQueue.GetComponent<DevoteeSpawner>();
            var devoteeList = devoteeSpawner.devoteeList;
            if (devoteeList.Count > 0)
            {
                Devotee firstDevotee = devoteeList[0];
                devoteeList.RemoveAt(0);
                waitingQueue.AddDevotee(firstDevotee);
                devoteeSpawner.RelocateAllDevotees();
                GameObject newDevoteeObject = Instantiate(devoteeSpawner.devoteePrefab, transform.position, Quaternion.identity);
                newDevoteeObject.transform.localScale = new Vector3(0.025f, 0.025f, 0.025f);
                Transform newDevoteeTransform = newDevoteeObject.transform;
                newDevoteeTransform.Rotate(new Vector3(0, 180, 0));
                newDevoteeTransform.position = new Vector3(41, 0, 93);
                Devotee newDevotee = newDevoteeObject.GetComponent<Devotee>();
                devoteeSpawner.devoteeList.Add(newDevotee);
            }
        }
    }

    public void ExitUpdate()
    {
        Debug.Log(waitingQueue.devoteeList.Count);
        Devotee exitDevotee = waitingQueue.GetFirstInQueue();
    }

    public void GetShoeToAlmirah()
    {
        // Accessing shoeQueue directly from the GameController
        List<GameObject> shoeQueue = GameController.Instance.shoeQueue;

        if (shoeQueue.Count > 0)
        {
            Debug.LogWarning("shoe objects in the queue.");
            GameObject firstShoe = shoeQueue[0]; // Dequeue the first shoe object
            shoeQueue.RemoveAt(0);
            firstShoe.SetActive(false); // Deactivate the shoe object
            // Attach the shoe to the character
            CharacterMovementManager.Instance.AttachShoe(firstShoe);
        }
        else
        {
            Debug.LogWarning("No shoe objects in the queue.");
        }
    }

    public void KeepShoeToAlmirah()
    {
        // Get a reference to the CharacterMovementManager instance
        CharacterMovementManager characterManagerInstance = CharacterMovementManager.Instance;

        // Ensure there is a CharacterMovementManager instance and controller is not null
        if (characterManagerInstance != null && characterManagerInstance.controller != null)
        {
            // Find the shoe object by name
            Transform shoeTransform = characterManagerInstance.controller.transform.Find("Slipper v3(Clone)");

            // Check if the shoe object is found
            if (shoeTransform != null)
            {
                GameObject shoe = shoeTransform.gameObject;

                // Deactivate the shoe
                //shoe.SetActive(false);
                shoe.transform.parent = ShoeAlmirahManager.transform;
                // Move the shoe to another place (for example, position (0,0,0))
                shoe.transform.position = ShoeAlmirahManager.transform.position;
                // Find the index of the first available almirah
                int almirahIndex = FindAvailableAlmirahIndex();

                // Add the shoe to the found almirah
                almirahManager.AddShoeToAlmirah(almirahIndex, shoe);
            }
            else
            {
                Debug.LogWarning("Shoe object not found with the name 'Slipper v3(Clone)'.");
            }
        }
        else
        {
            Debug.LogWarning("CharacterMovementManager instance or controller is null.");
        }
    }

    private int FindAvailableAlmirahIndex()
    {
        // Loop through almirahs to find the first one with space for the shoe
        for (int i = 0; i < almirahManager.GetAlmirahCount(); i++)
        {
            if (almirahManager.GetShoeCountInAlmirah(i) < 4)
            {
                return i; // Return the index of the first available almirah
            }
        }

        // If no available almirah is found, return -1
        return -1;
    }

}
