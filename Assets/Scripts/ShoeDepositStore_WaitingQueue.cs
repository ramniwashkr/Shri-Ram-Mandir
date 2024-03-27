using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ShoeDepositStore_WaitingQueue : MonoBehaviour
{
    //public GameObject Devotee;
    public List<Devotee> devoteeList;
    public List<Vector3> positionList;
    public Vector3 entrancePosition;

    public ShoeDepositStore_WaitingQueue(List<Vector3> positionList){
        this.positionList = positionList;
        entrancePosition = positionList[positionList.Count - 1] + new Vector3(-8f,0);
        devoteeList = new List<Devotee>();
    }

    public bool CanAddDevotee(){
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
            Devotee devotee= devoteeList[0];
            devoteeList.RemoveAt(0);
            RelocateAllDevotees();
            return devotee;
        }
    }

    private void RelocateAllDevotees()
    {
        //Debug.Log(devoteeList.Count);
        for(int i = 0; i < devoteeList.Count; i++)
        {
            devoteeList[i].MoveTo(positionList[i]);
        }
    }
}
