using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Money : MonoBehaviour
{
    public int startingMoney;
    public TextMeshProUGUI textMesh;

    private int _currentMoney;

	void Start ()
    {
        // Starting money
        _currentMoney = startingMoney;

        // Change text
        UpdateText();
	}
	
	public void AddMoney(int amountOfMoney)
    {
        _currentMoney += amountOfMoney;

        UpdateText();
    }

    private void UpdateText()
    {
        textMesh.text = _currentMoney + " RU";
    }
}
