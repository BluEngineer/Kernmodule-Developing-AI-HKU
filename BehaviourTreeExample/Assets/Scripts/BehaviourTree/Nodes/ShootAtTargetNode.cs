using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootAtTargetNode : ActionNode
{
    public GameObject projectilePrefab;
    public GameObject muzzleEffect;
    public int damage;
    public float shootForce;
    public float upAngleOffset;

    private Vector3 targetPosition;
    private Vector3 agentPosition;
    protected override void OnStart()
    {
        Debug.Log(blackboard.targetObject);
        if (blackboard.targetObject != null) targetPosition = blackboard.targetObject.transform.position;
        agentPosition = agent.gameObject.transform.position;
        Vector3 directionToTarget = (targetPosition - agent.projectilePoint.position).normalized;
        Vector3 currentDirection = agent.gameObject.transform.forward;

        GameObject projectile = GameObject.Instantiate(projectilePrefab, agent.projectilePoint.position, Quaternion.identity);
        GameObject.Instantiate(muzzleEffect, agent.projectilePoint.position, agent.projectilePoint.rotation);
        projectile.GetComponent<Rigidbody>().AddForce( new Vector3(directionToTarget.x, directionToTarget.y + upAngleOffset, directionToTarget.z) * shootForce, ForceMode.Impulse);
        projectile.GetComponent<DamagingProjectile>().damage = damage;
        projectile.GetComponent<DamagingProjectile>().sender = agent.gameObject;


        //blackboard.targetObject.GetComponent<IDamageable>().TakeDamage(agent.gameObject, damage);
        //Debug.Log(agent.name + " Damaged " + blackboard.targetObject.name + " for " + damage + " damage");


    }

    protected override void OnStop()
    {

    }

    protected override State OnUpdate()
    {
        return State.Success;
    }
}
