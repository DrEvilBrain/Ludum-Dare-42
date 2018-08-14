using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Traveling : MonoBehaviour
{
    public GameObject popUpBox;
    public AudioSource audioSource;
    public AudioClip[] audioClip = new AudioClip[3];
    public TextMeshProUGUI textMesh;

    private bool gamePaused;

	// Use this for initialization
	void Start ()
    {
        gamePaused = false;
        popUpBox.SetActive(false);
    }

    public void PauseTheGame()
    {
        gamePaused = true;
        TravelingAnimation();
    }

    private void TravelingAnimation()
    {
        popUpBox.SetActive(true);

        StartCoroutine(PlaySoundEffects()); 
    }

    private IEnumerator PlaySoundEffects()
    {
        audioSource.clip = audioClip[0];
        audioSource.Play();
        textMesh.text = "TRAVELING.";
        yield return new WaitForSeconds(1f);

        audioSource.clip = audioClip[1];
        audioSource.Play();
        textMesh.text = "TRAVELING..";
        yield return new WaitForSeconds(1f);

        audioSource.clip = audioClip[2];
        audioSource.Play();
        textMesh.text = "TRAVELING...";
        yield return new WaitForSeconds(1f);

        EndTravelingAnimation();
    }

    private void EndTravelingAnimation()
    {
        popUpBox.SetActive(false);
        gamePaused = false;
    }

    public bool IsGamePaused()
    {
        return gamePaused;
    }
}
