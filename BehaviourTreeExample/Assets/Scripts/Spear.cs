using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spear : MonoBehaviour
{
    private Rigidbody rigidBody;
    public int spearDamage;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        rigidBody = GetComponent<Rigidbody>();
        GetComponent<Rigidbody>().isKinematic = true;
        StartCoroutine(DeleteTimer());
        if (other.gameObject.GetComponent<IDamageable>() != null)
        {
            other.gameObject.GetComponent<IDamageable>().TakeDamage(gameObject, spearDamage);
            rigidBody.velocity = Vector3.zero;
            transform.SetParent(other.transform);
        }
    }

    public IEnumerator DeleteTimer()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }
}
