using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameTime : MonoBehaviour
{
    public GameEventManager gameEventManager;

    public TextMeshProUGUI textMesh;
    public enum TimeOfDay
    {
        Morning,
        Noon,
        Afternoon,
        Evening,
        Night
    }

    private int _dayNumber;
    private TimeOfDay _timeOfDay;

	void Start ()
    {
        // Initial time
        _dayNumber = 1;
        _timeOfDay = TimeOfDay.Morning;

        // Change text
        UpdateText();
	}

    private void UpdateText()
    {
        textMesh.text = _timeOfDay + "\nDay " + _dayNumber;
    }

    /// <summary>
    /// Returns current time of day
    /// </summary>
    public TimeOfDay GetTimeOfDay()
    {
        return _timeOfDay;
    }

    /// <summary>
    /// Returns current day number
    /// </summary>
    /// <returns></returns>
    public int GetDay()
    {
        return _dayNumber;
    }

    /// <summary>
    /// Changes current time period to next time period
    /// </summary>
    public void NextTimePeriod()
    {
        // Change to next time of day
        if(_timeOfDay == TimeOfDay.Morning)
        {
            _timeOfDay = TimeOfDay.Noon;
        }
        else if (_timeOfDay == TimeOfDay.Noon)
        {
            _timeOfDay = TimeOfDay.Afternoon;
        }
        else if (_timeOfDay == TimeOfDay.Afternoon)
        {
            _timeOfDay = TimeOfDay.Evening;
        }
        else if (_timeOfDay == TimeOfDay.Evening)
        {
            _timeOfDay = TimeOfDay.Night;
        }
        else if (_timeOfDay == TimeOfDay.Night)
        {
            _dayNumber++;
            _timeOfDay = TimeOfDay.Morning;

            if(_dayNumber == 30)
            {
                // Arrived at the Chernobyl nuclear power plant
                gameEventManager.Victory();
            }
        }

        // Change text once time is changed
        UpdateText();
    }

}
