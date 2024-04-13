using UnityEngine;

public abstract class Screen : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;

    public virtual void Open()
    {
        _canvasGroup.alpha = 1f;
        _canvasGroup.blocksRaycasts = true;
    }

    public virtual void Close()
    {
        _canvasGroup.alpha = 0f;
        _canvasGroup.blocksRaycasts = false;
    }
}
