using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Component references
    Rigidbody2D rb;  // Unity's physics component

    [SerializeField] float speed = 5f;
    [SerializeField] float jumpHeight = 5f;

    //keep track of current horizontal direction
    float direction = 0;

    //keep track of if the player is on the ground
    bool isGrounded = false;
    bool isFacingRight = true;

    // Start is called before the first frame update
    void Start()
    {
        // Get references to the components attached to the current GameObject
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move(direction);

        if ((isFacingRight && direction == -1) || (!isFacingRight && direction == 1))
            Flip();
    }

    void OnJump()
    {
        //if player is on the ground, jump
        //if (isGrounded)
        Jump();
    }

    private void Jump()
    {
        // Set the y velocity to some positive value while keeping the x and z whatever they were originally
        rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
    }

    void OnMove(InputValue moveVal)
    {
        //store input as a 2D vector
        float movDirection = moveVal.Get<float>();
        direction = movDirection;
    }

    private void Move(float x)
    {
        // Set the x component of velocity while keeping the y velocity what it originally is.
        rb.velocity = new Vector2(x * speed, rb.velocity.y);
    }

    //commonly used function but not used in this case
    // void OnCollisionEnter(Collision collision)
    // {

    // }

    // void OnCollisionStay(Collision collision)
    // {
    //     //check if angle between normal vector of object of contact and up vector is less than 45 degrees
    //     //AKA if-statement is true if player is touching another object that is 0 to 45 degrees slope
    //     if (Vector3.Angle(collision.GetContact(0).normal, Vector3.up) < 45f)
    //         isGrounded = true;
    //     else
    //         isGrounded = false;
    // }

    // void OnCollisionExit(Collision collision)
    // {
    //     isGrounded = false;
    // }


    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 newLocalScale = transform.localScale;
        newLocalScale.x *= -1f;
        transform.localScale = newLocalScale;
    }
}
