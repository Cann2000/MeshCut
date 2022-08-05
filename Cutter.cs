using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;  // !!!


public class Cutter : MonoBehaviour
{
    Material mat;
    GameObject kesilenobj;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && kesilenobj != null)
        {
            SlicedHull kesilen = Cut(kesilenobj, mat);

            //Kesilen Ust

            GameObject kesilenust = kesilen.CreateUpperHull(kesilenobj, mat);
            kesilenust.AddComponent<MeshCollider>().convex = true;
            kesilenust.AddComponent<Rigidbody>();
            kesilenust.layer = LayerMask.NameToLayer("Kesilebilir");

            //Kesilen alt

            GameObject kesilenalt = kesilen.CreateLowerHull(kesilenobj, mat);
            kesilenalt.AddComponent<MeshCollider>().convex = true;
            kesilenalt.AddComponent<Rigidbody>();
            kesilenalt.layer = LayerMask.NameToLayer("Kesilebilir");

            Destroy(kesilenobj);

            kesilenobj = null;

        }

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Kesilebilir"))
        {
            mat = other.gameObject.GetComponent<MeshRenderer>().material;
            kesilenobj = other.gameObject;
        }
    }

    public SlicedHull Cut(GameObject obj, Material crossSectionMaterial = null)
    {
        return obj.Slice(transform.position, transform.up, crossSectionMaterial);
    }
}
