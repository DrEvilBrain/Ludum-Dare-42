using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using FarrokhGames.Inventory;

public class GameEventManager : MonoBehaviour
{
    public TextMeshProUGUI textMesh;
    public ItemDrops itemDrops;
    public GameTime gameTime;
    public Money money;
    public StatManager statManager;
    public ScrollRect scrollBar;
    public AudioSource audioSource;

    private Queue<string> dialogueSentences;
    private bool dialogEnded;
    private bool gameOver;
    private bool gameWon;
    private string oldText;

	// Use this for initialization
	void Start ()
    {
        dialogueSentences = new Queue<string>();
        dialogEnded = true;
        gameOver = false;
	}
	
    /// <summary>
    /// Loads event dialogue
    /// </summary>
    /// <param name="dialogue"></param>
    public void StartDialogue (Dialogue dialogue)
    {
        DoPsiDamage();
        DoRadiationDamage();

        if (gameOver)
        {
            SceneManager.LoadScene("Game");
        }

        if (!gameOver && !gameWon)
        {
            dialogEnded = false;

            dialogueSentences.Clear();

            foreach (string sentence in dialogue.sentences)
            {
                dialogueSentences.Enqueue(sentence);
            }

            DisplaySentence();

            // Clear item drops
            itemDrops.Clear();
        }
    }

    /// <summary>
    /// Loads successful dialogue for Yes route
    /// </summary>
    /// <param name="dialogue"></param>
    public void DisplayChoiceYesSuccessSentences (Dialogue dialogue)
    {
        if (!gameOver)
        {
            dialogueSentences.Clear();

            foreach (string sentence in dialogue.chooseYesSuccess)
            {
                dialogueSentences.Enqueue(sentence);
            }

            DisplaySentenceForChoices();

            EndDialogue();

            // apply stats
            money.AddMoney(dialogue.moneyYesSuccess);
            statManager.AddHealth(dialogue.healthYesSuccess);
            statManager.AddPsi(dialogue.psiYesSuccess);
            statManager.AddRadiation(dialogue.radiationYesSuccess);

            // generate loot
            if (dialogue.generateLootOnSuccess)
            {
                foreach (ItemDefinition item in dialogue.lootOnSuccess)
                {
                    itemDrops.AddItemDrop(item);
                }
            }

            if (dialogue.randomLootOnSuccess)
            {
                itemDrops.GenerateRandomItems(dialogue.amountOfRandomLootOnSuccess);
            }
        }
    }

    /// <summary>
    /// Loads failure dialogue for Yes route
    /// </summary>
    /// <param name="dialogue"></param>
    public void DisplayChoiceYesFailSentences(Dialogue dialogue)
    {
        if (!gameOver)
        {
            dialogueSentences.Clear();

            foreach (string sentence in dialogue.chooseYesFail)
            {
                dialogueSentences.Enqueue(sentence);
            }

            DisplaySentenceForChoices();

            EndDialogue();

            // apply stats
            money.AddMoney(dialogue.moneyYesFail);
            statManager.AddHealth(dialogue.healthYesFail);
            statManager.AddPsi(dialogue.psiYesFail);
            statManager.AddRadiation(dialogue.radiationYesFail);

            // generate loot
            if (dialogue.generateLootOnSuccess)
            {
                foreach (ItemDefinition item in dialogue.lootOnFail)
                {
                    itemDrops.AddItemDrop(item);
                }
            }

            if (dialogue.randomLootOnSuccess)
            {
                itemDrops.GenerateRandomItems(dialogue.amountOfRandomLootOnFail);
            }
        }  
    }

    /// <summary>
    /// Loads successful dialogue for No route
    /// </summary>
    /// <param name="dialogue"></param>
    public void DisplayChoiceNoSentences(Dialogue dialogue)
    {
        if (!gameOver)
        {
            dialogueSentences.Clear();

            foreach (string sentence in dialogue.chooseNo)
            {
                dialogueSentences.Enqueue(sentence);
            }

            DisplaySentenceForChoices();

            EndDialogue();

            // apply stats
            money.AddMoney(dialogue.moneyNo);
            statManager.AddHealth(dialogue.healthNo);
            statManager.AddPsi(dialogue.psiNo);
            statManager.AddRadiation(dialogue.radiationNo);
        }
    }

