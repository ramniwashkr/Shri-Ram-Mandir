using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class InventoryUI : MonoBehaviour
{
    private TextMeshProUGUI flowersText;
    private TextMeshProUGUI BricksText;

    // Start is called before the first frame update
    void Start()
    {
        flowersText = GetComponent<TextMeshProUGUI>();
        BricksText = GetComponent<TextMeshProUGUI>();
    }

    public void UpdateFlowersText(CharacterInventory characterInventory)
    {
        flowersText.text = characterInventory.NumberOfFlowers.ToString();
    }

    public void UpdateBricksText(CharacterInventory characterInventory)
    {
        BricksText.text = characterInventory.NumberOfBricks.ToString();
    }
}
