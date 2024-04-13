using System;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    public event Action<int> Changed;

    [SerializeField] private PigSpawner _pigSpawner;

    private int _score;

    private void OnEnable()
    {
        _pigSpawner.PigDied += OnPigDied;
    }

    private void OnDisable()
    {
        _pigSpawner.PigDied -= OnPigDied;
    }

    public void Reset()
    {
        _score = 0;

        Changed?.Invoke(_score);
    }

    private void OnPigDied(Pig pig)
    {
        _score += pig.PointsCountForKill;
        Changed?.Invoke(_score);
    }
}
