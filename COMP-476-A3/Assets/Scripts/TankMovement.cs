﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMovement : MonoBehaviour
{
    [Tooltip("The forward movement speed of the tank.")]
    [SerializeField]
    private float advanceSpeed = 2.0f;

    [Tooltip("The reverse movement speed of the tank.")]
    [SerializeField]
    private float reverseSpeed = 1.0f;

    [Tooltip("The rotation speed of the tank.")]
    [SerializeField]
    private float rotationSpeed = 1.0f;

    #region InputHandling

    private void HandleInput()
    {
        if (Input.GetKey(KeyCode.W))
            MoveForward();
        if (Input.GetKey(KeyCode.S))
            MoveBackward();
        if (Input.GetKey(KeyCode.A))
            RotateLeft();
        if (Input.GetKey(KeyCode.D))
            RotateRight();
    }

    private void MoveForward()
    {
        this.transform.position += advanceSpeed * this.transform.forward * Time.deltaTime;
    }

    private void MoveBackward()
    {
        this.transform.position += reverseSpeed * this.transform.forward * -1.0f * Time.deltaTime;
    }

    private void RotateRight()
    {
        this.transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }

    private void RotateLeft()
    {
        this.transform.Rotate(Vector3.up, -rotationSpeed * Time.deltaTime);
    }

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
    }
}