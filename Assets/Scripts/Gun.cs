using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private float _cooldown;
    [SerializeField] private BulletPool _bulletPool;

    public bool IsCooldownState { get; private set; }

    private void OnEnable()
    {
        if (IsCooldownState)
            IsCooldownState = false;
    }

    public void Reset()
    {
        _bulletPool.Reset();
    }

    public bool TryShoot()
    {
        if (IsCooldownState)
            return false;

        StartCoroutine(SetCooldownState());

        _bulletPool.Get();

        return true;
    }

    private IEnumerator SetCooldownState()
    {
        IsCooldownState = true;

        yield return new WaitForSeconds(_cooldown);

        IsCooldownState = false;
    }
}
