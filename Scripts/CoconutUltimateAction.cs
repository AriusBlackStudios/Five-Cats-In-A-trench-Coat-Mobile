using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "CharacterAction/CoconutUltimate")]

public class CoconutUltimateAction : CharacterAction
{
     public string EnemyTag = "Enemy";
    
    public GameObject[] enemiesInScene;

    bool doOnce;
        private void Awake() 
    {
        enemiesInScene = GameObject.FindGameObjectsWithTag(EnemyTag);
    }
    public override void DoAction(PlayerController cat)
    {
        if (cat.isInteracting) return; //cannot do an action again
        cat.isUsingUlt = true;
        Debug.Log("Coconut Does her ultimate");
        base.DoAction(cat);
        StunEnemies();



        

    }

    public override void StopAction(PlayerController cat)
    {
        Debug.Log("Coconut Finished Her Ult");
        cat.isUsingUlt = false;
        base.StopAction(cat);
        ReleaseEnemies();


    }

    private void ReleaseEnemies(){
        if (doOnce) return;
        foreach (GameObject go in enemiesInScene)
        {
            //find enemy manager
            //set enemy target to this cat
        }
    }



    public void StunEnemies(){
        foreach (GameObject go in enemiesInScene)
        {
            //find enemy manager
            //stun enemy
        }
    }
}
