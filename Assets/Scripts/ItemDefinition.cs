using UnityEngine;
using FarrokhGames.Inventory;

/// <summary>
/// Scriptable Object representing an Inventory Item
/// </summary>
[CreateAssetMenu(fileName = "Item", menuName = "Inventory/Item", order = 1)]
public class ItemDefinition : ScriptableObject, IInventoryItem
{
    [SerializeField] private string _itemType;
    [SerializeField] private Sprite _sprite;
    [SerializeField] private InventoryShape _shape;
    [SerializeField] private bool _consumable;
    [SerializeField] private bool _useable;

    [SerializeField] private bool _healthEffect;
    [SerializeField] private int _healthAmount;
    [SerializeField] private bool _psiEffect;
    [SerializeField] private int _psiAmount;
    [SerializeField] private bool _radiationEffect;
    [SerializeField] private int _radiationAmount;
    [SerializeField] private string _sayOnUse;

    [SerializeField] private AudioClip _soundEffect;

    public string Name { get { return this.name; } }
    public string ItemType { get { return _itemType; } }
    public Sprite Sprite { get { return _sprite; } }
    public InventoryShape Shape { get { return _shape; } }
    public bool Consumable { get { return _consumable; } }
    public bool Useable { get { return _useable; } }

    public bool HealthEffect { get { return _healthEffect; } }
    public int HealthAmount { get { return _healthAmount; } }
    public bool PsiEffect { get { return _psiEffect; } }
    public int PsiAmount { get { return _psiAmount; } }
    public bool RadiationEffect { get { return _radiationEffect; } }
    public int RadiationAmount { get { return _radiationAmount; } }

    public string SayOnUse { get { return _sayOnUse; } }

    public AudioClip SoundEffect { get { return _soundEffect; } }

    /// <summary>
    /// Creates a copy if this scriptable object
    /// </summary>
    public IInventoryItem CreateInstance()
    {
        return ScriptableObject.Instantiate(this);
    }
}