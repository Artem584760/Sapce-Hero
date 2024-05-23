using System;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
   [Header("Prefabs")] [SerializeField] private Slider healthSlider;


   [Header("Values")] public float expMagnetSpeed;
   public float expMagnetRadius;

   public float currentHealth;
   public float maxHealth;
   
   [Serializable] public struct Resists
   {   
      public string name;
      public float resist;
   }
    
   public Resists[] resists;
   private string[] damageTypes;


   void Awake()
   {
      currentHealth = maxHealth;
      GameObject.FindWithTag("ExpMagnetZone").GetComponent<CircleCollider2D>().radius = expMagnetRadius;
      UpdateHealthValue();
   }

   private void Start()
   {
      GameObject weapon = GameObject.FindWithTag("Weapon");
      if (weapon != null)
      {
          weapon.transform.localPosition = Vector3.zero;
      }
      
      damageTypes = new string[resists.Length];
      for (int i = 0; i < resists.Length; i++)
      {
         damageTypes[i] = resists[i].name;
      }
     
   }
   
   private void OnCollisionStay2D(Collision2D other)
   {
      if (other.gameObject.CompareTag("Enemy"))
      {
         PlayerGiveDamage(other.gameObject.GetComponent<Enemy>().damageInSecond / 100, "Physic");
      }
   }

   private void UpdateHealthValue()
   {
      healthSlider.value = Mathf.Clamp01(currentHealth / maxHealth);
      if (currentHealth <= 0)
      {
         Death();
      }
   }

   public void PlayerGiveDamage(float damage,string damageType)
   {
      
      if (!damageTypes.Contains(damageType))
      {
         Debug.Log($"Тип урона '{damageType}' не был найден");
      }

      float resist = resists[Array.IndexOf(damageTypes, damageType)].resist;

   
      if (damage >= 0)
      {
        damage = damage * (10 / (10 + resist));
      }
      else
      {
         damage = damage * (2 - 10 / (10 + resist));
      }

      
      
      currentHealth -= damage;
      UpdateHealthValue();
   }

   private void Death()
   {
      SceneManager.LoadScene(0);
   }
}
