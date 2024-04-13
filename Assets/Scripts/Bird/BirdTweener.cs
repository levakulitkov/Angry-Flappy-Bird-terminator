using UnityEngine;
using DG.Tweening;

public class BirdTweener : MonoBehaviour
{
    [SerializeField] private Bird _bird;
    [SerializeField] private float _amplitude;
    [SerializeField] private float _duration;

    private Tween _tween;

    public void Run()
    {
        Stop();

        _tween = _bird.transform
            .DOMoveY(_amplitude, _duration)
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Yoyo);
    }

    public void Stop()
    {
        if (_tween != null) 
            DOTween.Kill(_bird.transform);
    }
}
