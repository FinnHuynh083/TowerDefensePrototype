using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

public class SimpleCameraController : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;
    [Header("Camera Zoom")]
    [SerializeField] private InputActionReference _zoom;
    [SerializeField] private float _minFOV;
    [SerializeField] private float _maxFOV;
    [SerializeField] private float _zoomValue;
    [Header("Camera Movement")]
    [SerializeField] private InputActionReference _mousePos;
    [SerializeField] private float _speed;
    [SerializeField] private float _minX;
    [SerializeField] private float _maxX;
    [SerializeField] private float _minZ;
    [SerializeField] private float _maxZ;

    private float _screenHeight;
    private float _screenWidth;

    private void Start()
    {
        _screenHeight = Screen.height;
        _screenWidth = Screen.width;
        //PrintScreenSize();
    }

    //chuyen mouse pos tren screen sang World Pos

    //di chuyen 2d = transform cua CameraHolder

    //zoom = camera field of view - clamp lai gia tri zoom min max

    private void Update()
    {
        UpdateScreeSize();
        //move
        UpdateCameraMoving();
        //zoom
        UpdateZooming();
        //print($"{_zoom.action.ReadValue<float>()}");
    }

    private void UpdateScreeSize()
    {
        if (_screenHeight != Screen.height)
        {
            _screenHeight = Screen.height;
        }
        if(_screenWidth != Screen.width)
        {
            _screenWidth = Screen.width;
        }
    }

    private void Zoom(float value)
    {
        _mainCamera.fieldOfView += value;
        if (_mainCamera.fieldOfView < _minFOV)
        {
            _mainCamera.fieldOfView = _minFOV;
        }
        if (_mainCamera.fieldOfView > _maxFOV)
        {
            _mainCamera.fieldOfView = _maxFOV;
        }
    }

    private void UpdateZooming()
    {
        //neu zoom value >0 zoomin
        // value<0 zoomout
        float zoom = _zoom.action.ReadValue<float>();
        if (zoom < 0)
        {
            Zoom(_zoomValue);
        }
        if (zoom > 0)
        {
            Zoom(-_zoomValue);
        }
    }

    //space button >> set camera ve vi tri ban dau

    private void UpdateCameraMoving()
    {
        var mousePos = _mousePos.action.ReadValue<Vector2>();
        var mousePosX = mousePos.x;
        var mousePosY = mousePos.y;

        float distance = _speed * Time.deltaTime;

        if(mousePosX<=0&& mousePosY>=0)
        {
            //move left
            if (transform.position.x > _minX)
            {
                transform.Translate(-distance, 0, 0);
            }
        }
        if(mousePosX>=_screenWidth&& mousePosY >= 0)
        {
            //move right
            if (transform.position.x < _maxX)
            {
                transform.Translate(distance, 0, 0);
            }
        }
        if (mousePosX>=0 && mousePosY <= 0)
        {
            //move down
            if(transform.position.z > _minZ)
            {
                transform.Translate(0, 0, -distance);
            }
        }
        if (mousePosX >= 0 && mousePosY >= _screenHeight)
        {
            //move up
            if(transform.position.z < _maxZ)
            {
                transform.Translate(0, 0, distance);
            }
        }
    }

}
