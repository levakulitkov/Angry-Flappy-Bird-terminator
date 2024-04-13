using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private Bird _bird;
    [SerializeField] private BirdTweener _birdTweener;
    [SerializeField] private PigSpawner _pigSpawner;
    [SerializeField] private StartScreen _startScreen;
    [SerializeField] private GameOverScreen _gameOverScreen;
    [SerializeField] private ScoreCounter _scoreCounter;

    private void OnEnable()
    {
        _bird.GameOver += OnGameOver;
    }

    private void OnDisable()
    {
        _bird.GameOver -= OnGameOver;
    }

    private void Start()
    {
        _gameOverScreen.Close();

        _startScreen.Open();

        _birdTweener.Stop();
        _birdTweener.Run();
    }

    public void OnStartGameButtonClick()
    {
        _startScreen.Close();

        _birdTweener.Stop();

        _pigSpawner.StartSpawning();
    }

    public void OnRestartGameButtonClick()
    {
        _gameOverScreen.Close();

        _bird.Reset();
        _pigSpawner.Reset();
        _scoreCounter.Reset();

        _startScreen.Open();

        _birdTweener.Run();

        Time.timeScale = 1;
    }

    private void OnGameOver()
    {
        Time.timeScale = 0;

        _gameOverScreen.Open();
    }
}
