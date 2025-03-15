using UnityEngine;

public class GunAnimation : MonoBehaviour
{
   
   public Animator anim;

   //check this bool before running anything that shoots, it prevents firing mid animation
   public bool animationInPro=false;
   //For testing
     /*  void Update()
       {
           if (Input.GetKeyDown(KeyCode.B)) 
           {
               shoot();
           }
   
           if (Input.GetKeyDown(KeyCode.G)) 
           {
               reload();
           }
       }*/
   
       public void shoot()
       {
           if (!animationInPro)
           {
               anim.SetTrigger("shoot");
           }
           
       }
   
       public void reload()
       {
           if (!animationInPro)
           {
               anim.SetTrigger("reload");
           }
       }

       public void AnimationFin()
       {
           animationInPro = false;
       }
   
}
