using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
 
public class PlayerController : MonoBehaviour
{
   PlayerInput playerInput;
   PlayerInput.MainActions input;
 
   CharacterController controller;
   Animator animator;
   AudioSource audioSource;
 
   [Header("Controller")]
   public float movespeed = 5;
   public float gravity = 9.8f;
   public float JumpHeight = 1.2f;
   // Player Settings.

   Vector3 _PlayerVelocity;
 
   bool isGrounded;
 
   [Header("Camera")]
   public Camera cam;
   public float sensitivity;
 
   [Header("Attacking")]
   public float attackDistance = 3f;
   public float attackDelay = 0.4f;
   public float attackSpeed = 1f;
   public int attackDamage = 1;
   public LayerMask attackLayer;
 
   public GameObject hitEffect;
   public AudioClip swordSwing;
   public AudioClip hitSound;
 
   bool attacking = false;
   bool readyToAttack = true;
   int attackCount;
   
 
   float xRotation = 0f;
 
   void Awake()
   {
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        audioSource = GetComponent<AudioSource>();
        // Gets the components needed.
 
        playerInput = new PlayerInput();
        input = playerInput.Main;
        AssignInputs();
        // Assigns inputs.
   }
 
   void Update()
   {
        isGrounded = controller.isGrounded;
        // Checks if the player is grounded.
 
        if(input.Attack.IsPressed())
        {
            Attack();
        }
        // Repeat Inputs.
 
        SetAnimations();
   }
 
   void FixedUpdate()
   {
        MoveInput(input.Movement.ReadValue<Vector2>());
        // Calls physics updates to the player's move input method.
   }
 
   void LateUpdate()
   {
        LookInput(input.Look.ReadValue<Vector2>());
        // Calls the look input method
   }

   void MoveInput(Vector2 input)
   {
        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = input.x;
        moveDirection.z = input.y;

        controller.Move(transform.TransformDirection(moveDirection) * movespeed * Time.deltaTime);
        _PlayerVelocity.y += gravity * Time.deltaTime;
        if(isGrounded && _PlayerVelocity.y < 0)
        _PlayerVelocity.y = -2f;
        controller.Move(_PlayerVelocity * Time.deltaTime);
   }
   void LookInput(Vector3 input)
    {
        float mouseX = input.x;
        float mouseY = input.y;

        xRotation -= (mouseY * Time.deltaTime * sensitivity);
        xRotation = Mathf.Clamp(xRotation, -80, 80);

        cam.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);

        transform.Rotate(Vector3.up * (mouseX * Time.deltaTime * sensitivity));
    }
 
   void OnEnable()
   {
        input.Enable();
   }
 
   void OnDisable()
   {
        input.Disable();
   }
 
   void Jump()
   {
        if (isGrounded)
        _PlayerVelocity.y = Mathf.Sqrt(JumpHeight * -3.0f * gravity);
   }
 
   void AssignInputs()
   {
        input.Jump.performed += ctx => Jump();
        input.Attack.started += ctx => Attack();
   }
 
   public void Attack()
   {
        if(!readyToAttack || attacking) return;
 
        readyToAttack = false;
        attacking = true;
 
        Invoke(nameof(ResetAttack), attackSpeed);
        Invoke(nameof(AttackRaycast), attackDelay);
        // Resets the attack.
 
        audioSource.pitch = Random.Range(0.9f, 1.1f);
        audioSource.PlayOneShot(swordSwing);

        if(attackCount == 0)
        {
            ChangeAnimationState(ATTACK1);
            attackCount++;
        }
        else
        {
            ChangeAnimationState(ATTACK2);
            attackCount = 0;
        }
   }
 
    void ResetAttack()
    {
        attacking = false;
        readyToAttack = true;
        // Changes the variables from true to false and from false to true.
    }
 
    void AttackRaycast()
    {
        if(Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit hit, attackDistance, attackLayer))
        {
            HitTarget(hit.point);
            // Checks if the target gets hit.

            if(hit.transform.TryGetComponent<Actor>(out Actor T))
            {
                T.TakeDamage(attackDamage);
                Debug.Log("Deal Damage");
                // Calls the Take damage method from the actor script.
            }
        }
    }
   
    void HitTarget(Vector3 pos)
    {
        audioSource.pitch = 1;
        audioSource.PlayOneShot(hitSound);
        // Plays hit sound.

        GameObject GO = Instantiate(hitEffect, pos, Quaternion.identity);
        Destroy(GO, 20);
    }

    public void ChangeAnimationState(string newState)
    {
        if (currentAnimationState == newState) return;
        // Stop the same animation from interrupting with itself.

        currentAnimationState = newState;
        animator.CrossFadeInFixedTime(currentAnimationState, 0.2f);
        // Play the animation
    }

    public const string IDLE = "Idle";
    public const string WALK = "Walk";
    public const string ATTACK1 = "Attack 1";
    public const string ATTACK2 = "Attack 2";
    // Variables for the animations.

    string currentAnimationState;

    void SetAnimations()
    {
        if(!attacking)
        // If the player is not attacking
        {
            if(_PlayerVelocity.x == 0 &&_PlayerVelocity.z == 0)
            {
                ChangeAnimationState(IDLE);
            }
            else
            {
                ChangeAnimationState(WALK);
            }
        }
    }


}
 