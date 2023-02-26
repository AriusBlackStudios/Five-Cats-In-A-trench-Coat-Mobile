using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using Mirror;
using Cinemachine;





public class PlayerController : NetworkBehaviour 
{

    public int coins;

    [Header("Switch Cat Input")]
    [SerializeField] InputAction switchCatInput0;
    [SerializeField] InputAction switchCatInput1;
    [SerializeField] InputAction switchCatInput2;
    [SerializeField] InputAction switchCatInput3;

    public CatIdentity[] switchCat = new CatIdentity[4];





    
    [Header("Cats")]
    public CatIdentity currentCat;
    public CatIdentity[] allCats;
  

    GameObject CatGraphic;

    [Space(10)]

    public bool isInteracting;
    Transform myAvatar;
    [SerializeField] CinemachineVirtualCamera virtualCamera;

    

    [Header("Stealth")]
    public float maxDetection =5;
    public float detectionRadius = 5;
    public float characterHeight;
    [Space(10)]


    [Header("Physics")]
    public Rigidbody2D rb;
    public Collider2D characterStandingCollider;
    public Collider2D characterCrouchingCollider;
    [SerializeField] InputAction movementInput;
    public float movementSpeed=5;
    public float currentSpeed=5;
    
    [Space(10)]
    Vector2 movementAmount;
    public CharacterAnimationController characterAnimator;

    public bool canRotate= true;
    


    [Header("Skill Input")]
    [SerializeField] InputAction characterSkillInput;
    [SerializeField] CharacterAction skillAction;
    public bool isUsingSkill;
    [Space(10)]


    [Header("Ultimate Input")]
    [SerializeField] InputAction CharacterUltInput;
    [SerializeField] CharacterAction ultimatAction;
    public bool isUsingUlt;


    [Header("ULT Stamina")]
    public float maxStamina;
    public float stamina;
    public Slider staminaBar;

    


    [Header("Item Interaction")]
    public Transform raycastOrigin;







    //functions 
    private void OnEnable() {
        movementInput.Enable();
        CharacterUltInput.Enable();
        characterSkillInput.Enable();
        switchCatInput0.Enable();
        switchCatInput1.Enable();
        switchCatInput2.Enable();
        switchCatInput3.Enable();
        UpdateToCat();
        CharacterUltInput.performed += DoCharacterUltimate;
        characterSkillInput.performed += DoCharacterSkill;
        CharacterUltInput.canceled += StopCharacterUltimate;
        characterSkillInput.canceled += StopCharacterSkill;
        switchCatInput0.performed += SwitchToCat0;
        switchCatInput1.performed += SwitchToCat1;
        switchCatInput2.performed += SwitchToCat2;
        switchCatInput3.performed += SwitchToCat3;

    }



    private void OnDisable() {
        movementInput.Disable();
        CharacterUltInput.Disable();
        characterSkillInput.Disable();
        switchCatInput0.Disable();
        switchCatInput1.Disable();
        switchCatInput2.Disable();
        switchCatInput3.Disable();
        
    }


    void Start()
    {
        
        InnitializeSwitchCatList();
    
        if(isLocalPlayer){
            virtualCamera = CinemachineVirtualCamera.FindObjectOfType<CinemachineVirtualCamera>();
            virtualCamera.Follow = this.transform;
            virtualCamera.LookAt = this.transform;
            staminaBar = FindObjectOfType<Slider>();
            Debug.Log("FOUND STAMINA BAR");
            if(staminaBar!= null){
                staminaBar.minValue= 0;
                staminaBar.maxValue = maxStamina;
                staminaBar.value=stamina;
            }
        }

    }

    private void InnitializeSwitchCatList(){

        int i =0;
        foreach( CatIdentity cat in allCats)
        {
            if (cat != currentCat && 1 < switchCat.Length-1){
                switchCat[i] =cat;
                i++;
            }
        }
    }

    private CatIdentity FindCatByName(string catName)
    {
        foreach( CatIdentity cat in allCats)
        {
            if (cat.catCodeName.ToLower() == catName.ToLower()){
                return cat;
            }
            else if (cat.catRealName.ToLower() == catName.ToLower()){
                return cat;
            }
        }
        return currentCat;
    }

