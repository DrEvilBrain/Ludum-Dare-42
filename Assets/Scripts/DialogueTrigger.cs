using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FarrokhGames.Inventory;

/// <summary>
/// Really bad way to handle ingame events and dialogue
/// </summary>

public class DialogueTrigger : MonoBehaviour
{
    public PlayerInventory playerInventory;
    public WeaponEquip weaponEquip;
    public ArmorEquip armorEquip;

    public Dialogue[] dialogue;
    
    private int dialogueNumber;
    private bool[][] chooseYesSuccess = new bool[6][];
    private bool didYesSucceed;

    /// <summary>
    /// Used to start a dialogue event
    /// </summary>
    public void TriggerDialogue()
    {
        // Generate random dialogueNumber
        dialogueNumber = Random.Range(0, dialogue.Length);

        // Start dialogue
        FindObjectOfType<GameEventManager>().StartDialogue(dialogue[dialogueNumber]); 
    }

    /// <summary>
    /// Used to go to the Yes route in a dialogue
    /// </summary>
    public void ChooseYes()
    {
        // Define chooseYesSuccess
        chooseYesSuccess[0] = new bool[dialogue[dialogueNumber].setA.Length];
        chooseYesSuccess[1] = new bool[dialogue[dialogueNumber].setB.Length];
        chooseYesSuccess[2] = new bool[dialogue[dialogueNumber].setC.Length];
        chooseYesSuccess[3] = new bool[dialogue[dialogueNumber].setD.Length];
        chooseYesSuccess[4] = new bool[dialogue[dialogueNumber].setE.Length];
        chooseYesSuccess[5] = new bool[dialogue[dialogueNumber].setF.Length];

        // Default set didYesSucceed to false
        didYesSucceed = false;

        // Generate requiredItemSets from individual sets
        dialogue[dialogueNumber].requiredItemSets[0] = dialogue[dialogueNumber].setA;
        dialogue[dialogueNumber].requiredItemSets[1] = dialogue[dialogueNumber].setB;
        dialogue[dialogueNumber].requiredItemSets[2] = dialogue[dialogueNumber].setC;
        dialogue[dialogueNumber].requiredItemSets[3] = dialogue[dialogueNumber].setD;
        dialogue[dialogueNumber].requiredItemSets[4] = dialogue[dialogueNumber].setE;
        dialogue[dialogueNumber].requiredItemSets[5] = dialogue[dialogueNumber].setF;

        // Check if dialogue has item requirements for success
        if (dialogue[dialogueNumber].requiredItemSets[0].Length != 0)
        {
            int i = 0;
            int j = 0;

            // Check if player has each of the items
            for (i = 0; i < dialogue[dialogueNumber].requiredItemSets.Length; i++)
            {
                for (j = 0; j < dialogue[dialogueNumber].requiredItemSets[i].Length; j++)
                {
                    if (playerInventory.GetPlayerInventory().Contains(dialogue[dialogueNumber].requiredItemSets[i][j])
                        || weaponEquip.GetWeaponEquip().Contains(dialogue[dialogueNumber].requiredItemSets[i][j])
                        || armorEquip.GetArmorEquip().Contains(dialogue[dialogueNumber].requiredItemSets[i][j]))
                    {
                        // Have a required item
                        chooseYesSuccess[i][j] = true;

                        Debug.Log("have item " + i + " " + j);
                    }
                    else
                    {
                        // Don't have a required item
                        chooseYesSuccess[i][j] = false;

                        Debug.Log("dont have item " + i + " " + j);
                    }
                }
            }

            // Check if a player has an entire set of required items
            for (int increment = 0; increment < 6; increment++)
            {
                if(DoesPlayerHaveRequiredItemSet(chooseYesSuccess[increment]) && chooseYesSuccess[increment].Length > 0)
                {
                    didYesSucceed = true;

                    // Remove consumed items
                    foreach (IInventoryItem item in dialogue[dialogueNumber].requiredItemSets[increment])
                    {
                        if (item.Consumable)
                        {
                            playerInventory.GetPlayerInventory().Remove(item);

                            Debug.Log("Removed " + item.Name);
                        }
                    }
                }
            }
            
            // Display dialogue
            if(didYesSucceed)
            {
                FindObjectOfType<GameEventManager>().DisplayChoiceYesSuccessSentences(dialogue[dialogueNumber]);
            }
            else
            {
                FindObjectOfType<GameEventManager>().DisplayChoiceYesFailSentences(dialogue[dialogueNumber]);
            }
        }

        // No item requirements for success
        else
        {
            FindObjectOfType<GameEventManager>().DisplayChoiceYesSuccessSentences(dialogue[dialogueNumber]);
        }
    }

    private bool DoesPlayerHaveRequiredItemSet(bool[] itemSet)
    {
        int i = 0;

        foreach (bool item in itemSet)
        {
            if (itemSet[i] == false)
            {
                return false;
            }

            i++;
        }

        return true;
    }

    private bool GetUsedItemSet(bool[] itemSet)
    {
        int i = 0;

        foreach (bool item in itemSet)
        {
            if (itemSet[i] == false)
            {
                return false;
            }

            i++;
        }

        return true;
    }

    /// <summary>
    /// Used to go to the No route in a dialogue
    /// </summary>
    public void ChooseNo()
    {
        FindObjectOfType<GameEventManager>().DisplayChoiceNoSentences(dialogue[dialogueNumber]);
    }
}
