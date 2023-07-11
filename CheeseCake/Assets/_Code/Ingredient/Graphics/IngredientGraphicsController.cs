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

    public float bounceMagnitude = 0.005f;
    public float bounceFrequency = 5f;

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

    public void ChangeIngredientGraphics(Ingredient.Type type)
    {
        // Destory previous graphics
        Destroy(transform.GetChild(0).gameObject);

        // Start up new graphics
        SetupCorrectGraphics(type);
    }

    private void SetupCorrectGraphics(Ingredient.Type type)
    {
        string path = GetCorrectString(type);

        graphics = Instantiate(Resources.Load(path + " Graphics"), transform) as GameObject;
    }

    private string GetCorrectString(Ingredient.Type type)
    {
        string path = "";

        switch (type)
        {
            case Ingredient.Type.milk:
                path = "Milk";
                break;
            //======================
            case Ingredient.Type.egg:
                path = "Egg";
                break;
            //======================
            case Ingredient.Type.flour:
                path = "Flours";
                break;
            //======================
            case Ingredient.Type.cake:
                path = "Cake";
                break;
            //======================
            case Ingredient.Type.cheese:
                path = "Cheese";
                break;
            //======================
            case Ingredient.Type.cheeseCake:
                path = "CheeseCake";
                break;
                //======================
        }

        return path;
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

    public void SpawnParticles(Ingredient.Type type)
    {
        string path = GetCorrectString(type) + " Particles";

        GameObject p = Instantiate(Resources.Load(path)) as GameObject;
        
        // Move
        p.transform.position = transform.position;

        // Set it to destroy itself
        Destroy(p, 10f);
    }
}
