using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "CharacterAction/Chance Ult")]
public class ChanceUltimate : CharacterAction
{

    public override void DoAction(PlayerController cat)
    {
        if (cat.isInteracting) return; //cannot do an action again
        Debug.Log("Chance is Invisible...");
        base.DoAction(cat);

        cat.isUsingUlt = true;

        cat.detectionRadius = 0;
        cat.UseStaminaForUlt();


    }

        public override void StopAction(PlayerController cat)
    {
        if (!cat.isInteracting) return; //cannot do an action again
        Debug.Log("Chance is no longer Invisible...");

        base.StopAction(cat);
        cat.isUsingUlt = false;


        cat.detectionRadius = cat.maxDetection;


    }
 
}
