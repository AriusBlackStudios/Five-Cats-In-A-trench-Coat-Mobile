using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CharacterAction/MacchiatoUltimate")]
public class MacchiatoUltimateSkill : CharacterAction
{

    public float roombaSpeed;
    public GameObject roomba;
    public Vector2 roombaDesiredVelocity;
    public override void DoAction(PlayerController cat)
    {
        if (cat.isInteracting) return; //cannot do an action again
        Debug.Log("Machiatto Is riding a roomba!");
        base.DoAction(cat);

        cat.isUsingUlt = true;


        cat.currentSpeed=roombaSpeed;
        cat.UpdateAnimBools(anim_CharacterActionBoolName,true);

        //handle collisions with other stuff some how?

    }

    public override void StopAction(PlayerController cat)
    {
        if (!cat.isInteracting) return; //cannot do an action again
        Debug.Log("Machiatto Has Released the Roomba");

        base.StopAction(cat);
        cat.isUsingUlt = false;

        roombaDesiredVelocity = cat.rb.velocity;
        cat.UpdateAnimBools(anim_CharacterActionBoolName,false);
        GameObject instanceRoomba = Instantiate(roomba,cat.transform.position,Quaternion.identity);
        Rigidbody2D roomba_rb =  instanceRoomba.GetComponent<Rigidbody2D>();
        roomba_rb.velocity = roombaDesiredVelocity;
        cat.currentSpeed=cat.movementSpeed;

    }
}
