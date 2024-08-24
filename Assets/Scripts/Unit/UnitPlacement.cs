using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine.Utility;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.UI;
using static UnityEngine.UI.CanvasScaler;

public class UnitPlacement : MonoBehaviour
{
    [SerializeField] private InputActionReference _mousePos;
    //[SerializeField] private InputActionReference _mouseClick;
    [SerializeField] private Gold _gold;


    [SerializeField] private Camera _mainCamera;
    [SerializeField] private LayerMask _placementLayerMask;
    [SerializeField] private Transform _basePos;
    [SerializeField] private GridPlacement[] gridPlacements;
    [SerializeField] private ToggleGroup _toggleGroup;


    //public UnityEvent<GameObject> ChangeUnitPrefab;
    //public UnityEvent OutOfGrid;
    //public UnityEvent OverLap;
    //public UnityEvent UnitDataIsNull;
    //public UnityEvent NotEnoughGold;



    [NonSerialized]public GameObject unitData;
    //[NonSerialized] public UnitScriptableObj _unitInfoData;

    public Vector3 MousePosToWorldPos()
    {
        Vector2 mousePos= _mousePos.action.ReadValue<Vector2>();

        Ray ray = _mainCamera.ScreenPointToRay((Vector3)mousePos);

        Physics.Raycast(ray,out RaycastHit hitInfo,float.MaxValue,_placementLayerMask);

        return hitInfo.point;
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

    public PlaceStatus PlaceUnit()
    {
        Vector3 worldPos = MousePosToWorldPos();
        if (IsInGridPlacement(worldPos))
        {
            GridPlacement gridPlacement = InGridPlacement(worldPos);
            Vector2Int gridPos = gridPlacement.WorldToGridPos(worldPos);

            //Vector2Int _unitDimension = unitData.GetComponent<Unit>().Dimension;
            Unit unitPrefab = unitData.GetComponent<Unit>();
            //if data null
            //noti select unit data
            //return
            if (unitPrefab == null)
            {
                //UnitDataIsNull.Invoke();
                return PlaceStatus.UnitDataIsNull;
            }
            if (gridPlacement.Fits(gridPos, unitPrefab.Dimension) == FitStatus.Fits)
            {
                //them += offset
                Vector3 _absWorldPos = gridPlacement.GridToWorldPos(gridPos);
                Vector3 _unitPos = new Vector3(_absWorldPos.x + unitPrefab.UnitPosOffset(unitPrefab.Dimension.x),
                    _absWorldPos.y, _absWorldPos.z + unitPrefab.UnitPosOffset(unitPrefab.Dimension.y));

                //place
                GameObject unit = Instantiate(unitData, _unitPos, Quaternion.identity);
                gridPlacement.Occupy(gridPos, unitPrefab.Dimension);

                unit.GetComponent<Targetter>()._basePos = _basePos;

                unit.GetComponent<UnitInfoPanel>()._unitPlacement = this;
                unit.GetComponent<UnitInfoPanel>()._gold = _gold;

                //them toggle group va get toggle cua unit va add vao toggle group
                Toggle toggle = unit.GetComponentInChildren<Toggle>();

                _toggleGroup.RegisterToggle(toggle);

                return PlaceStatus.Fits;
            }
            else if (gridPlacement.Fits(gridPos, unitPrefab.Dimension) == FitStatus.OutOfBound)
            {
                return PlaceStatus.OutOfBound;
            }
            else if(gridPlacement.Fits(gridPos, unitPrefab.Dimension) == FitStatus.OutOfBound)
            {
                return PlaceStatus.OutOfBound;
            }
            else
            {
                //xu ly overlap
                return PlaceStatus.Overlaps;
            }
        }
        else
        {
            return PlaceStatus.OutOfBound;
        }
    }
    // lay worl pos >> grid pos + dimesion
    public void DestroyUnit(Vector3 worldPos, GameObject gameObject)
    {
        GridPlacement gridPlacement = InGridPlacement(worldPos);

        Vector2Int unitDimension = gameObject.GetComponent<Unit>().Dimension;

        Vector2Int gridPos = gridPlacement.WorldToGridPos(worldPos);

        gridPlacement.UnOccupy(gridPos, unitDimension);

        //remove khoi toggle group
        _toggleGroup.UnregisterToggle(gameObject.GetComponentInChildren<Toggle>());

        Destroy(gameObject);
    }
}