    /// <summary>
    /// Displays sentences
    /// </summary>
    public void DisplaySentence()
    {
        if (!gameOver)
        {
            oldText = textMesh.text;
            string sentence = dialogueSentences.Dequeue();
            textMesh.text = oldText + "\n\n" + sentence;

            // move scrollbar to bottom
            Canvas.ForceUpdateCanvases();
            scrollBar.verticalNormalizedPosition = 0f;
            Canvas.ForceUpdateCanvases();
        } 
    }

    /// <summary>
    /// Displays sentences for chioces
    /// </summary>
    public void DisplaySentenceForChoices()
    {
        if (!gameOver)
        {
            oldText = textMesh.text;
            string sentence = dialogueSentences.Dequeue();
            textMesh.text = oldText + "\n" + sentence;

            // move scrollbar to bottom
            Canvas.ForceUpdateCanvases();
            scrollBar.verticalNormalizedPosition = 0f;
            Canvas.ForceUpdateCanvases();
        }
    }

    void EndDialogue()
    {
        // Allow ButtonManager to call next event from the Continue Traveling button
        dialogEnded = true;

        // move scrollbar to bottom
        Canvas.ForceUpdateCanvases();
        scrollBar.verticalNormalizedPosition = 0f;
        Canvas.ForceUpdateCanvases();
    }

    /// <summary>
    /// Returns wether the dialog has ended or not
    /// </summary>
    /// <returns></returns>
    public bool isDialogEnded()
    {
        return dialogEnded;
    }

    private void DoPsiDamage()
    {
        // do 5 HP damage when under 30% sanity
        if(statManager.GetPsi() < 30f)
        {
            statManager.AddHealth(-5f);

            oldText = textMesh.text;
            string sentence = "You head feels like it's spinning in circles.";
            textMesh.text = oldText + "\n\n" + sentence;
        } 
    }

    private void DoRadiationDamage()
    {
        // do 5 damage per 5 radiation
        if(statManager.GetRadiation() > 0f)
        {
            statManager.AddRadiation(-5f);
            statManager.AddHealth(-5f);

            oldText = textMesh.text;
            string sentence = "You don't feel very well.";
            textMesh.text = oldText + "\n\n" + sentence;
        }
    }

    /// <summary>
    /// Causes game over
    /// </summary>
    public void Death()
    {
        oldText = textMesh.text;
        string sentence = "LOST TO THE ZONE\nPress Continue Traveling to restart.";
        textMesh.text = oldText + "\n\n" + sentence;

        // move scrollbar to bottom
        Canvas.ForceUpdateCanvases();
        scrollBar.verticalNormalizedPosition = 0f;
        Canvas.ForceUpdateCanvases();

        gameOver = true;
    }

    /// <summary>
    /// Do the effect a used item has
    /// </summary>
    /// <param name="item"></param>
    public void UseItem(IInventoryItem item)
    {
        if (!gameOver)
        {
            string itemUseText = item.SayOnUse;

            oldText = textMesh.text;
            textMesh.text = oldText + "\n" + itemUseText;

            if(item.HealthEffect)
            {
                statManager.AddHealth(item.HealthAmount);
            }
            if(item.PsiEffect)
            {
                statManager.AddPsi(item.PsiAmount);
            }
            if(item.RadiationEffect)
            {
                statManager.AddRadiation(item.RadiationAmount);
            }

            // play sound effect
            if(item.SoundEffect != null)
            {
                audioSource.clip = item.SoundEffect;
                audioSource.Play();
            }

            // move scrollbar to bottom
            Canvas.ForceUpdateCanvases();
            scrollBar.verticalNormalizedPosition = 0f;
            Canvas.ForceUpdateCanvases();
        }
    }

    /// <summary>
    /// Arrived at CNPP
    /// </summary>
    public void Victory()
    {
        if(!gameOver)
        {
            oldText = textMesh.text;
            string sentence = "You have arrived at the Chernobyl nuclear power plant!\nYou are truly a legend amongst stackers!\nPress Continue Traveling to restart.";
            textMesh.text = oldText + "\n\n" + sentence;

            // move scrollbar to bottom
            Canvas.ForceUpdateCanvases();
            scrollBar.verticalNormalizedPosition = 0f;
            Canvas.ForceUpdateCanvases();

            gameWon = true;
        }
    }
    
    /// <summary>
    /// Check if you are dead
    /// </summary>
    /// <returns></returns>
    public bool isDead()
    {
        return gameOver;
    }
    
    /// <summary>
    /// Check if you won
    /// </summary>
    /// <returns></returns>
    public bool isWon()
    {
        return gameWon;
    }
}
