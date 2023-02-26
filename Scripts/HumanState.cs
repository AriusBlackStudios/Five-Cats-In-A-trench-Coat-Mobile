using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HumanState : MonoBehaviour
{
    public abstract HumanState State(HumanManager human);
}
