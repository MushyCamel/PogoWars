using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField]
    private float _jumpForce = 1000;
 
    public float rotSpeed = 5f;
    public float rot = 0f;
    public float hitDistance = 0.17f;
    public string jumpButton = "Jump_P1";
    public string horizontalCtrl = "Horizontal_P1";
    public int playerNumber = 1;              

    public string PlayerLayer = "Player1";

    [Header("Reference")]
    [SerializeField]
    private PogoSpring _pogoSpring;


    void Update()
    {
        //rotates the player based on horizontal input and rotation speed
       rot -= Input.GetAxisRaw (horizontalCtrl) * rotSpeed;
       transform.eulerAngles = new Vector3(0.0f, 0.0f, rot);

        //limits the amount of rotation and returns to the upright position if there is no input
        if (rot < 20 || rot > 20){
           rot = 0;
           
       }
        
        //if someone presses the jump button it calls QueueJump
        if (Input.GetButtonDown(jumpButton)) {
        _pogoSpring.QueueJump(_jumpForce);
        }

    }

    void FixedUpdate()
    {
        // Cast a ray straight down.
        //RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, _hitDistance);
        Debug.DrawRay(transform.position, -Vector2.up * hitDistance, Color.red);
        LayerMask mask = LayerMask.GetMask(PlayerLayer);



        // If it hits something
        var hits = Physics2D.RaycastAll(transform.position, -Vector2.up, hitDistance, mask);
        if (hits.Length > 0)
        {
            //go through each hit and if it hits an object with the player controller attached call hit.
            foreach (var hit in hits)
            {
                if (hit.collider.gameObject == gameObject)
                    continue;

                var cont = hit.collider.gameObject.GetComponent<PlayerController>();
                if (cont)
                    cont.Hit();
                Debug.Log("HIT");
            }
        }



    }

    public void Hit()
    { 

        // Try and find the HealthScript on the gameobject hit.
        HealthScript _playerHealth = GetComponent<CircleCollider2D>().GetComponent <HealthScript> ();

        // If the HealthScript component exist the enemy should take damage.
        if (_playerHealth != null )
                _playerHealth.TakeDamage();

    }




    
}
