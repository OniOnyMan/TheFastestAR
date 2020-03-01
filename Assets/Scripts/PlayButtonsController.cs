using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Threading;

public class PlayButtonsController : MonoBehaviour
{
    public TargetTrack target;

	public GameObject leftCowboy;
	public GameObject rightCowboy;
	public GameObject leftImage;
	public GameObject rightImage;
	public GameObject resetKey;
	public GameObject quitMenu;
	public GameObject logo;
	public GameObject playButton;
	public GameObject guidePanel;
    public GameObject pauseButton;
	public Timer timer;
	public GameObject wonSpriteLeft;
    public GameObject wonSpriteRight;
    public GameObject missedSpriteLeft;
	public GameObject missedSpriteRight;
	public GameObject drawSpriteLeft;
	public GameObject drawSpriteRight;

	public bool isEscapePressed = false;
	public bool isGameInStartMenu = true;
	public bool isGuideOpen = false;

    private bool _flag = true;

	// Use this for initialization
	void Start ()
	{
        logo.SetActive(_flag);
		pauseButton.SetActive(!_flag);
		quitMenu.SetActive(!_flag);
		leftImage.SetActive(!_flag);
		rightImage.SetActive(!_flag);
		resetKey.SetActive(!_flag);
		guidePanel.SetActive(!_flag);
        playButton.SetActive(!_flag);
        wonSpriteLeft.SetActive(!_flag);
        wonSpriteRight.SetActive(!_flag);
        missedSpriteLeft.SetActive(!_flag);
        missedSpriteRight.SetActive(!_flag);
	}
	
	// Update is called once per frame
	void  Update () 
	{
		//if running on Android, check for Menu/Home and exit
		if (Application.platform == RuntimePlatform.Android)
		{
			if (Input.GetKey (KeyCode.Escape) && !isEscapePressed) 
			{			
				isGuideOpen = false;
				this.PauseButtonPressed ();
				timer.GetComponent<Timer> ().PauseButtonPressed ();
			} 
			else if (Input.GetKey (KeyCode.Escape) && isGameInStartMenu && !isGuideOpen) 
			{
				this.QuitMenuCall ();
			}
		}
	}


	public void PlayButtonPressed()
	{
		isEscapePressed = false;
		isGameInStartMenu = false;
		leftCowboy.GetComponent<CowboyController> ().Reset ();
		rightCowboy.GetComponent<CowboyController> ().Reset ();
		pauseButton.SetActive(_flag);
		this.ResetKeyPressed ();
		//playButton.GetComponentInChildren<Text>().text = "Continue";
	}

	public void PauseButtonPressed()
	{
		guidePanel.SetActive(!_flag);
		pauseButton.SetActive(!_flag);
		leftImage.SetActive(!_flag);
		rightImage.SetActive(!_flag);
		resetKey.SetActive(!_flag);
		logo.SetActive(_flag);
		wonSpriteLeft.SetActive(!_flag);
        wonSpriteRight.SetActive(!_flag);
		missedSpriteLeft.SetActive(!_flag);
		missedSpriteRight.SetActive(!_flag);
		isEscapePressed = true;
		isGameInStartMenu = true;
        playButton.SetActive(target.IsTracked);
	    timer.TimeOut();
	    timer.isCalled = false;
        timer.bangSprite.SetActive(!_flag);
    }

	public void AgreeKeyPressed()
	{
		Application.Quit ();
	}

	public void DisagreePressed()
	{
		quitMenu.SetActive(!_flag);
		logo.SetActive(_flag);
	}

	public void QuitMenuCall()
	{
		logo.SetActive(!_flag);
		quitMenu.SetActive(_flag);
	}

	/*public void ImagePressed()
	{
		if ((leftCowboy.GetComponent<CowboyController> ().isShooted && rightCowboy.GetComponent<CowboyController> ().isDead) 
			|| (leftCowboy.GetComponent<CowboyController> ().isMissed && rightCowboy.GetComponent<CowboyController> ().isMissed)
			|| (leftCowboy.GetComponent<CowboyController> ().isDead && rightCowboy.GetComponent<CowboyController> ().isShooted)
			|| (leftCowboy.GetComponent<CowboyController> ().isMissed && rightCowboy.GetComponent<CowboyController> ().isShooted))
		{
			resetKey.SetActive(_flag);
			pauseButton..SetActive(!_flag);
		}
	}*/

