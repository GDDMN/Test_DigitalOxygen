using UnityEngine;
using UnityEngine.UI;

public class DeadEnemyCountUI : MonoBehaviour
{
    public Text text;
    private int count = 0;

    public void Increase()
    {
        count++;
        text.text = count.ToString();
    }
}
