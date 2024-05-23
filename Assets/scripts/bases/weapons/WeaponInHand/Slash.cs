using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class SlayerSlash : ProjectileBase
{
    private float increaseSpeed;
    private void Start()
    {
        WeaponInHand _weapon = transform.parent.GetComponent<WeaponInHand>();
        speed = _weapon.speed;
        damage = _weapon.damage;
        lifeTime = _weapon.lifeTime;
        increaseSpeed = _weapon.increaseSpeed;
        
        base.Start();
    }
   private void Update()
    {
        base.Update();
        transform.localScale += Vector3.one * increaseSpeed * Time.deltaTime;
        
    }

   private void OnTriggerEnter2D(Collider2D other)
  {
      base.OnTriggerEnter2D(other,"Physic");
      if (other.CompareTag("Enemy"))
      {
          Destroy(gameObject);
      }
  }
}
