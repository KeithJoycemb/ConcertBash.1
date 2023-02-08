
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FracturedObject : MonoBehaviour
{
    public GameObject originalSource;
    public GameObject fracturedSource;
    public float minForce = 5;
    public float maxForce = 100;
    public float explosionradius = 10;
    public float fragScaleFactor = 1;

    private GameObject fractObj;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Explode();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            Reset();
        }
    }

    void Reset()
    {
        Destroy(fractObj);
        originalSource.SetActive(true);
    }

    private void Explode()
    {
        if (originalSource != null)
        {
            originalSource.SetActive(false);
            if (fracturedSource != null)
            {
                fractObj = Instantiate(fracturedSource) as GameObject;

                foreach (Transform t in fractObj.transform)
                {
                    var rb = t.GetComponent<Rigidbody>();
                    if (rb != null)
                        rb.AddExplosionForce(Random.Range(minForce, maxForce), originalSource.transform.position, explosionradius);
                    StartCoroutine(Shrink(t, 2));
                }
                DestroyImmediate(fractObj);

            }
        }
    }

    IEnumerator Shrink(Transform t, float delay)
    {
        yield return new WaitForSeconds(delay);
        Vector3 newScale = t.localScale;
        while(newScale.x>=0)
        {
            newScale -= new Vector3(fragScaleFactor, fragScaleFactor, fragScaleFactor);
            t.localScale = newScale;
            yield return new WaitForSeconds(0.05f);
        }
    }
}