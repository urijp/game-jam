using System;
using UnityEngine;
using Random = System.Random;

public class WorkerController : MonoBehaviour
{
    public enum Status
    {
        Awake = 0,
        Exhausted = 1,
        Sleeping = 2,
    }

    public float difficultyRate;
    public double powerTime;
    public double minPowerTime;
    public double maxPowerTime;
    public Status status = Status.Awake;
    private float _timer;
    private static readonly int Exhausted = Animator.StringToHash("exhausted");
    private static readonly int Sleeping = Animator.StringToHash("sleeping");
    private Animator _animator;
    public AudioSource _audioSource;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _animator = GetComponent<Animator>();
        powerTime = new Random().NextDouble() * (maxPowerTime - minPowerTime) + minPowerTime;
    }

    void Update()
    {
        if (status == Status.Sleeping) return;
 
        _timer += Time.deltaTime;

        if (_timer >= powerTime)
        {
            if (status == Status.Awake) ExhaustWorker();
            else SleepWorker();
            _timer = 0;
        }
    }

    private void OnMouseDown()
    {
        AwakeWorker();
    }

    private void ExhaustWorker()
    {
        status = Status.Exhausted;
        _animator.SetBool(Exhausted, true);
    }

    private void SleepWorker()
    {
        status = Status.Sleeping;
        _animator.SetBool(Sleeping, true);
        powerTime -= difficultyRate;
        minPowerTime = Math.Max(minPowerTime - difficultyRate, 0);
        maxPowerTime = Math.Max(maxPowerTime - difficultyRate, 0);
    }

    private void AwakeWorker()
    {
        _audioSource.Play();
        status = Status.Awake;
        _animator.SetBool(Exhausted, false);
        _animator.SetBool(Sleeping, false);
    }
}
