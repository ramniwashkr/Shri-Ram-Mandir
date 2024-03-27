using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class CharacterInventory : MonoBehaviour
{
    public int NumberOfFlowers { get; private set; }
    public int NumberOfBricks { get; private set; }
    public UnityEvent<CharacterInventory> onFlowersCollected, onBricksCollected;
    public void FlowersCollected()
    {
        NumberOfFlowers++;
        onFlowersCollected.Invoke(this);
    }

    public void BricksCollected()
    {
        NumberOfBricks++;
        onBricksCollected.Invoke(this);
    }
}
