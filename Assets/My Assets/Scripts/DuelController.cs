using System;
using UnityEngine;
using UnityEngine.XR.WSA.Persistence;
using Random = UnityEngine.Random;

public class DuelController : MonoBehaviour
{
    public PlayerController PlayerOne;
    public PlayerController PlayerTwo;

    public bool IsTimerRun { get; private set; }
    public bool IsTimerOut { get; private set; }
    public bool IsWaitingRun { get; private set; }
    public bool IsWaitingOut { get; private set; }
    public int PlayerOneScore { get; private set; }
    public int PlayerTwoScore { get; private set; }

    private float _bangTime;
    private float _waitingTime;

    private void StartTimer()
    {
        IsTimerOut = false;
        IsTimerRun = GetComponent<RaycastScript>().IsBang = true;
        _bangTime = Random.Range(3, 14);
    }

    private void StopTimer()
    {
        IsTimerRun = false;
        IsTimerOut = true;
        _bangTime = -1f;
    }

    private void StartWaiting()
    {
        IsWaitingOut = false;
        IsWaitingRun = true;
        _waitingTime = 15f;
    }
    private void StopWaiting()
    {
        IsWaitingOut = false;
        IsWaitingRun = true;
        _waitingTime = -1f;
        ResultCheck();
    }

    private void ResultCheck()
    {
        GetComponent<RaycastScript>().IsBang = false;
        if (PlayerTwo.IsDead && !PlayerOne.IsDead)
        {
            PlayerOneScore++;
            PlayerOne.Won();
        }
        else if (PlayerOne.IsDead && !PlayerTwo.IsDead)
        {
            PlayerTwoScore++;
            PlayerTwo.Won();
        }
    }

    public void StartGame()
    {
        StartTimer();
    }

    public void RestartGame()
    {
        PlayerOne.Restart();
        PlayerTwo.Restart();
    }

    void Update()
    {
        if (IsTimerRun)
        {
            if (_bangTime > 0)
            {
                _bangTime -= Time.deltaTime;
            }
            else
            {
                StopTimer();
                StartWaiting();
            }
        } else if (IsWaitingRun)
        {
            if (_waitingTime > 0)
            {
                _waitingTime -= Time.deltaTime;
            }
            else
            {
                StopWaiting();
            }
        }
    }

    public ShotResult PlayerShoted(PlayerController player)
    {
        if (Random.Range(0f, 10f) > player.MisfireChanse)
        {
            if (Random.Range(player.NoMissChanse, 10f) > player.Enemy.HittedChanse)
            {
                return ShotResult.Allowed;
            }
            else return ShotResult.Missed;
        }
        else return ShotResult.Misfire;
    }

    public void PlayerDead(PlayerController player)
    {
        if (player.Enemy.IsDead)
        {
            StopWaiting();
        }
    }
}
