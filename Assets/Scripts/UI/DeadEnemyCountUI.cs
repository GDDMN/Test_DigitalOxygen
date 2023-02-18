using UnityEngine;
using UnityEngine.UI;

public class DeadEnemyCountUI : MonoBehaviour
{
    public Text text;
    public int count = 0;

    public void Increase()
    {
        count++;
        text.text = count.ToString();
    }
}
