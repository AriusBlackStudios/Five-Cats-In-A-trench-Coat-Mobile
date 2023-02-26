using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAction : ScriptableObject
{
    public string anim_CharacterActionBoolName="isInvisible";
    public Sprite skillIcon;
    public virtual void DoAction(PlayerController cat)
    {
        Debug.Log(" Action Performed");
        cat.isInteracting = true;
        cat.UpdateAnimBools(anim_CharacterActionBoolName,true);


    }

        public virtual void StopAction(PlayerController cat)
    {
        Debug.Log(" Action Canceled");
        cat.isInteracting = false;
        cat.UpdateAnimBools(anim_CharacterActionBoolName,false);


    }
}

