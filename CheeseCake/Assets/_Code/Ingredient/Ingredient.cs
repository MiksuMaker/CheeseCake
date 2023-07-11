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
    public Type type;
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
}
