﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path
{
    public readonly Vector3[] lookPoints;
    public readonly Line[] turnBoundaries;
    public readonly int finishLineIndex;
    public readonly int slowDownIndex;

    public Path(Vector3[] waypoints,Vector3 startPos,float turnDistance,float stoppingDst)
    {
        lookPoints = waypoints;
        turnBoundaries = new Line[lookPoints.Length];
        finishLineIndex = turnBoundaries.Length - 1;

        Vector2 prevPoint = V3ToV2(startPos);
        for (int i = 0; i < lookPoints.Length; i++)
        {
            Vector2 currentPoint = V3ToV2(lookPoints[i]);
            Vector2 dirToCurrentPoint = (currentPoint - prevPoint).normalized;
            Vector2 turnBoundaryPoint = (i==finishLineIndex)?turnBoundaryPoint=currentPoint: currentPoint - dirToCurrentPoint* turnDistance;
            turnBoundaries[i] = new Line(turnBoundaryPoint, prevPoint-dirToCurrentPoint*turnDistance);
            prevPoint = turnBoundaryPoint;
        }

        float dstFromEndpoint = 0f;

        for (int i = lookPoints.Length-1; i > 0; i--)
        {
            dstFromEndpoint += Vector3.Distance(lookPoints[i], lookPoints[i - 1]);
            if (dstFromEndpoint > stoppingDst)
            {
                slowDownIndex = i;
                break;
            }
        }
    }

    Vector2 V3ToV2(Vector3 v3)
    {
        return new Vector2(v3.x, v3.z);
    }

    public void DrawWithGizmos()
    {
        Gizmos.color = Color.black;
        foreach(Vector3 p in lookPoints)
        {
            Gizmos.DrawCube(p + Vector3.up,Vector3.one);
        }

        Gizmos.color = Color.white;
        foreach(Line l in turnBoundaries)
        {
            l.DrawWithGizmos(10f);
        }
    }
}
