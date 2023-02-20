using UnityEngine;
using UnityEngine.UI;

public class EnemyCountText : MonoBehaviour
{
    public Slider slider;
    public Text text;

    public void ShowCount()
    {
        int value = (int)slider.value;
        text.text = "Enemy count: " + value.ToString();
    }
}
