using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public Vector2Int Dimension;

    public float UnitPosOffset(int size)
    {
        switch (size)
        {
            case 1:
                return 0f;
            case 2:
                return 0.5f;
            case 3:
                return 1f;
            case 4:
                return 1.5f;
            case 5:
                return 2f;
        }
        return 0f;
    }
}
