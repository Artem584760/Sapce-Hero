using System;
using UnityEngine;

public class ChooseWeapon : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject startCanvas;
    [SerializeField] private GameObject gameCanvas;
    
    [Header("PrefabsArray")]
    public GameObject[] weapons;

    private void Awake()
    {
        Time.timeScale = 0;
    }

    public void SelectWeapon(int index)
    {
        Time.timeScale = 1f;
       startCanvas.SetActive(false);
        gameCanvas.SetActive(true);
        Instantiate(weapons[index], transform.position, Quaternion.identity, transform);
    }
}