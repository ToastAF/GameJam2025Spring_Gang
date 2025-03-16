using System;
using UnityEngine;

public class BBExplosionDamage : MonoBehaviour
{
    public int Damage;

   private void OnTriggerEnter(Collider other)
   {
      if (other.CompareTag("Enemy"))
      {
         other.GetComponent<EnemyHealth>().TakeDamage(Damage);
      }
   }
}
