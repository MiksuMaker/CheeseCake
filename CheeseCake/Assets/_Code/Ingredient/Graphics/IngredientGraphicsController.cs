using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientGraphicsController : MonoBehaviour
{
    #region Properties
    [SerializeField]
    protected float idleSpinSpeed = 10f;
    [SerializeField]
    protected float touchSpinSpeed = 50f;
    float currentSpinSpeed;

    public float bounceMagnitude = 0.5f;
    public float bounceFrequency = 1f;

    [SerializeField]
    protected GameObject graphics;
    [SerializeField]
    protected GameObject particles;

    protected IEnumerator graphicsUpdateLoop;
    #endregion

    #region Setup
    public void SetupIngredientGraphics(Ingredient.Type type = Ingredient.Type.milk)
    {
        // Hook up graphics
        //graphics = transform.GetChild(0).gameObject;
        //if (graphics == null) { Debug.LogWarning("No Graphics component found on " + transform.parent.name); }

        currentSpinSpeed = idleSpinSpeed;

        SetupCorrectGraphics(type);

        graphicsUpdateLoop = GraphicsUpdateLoop();
        StartCoroutine(graphicsUpdateLoop);
    }

    private void SetupCorrectGraphics(Ingredient.Type type)
    {
        string path = "";

        switch (type)
        {
            case Ingredient.Type.milk:
                path = "Milk Graphics";
                break;
            //======================
            case Ingredient.Type.egg:
                path = "Egg Graphics";
                break;
            //======================
            case Ingredient.Type.flour:
                path = "Flour Graphics";
                break;
            //======================
            case Ingredient.Type.cake:
                path = "Cake Graphics";
                break;
            //======================
            case Ingredient.Type.cheese:
                path = "Cheese Graphics";
                break;
            //======================
            case Ingredient.Type.cheeseCake:
                path = "CheeseCake Graphics";
                break;
                //======================
        }

        graphics = Instantiate(Resources.Load(path), transform) as GameObject;
    }


    IEnumerator GraphicsUpdateLoop()
    {
        while (true)
        {
            yield return new WaitForEndOfFrame();

            SpinAndHover();
        }
    }


    #endregion

    #region Functions
    protected virtual void SpinAndHover()
    {
        graphics.transform.Rotate(0f, currentSpinSpeed * Time.deltaTime, 0f);

        // Hover
        graphics.transform.position = graphics.transform.position + Vector3.up * Mathf.Sin(Time.time * bounceFrequency) * bounceMagnitude;
    }
    #endregion

    #region Collision
    public void TurnSpinOnOff(bool onOff)
    {
        if (onOff)
        {
            currentSpinSpeed = touchSpinSpeed;
        }
        else
        {
            currentSpinSpeed = idleSpinSpeed;
        }
    }
    #endregion
}
