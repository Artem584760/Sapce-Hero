using System.Collections;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public void StartBurning(float interval,float damage,float burnTime,short effectLvl)
    {
        StartCoroutine(Burn(interval, damage, burnTime,effectLvl));
    }

    IEnumerator Burn(float interval, float damage, float effectTime, short effectLvl)
    {
        Component target = GetComponent<Enemy>() ?? (Component)GetComponent<Player>();
        
        if (target != null)
        {
           damage *= effectLvl;
           SpriteRenderer spriteRenderer = target.GetComponentInChildren<SpriteRenderer>();
           
	       spriteRenderer.color = Color.yellow;

           while (effectTime > 0)
           {
               yield return new WaitForSeconds(interval);

               if (target is Enemy enemy)
               {
                   enemy.EnemyGiveDamage(damage, "Fire");
               }
               else if (target is Player player)
               {
                   player.PlayerGiveDamage(damage, "Fire");
               }

              effectTime -= interval;
           }

           spriteRenderer.color = Color.white;
            
        }
        Destroy(this); 
    }
}
