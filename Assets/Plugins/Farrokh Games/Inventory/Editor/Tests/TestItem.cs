using UnityEngine;

namespace FarrokhGames.Inventory
{
    public class TestItem : IInventoryItem
    {
        public string Name { get; private set; }
        public string ItemType { get; private set; }
        public Sprite Sprite { get; private set; }
        public InventoryShape Shape { get; private set; }
        public bool Consumable { get; private set; }
        public bool Useable { get; private set; }

        public bool HealthEffect { get; private set; }
        public int HealthAmount { get; private set; }
        public bool PsiEffect { get; private set; }
        public int PsiAmount { get; private set; }
        public bool RadiationEffect { get; private set; }
        public int RadiationAmount { get; private set; }

        public string SayOnUse { get; private set; }

        public AudioClip SoundEffect { get; private set; }

        public TestItem(string name, string itemType, Sprite sprite, InventoryShape shape, bool consumable, bool useable,
            bool healthEffect, int healthAmount, bool psiEffect, int psiAmount, bool radiationEffect, int radiationAmount, string sayOnUse,
            AudioClip soundEffect)
        {
            Name = name;
            ItemType = itemType;
            Sprite = sprite;
            Shape = shape;
            Consumable = consumable;
            Useable = useable;

            HealthEffect = healthEffect;
            HealthAmount = healthAmount;
            PsiEffect = psiEffect;
            PsiAmount = psiAmount;
            RadiationEffect = radiationEffect;
            RadiationAmount = radiationAmount;
            SayOnUse = sayOnUse;

            SoundEffect = soundEffect;
        }
    }
}