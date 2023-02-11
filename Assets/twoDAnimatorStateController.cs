using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class twoDAnimatorStateController : MonoBehaviour
{
    Animator animator;
    float velocityZ = 0.0f;
    float velocityX = 0.0f;
    public float acceleration = 2.0f;
    public float deceleration = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        bool forwardPressed = Input.GetKey("w");
        bool leftPressed = Input.GetKey("a");
        bool rightPressed = Input.GetKey("d");
        bool runPressed = Input.GetKey("left shift");

        if(forwardPressed && VelocityZ < 0.5f && !runPressed)
        {
            VelocityZ += Time.deltaTime * acceleration;
        }
        if(leftPressed && VelocityX > -0.5F && !runPressed)
        {
            VelocityX -= Time.deltaTime * acceleration;
        }
        if(rightPressed && VelocityX<0.5f && !runPressed)
        {
            VelocityX += Time.deltaTime * acceleration;
        }
        if(!forwardPressed && VelocityZ <0.0F)
        {
            VelocityZ = 0.0f;
        }
        if(!leftPressed && VelocityX <0.0F)
        {
            VelocityX += Time.deltaTime * deceleration;
        }
        if(!rightPressed && VelocityX >0.0f)
        {
            VelocityX -= Time.deltaTime * deceleration;
        }
        if(!leftPressed &&!rightPressed&& VelocityX !=0.0f &&(VelocityX >-0.05f && VelocityX<0.05f))
        {
            VelocityX = 0.0f;
        }
        animator.SetFloat("velocityZ", VelocityZ);
        animator.SetFloat("velocityX", VelocityX);


    }
}
