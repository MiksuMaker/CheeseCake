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
    public void SetupIngredient(Type _type)
    {
        type = _type;

        // Setup graphics
        graphics = GetComponentInChildren<IngredientGraphicsController>();
        graphics.SetupIngredientGraphics(type);

        // Check for Milk
        if (type == Type.milk)
        {
            StartMilkAging();
        }
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
                other.GetMixedAndDestroyed();
                break;
            // ==============
            case (Type.flour, Type.egg):
                StartCoroutine(Mix(Type.cake));
                other.GetMixedAndDestroyed();
                break;
            // ==============
            case (Type.cheese, Type.cake):
                StartCoroutine(Mix(Type.cheeseCake));
                other.GetMixedAndDestroyed();
                break;
            // ==============
            case (Type.cake, Type.cheese):
                StartCoroutine(Mix(Type.cheeseCake));
                other.GetMixedAndDestroyed();
                break;
            // ==============
            default:
                // Can't mix
                break;
        }
    }

    IEnumerator Mix(Type newType)
    {
        WaitForSeconds wait = new WaitForSeconds(0.1f);

        mixingInProcess = true;

        yield return wait;

        GetMixed(newType);
        
        mixingInProcess = false;
    }

    private void GetMixed(Type newType)
    {
        // Spawn particles
        graphics.SpawnParticles(type);

        type = newType;
        graphics.ChangeIngredientGraphics(newType);
    }

    public void GetMixedAndDestroyed()
    {
        if (mixingInProcess) { return; }

        // Do the UI particles
        graphics.SpawnParticles(type);

        // Destroy itself
        Destroy(gameObject);
    }
    #endregion

    #region Milk
    public void StartMilkAging()
    {
        StartCoroutine(MilkAgeTimer());
    }

    IEnumerator MilkAgeTimer()
    {
        float increment = 0.1f;
        WaitForSeconds wait = new WaitForSeconds(increment);

        float milkToCheeseTime = 2f;
        float waitedTime = 0f;

        while (waitedTime < milkToCheeseTime)
        {
            waitedTime += increment;

            // Speed up spinning
            //graphics.currentSpinSpeed *= 1.5f;

            yield return wait;
        }

        // Turn to milk
        GetMixed(Type.cheese);
    }
    #endregion
}
