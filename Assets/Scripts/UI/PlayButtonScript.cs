using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayButtonScript : MonoBehaviour
{
    public EnemySpawnManager spawnManager;
    public Slider slider;
    public PlayerController player;
    public Image menuImage;

    public HealthbarUI healthBar;

    public void StartGame()
    {
        int enemyCount = (int)slider.value;
        if (enemyCount <= 0)
            return;

        spawnManager.EnemyCount = enemyCount;
        spawnManager.StartInitialization();
        player.GameStarted = true;

        healthBar.gameObject.SetActive(true);
        menuImage.gameObject.SetActive(false);
        slider.gameObject.SetActive(false);
        this.gameObject.SetActive(false);
    }
}
