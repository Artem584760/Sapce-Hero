    using System;
    using System.Collections;
    using Unity.VisualScripting;
    using UnityEngine;

public class Poison : MonoBehaviour
{
    private string damageType = "Poison";
    
    public void StartPoisoning(float interval, float damage, float effectTime, float resistDamage,short effectLvl)
    {
        StartCoroutine(PoisonCoroutine(interval, damage, effectTime, resistDamage,effectLvl));
    }

    private IEnumerator PoisonCoroutine(float interval, float damage, float burnTime, float resistDamage,short effectLvl)
    {
        Component target = GetComponent<Enemy>() as Component ?? GetComponent<Player>();
        AdjustResistances(target, -resistDamage);

        damage *= effectLvl;

        var spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        if (spriteRenderer != null) 
        {
            spriteRenderer.color = Color.magenta;
        }

        while (burnTime > 0)
        {
            yield return new WaitForSeconds(interval);
            if (target is Enemy enemy)
            {
                enemy.EnemyGiveDamage(damage, damageType);
            }
            else if (target is Player player)
            {
                player.PlayerGiveDamage(damage, damageType);
            }
            burnTime -= interval;
        }

        AdjustResistances(target, resistDamage);

        if (spriteRenderer != null && GetComponents<Poison>().Length == 1) 
        {
            spriteRenderer.color = Color.white;
        }

        Destroy(this);
    }

    private void AdjustResistances(Component target, float adjustValue)
    {
       
        if (target is Enemy enemy)
        {
            for (int i = 0; i < enemy.resists.Length; i++)
            {
                if (enemy.resists[i].name != damageType)
                {
                    enemy.resists[i].resist += adjustValue;
                }
                
            }
        }
        else if (target is Player player)
        {
            for (int i = 0; i < player.resists.Length; i++)
            {
                if (player.resists[i].name != damageType)
                {
                    player.resists[i].resist += adjustValue;
                }
               
            }
        }
    }
}