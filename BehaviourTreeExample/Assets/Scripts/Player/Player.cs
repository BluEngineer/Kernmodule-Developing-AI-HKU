using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;

public class Player : MonoBehaviour, IDamageable
{
    public Transform Camera;
    [SerializeField] private float rotationSpeed = 180f;
    [SerializeField] private float moveSpeed = 3;
    [SerializeField] private float deathForce = 1000;
    [SerializeField] private GameObject ragdoll;
    private Rigidbody rb;
    private Animator animator;
    private float vert = 0;
    private float hor = 0;
    private Vector3 moveDirection;
    private Collider mainCollider;

    [Header("Spear")] 
    public LayerMask rayMask;
    public GameObject spearPrefab;
    public float throwForce;
    public GameObject throwPoint;

    [Header("UI")] 
    public Slider playerHealthBar;

    public int maxHealth;
    public int health;

    public TextMeshProUGUI feedbackText;
    // Start is called before the first frame update
    void Start()
    {
        playerHealthBar.maxValue = maxHealth;
        playerHealthBar.value = maxHealth;
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        mainCollider = GetComponent<Collider>();
        var rigidBodies = GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rib in rigidBodies)
        {
            rib.isKinematic = true;
            //rib.useGravity = false;
        }

        var cols = GetComponentsInChildren<Collider>();
        foreach (Collider col in cols)
        {
            if (col.isTrigger) { continue; }
            col.enabled = false;
        }
        mainCollider.enabled = true;
        rb.isKinematic = false;
    }

    // Update is called once per frame
    void Update()
    {
        vert = Input.GetAxis("Vertical");
        hor = Input.GetAxis("Horizontal");
        Vector3 forwardDirection = Vector3.Scale(new Vector3(1, 0, 1), Camera.transform.forward);
        Vector3 rightDirection = Vector3.Cross(Vector3.up, forwardDirection.normalized);
        moveDirection = forwardDirection.normalized * vert + rightDirection.normalized * hor;
        if (moveDirection != Vector3.zero)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(moveDirection.normalized, Vector3.up), rotationSpeed * Time.deltaTime);
        }
        transform.position += moveDirection.normalized * moveSpeed * Time.deltaTime;

        bool isMoving = hor != 0 || vert != 0;
        ChangeAnimation(isMoving ? "Walk Crouch" : "Crouch Idle", isMoving ? 0.05f : 0.15f);
    }

    private void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ThrowSpear();
        }
    }

    public void TakeDamage(GameObject attacker, int damage)
    {
        health -= damage;
        playerHealthBar.value = health;
        
        if (health <= 0)
        {
            animator.enabled = false;
            var cols = GetComponentsInChildren<Collider>();
            foreach (Collider col in cols)
            {
                col.enabled = true;
            }
            mainCollider.enabled = false;

            var rigidBodies = GetComponentsInChildren<Rigidbody>();
            foreach (Rigidbody rib in rigidBodies)
            {
                rib.isKinematic = false;
                rib.useGravity = true;
                rib.AddForce(Vector3.Scale(new Vector3(1,0.5f,1),(transform.position - attacker.transform.position).normalized * deathForce));
            }
            ragdoll.transform.SetParent(null);

            gameObject.SetActive(false);        
        }

    }

    private void GetComponentsRecursively<T>(GameObject obj, ref List<T> components)
    {
        T component = obj.GetComponent<T>();
        if(component != null)
        {
            components.Add(component);
        }
        foreach(Transform t in obj.transform)
        {
            if(t.gameObject == obj) { continue; }
            GetComponentsRecursively<T>(t.gameObject, ref components);
        }
    }

    private void ThrowSpear()
    {
        GameObject spear = Instantiate(spearPrefab, throwPoint.transform.position, throwPoint.transform.rotation);
        Ray rayOrigin;
        RaycastHit hitInfo;
        if (Physics.Raycast(Camera.transform.position, Camera.transform.forward, out hitInfo, Mathf.Infinity,rayMask))
        {
            
            Vector3 dir = (throwPoint.transform.position - hitInfo.point).normalized;
            //Debug.Log(hitInfo.point);
            spear.transform.rotation = Quaternion.LookRotation(dir, Vector3.up);
            spear.GetComponent<Rigidbody>().AddForce(-dir * throwForce, ForceMode.Impulse);        
        }
        


    }
    
    private void ChangeAnimation(string animationName, float fadeTime)
    {
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName(animationName) && !animator.IsInTransition(0))
        {
            animator.CrossFade(animationName, fadeTime);
        }
    }
}
