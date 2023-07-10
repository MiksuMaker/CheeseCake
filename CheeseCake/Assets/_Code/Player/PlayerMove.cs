using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    #region Properties
    [SerializeField]
    float moveSpeed = 5f;

    [SerializeField]
    GameObject graphics;
    #endregion

    #region Setup

    #endregion

    #region Functions
    public void HandleMove(Vector3 moveVector)
    {
        // Turn into the direction of moving
        HandleTurn(moveVector);

        // Move into the direction
        transform.position += moveVector.normalized * moveSpeed * Time.deltaTime;
    }

    private void HandleTurn(Vector3 directionVector)
    {
        graphics.transform.LookAt(transform.position + directionVector);
    }
    #endregion
}
