using UnityEngine;

namespace FarrokhGames.Inventory
{
    public interface IInventoryItem
    {
        string Name { get; }
        string ItemType { get; }
        Sprite Sprite { get; }
        InventoryShape Shape { get; }
        bool Consumable { get; }
        bool Useable { get; }

        bool HealthEffect { get; }
        int HealthAmount { get; }
        bool PsiEffect { get; }
        int PsiAmount { get; }
        bool RadiationEffect { get; }
        int RadiationAmount { get; }
    
        string SayOnUse { get; }

        AudioClip SoundEffect { get; }
    }
}