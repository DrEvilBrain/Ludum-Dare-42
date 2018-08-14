using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FarrokhGames.Inventory;

[RequireComponent(typeof(InventoryRenderer))]
public class ItemDrops : MonoBehaviour
{
    [SerializeField] private int _width;
    [SerializeField] private int _height;
    [SerializeField] private ItemDefinition[] _definitions;

    private InventoryManager inventory;

    void Start()
    {
        // Initialize inventory
        inventory = new InventoryManager(_width, _height);

        // Sets the renderers's inventory to trigger drawing
        GetComponent<InventoryRenderer>().SetInventory(inventory);
    }

    /// <summary>
    /// Used to make random loot drops after events are resolved.
    /// Amount integer determines how many items are generated.
    /// </summary>
    /// <param name="amount"></param>
    public void GenerateRandomItems(int amount)
    {
        // Fill inventory with random items
        for (int i = 0; i < amount; i++)
        {
            inventory.Add(_definitions[Random.Range(0, _definitions.Length)].CreateInstance());
        }
    }

    /// <summary>
    /// Add specified item to loot drops
    /// </summary>
    /// <param name="item"></param>
    public void AddItemDrop(ItemDefinition item)
    {
        inventory.Add(item.CreateInstance());
    }

    /// <summary>
    /// Clear item drops
    /// </summary>
    public void Clear()
    {
        inventory.Clear();
    }
}
