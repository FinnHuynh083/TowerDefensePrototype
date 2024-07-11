using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UnitGhost : MonoBehaviour
{
    [SerializeField] private InputActionReference _mousePos;
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private LayerMask _placementLayerMask;
    [SerializeField] private float _speed;

    private GameObject _currentGhostUnit;

    // Update is called once per frame
    void LateUpdate()
    {
        UpdatePosition();
    }
    //mouse to world post
    public Vector3 MousePosToWorldPos()
    {
        Vector2 mousePos = _mousePos.action.ReadValue<Vector2>();

        Ray ray = _mainCamera.ScreenPointToRay((Vector3)mousePos);

        Physics.Raycast(ray, out RaycastHit hitInfo, float.MaxValue, _placementLayerMask);

        return hitInfo.point;
    }
    //cap nhat pos cua ghost
    private void UpdatePosition()
    {
        if (_currentGhostUnit != null)
        {
            var unitPrefab =  _currentGhostUnit.GetComponent<Unit>();

            Vector3 _mousePos = MousePosToWorldPos();

            float desirePosX = _mousePos.x + unitPrefab.UnitPosOffset(unitPrefab.Dimension.x);
            float desirePosY = _mousePos.y;
            float desirePosZ = _mousePos.z + unitPrefab.UnitPosOffset(unitPrefab.Dimension.y);

            var desiredPos = new Vector3(desirePosX, desirePosY, desirePosZ);

            _currentGhostUnit.transform.position = desiredPos;
            //set position cua unitghost = voi mouse pos
        }
    }
    //sinh ra ghost
    public void CreateGhostUnit(GameObject ghostUnitPrefab)
    {
        _currentGhostUnit = Instantiate(ghostUnitPrefab, MousePosToWorldPos(), Quaternion.identity);
    }

    //destrooy unit ghost cu~
    public void DestroyGhostUnit()
    {
        if (_currentGhostUnit != null)
        {
            Destroy(_currentGhostUnit);

        }
    }
}
