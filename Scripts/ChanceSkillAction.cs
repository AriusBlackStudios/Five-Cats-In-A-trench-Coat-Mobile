using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "CharacterAction/ChanceSkill")]
public class ChanceSkillAction : CharacterAction
{

    //skill description, Crawl
    public bool canStand;
    public float crawlingDetection=2.5f;

    public float canStandHeight = 2.5f;
    
    public override void DoAction(PlayerController cat)
    {
        if (cat.isInteracting) return; //cannot do an action again
        Debug.Log("Chance is sneaking...");
        base.DoAction(cat);




        ToggleColliders(cat);
        CheckAboveHead(cat);
  
        cat.currentSpeed = cat.movementSpeed/2;

    }

    public override void StopAction(PlayerController cat)
    {
        CheckAboveHead(cat);
        if(!canStand){return;}
        
        Debug.Log("Chance is no longer Sneaking...");
        base.StopAction(cat);
        

        

        ToggleColliders(cat);
        cat.currentSpeed = cat.movementSpeed;
        cat.detectionRadius = cat.maxDetection;
    }

    private void ToggleColliders(PlayerController cat){
        cat.characterCrouchingCollider.enabled =!cat.characterCrouchingCollider.enabled;
        cat.characterStandingCollider.enabled = !cat.characterStandingCollider.enabled;
    }
     private void CheckAboveHead(PlayerController cat){
        // Cast a ray straight down.
        RaycastHit2D hit = Physics2D.Raycast(cat.transform.position, Vector2.up);

        // If it hits something...
        if (hit.collider != null)
        {
            // Calculate the distance from the surface and the "error" relative
            // to the floating height.
            float distance = Mathf.Abs(hit.point.y - cat.transform.position.y);
            if(distance< canStandHeight){
                canStand= false;
                cat.detectionRadius = crawlingDetection;
            }
            else{
                canStand=true;
                cat.detectionRadius = 0; //in hiding, cannot be detected
            }


        }
    }
}
