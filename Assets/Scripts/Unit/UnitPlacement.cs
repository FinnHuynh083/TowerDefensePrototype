using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class UnitPlacement : MonoBehaviour
{
    [SerializeField] private InputActionReference _mousePos;
    [SerializeField] private InputActionReference _mouseClick;
    [SerializeField] private InputActionReference _UnitChoosing;
    [SerializeField] private InputActionReference _UnitChoosing2;


    [SerializeField] private Camera _mainCamera;
    [SerializeField] private LayerMask _placementLayerMask;
    [SerializeField] private Transform _basePos;
    [SerializeField] private GridPlacement[] gridPlacements;

    [SerializeField] private GameObject _unitPrefab;
    [SerializeField] private GameObject _unitPrefab2;

    public UnityEvent<GameObject> ChangeUnitPrefab;

    private GameObject unitData;

    public Vector3 MousePosToWorldPos()
    {
        Vector2 mousePos= _mousePos.action.ReadValue<Vector2>();

        Ray ray = _mainCamera.ScreenPointToRay((Vector3)mousePos);

        Physics.Raycast(ray,out RaycastHit hitInfo,float.MaxValue,_placementLayerMask);

        return hitInfo.point;
    }

    private void Update()
    {
        //Vector3 worldPos = MousePosToWorldPos();
        //print($"WorldPos: {worldPos}");

        //bool inGridRange = IsInGridPlacement(MousePosToWorldPos());
        //print($"{inGridRange}");
        if (_UnitChoosing.action.triggered)
        {
            unitData = _unitPrefab;
            ChangeUnitPrefab.Invoke(unitData);
        }

        if (_UnitChoosing2.action.triggered)
        {
            unitData = _unitPrefab2;
            ChangeUnitPrefab.Invoke(unitData);
        }

        if (_mouseClick.action.triggered)
        {
            PlaceUnit();
        }
    }

    private bool IsInGridPlacement(Vector3 worldPos)
    {
        for (int i = 0; i < gridPlacements.Length; i++)
        {
            Vector3 gridPlacementPos = gridPlacements[i].transform.position;

            float extendedPosX = gridPlacementPos.x + gridPlacements[i].Dimension.x;
            float extendedPosZ = gridPlacementPos.z + gridPlacements[i].Dimension.y;

            if (worldPos.x >= gridPlacementPos.x && worldPos.z >= gridPlacementPos.z &&
                worldPos.x <= extendedPosX && worldPos.z <= extendedPosZ)
            {
                return true;
            }
        }
        return false;
    }

    private GridPlacement InGridPlacement(Vector3 worldPos)
    {
        for (int i = 0; i < gridPlacements.Length; i++)
        {
            Vector3 gridPlacementPos = gridPlacements[i].transform.position;

            float extendedPosX = gridPlacementPos.x + gridPlacements[i].Dimension.x;
            float extendedPosZ = gridPlacementPos.z + gridPlacements[i].Dimension.y;

            if (worldPos.x >= gridPlacementPos.x && worldPos.z >= gridPlacementPos.z &&
                worldPos.x <= extendedPosX && worldPos.z <= extendedPosZ)
            {
                return gridPlacements[i];
            }
        }
        return new GridPlacement();
    }

    public void PlaceUnit()
    {
        Vector3 worldPos = MousePosToWorldPos();
        if (IsInGridPlacement(worldPos))
        {
            GridPlacement gridPlacement = InGridPlacement(worldPos);
            Vector2Int gridPos= gridPlacement.WorldToGridPos(worldPos);

            //Vector2Int _unitDimension = unitData.GetComponent<Unit>().Dimension;
            Unit unitPrefab = unitData.GetComponent<Unit>();

            if (gridPlacement.Fits(gridPos, unitPrefab.Dimension) == FitStatus.Fits)
            {
                //them += offset
                Vector3 _absWorldPos = gridPlacement.GridToWorldPos(gridPos);
                Vector3 _unitPos = new Vector3(_absWorldPos.x+ unitPrefab.UnitPosOffset(unitPrefab.Dimension.x),
                    _absWorldPos.y, _absWorldPos.z+ unitPrefab.UnitPosOffset(unitPrefab.Dimension.y));
                //place
                GameObject unit = Instantiate(unitData, _unitPos, Quaternion.identity);
                gridPlacement.Occupy(gridPos, unitPrefab.Dimension);

                unit.GetComponent<Targetter>()._basePos = _basePos;
            }
            else if(gridPlacement.Fits(gridPos, unitPrefab.Dimension) == FitStatus.OutOfBound)
            {
                //xu ly out bound
            }
            else
            {
                //xu ly overlap
                print("Overlap");
            }
        }
        else
        {
            print("Out of Grid Placement");
        }
    }

}
