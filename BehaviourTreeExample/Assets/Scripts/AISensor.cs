using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class AISensor : MonoBehaviour
{
    public float distance = 10;
    public float angle = 40;
    public float height = 1;
    public Color coneColor = Color.cyan;
    public int scanFrequency = 30;
    public LayerMask layers;
    public LayerMask occlusionLayers;
    
    private Mesh mesh;

    public List<GameObject> objectsInVision = new List<GameObject>();
    private Collider[] collidersInRange = new Collider[50];
    private int objectInSphereCount;
    private float scanInterval;
    public float sensorHeightOffset;
    private float scanTimer;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        scanTimer -= Time.deltaTime;
        if (scanTimer < 0)
        {
            scanTimer += scanInterval;
            Scan();
        }
    }

    private void Scan()
    {
        objectInSphereCount = Physics.OverlapSphereNonAlloc(transform.position, distance, collidersInRange, layers,
            QueryTriggerInteraction.Collide);
        
        objectsInVision.Clear();
        for (int i = 0; i < objectInSphereCount; i++)
        {
            GameObject _obj = collidersInRange[i].gameObject;
            if (IsInSight(_obj))
            {
                objectsInVision.Add(_obj);
            }
        }
    }
    
    
    private Mesh CreateVisionConeMesh()
    {
        Mesh mesh = new Mesh();

        int numTriangles = 8;
        int numVertices = numTriangles * 3;

        Vector3[] vertices = new Vector3[numVertices];
        int[] triangles = new int[numVertices];

        Vector3 bottomCenter = Vector3.zero;
        Vector3 bottomLeft = Quaternion.Euler(0, -angle, 0) * Vector3.forward * distance;
        Vector3 bottomRight = Quaternion.Euler(0, angle, 0) * Vector3.forward * distance;

        Vector3 topLeft = bottomLeft + Vector3.up * height;
        Vector3 topCenter = bottomCenter + Vector3.up * height;
        Vector3 topRight = bottomRight + Vector3.up * height;

        int vertIndex = 0;
        
        //left face
        vertices[vertIndex++] = bottomCenter;
        vertices[vertIndex++] = bottomLeft;
        vertices[vertIndex++] = topLeft;
        
        vertices[vertIndex++] = topLeft;
        vertices[vertIndex++] = topCenter;
        vertices[vertIndex++] = bottomCenter;
        
        //right face 
        vertices[vertIndex++] = bottomCenter;
        vertices[vertIndex++] = topCenter;
        vertices[vertIndex++] = topRight;

        vertices[vertIndex++] = topRight;
        vertices[vertIndex++] = bottomRight;
        vertices[vertIndex++] = bottomCenter;


        //far face
        vertices[vertIndex++] = bottomLeft;
        vertices[vertIndex++] = bottomRight;
        vertices[vertIndex++] = topRight;

        vertices[vertIndex++] = topRight;
        vertices[vertIndex++] = topLeft;
        vertices[vertIndex++] = bottomLeft;

        //top face
        vertices[vertIndex++] = topCenter;
        vertices[vertIndex++] = topLeft;
        vertices[vertIndex++] = topRight;

        //bottom face
        vertices[vertIndex++] = bottomCenter;
        vertices[vertIndex++] = bottomLeft;
        vertices[vertIndex++] = bottomRight;

        for (int i = 0; i < vertices.Length; i++)
        {
            triangles[i] = i;
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        
        return mesh;
    }

    public bool IsInSight(GameObject _obj)
    {
        Vector3 origin = transform.position;
        Vector3 dest = _obj.transform.position;
        Vector3 direction = dest - origin;
        if (direction.y < 0 || direction.y > height)
        {
            return false;
        }

        direction.y = 0;
        float deltaAngle = Vector3.Angle(direction, transform.forward);
        if (deltaAngle > angle)
        {
            return false;
        }

        origin.y += height / 2;
        dest.y = origin.y;
        if (Physics.Linecast(origin, dest, occlusionLayers))
        {
            return false;
        }
        return true;
    }
    
    private void OnValidate()
    {
        mesh = CreateVisionConeMesh();
    }

    public GameObject FindClosestVisibleObjectWithTag(string _tag)
    {
        GameObject closestObject = null;
        float shortestDist = 10000;

        List<GameObject> taggedObjects = new List<GameObject>();

        if (objectsInVision != null)
        {
            foreach (var o in objectsInVision)
            {
                if (o.tag == _tag)
                {
                    taggedObjects.Add(o);

                }
  
            }
        
            for (int i = 0; i < taggedObjects.Count; i++)
            {
                float tempDist = Vector3.Distance(transform.position, taggedObjects[i].transform.position);
            
                if (tempDist < shortestDist)
                {
                    shortestDist = tempDist;
                    closestObject = taggedObjects[i];
                }
            }
     
        }

        return closestObject;
    }
    
    private void OnDrawGizmos()
    {
        if (mesh)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawMesh(mesh, transform.position, transform.rotation);
        }
        
        Gizmos.DrawWireSphere(transform.position, distance);
        for (int i = 0; i < objectInSphereCount; i++)
        {
            //Gizmos.DrawSphere(collidersInRange[i].transform.position, 0.2f);
        }

        Gizmos.color = Color.green;
        foreach (var o in objectsInVision)
        {
            Gizmos.DrawSphere(o.transform.position, 0.7f);
        }
    }
}
