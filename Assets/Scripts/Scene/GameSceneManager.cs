using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    public PlayerController player;
    public EnemySpawnManager enemySpawner;
    public GameObject looseWinWindow;


    private void Start()
    {
        player.OnDeath += Lose;
        enemySpawner.OnEveryEnemyDeth += Win;
    }

    private void OnDestroy()
    {
        player.OnDeath -= Lose;
        enemySpawner.OnEveryEnemyDeth -= Win;
    }

    private void OnDisable()
    {
        player.OnDeath -= Lose;
        enemySpawner.OnEveryEnemyDeth -= Win;
    }

    private void Win()
    {
        looseWinWindow.SetActive(true);
    }

    private void Lose()
    {
        looseWinWindow.SetActive(true);
    }

    public void RestartScene()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
