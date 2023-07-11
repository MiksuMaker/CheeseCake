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

    IEnumerator mixingCoroutine;
    bool mixingInProcess = false;
    #endregion

    #region Setup
    private void Start()
    {
        // Setup graphics
        graphics = GetComponentInChildren<IngredientGraphicsController>();
        graphics.SetupIngredientGraphics(type);
    }
    #endregion

    #region Functions

    #endregion

    #region Collision
    protected virtual void OnCollisionEnter(Collision other)
    {
        graphics.TurnSpinOnOff(true);

        HandleCollision(other);
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

    #region Ingredient Detection
    private void HandleCollision(Collision c)
    {
        // if player

        // if oven

        // Ingredients
        if (mixingInProcess) { return; }

        if (c.collider.gameObject.layer == LayerMask.NameToLayer("Ingredient"))
        {
            Ingredient other = c.collider.gameObject.GetComponent<Ingredient>();

            HandleIngredientMixing(other);

            // Destroy the other
            other.GetMixed();
        }
    }
    #endregion

    #region Ingredient Changing
    private void HandleIngredientMixing(Ingredient other)
    {
        Type otherType = other.type;

        switch (type, otherType)
        {
            case (Type.egg, Type.flour):
                StartCoroutine(Mix(Type.cake));
                break;
            // ==============
            case (Type.flour, Type.egg):
                StartCoroutine(Mix(Type.cake));

                break;
            // ==============
            case (Type.cheese, Type.cake):
                StartCoroutine(Mix(Type.cheeseCake));

                break;
            // ==============
            case (Type.cake, Type.cheese):
                StartCoroutine(Mix(Type.cheeseCake));

                break;
            // ==============
            default:
                // Can't mix
                break;
        }
    }

    IEnumerator Mix(Type newType)
    {
        WaitForSeconds wait = new WaitForSeconds(0.5f);

        mixingInProcess = true;

        yield return wait;

        // Change type
        type = newType;
        graphics.ChangeIngredientGraphics(newType);


        mixingInProcess = false;
    }

    public void GetMixed()
    {
        if (mixingInProcess) { return; }

        // Do the UI particles

        // Destroy itself
        Destroy(gameObject);
    }
    #endregion
}
