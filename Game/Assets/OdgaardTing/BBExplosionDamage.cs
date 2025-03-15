using System;
using UnityEngine;

public class BBExplosionDamage : MonoBehaviour
{
   private void OnTriggerEnter(Collider other)
   {
      if (other.CompareTag("Enemy"))
      {
         other.GetComponent<EnemyHealth>().TakeDamage(100);
      }
   }
}
