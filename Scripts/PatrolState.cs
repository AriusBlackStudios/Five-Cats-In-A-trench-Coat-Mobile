using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : HumanState
{

    public HumanState followState;

    public Transform[] patrolWaypoints;

    bool catFound;
    public override HumanState State(HumanManager human)
    {
        DetectCat();
        PatrolRoute();
        if(human.catTarget != null)
        {
            return followState;
        }
        else
        { 
            return this;
        }

        
    }

    private void DetectCat()
    {

    }

    private void PatrolRoute()
    {

    }
}
