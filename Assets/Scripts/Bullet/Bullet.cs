using System;
using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    public event Action<Bullet> TargetReached;

    [SerializeField] private float _speed;

    private void Update()
    {
        transform.position += _speed * Time.deltaTime * transform.up;
    }

    public void ReachTarget()
    {
        TargetReached?.Invoke(this);
    }
}
