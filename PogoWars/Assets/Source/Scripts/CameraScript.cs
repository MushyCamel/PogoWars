using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraScript : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] private float _zoomSmooth = 5.0f;
    [SerializeField] private Vector3 _offset;
    public float _smoothTime = .5f;
    public float _minZoom = 4f;
    public float _maxZoom = 1f;
    public float _zoomLimit = 50f;

    private Vector3 velocity;

    [Header("Reference")]
    [SerializeField] private Camera _camera;

    private GameObject[] _players;

    private void Update()
    {
        //If no players in the scene
        if (_players == null || _players.Length == 0)
        {
            _players = GameObject.FindGameObjectsWithTag("Player");
            return;
        }

            Move();
            Zoom();
        }

        //takes the greatest distance between players and changes the zoom 
        void Zoom()
        {

            float newZoom = Mathf.Lerp(_maxZoom, _minZoom, GetGreatestDistance() / _zoomLimit);
        //smooths out the transitions over time.
        _camera.orthographicSize = Mathf.Lerp(_camera.orthographicSize, newZoom, Time.deltaTime);
        }

        
        void Move()
        {
            Vector3 centerPoint = GetCeneterPoint();

            Vector3 newPosition = centerPoint + _offset;
            
        //lock the position on the y axis due to the players bouncing up and down
            newPosition.y = 0;

            transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, _smoothTime);
        }

        //Calculates the greatest distance between the two furthest players and creates a box around them. Return the size of the distance on the x axis
        float GetGreatestDistance()
        {
            var _bounds = new Bounds(_players[0].transform.position, Vector3.zero);
            for (int i = 0; i < _players.Length; i++)
            {
                _bounds.Encapsulate(_players[i].transform.position);
            }
            return _bounds.size.x;
        }



        Vector3 GetCeneterPoint()
        {
            //get the position of the player
            if (_players.Length == 1)
            {
                return _players[0].transform.position;
            }

            //for each player ecapsulate them
            var _bounds = new Bounds(_players[0].transform.position, Vector3.zero);
            for (int i = 0; i < _players.Length; i++)
            {
                _bounds.Encapsulate(_players[i].transform.position);
            }

            //return the center point of the ecapsulated players
            return _bounds.center;
        }
    }