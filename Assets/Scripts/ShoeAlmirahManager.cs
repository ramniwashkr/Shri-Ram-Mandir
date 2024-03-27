using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShoeAlmirahManager : MonoBehaviour 
{
    public List<List<GameObject>> almirahs; // List of almirahs, each containing shoes

    public ShoeAlmirahManager(int numberOfAlmirahs)
    {
        almirahs = new List<List<GameObject>>();

        // Create almirahs
        for (int i = 0; i < numberOfAlmirahs; i++)
        {
            almirahs.Add(new List<GameObject>());
        }
    }

    // Method to add a shoe to the almirah at the specified index
    public bool AddShoeToAlmirah(int almirahIndex, GameObject shoe)
    {
        // Check if almirahIndex is valid
        if (almirahIndex >= 0 && almirahIndex < almirahs.Count)
        {
            // Check if almirah has space for more shoes
            if (almirahs[almirahIndex].Count < 4)
            {
                // Add shoe to the almirah
                //Debug.Log("Shoe Kept");
                almirahs[almirahIndex].Add(shoe);
                return true;
            }
            else
            {
                Debug.LogWarning("Almirah is full. Cannot add more shoes.");
                return false;
            }
        }
        else
        {
            Debug.LogWarning("Invalid almirah index.");
            return false;
        }
    }

    // Method to get the almirah count
    public int GetAlmirahCount()
    {
        return almirahs.Count;
    }

    // Method to get the shoe count in the almirah at the specified index
    public int GetShoeCountInAlmirah(int almirahIndex)
    {
        if (almirahIndex >= 0 && almirahIndex < almirahs.Count)
        {
            return almirahs[almirahIndex].Count;
        }
        else
        {
            Debug.LogWarning("Invalid almirah index.");
            return 0;
        }
    }
}
