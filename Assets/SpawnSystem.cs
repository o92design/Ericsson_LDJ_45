using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSystem : MonoBehaviour
{

    public GameObject m_SpawnPoint;
    public GameObject m_debugSpawnObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnObject(GameObject objectToSPawn)
    {
        GameObject Object = Instantiate(objectToSPawn,m_SpawnPoint.transform.position, Quaternion.identity);
    }
}
