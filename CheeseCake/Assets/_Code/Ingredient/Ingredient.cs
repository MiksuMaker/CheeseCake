using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : MonoBehaviour
{
    #region Properties
    IngredientGraphicsController graphics;

    public enum Type
    {
        milk, egg, flour,
        cheese, cake, 
        cheeseCake,
    }
    [Header("Ingredient Type")]
    public Type type;

    IEnumerator collionCoroutine;
    #endregion

    #region Setup
    private void Start()
    {
        // Setup graphics
        graphics = GetComponentInChildren<IngredientGraphicsController>();
        graphics.SetupIngredientGraphics();
    }
    #endregion

    #region Functions

    #endregion

    #region Collision
    protected virtual void OnCollisionEnter(Collision other)
    {
        graphics.TurnSpinOnOff(true);
        //GetPickedUp();
    }

    protected virtual void OnCollisionExit(Collision other)
    {
        graphics.TurnSpinOnOff(false);
    }

    protected virtual void GetPickedUp()
    {
        //if (collionCoroutine == null)
        //{
        //    collionCoroutine = CollisionCoroutine();
        //    StartCoroutine(collionCoroutine);
        //}
    }

    protected virtual IEnumerator CollisionCoroutine()
    {
        yield return new WaitForSeconds(1f);
    }
    #endregion
}
