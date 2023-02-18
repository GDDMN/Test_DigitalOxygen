using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayButtonScript : MonoBehaviour
{
    public EnemySpawnManager spawnManager;
    public InputField inputField;
    public PlayerController player;

    public HealthbarUI healthBar;

    public void StartGame()
    {
        string value = inputField.text;
        int enemyCount = Int32.Parse(value);
        if (enemyCount <= 0)
            return;

        spawnManager.EnemyCount = enemyCount;
        spawnManager.StartInitialization();
        player.GameStarted = true;

        healthBar.gameObject.SetActive(true);
        inputField.gameObject.SetActive(false);
        this.gameObject.SetActive(false);
    }
}
