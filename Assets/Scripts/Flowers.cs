using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flowers : MonoBehaviour
{

    private void Start()
    {
        gameObject.SetActive(true);
    }
    private void OnTriggerEnter(Collider other)
    {
        CharacterInventory characterInventory = other.GetComponent<CharacterInventory>();

        if(characterInventory != null)
        {
            characterInventory.FlowersCollected();
            gameObject.SetActive(false);
        }
    }
}
