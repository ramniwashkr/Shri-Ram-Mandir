using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DevoteeSpawner : MonoBehaviour
{
    public GameObject EntryPlane;
    public GameObject devoteePrefab;
    public List<GameObject> devoteeGameObject;
    public List<Devotee> devoteeList;
    private List<Vector3> positionList = new List<Vector3>();
    private Vector3 entrancePosition;
    // float movementSpeed=5;
    //private float rotationSpeed=5;
    public void Start()
    {
        entrancePosition = EntryPlane.transform.position;
        entrancePosition.y = 0;
        // Example: Instantiate 5 Devotees
        for (int i = 0; i < 5; i++)
        {
            GameObject newDevoteeObject = Instantiate(devoteePrefab, transform.position, Quaternion.identity);
            newDevoteeObject.transform.localScale = new Vector3(0.025f, 0.025f, 0.025f);
            devoteeGameObject.Add(newDevoteeObject);
            Devotee newDevotee = newDevoteeObject.GetComponent<Devotee>();
            if (newDevotee != null)
            {
                // Call any methods or set properties of the Devotee
                var pos = entrancePosition;
                pos.x += (3f);
                pos.z += (i * 2);
                //new Vector3(42, 0, 82 + (i * 2));
                newDevotee.MoveTo(pos);
                //positionList.Add(pos);
                positionList.Add(pos);
                Transform newDevoteeTransform = newDevoteeObject.transform;
                newDevoteeTransform.Rotate(new Vector3(0, 180, 0));
                devoteeList.Add(newDevotee);
            }
            else
            {
                Debug.LogError("Devotee component not found on instantiated GameObject!");
            }
        }
    }

    public bool CanAddDevotee()
    {
        return devoteeList.Count < positionList.Count;
    }

    public void AddDevotee(Devotee devotee)
    {
        devoteeList.Add(devotee);
        devotee.MoveTo(entrancePosition, () =>
        {
            devotee.MoveTo(positionList[devoteeList.IndexOf(devotee)]);
        });
    }

    public Devotee GetFirstInQueue()
    {
        if (devoteeList.Count == 0)
        {
            return null;
        }
        else
        {
            Devotee devotee = devoteeList[0];
            devoteeList.RemoveAt(0);
            RelocateAllDevotees();
            return devotee;
        }
    }

    public void RelocateAllDevotees()
    {
        for (int i = 0; i < devoteeList.Count; i++)
        {
            devoteeList[i].MoveTo(positionList[i]);
        }
    }

}
