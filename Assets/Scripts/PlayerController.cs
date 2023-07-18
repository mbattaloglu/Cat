using System;
using System.Collections;
using Cat.Abstracts.Inputs;
using Cat.Abstracts.Movements;
using Cat.Concreates.Inputs;
using Cat.Concreates.Movements;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private IPlayerInput playerInput;
    private IPlayerMovement playerMovement;

    private Animator animator;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float collisionOffset;

    [SerializeField] private ContactFilter2D contactFilter2D;

    private void Awake()
    {
        playerInput = new ComputerInput();
        playerMovement = new MoveWithRigidbody(GetComponent<Rigidbody2D>(), moveSpeed, contactFilter2D, collisionOffset);
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        ((ComputerInput)playerInput).playerInputActions.Fire.performed += ctx => Taunt();
    }

    private void Taunt()
    {
        StartCoroutine(TauntAnimation());
    }

    private void Update()
    {
        animator.SetBool("Walk", playerInput.Horizontal != 0 || playerInput.Vertical != 0);
        transform.localScale = new Vector3(playerInput.Horizontal > 0 ? 5 : playerInput.Horizontal < 0 ? -5 : transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    private void FixedUpdate()
    {
        playerMovement.Move(playerInput.Horizontal, playerInput.Vertical);
    }

    private IEnumerator TauntAnimation()
    {
        animator.SetBool("Taunt", true);
        yield return new WaitForSeconds(0.37f);
        animator.SetBool("Taunt", false);
    }
}
