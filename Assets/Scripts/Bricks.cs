using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bricks : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        CharacterInventory characterInventory = other.GetComponent<CharacterInventory>();

        if (characterInventory != null)
        {
            characterInventory.BricksCollected();
            gameObject.SetActive(false);
        }
    }
}
