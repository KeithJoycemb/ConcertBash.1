using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VAnimationStateController : MonoBehaviour
{
    Animator animator;
    float velocity = 0.0f;
    public float acceleration = 0.1f;
    public float deceleration = 0.5f;
    int VelocityHash;

    public Animator Animator { get => animator; set => animator = value; }


    // Start is called before the first frame update
    void Start()
    {
        Animator = GetComponent<Animator>();
        VelocityHash = Animator.StringToHash("Velocity");
    }

    // Update is called once per frame
    void Update()
    {
        bool forwardPressed = Input.GetKey("w");
        bool runPressed = Input.GetKey("left shift");

        if(forwardPressed && velocity < 1.0f)
        {
            velocity += Time.deltaTime * acceleration;
        }
        if(!forwardPressed && velocity >0.0f)
        {
            velocity -= Time.deltaTime * deceleration;
        }
        if(!forwardPressed && velocity < 0.0f)
        {
            velocity = 0.0f;
        }
        
        Animator.SetFloat(VelocityHash, velocity);
    }
}
