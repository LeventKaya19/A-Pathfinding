﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node :IHeapItem<Node>
{
    public Vector3 worldPos;
    public bool walkable;
    public int gridX;
    public int gridY;
    public int movementPenalty;

    public int gCost;
    public int hCost;
    public Node parent;
    int heapIndex;

    public Node(bool _walkable,Vector3 _worldPos,int _gridX,int _gridY, int _penalty)
    {
        movementPenalty = _penalty;
        walkable = _walkable;
        worldPos = _worldPos;
        gridX = _gridX;
        gridY = _gridY;
    }

    public int fCost
    {
        get
        {
            return gCost + hCost;
        }
    }

    public int HeapIndex
    {
        get
        {
            return heapIndex;
        }
        set
        {
            heapIndex = value;
        }
    }
    public int CompareTo(Node other)
    {
        int compare = fCost.CompareTo(other.fCost);
        if (compare == 0) compare = hCost.CompareTo(other.hCost);
        return -compare;
    }
}
