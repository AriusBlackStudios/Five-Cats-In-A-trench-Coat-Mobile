using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "CharacterAction/CoconutSkill")]
public class CoconutSkillAction : CharacterAction
{

    public float skillRadius;
    public float stunPhaseDurration;
    public string EnemyTag = "Enemy";
    public List<GameObject> attractedEnemies;
    public GameObject[] enemiesInScene;

    bool doOnce;

    private void Awake() 
    {
        enemiesInScene = GameObject.FindGameObjectsWithTag(EnemyTag);
    }
    public override void DoAction(PlayerController cat)
    {
        if (cat.isInteracting) return; //cannot do an action again
        cat.isUsingSkill = true;
        Debug.Log("Coconut Uses Her Skill");
        base.DoAction(cat);
        AttaractLocalEnemy(cat);
        LeadEnemies();
        doOnce=true;
    }

    public override void StopAction(PlayerController cat)
    {
        Debug.Log("coconut stops using her skill");
        cat.isUsingSkill = false;
        base.StopAction(cat);
        doOnce = false;
        StunEnemies();
        attractedEnemies.Clear();



    }

    private void AttaractLocalEnemy(PlayerController cat){
        foreach( GameObject go in enemiesInScene)
        {
            float distance = (go.transform.position - cat.transform.position).magnitude;
            if(Mathf.Abs(distance) <= skillRadius)
            {
                attractedEnemies.Add(go);
            }
        }
    }

    private void LeadEnemies(){
        if (doOnce) return;
        foreach (GameObject go in attractedEnemies)
        {
            //find enemy manager
            //set enemy target to this cat
        }
    }



    public void StunEnemies(){
        foreach (GameObject go in attractedEnemies)
        {
            //find enemy manager
            //stun enemy
        }
    }

}
