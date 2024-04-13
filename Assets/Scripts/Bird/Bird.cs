using System;
using UnityEngine;

[RequireComponent(typeof(BirdMover), typeof(BirdCollisionHandler))]
public class Bird : MonoBehaviour
{
    public event Action GameOver;

    private BirdMover _birdMover;
    private BirdCollisionHandler _birdCollisionHandler;
    private Gun _gun;

    private void Awake()
    {
        _birdMover = GetComponent<BirdMover>();
        _birdCollisionHandler = GetComponent<BirdCollisionHandler>();
        _gun = GetComponentInChildren<Gun>();
    }

    private void OnEnable()
    {
        _birdCollisionHandler.CollisionDetected += ProcessCollision;
    }

    private void OnDisable()
    {
        _birdCollisionHandler.CollisionDetected -= ProcessCollision;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            _birdMover.Flap();
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            _gun.TryShoot();
        }
    }

    public void Reset()
    {
        _birdMover.Reset();
        _gun.Reset();
    }

    public void ProcessCollision(IInteractable interactable)
    {
        if (interactable is Ground)
        {
            Die();
        }
        else if (interactable is PigBullet pigBullet)
        {
            pigBullet.ReachTarget();

            Die();
        }
    }

    public void Die()
    {
        GameOver?.Invoke();
    }
}