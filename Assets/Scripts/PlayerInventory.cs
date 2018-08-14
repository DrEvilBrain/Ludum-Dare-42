using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using FarrokhGames.Inventory;

[RequireComponent(typeof(InventoryRenderer))]
public class PlayerInventory : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private int _width;
    [SerializeField] private int _height;
    [SerializeField] private ItemDefinition[] _definitions;

    private InventoryManager inventory;
    private InventoryRenderer _renderer;

    public ClickManager clickManager;
    public GameEventManager gameEventManager;

    private IInventoryItem _item;

    void Start()
    {
        // Initialize inventory
        inventory = new InventoryManager(_width, _height);

        // Add starting equipment
        GenerateStartingEquipment();

        // Sets the renderers's inventory to trigger drawing
        _renderer = GetComponent<InventoryRenderer>();
        GetComponent<InventoryRenderer>().SetInventory(inventory);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (Input.GetMouseButtonDown(0))
        {
            var grid = ScreenToGrid(eventData.position);
            _item = inventory.GetAtPoint(grid);

            if (clickManager.DoubleClick() && _item != null & _item.Useable)
            {
                // use the item
                gameEventManager.UseItem(_item);

                // remove item
                inventory.Remove(_item);
            }
        }
    }

    private void AddRandomItem()
    {
        inventory.Add(_definitions[Random.Range(0, _definitions.Length)].CreateInstance());
    }

    private void GenerateStartingEquipment()
    {
        // 3 random, 1 medkit, 2 bandages, 3 pistol ammo, 1 bolt, 1 cigarette, 1 vodka, 2 rations, 1 energy drink, 1 knife
        AddRandomItem();
        AddRandomItem();
        AddRandomItem();
        inventory.Add(_definitions[9].CreateInstance()); //medkit
        inventory.Add(_definitions[1].CreateInstance()); //bandage
        inventory.Add(_definitions[1].CreateInstance());
        inventory.Add(_definitions[11].CreateInstance()); //pistol ammo
        inventory.Add(_definitions[11].CreateInstance());
        inventory.Add(_definitions[11].CreateInstance());
        inventory.Add(_definitions[2].CreateInstance()); //bolt
        inventory.Add(_definitions[3].CreateInstance()); //cigarette
        inventory.Add(_definitions[16].CreateInstance()); //vodka
        inventory.Add(_definitions[12].CreateInstance()); //rations
        inventory.Add(_definitions[12].CreateInstance());
        inventory.Add(_definitions[5].CreateInstance()); //energy drink
        inventory.Add(_definitions[7].CreateInstance()); //knife
    }

    private Vector2Int ScreenToGrid(Vector2 screenPoint)
    {
        var pos = Vector2.zero;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            _renderer.RectTransform,
            screenPoint,
            null,
            out pos
        );
        pos.x += _renderer.RectTransform.sizeDelta.x / 2;
        pos.y += _renderer.RectTransform.sizeDelta.y / 2;
        return new Vector2Int(Mathf.FloorToInt(pos.x / _renderer.CellSize.x), Mathf.FloorToInt(pos.y / _renderer.CellSize.y));
    }

    /// <summary>
    /// Gets the PlayerInventory InventoryManager
    /// </summary>
    /// <returns></returns>
    public InventoryManager GetPlayerInventory()
    {
        return inventory;
    }
}
