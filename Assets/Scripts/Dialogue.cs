using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FarrokhGames.Inventory;

[System.Serializable]
public class Dialogue
{
    public string name;

    [TextArea(3, 10)]
    public string[] sentences;
    public ItemDefinition[][] requiredItemSets = new ItemDefinition[6][];

    public ItemDefinition[] setA;
    public ItemDefinition[] setB;
    public ItemDefinition[] setC;
    public ItemDefinition[] setD;
    public ItemDefinition[] setE;
    public ItemDefinition[] setF;

    [TextArea(3, 10)]
    public string[] chooseYesSuccess;
    public int moneyYesSuccess;
    public int healthYesSuccess;
    public int psiYesSuccess;
    public int radiationYesSuccess;

    public bool generateLootOnSuccess;
    public bool randomLootOnSuccess;
    public int amountOfRandomLootOnSuccess;
    public ItemDefinition[] lootOnSuccess;

    [TextArea(3, 10)]
    public string[] chooseYesFail;
    public int moneyYesFail;
    public int healthYesFail;
    public int psiYesFail;
    public int radiationYesFail;

    public bool generateLootOnFail;
    public bool randomLootOnFail;
    public int amountOfRandomLootOnFail;
    public ItemDefinition[] lootOnFail;

    [TextArea(3, 10)]
    public string[] chooseNo;
    public int moneyNo;
    public int healthNo;
    public int psiNo;
    public int radiationNo;
}