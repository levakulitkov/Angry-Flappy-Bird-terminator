using UnityEngine;
using UnityEngine.Pool;

public abstract class Pool<TPoolable> : MonoBehaviour where TPoolable : Behaviour
{
    [SerializeField] private TPoolable _template;
    [SerializeField] private Transform _startPosition;

    private Transform _container;
    private ObjectPool<TPoolable> _pool;

    private void Start()
    {
        _container = Camera.main.transform;
        _pool = new ObjectPool<TPoolable>(Create, OnGet, OnRelease, OnDestroyObject, false);
    }

    public virtual TPoolable Get()
        => _pool.Get();

    public virtual void Release(TPoolable poolable)
        => _pool.Release(poolable);

    public virtual void Reset()
    {
        Clear();
        TPoolable[] poolableObjects = FindObjectsOfType<TPoolable>(true);

        foreach (TPoolable poolable in poolableObjects)
            Destroy(poolable.gameObject);
    }

    private void Clear()
        => _pool.Clear();

    private TPoolable Create()
        => GameObject.Instantiate(_template, _startPosition.position, _startPosition.rotation * _template.transform.rotation, _container);

    private void OnGet(TPoolable poolable)
    {
        poolable.transform.position = _startPosition.position;
        poolable.transform.rotation = _startPosition.rotation * _template.transform.rotation;
        poolable.gameObject.SetActive(true);
    }

    private void OnRelease(TPoolable poolable)
        => poolable.gameObject.SetActive(false);

    private void OnDestroyObject(TPoolable poolable)
        => GameObject.Destroy(poolable);
}
