using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSystem : MonoBehaviour
{

    public GameObject m_SpawnPoint;
    public GameObject m_debugSpawnObject;
    public int m_maxNumberOfStrössel = 20;
    // Start is called before the first frame update
    void Start()
    {
        m_debugSpawnObject = Resources.Load("Prefabs/Default_cube", typeof(GameObject)) as GameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DebugSpawn()
    {
        Strössla(m_debugSpawnObject);
    }

    public void Strössla(GameObject strössel)
    {
        for (int i = 0; i < Random.Range(2, m_maxNumberOfStrössel); i++)
        {
            SpawnObject(strössel, new Vector3(this.transform.position.x + Random.Range(-5,5), this.transform.position.y + Random.Range(-5, 5), this.transform.position.z + Random.Range(-5, 5)));
        }
    }

    public void SpawnObject(GameObject objectToSPawn, Vector3 pos)
    {
        GameObject Object = Instantiate(objectToSPawn,pos, Quaternion.identity);
    }
}
