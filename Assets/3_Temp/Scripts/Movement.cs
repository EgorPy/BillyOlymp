using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Movement : MonoBehaviour
{
    public float moveSpeed;
    public float speed = 5.0f;
    public float rotationSpeed = 5.0f;
    public float jumpForce = 5.0f; 
    public bool isOnGround = true;

    public float playerHeight;
    public float groundDrag;
    public LayerMask whatIsGtound;
    bool grounded; 

    public Vector3 movement;

  //  private float horizontalInput;
  //  private float forvardInput;
    private Rigidbody rb; 


    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
       
        //rotate
        if (movement != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movement, Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }


        // get player input
        //   horizontalInput = Input.GetAxis("Horizontal");
        //   forvardInput = Input.GetAxis("Vertical");

        //Move the player forward
        //   transform.Translate(Vector3.forward * Time.deltaTime * speed * forvardInput,0);
        //   transform.Translate(Vector3.right * Time.deltaTime * speed * horizontalInput,0);

        //ground check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGtound);

        //handle drag
        if (grounded)
            rb.drag = 0;

      //  MyInput();
        SpeedControl(); 



        //
        movement = new Vector3(Input.GetAxis("Horizontal"), 0 , Input.GetAxis("Vertical"));
        movement.Normalize();

        //Let the player jump
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false; 
        }
    }
    void FixedUpdate()
    {
        moveCharacter(movement);
    }

    void moveCharacter(Vector3 direction)
    {
      //  rb.velocity = direction * speed;
          rb.MovePosition(transform.position + (direction * speed * Time.deltaTime));
     }

    void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        //limit velosity if needed
        if(flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);;
        }
                
                
    }    
           
    


    void OnCollisionEnter(Collision collision)
{
    //if (collision.gameObject.CompareTag("Ground"))
   //{
        isOnGround = true;
    //}
}
}
