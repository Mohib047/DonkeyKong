using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    //Variable Declaration
    public GameObject barrelsPrefab;

    void Spawner() 
    {
        Instantiate(barrelsPrefab, transform.position, barrelsPrefab.transform.rotation);
    }
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Spawner", 1f, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
