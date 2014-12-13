using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

public class PathDefinition : MonoBehaviour 
{
    public Transform[] points;

    public IEnumerator<Transform> GetPathsEnumerator()
    {
        if (points == null || points.Length < 1)
            yield break;

        var direction = 1;
        var index = 0;
        while(true)
        {
            yield return points[index];

            if (points.Length == 1)
                continue;

            if (index <= 0)
                direction = 1;
            else if (index >= points.Length - 1)
                direction = -1;

            index = index + direction;
        }



    }

    public void OnDrawGizmos()
    {
        
        //Make sure we have enough points to make a line
        if (points == null || points.Length < 2)
            return;

        var _points = points.Where(t => t != null).ToList();
        if (_points.Count < 2)
            return;


        //lopp through all the points to draw a line
        for(var i = 1; i < _points.Count; i++)
        {
            Gizmos.DrawLine(_points[i-1].position, _points[i].position);
        }

    }

}
