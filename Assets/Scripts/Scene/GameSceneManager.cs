using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    public PlayerController player;
    public EnemySpawnManager enemySpawner;

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

    public void Win()
    {
        RestartScene();
    }

    public void Lose()
    {
        RestartScene();
    }

    private void RestartScene()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
