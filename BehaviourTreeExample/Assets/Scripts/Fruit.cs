using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour, IEdible
{
    public float foodHungerValue;
    public float foodValue { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        foodValue = foodHungerValue;
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }


    public void Interact()
    {
        if (gameObject != null)
        {
            
            gameObject.SetActive(false);
 
        }
        
    }

}
