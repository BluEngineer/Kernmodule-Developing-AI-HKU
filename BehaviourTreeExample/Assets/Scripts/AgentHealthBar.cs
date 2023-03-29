using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class AgentHealthBar : MonoBehaviour, IDamageable
{
    public int agentMaxHealth;
    public int currentHealth;
    public Slider healthBarUI;

    public GameObject ragdollPrefab;
    
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = agentMaxHealth;
        healthBarUI.maxValue = agentMaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        healthBarUI.value = currentHealth;


    }

    public void TakeDamage(GameObject attacker, int damage)
    {
        currentHealth -= damage;
        Debug.Log(attacker + "  attacked" + name);

        if (currentHealth < 0)
        {
            Death();

        }
        
    }

    private void Death()
    {
        float yPos;
        yPos = transform.position.y - 0.516f;

        GameObject ragDoll = Instantiate(ragdollPrefab, new Vector3(transform.position.x, yPos, transform.position.z) , transform.rotation);
        ragDoll.transform.localScale = new Vector3(8.152189f * transform.localScale.x, 8.152189f * transform.localScale.y, 8.152189f * transform.localScale.z) ;
        ragDoll.GetComponent<Rigidbody>().velocity = GetComponent<NavMeshAgent>().velocity;
        Destroy(this.gameObject);
    }
}
