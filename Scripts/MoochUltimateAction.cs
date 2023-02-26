using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "CharacterAction/MoochUltimate")]
public class MoochUltimateAction : CharacterAction
{
    public float chargeSpeed=10;
    public override void DoAction(PlayerController cat)
    {
        if (cat.isInteracting) return; //cannot do an action again
        Debug.Log("Mooch Is Raging");
        base.DoAction(cat);

        cat.isUsingUlt = true;
        cat.currentSpeed=chargeSpeed;

        //handle collisions with other stuff some how?

    }

        public override void StopAction(PlayerController cat)
    {
        if (!cat.isInteracting) return; //cannot do an action again
        Debug.Log("Mooch Is Done Raging");

        base.StopAction(cat);
        cat.isUsingUlt = false;

        cat.currentSpeed=cat.movementSpeed;

    }
}
