using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "CharacterAction/BossSkill")]
public class BossSkillAction : CharacterAction
{
    public float skillRadius;
    public string XRayTag = "XRAY";

    public float targetOpacity;
    public LayerMask interactionLayer;
    public GameObject XRayObject;

    bool doOnce;

 


     public override void DoAction(PlayerController cat)
    {
        if (cat.isInteracting) return; //cannot do an action again
        Debug.Log("Boss is using His Skill");
        base.DoAction(cat);
        cat.isUsingSkill =true;

        //enable xray vision
        DetectXRayAbleObjects(cat);
        SetOpacity(targetOpacity,true);


        //serch for all tagged objects in radius of skill, set item opasity lower to "see through it" 

    }


    public override void StopAction(PlayerController cat)
    {
        Debug.Log("Boss is Finished with his Skill");
        cat.isUsingSkill =false;
        base.StopAction(cat);
        doOnce=false;
        SetOpacity(1f);

        




    }


    private void DetectXRayAbleObjects(PlayerController cat){
        XRayObject = cat.DetectWithOverLapSphere(XRayTag,skillRadius,interactionLayer);

    }


    private void SetOpacity(float opacity,bool toggle=false)
    {
        if(doOnce)return;
        if (XRayObject != null)
        {
            SpriteRenderer image = XRayObject.GetComponent<SpriteRenderer>();
            if(image!= null)
            {
                image.color = new Color(1f,1f,1f,opacity);
                doOnce =toggle;
            }
        }

        
       
    }
}
