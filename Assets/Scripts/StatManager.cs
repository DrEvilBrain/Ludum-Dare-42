using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatManager : MonoBehaviour
{
    public float maxHealth;
    public float maxPsi;
    public float maxRadiation;

    public Image healthBar;
    public Image psiBar;
    public Image radiationBar;

    public TextMeshProUGUI healthText;
    public TextMeshProUGUI psiText;
    public TextMeshProUGUI radiationText;

    private float currentHealth;
    private float currentPsi;
    private float currentRadiation;

    public GameEventManager gameEventManager;

	// Use this for initialization
	void Start ()
    {
        currentHealth = maxHealth;
        currentPsi = maxPsi;
        currentRadiation = 0;

        healthBar.fillAmount = currentHealth / maxHealth;
        healthText.text = "Health: " + currentHealth.ToString();

        psiBar.fillAmount = currentPsi / maxPsi;
        psiText.text = "Psi: " + currentPsi.ToString();

        radiationBar.fillAmount = currentRadiation / maxRadiation;
        radiationText.text = "Radiation: " + currentRadiation.ToString();
    }

    /// <summary>
    /// Add health by value
    /// </summary>
    /// <param name="health"></param>
    public void AddHealth(float health)
    {
        currentHealth += health;
        
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        else if (currentHealth < 0)
        {
            // Dead
            currentHealth = 0;
            gameEventManager.Death();
        }

        healthBar.fillAmount = currentHealth / maxHealth;
        healthText.text = "Health: " + currentHealth.ToString();
    }

    /// <summary>
    /// Add psi by value
    /// </summary>
    /// <param name="psi"></param>
    public void AddPsi(float psi)
    {
        currentPsi += psi;

        if (currentPsi > maxPsi)
        {
            currentPsi = maxPsi;
        }
        else if (currentPsi < 0)
        {
            currentPsi = 0;
        }

        psiBar.fillAmount = currentPsi / maxPsi;
        psiText.text = "Psi: " + currentPsi.ToString();
    }

    /// <summary>
    /// Add radiation by value
    /// </summary>
    /// <param name="radiation"></param>
    public void AddRadiation(float radiation)
    {
        currentRadiation += radiation;

        if (currentRadiation > maxRadiation)
        {
            currentRadiation = maxRadiation;
        }
        else if (currentRadiation < 0)
        {
            currentRadiation = 0;
        }

        radiationBar.fillAmount = currentRadiation / maxRadiation;
        radiationText.text = "Radiation: " + currentRadiation.ToString();
    }

    public float GetHealth()
    {
        return currentHealth;
    }

    public float GetPsi()
    {
        return currentPsi;
    }

    public float GetRadiation()
    {
        return currentRadiation;
    }
}