	public void LeftImagePressed()
	{
		/*leftImage.SetActive(!_flag);
		if (!leftCowboy.GetComponent<CowboyController> ().isBang) 
		{
			missedSpriteLeft.SetActive(_flag);
		}
		else if (!leftCowboy.GetComponent<CowboyController> ().isDead && rightCowboy.GetComponent<CowboyController> ().isDead)
		{
			rightImage.SetActive(!_flag);
			wonSprite.SetActive(_flag);
		}
		else if (leftCowboy.GetComponent<CowboyController> ().isDead && rightCowboy.GetComponent<CowboyController> ().isDead)
		{
			drawSpriteLeft.SetActive(_flag);
		}
		if (leftCowboy.GetComponent<CowboyController> ().isMissed && rightCowboy.GetComponent<CowboyController> ().isMissed) 
		{
			resetKey.SetActive(_flag);
			timer.GetComponent<Timer> ().Stop ();
			timer.GetComponent<Timer> ().readySprite.SetActive(!_flag);
		} 
		else if (!(leftCowboy.GetComponent<CowboyController> ().isMissed || rightCowboy.GetComponent<CowboyController> ().isMissed))
		{
			resetKey.SetActive(_flag);
			timer.GetComponent<Timer> ().bangSprite.SetActive(!_flag);
		}*/
		if (!leftCowboy.GetComponent<CowboyController> ().isBang) 
		{
			leftImage.SetActive(!_flag);
			missedSpriteLeft.SetActive(_flag);
		} 
		else 
		{
			leftImage.SetActive(!_flag);
			rightImage.SetActive(!_flag);
			wonSpriteLeft.SetActive(_flag);
			resetKey.SetActive(_flag);
			timer.GetComponent<Timer> ().bangSprite.SetActive(!_flag);
			//pauseButton..SetActive(!_flag);
			//isEscapePressed = true;
		}
		if (leftCowboy.GetComponent<CowboyController> ().isMissed && rightCowboy.GetComponent<CowboyController> ().isMissed) 
		{
			resetKey.SetActive(_flag);
			timer.GetComponent<Timer> ().Stop ();
			timer.GetComponent<Timer> ().readySprite.SetActive(!_flag);
			//pauseButton..SetActive(!_flag);
			//isEscapePressed = true;
		}
	}

	public void RightImagePressed()
	{
		/*rightImage.SetActive(!_flag);
		if (!rightCowboy.GetComponent<CowboyController> ().isBang) 
		{
			missedSpriteRight.SetActive(_flag);
		}
		else if (!rightCowboy.GetComponent<CowboyController> ().isDead && leftCowboy.GetComponent<CowboyController> ().isDead)
		{
			leftImage.SetActive(!_flag);
			wonSprite.SetActive(_flag);
		}
		else if (rightCowboy.GetComponent<CowboyController> ().isDead && leftCowboy.GetComponent<CowboyController> ().isDead)
		{
			drawSpriteLeft.SetActive(_flag);
		}
		if (rightCowboy.GetComponent<CowboyController> ().isMissed && leftCowboy.GetComponent<CowboyController> ().isMissed) 
		{
			resetKey.SetActive(_flag);
			timer.GetComponent<Timer> ().Stop ();
			timer.GetComponent<Timer> ().readySprite.SetActive(!_flag);
		} 
		else if (!(rightCowboy.GetComponent<CowboyController> ().isMissed || leftCowboy.GetComponent<CowboyController> ().isMissed))
		{
			resetKey.SetActive(_flag);
			timer.GetComponent<Timer> ().bangSprite.SetActive(!_flag);
		}*/
		if (!rightCowboy.GetComponent<CowboyController> ().isBang) 
		{
			rightImage.SetActive(!_flag);
			missedSpriteRight.SetActive(_flag);
		}
		else
		{
			leftImage.SetActive(!_flag);
			rightImage.SetActive(!_flag);
			wonSpriteRight.SetActive(_flag);
			resetKey.SetActive(_flag);
			timer.GetComponent<Timer> ().bangSprite.SetActive(!_flag);
			//pauseButton..SetActive(!_flag);
			//isEscapePressed = true;
		}
		if (leftCowboy.GetComponent<CowboyController> ().isMissed && rightCowboy.GetComponent<CowboyController> ().isMissed) 
		{
			resetKey.SetActive(_flag);
			timer.GetComponent<Timer> ().Stop ();
			timer.GetComponent<Timer> ().readySprite.SetActive(!_flag);
			//pauseButton..SetActive(!_flag);
			//isEscapePressed = true;
		}
	}

	public void ResetKeyPressed()
	{
		logo.SetActive(!_flag);
		leftImage.SetActive(_flag);
		rightImage.SetActive(_flag);
		resetKey.SetActive(!_flag);
		//pauseButton.transform.position = pauseButtonSpawnInPoint.transform.position;
		//isEscapePressed = false;
		wonSpriteLeft.SetActive(!_flag);
	    wonSpriteRight.SetActive(!_flag);
		missedSpriteLeft.SetActive(!_flag);
		missedSpriteRight.SetActive(!_flag);
	}

	public void GuideButtonPressed()
	{
		logo.SetActive(!_flag);
		guidePanel.SetActive(_flag);
		isGuideOpen = true;
		isEscapePressed = false;
	}

	public void CloseButtonPressed()
	{
		guidePanel.SetActive(!_flag);
		logo.SetActive(_flag);
		isGuideOpen = false;
		isEscapePressed = true;
	}
}
