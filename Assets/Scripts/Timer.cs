using UnityEngine;
using System;
using System.Collections;

public class Timer : MonoBehaviour 
{
	public GameObject readySprite;
	public GameObject bangSprite;
	public GameObject leftCowboy;
	public GameObject rightCowboy;

	private float _time;
	public bool isCalled;
    private bool _flag = true;

	// Use this for initialization
	void Start ()
	{
		isCalled = false;
		readySprite.SetActive(!_flag);
		bangSprite.SetActive(!_flag);
	}

	public void Stop()
	{
		isCalled = false;
	}

	public void Call()
	{
		isCalled = true;
		_time = UnityEngine.Random.Range (3, 9);
		readySprite.SetActive(_flag);
		bangSprite.SetActive(!_flag);
	}
	// Update is called once per frame
	void Update () 
	{
		if (isCalled) 
		{
			if (_time > 0) 
			{
				_time -= Time.deltaTime;
			}
			if (Math.Truncate (_time) == 0)
			{
				this.TimeOut ();
				_time = -1;
			}
		}
	}		

	public void TimeOut()
	{
		readySprite.SetActive(!_flag);
		bangSprite.SetActive(_flag);
		leftCowboy.GetComponent<CowboyController> ().Bang ();
		rightCowboy.GetComponent<CowboyController> ().Bang ();
	}

	public void PauseButtonPressed ()
	{
		this.Start ();
	}
}
