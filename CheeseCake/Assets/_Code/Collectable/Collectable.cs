using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{

    #region Properties
    [SerializeField]
    float spinTimeBeforeDestruction = 2f;
    [SerializeField]
    float spinSpeed = 5f;
    [SerializeField]
    float spincrease = 5f;

    public float bounceMagnitude = 0.5f;
    public float bounceFrequency = 1f;

    [SerializeField]
    GameObject graphics;
    [SerializeField]
    GameObject particles;

    IEnumerator spinner;
    #endregion

    #region Setup
    private void Start()
    {
        
    }

    private void Update()
    {
        SpinAndHover();
    }
    #endregion

    #region Functions
    private void SpinAndHover()
    {                                     
        graphics.transform.Rotate(0f, spinSpeed * Time.deltaTime, 0f);

        // Hover
        graphics.transform.position = graphics.transform.position + Vector3.up * Mathf.Sin(Time.time * bounceFrequency) * bounceMagnitude;
    }
    #endregion

    #region Collision
    private void OnTriggerEnter(Collider other)
    {
        GetPickedUp();
    }

    private void GetPickedUp()
    {
        if (spinner == null)
        {
            bounceMagnitude = 0f;

            spinner = EndSpinner();
            StartCoroutine(spinner);
        }
    }

    IEnumerator EndSpinner()
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
