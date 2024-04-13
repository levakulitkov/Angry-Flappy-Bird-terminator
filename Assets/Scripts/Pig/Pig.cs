using System;
using System.Collections;
using UnityEngine;

public class Pig : MonoBehaviour
{
    public event Action<Pig> Died;

    [SerializeField] private float _speed;
    [SerializeField] private float _randomDelayBeforeShoot;
    [SerializeField] private int _pointsCountForKill;

    private bool _isMoving;
    private Gun _gun;
    private Coroutine _coroutine;

    public int PointsCountForKill => _pointsCountForKill;
    public FiringPosition FiringPosition { get; private set; }

    private void Awake()
    {
        _gun = GetComponentInChildren<Gun>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out BirdBullet bullet))
        {
            bullet.ReachTarget();

            Die();
        }
    }

    public void Reset()
    {
        StopCoroutine(_coroutine);

        _gun.Reset();
    }

    public void AssignFiringPosition(FiringPosition firingPosition)
    {
        FiringPosition = firingPosition;

        if (!_isMoving)
            _coroutine = StartCoroutine(MoveToFiringPosition());
    }

    public void Die()
    {
        _isMoving = false;

        Died?.Invoke(this);
    }

    private IEnumerator MoveToFiringPosition()
    {
        _isMoving = true;

        while (enabled)
        {
            if (transform.position == FiringPosition.transform.position)
                break;

            transform.position = Vector3.MoveTowards(transform.position, FiringPosition.transform.position, _speed * Time.deltaTime);

            yield return null;
        }

        _isMoving = false;

        _coroutine = StartCoroutine(Firing());
    }

    private IEnumerator Firing()
    {
        var waitCooldown = new WaitUntil(() => _gun.IsCooldownState);
        
        while (enabled)
        {
            yield return new WaitForSeconds(UnityEngine.Random.Range(0, _randomDelayBeforeShoot));

            _gun.TryShoot();

            yield return waitCooldown;
        }
    }
}
