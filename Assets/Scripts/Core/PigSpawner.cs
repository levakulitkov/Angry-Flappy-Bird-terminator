using System;
using System.Collections;
using UnityEngine;

public class PigSpawner : MonoBehaviour
{
    public event Action<Pig> PigDied;

    [SerializeField] private float _minSpawnDelay;
    [SerializeField] private float _maxSpawnDelay;
    [SerializeField] private PigPool _pigPool;

    private FiringPosition[] _firingPositions;
    private Coroutine[] _coroutines;

    private void Start()
    {
        _firingPositions = GetComponentsInChildren<FiringPosition>();
        _coroutines = new Coroutine[_firingPositions.Length];
    }

    public void StartSpawning()
    {
        for (int i = 0; i < _firingPositions.Length; i++)
            _coroutines[i] = StartCoroutine(SpawnWithDelay(_firingPositions[i]));
    }

    public void Reset()
    {
        foreach (Coroutine coroutine in _coroutines)
            StopCoroutine(coroutine);

        _pigPool.Reset();
    }

    private void OnPigDied(Pig pig)
    {
        pig.Died -= OnPigDied;

        PigDied?.Invoke(pig);

        int firingPositionIndex = Array.IndexOf(_firingPositions, pig.FiringPosition);

        _pigPool.Release(pig);

        _coroutines[firingPositionIndex] = StartCoroutine(SpawnWithDelay(_firingPositions[firingPositionIndex]));
    }

    private IEnumerator SpawnWithDelay(FiringPosition firingPosition)
    {
        yield return new WaitForSeconds(UnityEngine.Random.Range(_minSpawnDelay, _maxSpawnDelay));

        Pig pig = _pigPool.Get();

        pig.Died += OnPigDied;

        pig.AssignFiringPosition(firingPosition);
    }
}
