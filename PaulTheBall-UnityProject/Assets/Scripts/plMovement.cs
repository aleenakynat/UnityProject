using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plMovement : MonoBehaviour
{
   // public Rigidbody rb;
    public float speed = 20;
    //public float force;
    public float gravity = 28;
    public float jumpSpeed = 12;
    public CharacterController characterController;
    public float fwdSpeed=2.3f;

    private Vector3 moveDirection = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<CharacterController>();
        fwdSpeed = GameManager.Instance.increaseSpeed(fwdSpeed);

    }

    // Update is called once per frame
    void Update()
    {
        //moveDirection = Vector3.zero;
        if (characterController.isGrounded)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, fwdSpeed);
            moveDirection *= speed;
            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpSpeed;
                AudioManager.Instance.PlayAudio("Jump");
            }

        }
        moveDirection.y -= gravity * Time.deltaTime;
        characterController.Move(moveDirection * Time.deltaTime);
        if (GameManager.Instance.plheight < -3)
        {
            GameManager.Instance.GameOver();
            GameManager.Instance.gameText.text = "Oops, you fell! :(";
        }
    }
          
}
