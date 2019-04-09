using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZoneScript : MonoBehaviour
{
    
    // turn off the player on touch.
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == ("Player"))
        
                coll.gameObject.SetActive(false);
        
    }
}
