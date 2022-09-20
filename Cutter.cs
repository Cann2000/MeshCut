using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;  // !!!


public class Cutter : MonoBehaviour
{
    Material mat;
    GameObject CutObj;


    void Update()
    {
        if (Input.GetMouseButtonDown(0) && CutObj != null)
        {
            SlicedHull MeshCut = Cut(CutObj, mat);

            //Kesilen Ust

            GameObject CutTop = MeshCut.CreateUpperHull(CutObj, mat);
            CutTop.AddComponent<MeshCollider>().convex = true;
            CutTop.AddComponent<Rigidbody>();
            CutTop.layer = LayerMask.NameToLayer("Kesilebilir");

            //Kesilen alt

            GameObject CutBottom = MeshCut.CreateLowerHull(CutObj, mat);
            CutBottom.AddComponent<MeshCollider>().convex = true;
            CutBottom.AddComponent<Rigidbody>();
            CutBottom.layer = LayerMask.NameToLayer("Kesilebilir");

            Destroy(CutObj);

            CutObj = null;

        }

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Kesilebilir"))
        {
            mat = other.gameObject.GetComponent<MeshRenderer>().material;
            CutObj = other.gameObject;
        }
    }

    public SlicedHull Cut(GameObject obj, Material crossSectionMaterial = null)
    {
        return obj.Slice(transform.position, transform.up, crossSectionMaterial);
    }
}
