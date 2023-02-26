using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCatState : HumanState
{
    public HumanState dazeState;
    public HumanState patrolState;


    public float maximumFollowDistance;
    public override HumanState State(HumanManager human)
    {
        if(human.catTarget == null) return patrolState;

        return this;
        
    }
}
