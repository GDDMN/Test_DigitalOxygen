using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameSceneManager : MonoBehaviour
{
    public PlayerController player;
    public EnemySpawnManager enemySpawner;
    public GameObject looseWinWindow;
    public Image lose;
    public Image win;

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
        win.gameObject.SetActive(true);
    }

    private void Lose()
    {
        looseWinWindow.SetActive(true);
        lose.gameObject.SetActive(true);
    }

    public void RestartScene()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
