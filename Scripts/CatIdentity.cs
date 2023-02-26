using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Five Cats/Cat Identity")]
public class CatIdentity : ScriptableObject
{
    public string catRealName;
    public string catCodeName;
    public CharacterAction ultimatAction;
    public CharacterAction skillAction;
    public GameObject catSprite;
}
