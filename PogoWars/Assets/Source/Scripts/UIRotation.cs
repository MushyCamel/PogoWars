using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIRotation : MonoBehaviour
{
    //Script is used to make sure the health bar stays the same so it doesn't rotate with the player. (via unity tutorial)
    
    public bool useRelativeRotation = true;  

    private Quaternion relativeRotation;          

    // Start is called before the first frame update
    void Start()
    {
        relativeRotation = transform.parent.localRotation;

    }

    // Update is called once per frame
    void Update()
    {
        if (useRelativeRotation)
            transform.rotation = relativeRotation;
    }
}
