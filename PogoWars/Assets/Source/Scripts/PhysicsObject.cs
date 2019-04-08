using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsObject : MonoBehaviour
{

    public float gravityModifier = 1f;
    public float minGroundNormalY = 0.65f;

    protected const float paddingRadius = 0.01f; //anti-stuck-inside-object value
    protected float minimumMoveDistance = 0.001f; 

    protected Vector2 groundNormal;
    protected bool grounded;
    protected Vector2 velocity;
    protected Rigidbody2D rb2d;
    protected ContactFilter2D contactFilter;
    protected RaycastHit2D[] hitBuffer = new RaycastHit2D[16];
    protected List<RaycastHit2D> hitBufferList = new List<RaycastHit2D> (16);
   

    void OnEnable()
    {
        rb2d = GetComponent<Rigidbody2D> ();
    }


    // Start is called before the first frame update
    //Uses the settings in Physics2D to see what layers are being checked against each other
    void Start()
    {
        contactFilter.useTriggers = false;
        contactFilter.SetLayerMask (Physics2D.GetLayerCollisionMask (gameObject.layer));
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Moves the object down every frame (gravity)
    void FixedUpdate()
    {
        velocity += gravityModifier * Physics2D.gravity * Time.deltaTime;

        Vector2 deltaPosition = velocity * Time.deltaTime;

        Vector2 move = Vector2.up * deltaPosition.y;

        Movement (move, true);
    }

    //Moves the object based on fixed update values
    void Movement(Vector2 move, bool yMovement)
    {
        float distance = move.magnitude;

        // only checks for collision if it's moving
        if (distance > minimumMoveDistance)
        {
           int count = rb2d.Cast(move, contactFilter, hitBuffer, distance + paddingRadius);
           hitBufferList.Clear (); //avoid old data

           for (int i = 0; i < count; i++)
           {
            hitBufferList.Add (hitBuffer [i]);
           }

            for (int i = 0; i < hitBufferList.Count; i++)
            {
               Vector2 currentNormal = hitBufferList [i].normal;

               //used to check if grounded - only on a certain angle
               if (currentNormal.y > minGroundNormalY)
               {
                   grounded = true;
                   if (yMovement)
                   {
                       groundNormal = currentNormal;
                       currentNormal.x = 0;
                   }
               }

               float projection = Vector2.Dot (velocity, currentNormal);
               if (projection < 0)
               {
                   velocity = velocity - projection * currentNormal;
               }
               
                float modifiedDistance = hitBufferList [i].distance - paddingRadius;
                distance = modifiedDistance < distance ? modifiedDistance : distance;


            }
        }

        rb2d.position = rb2d.position + move.normalized * distance;
    }

}
