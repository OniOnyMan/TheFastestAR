using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public DuelController DuelController;
    public GameObject BulletPrefab;

    public PlayerController Enemy { get; private set; }
    public bool IsDead { get; private set; }
    public bool IsReloading { get; private set; }
    /// <summary>
    /// Вероятность словить пулю в себя
    /// </summary>
    public float HittedChanse { get; private set; } 
    /// <summary>
    /// Вероятность осечки
    /// </summary>
    public float MisfireChanse { get; private set; }
    /// <summary>
    /// Вероятность промазать по противнику
    /// </summary>
    public float NoMissChanse { get; private set; }


    private Animator _animator;
    private Transform _bulletSpawnPoint;
    private Text _statusText;
    
    public void Hitted()
    {
        if (!IsDead && !IsReloading)
        {
            Shot(DuelController.PlayerShoted(this));
            //TODO: rework to PlayerHitted. DuelControlle must solve who will miss...
        }
    }

    public void ReloadFinish()
    {
        IsReloading = false;
    }

    public void Won()
    {
        Win();
    }

    public void Restart()
    {
        Rise();
    }

    void Start()
    {
        _animator = gameObject.GetComponentInChildren<Animator>();
        Debug.Log(gameObject.name + "'s animator is " + _animator);
        _bulletSpawnPoint = FindElementInChildByName("BulletSpawnPoint");
        Debug.Log(gameObject.name + "'s bullet spawn ponit is " + _bulletSpawnPoint.position);
        _statusText = FindElementInChildByName("StatusText").GetComponent<Text>(); //будет null, косяк в префабе
        Debug.Log(gameObject.name + "'s status text is " + _statusText.transform.position);
        Enemy = GameObject.FindGameObjectWithTag(gameObject.tag == "PlayerOne" ? "PlayerTwo" : "PlayerOne").GetComponent<PlayerController>();
        Debug.Log(gameObject.name + "'s enemy is " + Enemy.name);
        HittedChanse = PlayerPrefs.GetFloat(gameObject.name + "HittedChanse");
        NoMissChanse = PlayerPrefs.GetFloat(gameObject.name + "NoMissChanse");
        MisfireChanse = PlayerPrefs.GetFloat(gameObject.name + "MisfireChanse");
        Debug.Log(gameObject.name + "'s stats: HTCH = " + HittedChanse + "; NMCH = " + NoMissChanse + "; MFCH = " + MisfireChanse);
    }

    void OnCollisionEnter(Collision collisionObject)
    {
        if (collisionObject.gameObject.name.Contains("Bullet"))
        {
            if (!IsDead)
            {
                Die();
                DuelController.PlayerDead(this);
            }
        }
    }

    private void Die()
    {
        _animator.SetBool("IsDead", IsDead = true);
        _statusText.text = "";
    }

    private void Rise()
    {
        _animator.SetBool("IsDead", IsDead = false);
        _statusText.text = "";
    }

    private void Win()
    {
        _animator.SetTrigger("Gotcha");
        _statusText.text = "Victory";
    }

    private void Shot(ShotResult result)
    {
        _animator.SetTrigger("Shot");
        if (result == ShotResult.Allowed)
            CreateBullet();
        else
            StartCoroutine(Reload(result));
    }

    private IEnumerator Reload(ShotResult result)
    {
        _statusText.text = result == ShotResult.Missed ? "Miss" : "Misfire";
        yield return new WaitForSecondsRealtime(1.2f); //todo: test this shit
        _animator.SetTrigger(result == ShotResult.Misfire ? "Failure" : "Missed");
        _statusText.text = "Reload";
        IsReloading = true;
    }

    private void CreateBullet()
    {
        var bulletInstance = Instantiate(BulletPrefab, _bulletSpawnPoint.position, Quaternion.identity, _bulletSpawnPoint);
        bulletInstance.GetComponent<Rigidbody>().useGravity = false;
        bulletInstance.GetComponent<Rigidbody>().AddForce((Enemy.transform.position - transform.position).normalized * 10);
    }

    private Transform FindElementInChildByName(string name)
    {
        for (var i = 0; i < transform.childCount; i++)
        {
            var bsp = transform.GetChild(i).Find(name);
            if (bsp != null) return bsp;
        }

        throw new NullReferenceException();
    }
}
