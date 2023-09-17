using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //References
    [Header("References")]
    public Transform trans;
    public Transform modelTrans;
    public CharacterController _characterController;

    //Movement
    [Header("Movement")]
    public float moveSpeed = 24;
    public float timeToMaxSpeed = .26f;
    
    private float VelocityGainPerSecond
    { get { return moveSpeed / timeToMaxSpeed; } }

    public float timeToLoseMaxSpeed=.2f;
    private float VelocityLossPerSecond { get { return moveSpeed / timeToLoseMaxSpeed; } }

    public float reverseMomentumMultiplier = 2.2f;

    private Vector3 movementVelocity = Vector3.zero;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    public void Movement()
    {
        //if W or Up arrow Key is Held:
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            if (movementVelocity.z >= 0)
                movementVelocity.z = Mathf.Min(moveSpeed, movementVelocity.z + VelocityGainPerSecond * Time.deltaTime);


            else
                movementVelocity.z = Mathf.Min(0, movementVelocity.z + VelocityGainPerSecond * reverseMomentumMultiplier * Time.deltaTime);
        }

        //If S or Down Key is Held: 
        else if(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            if (movementVelocity.z > 0)
                movementVelocity.z = Mathf.Max(0, movementVelocity.z - VelocityGainPerSecond * reverseMomentumMultiplier * Time.deltaTime);


            else
                movementVelocity.z = Mathf.Max(-moveSpeed, movementVelocity.z - VelocityGainPerSecond * Time.deltaTime);
        }

        else
        {
            if (movementVelocity.z > 0)
                movementVelocity.z = Mathf.Max(0,movementVelocity.z - VelocityLossPerSecond * Time.deltaTime);

            else
                movementVelocity.z = Mathf.Min(0, movementVelocity.z + VelocityLossPerSecond * Time.deltaTime);
        }


        //If D or RightArrow is Held
        if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            if (movementVelocity.x >= 0)
                movementVelocity.x = Mathf.Min(moveSpeed, movementVelocity.x + VelocityGainPerSecond * Time.deltaTime);

            else
                movementVelocity.x = Mathf.Min(0, movementVelocity.x + VelocityGainPerSecond * reverseMomentumMultiplier * Time.deltaTime);
        }

        //if A or LeftArrow is Held
        else if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            if (movementVelocity.x > 0)
                movementVelocity.x = Mathf.Max(0, movementVelocity.x - VelocityGainPerSecond * reverseMomentumMultiplier * Time.deltaTime);

            else
                movementVelocity.x = Mathf.Max(-moveSpeed, movementVelocity.x - VelocityGainPerSecond * Time.deltaTime);
        }

        else
        {
            if (movementVelocity.x > 0)
                movementVelocity.x = Mathf.Max(0, movementVelocity.x - VelocityLossPerSecond * Time.deltaTime);

            else
                movementVelocity.x = Mathf.Min(0, movementVelocity.x + VelocityLossPerSecond * Time.deltaTime);
        }


        //Applying Movement

        if(movementVelocity.x != 0 || movementVelocity.z != 0)
        {
            _characterController.Move(movementVelocity * Time.deltaTime);
            modelTrans.rotation = Quaternion.Slerp(modelTrans.rotation, Quaternion.LookRotation(movementVelocity), .18f);
        }
    }
}
