/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private GameObject prefab;
    private float spawnInMinSeconds = 1.0f;
    private float spawnInMaxSeconds = 1.0f;
    private float movementSpeed = 0.1f;
    private float generatedSeconds;
    private float destroyInSeconds = 20.0f;
    private float spawntimer;

    void Awake()
    {
        spawntimer = generatedSeconds = Random.range(spawnInMinSeconds, spawnInMaxSeconds);
    }
    // Start is called before the first frame update
    void Start()
    {
        var anchor = AnchorManager.Instance.VRAnchorManager.AddAnchor(new Pose(transform
            ));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
*/