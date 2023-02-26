using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CharacterAction/MacchiatoSkill")]
public class MacchiatoSkillAction : CharacterAction
{

    public string hackableTag;
    public float skillradius;
    public float hackingspeed=5;

    public LayerMask interactionLayer;
    public GameObject hackableObject;

    public bool doOnce;
    public float ultStamina;
    public override void DoAction(PlayerController cat)
    {
        if (cat.isInteracting) return; //cannot do an action again
       
        FindItemToHack(cat);
        if(hackableObject != null)
        {
             base.DoAction(cat);
        }


    }

    public override void StopAction(PlayerController cat)
    {
        if (!cat.isInteracting) return;
        Debug.Log("Macchiato Has Stopped Hacking");
        base.StopAction(cat);
        
        ExitHacking(cat);



    }

    public void FindItemToHack(PlayerController cat)
    {
        hackableObject =cat.DetectWithOverLapSphere(hackableTag,skillradius,interactionLayer);
        if(hackableObject != null)
        {
            cat.currentSpeed = 0;
            HackItem(cat);

        }
        
    }
    public void HackItem(PlayerController cat){
        if (!doOnce){
            ultStamina =cat.stamina;
            cat.stamina=0;
            cat.staminaBar.value= cat.stamina;
            doOnce = true;
        }
        cat.stamina += hackingspeed *Time.deltaTime;
        cat.staminaBar.value= cat.stamina;
        if(cat.stamina >= cat.maxStamina)
        {
             StopAction(cat);
             Debug.Log("done hacking");
             
        }
    }

    public void ExitHacking(PlayerController cat)
    {
        doOnce= false;
        cat.stamina=ultStamina;
        cat.staminaBar.value= cat.stamina;
        cat.currentSpeed = cat.movementSpeed;
    }
}
