using System;
using UnityEngine;
using FarrokhGames.Inventory;
using TMPro;

[RequireComponent(typeof(InventoryRenderer))]
public class ArmorEquip : MonoBehaviour
{
    public bool _showItemName;
    public TextMeshProUGUI _textMesh;
    public string _prefix;

    private InventoryManager inventory;
    [SerializeField] private int _width;
    [SerializeField] private int _height;
    [SerializeField] private ItemDefinition[] _definitions;

    private void Start()
    {
        // Initialize inventory
        inventory = new InventoryManager(_width, _height);

        // Sets the renderers's inventory to trigger drawing
        GetComponent<InventoryRenderer>().SetInventory(inventory);

        // Set armor to Leather Jacket
        inventory.Add(_definitions[0]);
    }

    private void Update()
    {
        // Update text box
        if (inventory.OnCleared != null)
        {
            _textMesh.text = _prefix + "None";
        }

        if (inventory.OnItemAdded != null && inventory.AllItems.ToArray().Length > 0)
        {
            string itemName = inventory.AllItems.ToArray()[0].Name;
            _textMesh.text = _prefix + itemName.Replace("(Clone)", "").Trim();
        }
    }

    /// <summary>
    /// Returns current armor equipment
    /// </summary>
    /// <returns></returns>
    public InventoryManager GetArmorEquip()
    {
        return inventory;
    }
}
