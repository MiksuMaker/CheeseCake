using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientSpawner : MonoBehaviour
{
    #region Properties
    [Header("Spawning")]
    [SerializeField]
    float spawnWaitTime = 3f;
    [SerializeField]
    int initialSpawnsAmount = 5;
    [SerializeField]
    int spawnMax = 10;
    [Header("Spawn Area")]
    [SerializeField]
    float spawnWidth = 5f;
    [SerializeField]
    float spawnDepth = 5f;

    [Header("Parent")]
    [SerializeField]
    GameObject parent;

    List<GameObject> ingredientsList = new List<GameObject>();
    #endregion

    #region Setup
    private void Awake()
    {
        // Fetch references

    }

    private void Start()
    {
        DoInitialSpawnRound();

        // Start spawning sequence'
        StartCoroutine(SpawnCoroutine());
    }


    #endregion

    #region Spawning
    private void DoInitialSpawnRound()
    {
        for (int i = 0; i < initialSpawnsAmount; i++)
        {
            SpawnNewIngredient();
        }
    }

    IEnumerator SpawnCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnWaitTime);

            if (ingredientsList.Count <= spawnMax)
            {
                // Spawn new ingredient
                SpawnNewIngredient();

            }
        }
    }

    private void SpawnNewIngredient()
    {
        GameObject newIng = Instantiate(Resources.Load("Ingredient"), parent.transform) as GameObject;

        // Move to random location
        newIng.transform.position = new Vector3(Random.Range(-spawnWidth, spawnWidth),
                                                5f,
                                                Random.Range(-spawnDepth, spawnDepth));

        // Add to the list
        ingredientsList.Add(newIng);

        // Randomize type
        newIng.GetComponent<Ingredient>().SetupIngredient(RandomizeIngredientType());
    }

    private Ingredient.Type RandomizeIngredientType()
    {
        int num = Random.Range(0, 3);

        Ingredient.Type type = Ingredient.Type.milk;

        if (num == 0)
        {
            type = Ingredient.Type.milk;
        }
        else if (num == 1)
        {
            type = Ingredient.Type.egg;
        }
        else if (num == 2)
        {
            type = Ingredient.Type.flour;
        }

        return type;
    }
    #endregion
}
