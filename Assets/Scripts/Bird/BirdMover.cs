using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BirdMover : MonoBehaviour
{
    [SerializeField] private Vector2 _startPosition;
    [SerializeField] private float _speed = 3f;
    [SerializeField] private float _gravityScale = 4f;
    [SerializeField] private float _flapForce = 15f;
    [SerializeField] private float _rotationUpSpeed = 600f;
    [SerializeField] private float _rotationDownSpeed = 200f;
    [SerializeField] private float _minRotationZ = -60f;
    [SerializeField] private float _maxRotationZ = 40f;
    [SerializeField] private float _delayBeforeRotationDown = 0.3f;

    private Rigidbody2D _rigidbody2D;
    private Quaternion _maxRotation;
    private Quaternion _minRotation;
    private bool _isGameStarted;
    private bool _isRotationUpState;
    private bool _isFreezedRotationDown;
    private Coroutine _freezingRotationDownCoroutine;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();

        _maxRotation = Quaternion.Euler(0, 0, _maxRotationZ);
        _minRotation = Quaternion.Euler(0, 0, _minRotationZ);
    }

    private void Start()
    {
        Reset();
    }

    public void Flap()
    {
        if (!_isGameStarted)
        {
            _isGameStarted = true;

            _rigidbody2D.gravityScale = _gravityScale;

            StartCoroutine(RotateDownRoutine());
        }

        _rigidbody2D.velocity = new Vector2(_speed, _flapForce);

        RotateUp();
    }

    public void Reset()
    {
        _isGameStarted = false;
        _isRotationUpState = false;

        _rigidbody2D.gravityScale = 0f;
        _rigidbody2D.velocity = Vector2.zero;

        transform.SetPositionAndRotation(_startPosition, Quaternion.identity);
    }

    private void RotateUp()
    {
        if (_freezingRotationDownCoroutine != null)
            StopCoroutine(_freezingRotationDownCoroutine);

        _freezingRotationDownCoroutine = StartCoroutine(FreezeRotationDown());

        if (!_isRotationUpState)
            StartCoroutine(SetRotationUpState());
    }

    private void RotateDown()
    {
        if (!_isRotationUpState && !_isFreezedRotationDown)
            transform.rotation = Quaternion.RotateTowards(transform.rotation, _minRotation, _rotationDownSpeed * Time.deltaTime);
    }

    private IEnumerator RotateDownRoutine()
    {
        while (_isGameStarted)
        {
            RotateDown();

            yield return null;
        }
    }

    private IEnumerator SetRotationUpState()
    {
        _isRotationUpState = true;

        while (_isRotationUpState)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, _maxRotation, _rotationUpSpeed * Time.deltaTime);
            yield return null;

            if (transform.rotation == _maxRotation)
                _isRotationUpState = false;
        }
    }

    private IEnumerator FreezeRotationDown()
    {
        _isFreezedRotationDown = true;

        yield return new WaitForSeconds(_delayBeforeRotationDown);

        _isFreezedRotationDown = false;
    }
}