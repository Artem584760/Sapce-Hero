using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Expreince : MonoBehaviour
{ 
    public GameObject ExpCrystalPrefab;
    public float currentExp, needExpToNextLevel;
    public Slider expProgressBar;
    private short currentLvl = 1;
    [SerializeField] private TextMeshProUGUI lvlText;
    
    
   public void UpgradeExpValue(float value)
   {
       currentExp += value;
       float remainder = 0;
       if (currentExp >= needExpToNextLevel)
       {
           remainder = currentExp - needExpToNextLevel;
           currentExp = 0;
           needExpToNextLevel *= 1.2f;
           
           currentLvl++;
           lvlText.text = currentLvl + " lvl";

       }
       expProgressBar.value = Mathf.Clamp01((currentExp + remainder) / needExpToNextLevel);
   }
}