    private void Update() 
    {
        if (!isOwned) return;
        movementAmount = movementInput.ReadValue<Vector2>();
        if(characterAnimator!= null)
        {
        characterAnimator.catAnim.SetFloat("movement",movementAmount.magnitude);
        }
        if(movementAmount.x != 0 && canRotate){
            transform.localScale= new Vector2(Mathf.Sign(movementAmount.x),1);  
        }
        if(stamina < maxStamina && !isUsingUlt && !isUsingSkill) 
        {
            stamina += Time.deltaTime;
            staminaBar.value= stamina;
        }
    }
    public void UseStaminaForUlt(){
         if (stamina > 0 && isUsingUlt)
        {
            stamina -= Time.deltaTime;
            staminaBar.value= stamina;
            if(stamina<=0 )ultimatAction.StopAction(this);
        }
    }
    private void FixedUpdate() 
    {
        if (!isOwned) return;
        if (characterAnimator==null) return;
        rb.velocity = movementAmount * currentSpeed;
    }

    public void UpdateAnimBools(string _boolName,bool _value)
    {
        characterAnimator.catAnim.SetBool(_boolName, _value);
        characterAnimator.hatAnim.SetBool(_boolName, _value);
        characterAnimator.chestAnim.SetBool(_boolName, _value);
        

    }

    public GameObject detectItemWithRaycast(string detectionTag,float skillDistance, LayerMask interactionLayer){
         RaycastHit2D hit = Physics2D.Raycast(raycastOrigin.position, raycastOrigin.right,skillDistance,interactionLayer);
         if (hit)
        {
            Debug.Log("mooch found colliders");
            //detect if item is pushable
            Debug.Log("Hit name" + hit.collider.gameObject.name);
            if(hit.collider.tag == detectionTag)
            {
                 Debug.Log("found pushable");
                return hit.collider.gameObject;
            }
            else{
                 Debug.Log("didnt find pushable");
                return null;
            }
        }

        Debug.Log("didint see anything");
        return null;

    }

    public GameObject DetectWithOverLapSphere(string detectionTag,float radius, LayerMask interactionLayer)
    {
        float minDistance = Mathf.Infinity;
        GameObject closestInteractable = null;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position,radius,interactionLayer);
         if (colliders.Length > 0)
        {

            foreach(Collider2D col in colliders)
            {
                if(col.tag == detectionTag){
                    float distance = Mathf.Abs(Vector3.Distance(transform.position, col.gameObject.transform.position));
                    if(distance < minDistance)
                    {
                        closestInteractable = col.gameObject;
                        minDistance = distance;
                    }
                }
                
            }
        }

        return closestInteractable;

    }


//CALL BACK EVENTS
    private void DoCharacterSkill(InputAction.CallbackContext context){
        if (!isOwned) return;
        Debug.Log("Player Has Done Character Skill");
        skillAction.DoAction(this);
    }
    private void DoCharacterUltimate(InputAction.CallbackContext context){
        if (!isOwned) return;
        if(stamina <= 0) return;

        ultimatAction.DoAction(this);
        Debug.Log("Player Has Done Character Ultimate");
    }

    private void StopCharacterSkill(InputAction.CallbackContext context){
        if (!isOwned) return;
        Debug.Log("Player Has Stopped Character Skill");
        skillAction.StopAction(this);
    }

    private void StopCharacterUltimate(InputAction.CallbackContext context){
        if (!isOwned) return;
        ultimatAction.StopAction(this);
        Debug.Log("Player Has Stopped Character Ultimate");
    }


    private void UpdateToCat()
    {
        GameObject oldSprite=null;
        if (CatGraphic != null){
            oldSprite = CatGraphic; 
        }
        CatGraphic = Instantiate(currentCat.catSprite,this.transform);
        characterAnimator = CatGraphic.GetComponent<CharacterAnimationController>();
        
        if (oldSprite != null){
            Destroy(oldSprite);
        }
        skillAction = currentCat.skillAction;
        ultimatAction= currentCat.ultimatAction;
        myAvatar = transform.GetChild(0);
        

        
        
    }
  
    public void SwitchToCat(int newCatIndex){
        if(isInteracting) return;
        currentCat =switchCat[newCatIndex];
        UpdateToCat();
        InnitializeSwitchCatList();

    }

    public void SwitchToCat0(InputAction.CallbackContext context)
    {
        SwitchToCat(0);
    }
    public void SwitchToCat1(InputAction.CallbackContext context)
    {
        SwitchToCat(1);
    }
    public void SwitchToCat2(InputAction.CallbackContext context)
    {
        SwitchToCat(2);
    }

    public void SwitchToCat3(InputAction.CallbackContext context)
    {
        SwitchToCat(3);
    }

}
