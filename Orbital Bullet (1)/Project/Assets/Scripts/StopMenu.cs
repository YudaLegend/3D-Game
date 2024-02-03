using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public bool pauseButton;
    private Canvas canvasComponent;
    private AudioSource[] allAudioSources;

    void Start()
    {
        pauseButton = false;
        canvasComponent = GetComponent<Canvas>();
        Time.timeScale = 1f;
        // Ensure we have a Canvas component attached
        if (canvasComponent == null)
        {
            Debug.LogWarning("No Canvas component found on this GameObject.");
        }
        canvasComponent.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKey(KeyCode.Escape) && !pauseButton)
        {
            // Toggle between pausing and unpausing the game
            if (Time.timeScale == 0f)
            {
                // If the game is already paused, resume it
                Time.timeScale = 1f;
                canvasComponent.enabled = false;
                //ContinueAllAudio();
            }
            else
            {
                // If the game is not paused, set the time scale to 0 to pause the game
                Time.timeScale = 0f;
                canvasComponent.enabled = true;
                //StopAllAudio();
            }
            pauseButton = true;
        }else if (Input.GetKeyUp(KeyCode.Escape) && pauseButton){
            pauseButton = false;
        }
    }
    public void ButtonClickAction()
    {
        Time.timeScale = 0f;
        canvasComponent.enabled = true;
    }

    public void Continue()
    {
        Time.timeScale = 1f;
        canvasComponent.enabled = false;
    }

    void StopAllAudio() {
        allAudioSources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
        foreach( AudioSource audioS in allAudioSources) {
            audioS.Pause();
	    }
    }

    void ContinueAllAudio(){
        foreach( AudioSource audioS in allAudioSources) {
            audioS.Play();
        }
    }
}
