using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Player _player;

    private void Awake()
    {
        Time.timeScale = 1;
    }

    private void OnEnable()
    {
        _player.OnDeathEvent += GameOver;
    }

    private void GameOver()
    {
        Time.timeScale = 0;
    }

    private void OnDisable()
    {
        _player.OnDeathEvent -= GameOver;
    }
}
