using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/**
    Timer handles the synchronization of level elements and the audio being played
*/
public class Timer : MonoBehaviour
{
    public Text timerText;
    public Text actionText;
    private float startTime;
    private bool playState;
    public Button pauseBtn;
    private Dictionary<string,string> mileStones = new Dictionary<string,string>();
    private float pauseTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        playState = false;
        //add milestones
        mileStones.Add("0:0","Start");
        mileStones.Add("0:15","New Weapon");
        mileStones.Add("0:29","New Enemy");
        mileStones.Add("0:44","New Action");
        mileStones.Add("0:58","New Action");
    }

    /*
        Plays the timer and enables the pause button
    */
    public void Play(){
        playState = true;
        pauseBtn.enabled = true;
        startTime = Time.time;
    }

    /*
        Pauses the timer and disables the pause button
    */
    public void Pause(){
        pauseBtn.enabled = false;
        pauseTime += Time.time - startTime;
        playState = false;
    }

    /*
        Resets timer to 0:0 and updates the display
    */
    public void Reset(){
        playState = false;
        pauseBtn.enabled = false;
        timerText.text = 0 + ":" + 0; //format display
        pauseTime = 0;
    }

    // Update is called once per frame and updates the timer directly
    void Update()
    {
        if(playState == true){ // if not currently paused
            float elapsed;
            if(pauseTime != 0){ // has been paused before so factor in pauseTime
                elapsed =  Time.time - startTime + pauseTime;
            }else {
                elapsed = Time.time - startTime; //time since start without pauses
            }
            float minuites = ((int) elapsed / 60);
            float seconds = (elapsed % 60);
            string current = minuites.ToString() + ":" + seconds.ToString("f0");

            if(mileStones.ContainsKey(current)){ //check if we hav hit a milestone then call action
                actionText.text = mileStones[current];
            }
            timerText.text = minuites.ToString() + ":" + seconds.ToString("f4"); //format display
        }
    }
}
