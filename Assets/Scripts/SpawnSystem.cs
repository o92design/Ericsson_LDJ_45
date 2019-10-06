using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSystem : MonoBehaviour
{

    public GameObject m_SpawnPoint;
    public GameObject m_debugSpawnObject;
    public GameObject m_debugSpawnHuman;

    public List<GameObject> m_prefabs;

    public int m_maxNumberOfStrössel = 20;
    // Start is called before the first frame update
    void Start()
    {
        m_debugSpawnObject = Resources.Load("Prefabs/Default_cube", typeof(GameObject)) as GameObject;
        m_debugSpawnHuman = Resources.Load("Prefabs/Human", typeof(GameObject)) as GameObject;
        m_prefabs = new List<GameObject>();
        m_prefabs.Add(Resources.Load("Prefabs/tree_001", typeof(GameObject)) as GameObject);
        m_prefabs.Add(Resources.Load("Prefabs/tree_002", typeof(GameObject)) as GameObject);
        m_prefabs.Add(Resources.Load("Prefabs/tree_003", typeof(GameObject)) as GameObject);
        m_prefabs.Add(Resources.Load("Prefabs/pyramid_001", typeof(GameObject)) as GameObject);
        m_prefabs.Add(Resources.Load("Prefabs/rock_001", typeof(GameObject)) as GameObject);
        m_prefabs.Add(Resources.Load("Prefabs/Human", typeof(GameObject)) as GameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DebugSpawn()
    {
        Strössla(m_debugSpawnObject);
    }

    public void DebugSpawnHumans()
    {
        Strössla(m_debugSpawnHuman);
    }

    public void DebugSpawnTrees(int i)
    {
        Strössla(m_prefabs[i]);
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
        GameObject Object = Instantiate(objectToSPawn,pos, objectToSPawn.gameObject.transform.rotation);
    }
}
