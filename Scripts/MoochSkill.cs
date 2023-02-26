using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "CharacterAction/MoochSkill")]
public class MoochSkill : CharacterAction
{

    public float skillRadius =1f;
    public string pushableobject;
    public GameObject pushableItem;
    public bool foundItem;
    public LayerMask interactionLayer;

    public override void DoAction(PlayerController cat)
    {
        if (cat.isInteracting) return; //cannot do an action again
        base.DoAction(cat);
        FindItemToPush(cat);
        

            
        

    }

    public override void StopAction(PlayerController cat)
    {
        Debug.Log("Mooch is no longer Pushing...");
        base.StopAction(cat);
        ReleaseItem(cat);


    }

    public void FindItemToPush(PlayerController cat)
    {

        pushableItem =cat.detectItemWithRaycast(pushableobject,skillRadius,interactionLayer);
        if(pushableItem!=null){
            Debug.Log("Mooch is Pushing something heavy...");
            cat.currentSpeed = cat.movementSpeed/2;
            pushableItem.transform.SetParent(cat.transform);
            foundItem=true;
            cat.UpdateAnimBools(anim_CharacterActionBoolName,foundItem);
            cat.canRotate =false;
        }
        else
        {
            foundItem=false;
            Debug.Log("Mooch doesnt see anything he can push");
            cat.UpdateAnimBools(anim_CharacterActionBoolName,foundItem);
            cat.canRotate= true;
        }

    }


    public void ReleaseItem(PlayerController cat)
    {
        pushableItem.transform.SetParent(null);
        pushableItem = null;
        foundItem=false;
        cat.currentSpeed = cat.movementSpeed;
        cat.UpdateAnimBools(anim_CharacterActionBoolName,foundItem);
        cat.canRotate= true;
    }

}
