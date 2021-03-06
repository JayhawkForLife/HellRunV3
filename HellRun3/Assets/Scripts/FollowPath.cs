﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FollowPath : MonoBehaviour 
{
    

    
    public PathDefinition path;
    public float speed = 1;
    public float maxDistanceToGoal = .2f;

    private IEnumerator<Transform> _currentPoint;
    
    public void Start()
    {
        if(path == null)
        {
            Debug.LogError("Path cannot be null", gameObject);
            return;
        }

        _currentPoint = path.GetPathsEnumerator();
        _currentPoint.MoveNext();

        if (_currentPoint.Current == null)
            return;

        transform.position = _currentPoint.Current.position;
    }

    public void Update()
    {
        if (_currentPoint == null || _currentPoint.Current == null)
            return;

        
       transform.position = Vector3.MoveTowards(transform.position, _currentPoint.Current.position, Time.deltaTime * speed);
        

        var distanceSquared = (transform.position - _currentPoint.Current.position).sqrMagnitude;
        if (distanceSquared < maxDistanceToGoal * maxDistanceToGoal)
            _currentPoint.MoveNext();
    }
}
