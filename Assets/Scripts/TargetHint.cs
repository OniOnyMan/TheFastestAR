using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetHint : MonoBehaviour
{
    public TargetTrack target;
    public GameObject playButton;
    private PlayButtonsController ui;

	// Use this for initialization
	void Start ()
	{
	    target.TrackStateChanged += ShowMenu;
	    ui = this.GetComponent<PlayButtonsController>();
	}

    public void ShowMenu(bool flag)
    {
        if (flag)
        {
            if (ui.isGameInStartMenu)
                playButton.SetActive(flag);
        }
        else
        {
            if(ui.isGameInStartMenu)
                playButton.SetActive(flag);
            else ui.PauseButtonPressed();
        }
    }

	// Update is called once per frame
	void Update () {
		
	}
}
