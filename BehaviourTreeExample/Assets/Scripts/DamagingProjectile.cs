using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagingProjectile : MonoBehaviour
{
    [HideInInspector] public int damage;
    [HideInInspector] public GameObject sender;

    public GameObject impactEffect;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<IDamageable>() != null )
        {
            collision.gameObject.GetComponent<IDamageable>().TakeDamage(sender, damage);

        }
        Instantiate(impactEffect, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
