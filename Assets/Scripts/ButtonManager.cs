using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public Button _continueTraveling;
    public Button _yes;
    public Button _no;

    public GameTime _timeManager;
    public DialogueTrigger _dialogueTrigger;
    public GameEventManager _gameEventManager;
    public Traveling _traveling;

    void Start()
    {
        _continueTraveling.onClick.AddListener(ContinueTravelingEvent);
        _yes.onClick.AddListener(YesEvent);
        _no.onClick.AddListener(NoEvent);
    }

	void ContinueTravelingEvent()
    {
        // Check if event has been resolved and if game is paused
        if (_gameEventManager.isDialogEnded() && !_traveling.IsGamePaused())
        {
            if (_gameEventManager.isDead())
            {
                // Skip travel animation

                // Go to next time period if allowed
                _timeManager.NextTimePeriod();

                // Go to next event
                _dialogueTrigger.TriggerDialogue();
            }
            else if (_gameEventManager.isWon())
            {
                SceneManager.LoadScene("Game");
            }
            else
            {
                // Pause to play traveling animation
                _traveling.PauseTheGame();
                StartCoroutine(DialogueAndTimeTrigger());
            } 
        }
    }

    private IEnumerator DialogueAndTimeTrigger()
    {
        yield return new WaitForSeconds(3f);

        // Go to next time period if allowed
        _timeManager.NextTimePeriod();

        // Go to next event
        _dialogueTrigger.TriggerDialogue();
    }

    void YesEvent()
    {
        // Check if event has not been resolved and if game is paused
        if (!_gameEventManager.isDialogEnded() && !_traveling.IsGamePaused())
        {
            // Send yes to GameEventManager
            _dialogueTrigger.ChooseYes();
        }
    }

    void NoEvent()
    {
        // Check if event has not been resolved and if game is paused
        if (!_gameEventManager.isDialogEnded() && !_traveling.IsGamePaused())
        {
            // Send no to GameEventManager
            _dialogueTrigger.ChooseNo();
        }
    }
}
