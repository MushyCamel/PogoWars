using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField]
    private float _jumpForce = 1000;
    [SerializeField]
    public float _rotSpeed = 5f;
    [SerializeField]
    public float _rot = 0f;
    public float _hitDistance = 0.17f;
    public string JumpButton = "Jump_P1";
    public string HorizontalCtrl = "Horizontal_P1";
    public int _playerNumber = 1;              


    public string PlayerLayer = "Player1";



    [Header("Reference")]
    [SerializeField]
    private PogoSpring _pogoSpring;

    void Start() {
        }


    void Update()
    {
        //rotates the player based on horizontal input and rotation speed
       _rot -= Input.GetAxisRaw (HorizontalCtrl) * _rotSpeed;
       transform.eulerAngles = new Vector3(0.0f, 0.0f, _rot);

        //limits the amount of rotation and returns to the upright position if there is no input
        if (_rot < 20 || _rot > 20){
           _rot = 0;
           
       }
        
        //
        if (Input.GetButtonDown(JumpButton)) {
        _pogoSpring.QueueJump(_jumpForce);
        }

    }

    void FixedUpdate()
    {
                // Cast a ray straight down.
        //RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, _hitDistance);
        Debug.DrawRay(transform.position, -Vector2.up * _hitDistance, Color.red);
        LayerMask mask = LayerMask.GetMask(PlayerLayer);



        // If it hits something
        var hits = Physics2D.RaycastAll(transform.position, -Vector2.up, _hitDistance, mask);
        if (hits.Length > 0)
        {
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
