using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CharacterAction/BossUltimate")]
public class BossUltAction : CharacterAction
{
    public GameObject lazerbeamPREFAB;

    public float lazerBeamRange;
    GameObject lazerbeamInstance;
     public override void DoAction(PlayerController cat)
    {
        if (cat.isInteracting) return; //cannot do an action again
        cat.isUsingUlt = true;
        Debug.Log("Boss is Performing His Ult");
        base.DoAction(cat); 
        lazerbeamInstance = Instantiate(lazerbeamPREFAB,cat.transform);
        //get the line renderer component
        //expand line renderer
        

    }

    public override void StopAction(PlayerController cat)
    {
        Debug.Log("Boss is Finished His Ult.");
        cat.isUsingUlt = false;
        base.StopAction(cat);
        GameObject toBeDestroyed = lazerbeamInstance;
        lazerbeamInstance=null;
        Destroy(toBeDestroyed);
        


    }
}
