using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameOverScreen : Screen
{
    [SerializeField] private Button _restartGameButton;

    public override void Close()
    {
        base.Close();

        _restartGameButton.enabled = false;
    }

    public override void Open()
    {
        base.Open();

        _restartGameButton.enabled = true;
    }
}
