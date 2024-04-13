using UnityEngine;
using UnityEngine.UI;

public class StartScreen : Screen
{
    [SerializeField] private Button _startGameButton;

    public override void Close()
    {
        base.Close();

        _startGameButton.enabled = false;
    }

    public override void Open()
    {
        base.Open();

        _startGameButton.enabled = true;
    }
}
