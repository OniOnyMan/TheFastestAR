using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class CowboyController : MonoBehaviour
{
	public Animator ani;

	public Transform bullet;
	public GameObject bulletSpawnPoint;
	public GameObject enemy;
	public bool isDead = false;
	public bool isShooted = false;
	public bool isBang = false;
	public bool isMissed = false;
	private bool _isTimerCall = false;
	private float _waitEndOfAnimation;

	public void Bang ()
	{
		isBang = true; 
	}

	// Use this for initialization
	void Start () 
	{
	}	
	// Update is called once per frame
	void Update()
	{
		if(_isTimerCall)
		{
			_waitEndOfAnimation -= Time.deltaTime;
			if (_waitEndOfAnimation < 0) 
			{
				_isTimerCall = false;
				//this.CreateBullet ();
				enemy.GetComponent<CowboyController> ().Kill ();
				ani.SetTrigger ("IsWon");
			}
		}
	}
	void OnCollisionEnter(Collision col)
	{
		if (col.gameObject.name.Contains ("Bullet"))
		{
			if (!isDead) 
			{
				this.Kill ();
				//_isDead = true;
				//ani.SetBool("IsDead",true);
				//this.transform.localScale = this.transform.localScale/2;
			}
		}
	}

	public void Shoot()
	{
		if(!isDead && !isShooted && !isBang)
		{
			ani.SetTrigger ("IsShooted");
			isMissed = true;
			ani.SetTrigger ("IsMissed");
		}
		if (!isDead && !isShooted && isBang) 
		{
			ani.SetTrigger ("IsShooted");
			isShooted = true;
			_isTimerCall = true;
			//enemy.GetComponent<CowboyController> ().Kill ();
		}
	}

	void CallTimer()
	{
		_waitEndOfAnimation = 1.07f;
		_isTimerCall = true;
	}

	void CreateBullet()
	{
		Transform BulletInstance = (Transform)Instantiate (bullet, bulletSpawnPoint.transform.position, Quaternion.identity, enemy.transform);
		BulletInstance.GetComponent<Rigidbody> ().useGravity = false;
		BulletInstance.GetComponent<Rigidbody> ().AddForce ((enemy.transform.position - this.transform.position).normalized * 55000);
	}

	public void Reset()
	{
		isDead = false;
		isShooted = false;
		isBang = false;
		isMissed = false;
		ani.SetBool("IsDead",false);

	}
	void Kill()
	{
		isDead = true;
		ani.SetBool ("IsDead", true);
	}
}
