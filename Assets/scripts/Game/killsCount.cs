using TMPro;
using UnityEngine;

public class KillsCount : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI killsCountText;

    public int killsCount;

    public void UpdateKillsCountText()
    {
        killsCountText.text = killsCount.ToString();
    }
    
}
