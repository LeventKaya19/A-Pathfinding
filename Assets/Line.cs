﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Line
{
    const float verticalLineGradient = 1e5f;

    float gradient;
    float y_intercept;
    Vector2 pointOnline_1;
    Vector2 pointOnline_2;

    float gradientPerpendicular;

    bool approachSide;

    public Line(Vector2 pointOnLine,Vector2 pointPerpendicularToLine)
    {
        float dx = pointOnLine.x - pointPerpendicularToLine.x;
        float dy = pointOnLine.y - pointPerpendicularToLine.y;

        if (dx == 0) gradientPerpendicular = verticalLineGradient;
        else gradientPerpendicular = dy / dx;

        if (gradientPerpendicular == 0) gradient = verticalLineGradient;
        else gradient = -1 / gradientPerpendicular;

        y_intercept = pointOnLine.y - gradient * pointOnLine.x;
        pointOnline_1 = pointOnLine;
        pointOnline_2 = pointOnLine + new Vector2(1, gradient);

        approachSide = false;
        approachSide = GetSide(pointPerpendicularToLine);
    }


    bool GetSide(Vector2 p)
    {
        return (p.x - pointOnline_1.x) * (pointOnline_2.y - pointOnline_1.y) > (p.y - pointOnline_1.y) * (pointOnline_2.x - pointOnline_1.x);
    }

    public bool HasCrossedLine(Vector2 p)
    {
        return GetSide(p) != approachSide;
    }

    public float DistanceFromPoint(Vector2 point)
    {
        float y_interceptPerpendiculat = point.y - gradientPerpendicular * point.x;
        float intersectX = (y_interceptPerpendiculat - y_intercept) / (gradient - gradientPerpendicular);
        float intersectY = gradient * intersectX + y_intercept;
        return Vector2.Distance(point, new Vector2(intersectX, intersectY));
    }
    
    public void DrawWithGizmos(float length)
    {
        Vector3 lineDir = new Vector3(1, 0, gradient).normalized;
        Vector3 lineCenter = new Vector3(pointOnline_1.x, 0, pointOnline_1.y) + Vector3.up;
        Gizmos.DrawLine(lineCenter - lineDir * length / 2f, lineCenter + lineDir * length / 2f);
    }
}
