using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DazeState : HumanState
{
    public HumanState followCat;
    public HumanState patrolState;
     public override HumanState State(HumanManager human)
    {
        return this;
        
    }

}
