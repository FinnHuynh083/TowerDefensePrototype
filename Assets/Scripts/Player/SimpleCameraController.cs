using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SimpleCameraController : MonoBehaviour
{
    [SerializeField] private Transform _CameraHolder;
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private InputActionReference _mousePos;

    private float _screenHeight;
    private float _screenWidth;

    private void Start()
    {
        _screenHeight = Screen.height;
        _screenWidth = Screen.width;
        //PrintScreenSize();
    }

    private void PrintScreenSize()
    {
        print($"H: {_screenHeight}, W:{_screenWidth}");
    }
    private void PrintMousePos()
    {
        print($"{_mousePos.action.ReadValue<Vector2>()}");
    }

    //chuyen mouse pos tren screen sang World Pos

    //di chuyen 2d = transform cua CameraHolder

    //zoom = camera field of view - clamp lai gia tri zoom min max

    private void Update()
    {
        //ham di chuyen
        //UpdateCameraNavigation();

        //ham zoom
        //UpdateZooming();

        PrintMousePos();
    }

    //private void UpdateCameraNavigation()
    //{
    //    float _mousePosH =
    //}

    private void UpdateZooming()
    {

    }

    //di chuyen 2d = transform cua CameraHolder

    //dk di chuyen khi chuot cham mep mang hinh

    //zoom = camera field of view - clamp lai gia tri zoom min max



}
