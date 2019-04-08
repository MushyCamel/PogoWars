using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PogoSpring : MonoBehaviour
{

  #region Unity Editor 

  [Header("Properties")]
  [SerializeField]
  private Vector3 _springOrigin = Vector3.zero;
  [SerializeField]
  private float _springDistance = -0.8f;
  [SerializeField]
  private float _springStiffness = 10;
  [SerializeField]
  private LayerMask _springLayerMask;

  [Header("References")]
  [SerializeField]
  private Rigidbody2D _rigidbody;

  #endregion

  private float _queuedJump = 0;

  private void Update()
  {
    if (_rigidbody == null)
      return;

    //Stabalisers
    //_rigidbody.AddTorque(-Mathf.Sin(transform.eulerAngles.z));

    //Get the distance from floor (max = _springDistance)
    var relativePosition = _springOrigin + transform.position;
    var hit = Physics2D.Raycast(relativePosition, -transform.up, _springDistance, _springLayerMask);
    if (!hit)
      return;

    //Only in Unity editor for debugging
#if UNITY_EDITOR
    Debug.DrawRay(relativePosition, -transform.up * hit.distance, Color.green);
#endif

    float force = 0;

    //Add more force the closer the object is to the floor
    force = (_springDistance - hit.distance) * _springStiffness;

    //Apply queued jump's force
    force += _queuedJump;
    _queuedJump = 0;

    //Apply force
    _rigidbody.AddForce(transform.up * force * Time.deltaTime * 1000);
  }

  public void QueueJump(float force)
  {
    _queuedJump = force;
  }
}