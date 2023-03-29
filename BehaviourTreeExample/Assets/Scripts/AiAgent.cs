using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AgentHealthBar))]

public class AiAgent : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public string objectInTriggerTag;
    public List <GameObject> objectsInTrigger;
    public IInteractable interactableInRange;

    public AISensor sensor;
    public Transform projectilePoint;

    private Vector3 previousPosition;

    public float animatorWalkSpeedMultiplier;
    public float currentSpeed;

    public Animator animator;
    public TextMeshProUGUI feedbackText;
    public AgentHealthBar healthComponent;

    public GameObject ParticleTrail;
    //status effect stuff (dit moet echt verplaatst worden spaghetrti code)
    public GameObject slowedGraphic;
    public bool slowed;

    // Start is called before the first frame update
    void Start()
    {
        //sensor = GetComponent<AISensor>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        healthComponent = GetComponent<AgentHealthBar>();
    }

    // Update is called once per frame
    void Update()
    {
        CalculateAgentSpeed();  
        UpdateAnimator();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!objectsInTrigger.Contains(other.gameObject))
        {
            //add the object to the list
            objectsInTrigger.Add(other.gameObject);
        }

    }

    private void OnTriggerStay(Collider other)
    {
        objectInTriggerTag = other.tag;

        if (other.GetComponent<IInteractable>() != null)
        {
            interactableInRange = other.GetComponent<IInteractable>();
        }

        //Debug.Log(objectInTriggerTag);
    }

    private void OnTriggerExit(Collider other)
    {
        objectInTriggerTag = null;
        //if the object is in the list
        if (objectsInTrigger.Contains(other.gameObject))
        {
            //remove it from the list
            objectsInTrigger.Remove(other.gameObject);
        }
        if (other.GetComponent<IInteractable>() != null)
        {
            interactableInRange = null;
        }
    }

    public GameObject FindClosestObjectWithTag(string _tag)
    {
        GameObject closestObject = null;
        float shortestDist = 10000;
        GameObject[] objects = GameObject.FindGameObjectsWithTag(_tag);
        
        for (int i = 0; i < objects.Length; i++)
        {
            float tempDist = Vector3.Distance(transform.position, objects[i].transform.position);
            
            if (tempDist < shortestDist)
            {
                shortestDist = tempDist;
                closestObject = objects[i];
            }
        }

        return closestObject;

    }

    private void CalculateAgentSpeed()
    {
        Vector3 curMove = transform.position - previousPosition;
        currentSpeed = curMove.magnitude / Time.deltaTime;
        previousPosition = transform.position;
    }

    public void UpdateUIText(string _newText)
    {
        feedbackText.text = _newText;
    }
    
    private void UpdateAnimator()
    {
        if (currentSpeed > 0.3)
        {
            animator.SetBool("Moving" ,true);
        }
        else
        {
            animator.SetBool("Moving" ,false);

        }
        float speed = currentSpeed * animatorWalkSpeedMultiplier;
        
        animator.SetFloat("MoveX", speed);
    }

    private void ApplySlow()
    {
        if (slowed)
        {
            GetComponent<AiAgent>().currentSpeed /= 2;
            slowedGraphic.SetActive(true);

        }
        else
        {
            slowedGraphic.SetActive(true);

        }
    }


}
