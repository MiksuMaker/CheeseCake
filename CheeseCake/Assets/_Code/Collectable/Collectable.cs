using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{

    #region Properties
    [SerializeField]
    protected float spinTimeBeforeDestruction = 2f;
    [SerializeField]
    protected float spinSpeed = 5f;
    [SerializeField]
    protected float spincrease = 5f;

    public float bounceMagnitude = 0.5f;
    public float bounceFrequency = 1f;

    [SerializeField]
    protected GameObject graphics;
    [SerializeField]
    protected GameObject particles;

    protected IEnumerator spinner;
    #endregion

    #region Setup

    protected void Update()
    {
        SpinAndHover();
    }
    #endregion

    #region Functions
    protected virtual void SpinAndHover()
    {                                     
        graphics.transform.Rotate(0f, spinSpeed * Time.deltaTime, 0f);

        // Hover
        graphics.transform.position = graphics.transform.position + Vector3.up * Mathf.Sin(Time.time * bounceFrequency) * bounceMagnitude;
    }
    #endregion

    #region Collision
    protected virtual void OnTriggerEnter(Collider other)
    {
        GetPickedUp();
    }

    protected virtual void GetPickedUp()
    {
        if (spinner == null)
        {
            bounceMagnitude = 0f;

            spinner = EndSpinner();
            StartCoroutine(spinner);
        }
    }

    protected virtual IEnumerator EndSpinner()
    {
        float increment = 0.1f;
        float time = 0f;
        float timeLimit = spinTimeBeforeDestruction;
        WaitForSeconds wait = new WaitForSeconds(increment);

        while (time < timeLimit)
        {
            time += increment;

            //spinSpeed += (spincrease);
            spinSpeed *= spincrease;

            yield return wait;
        }

        GameObject partic = Instantiate(particles) as GameObject;
        partic.transform.position = transform.position;
        Destroy(partic, 5f);

        Destroy(gameObject);
    }
    #endregion
}
