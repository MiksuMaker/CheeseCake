using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputs : MonoBehaviour
{
    #region Properties
    PlayerMove playerMove;
    #endregion

    #region Setup
    private void Start()
    {
        // Hook up to PlayerController
        playerMove = FindObjectOfType<PlayerMove>();
    }

    private void Update()
    {
        CheckInputs();
    }
    #endregion

    #region Functions
    private void CheckInputs()
    {
        // Check if Player presses WASD
        CheckMoveInputs();
    }

    private void CheckMoveInputs()
    {
        Vector3 moveVector = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            moveVector += Vector3.forward;
        }
        if (Input.GetKey(KeyCode.A))
        {
            moveVector += Vector3.left;

        }
        if (Input.GetKey(KeyCode.S))
        {
            moveVector += Vector3.back;

        }
        if (Input.GetKey(KeyCode.D))
        {
            moveVector += Vector3.right;

        }

        // Send moveInputs
        playerMove.HandleMove(moveVector);
    }
    #endregion
}
