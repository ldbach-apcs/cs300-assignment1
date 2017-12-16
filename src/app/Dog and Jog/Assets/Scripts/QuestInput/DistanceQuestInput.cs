using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceQuestInput : IQuestInput {

    public double totalDistance = 0;

    // Because the Pedometer measure distance only from 
    // the start of session, we load distance of previous
    // sessions
    private float prevDistance = 0;
    public static string PREV_DISTANCE = "previous_distance";

	// This String is for passing QuestInputData
	public static string INPUT_DISTANCE = "input_distance";

    private PedometerU.Pedometer pedometer;
    
	public DistanceQuestInput()
	{
		prevDistance = PlayerPrefs.GetFloat (PREV_DISTANCE, 0f);
		pedometer = new PedometerU.Pedometer (OnStep);
	}

	public void Init()
	{
		OnStep(0, 0);
	}

    /*
     * This function is used to handle Pedometer reading
     * @param
     * steps: steps taken from the start of session
     * distance: meters walked from start of sesssion
     */
    private void OnStep(int steps, double distance)
    {
        totalDistance = distance + prevDistance;
		var data = new QuestInputData (INPUT_DISTANCE); 
		data.PutValue (totalDistance);
        Notify(data);
    }
    
    public void Destroy()
    {
        // Close and clean up the pedometer
        pedometer.Dispose();
        pedometer = null;

		// save the current step state
		PlayerPrefs.SetFloat(PREV_DISTANCE, (float) totalDistance);
    }
}
