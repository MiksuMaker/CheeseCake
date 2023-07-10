using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{

    #region Properties
    [SerializeField]
    float spinSpeed = 5f;

    public float bounceMagnitude = 0.5f;
    public float bounceFrequency = 1f;

    [SerializeField]
    GameObject graphics;
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
        //graphics.transform.rotation = Quaternion.Euler(transform.rotation.x,
        //                                                  transform.rotation.y + spinSpeed * Time.deltaTime,
        //                                                  transform.rotation.z);

        graphics.transform.Rotate(0f, spinSpeed * Time.deltaTime, 0f);

        // Hover
        graphics.transform.position = graphics.transform.position + Vector3.up * Mathf.Sin(Time.time * bounceFrequency) * bounceMagnitude;
    }
    #endregion
}
