using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class SpawnSystem : MonoBehaviour
{

    public GameObject m_SpawnPoint;
    public GameObject m_debugSpawnObject;
    public GameObject m_debugSpawnHuman;

    public List<GameObject> m_prefabs;

    public TextMeshProUGUI m_slidervalue;
    public Slider m_valueslider;
    public TextMeshProUGUI m_slidervalue2;
    public Slider m_valueslider2;


    public int m_maxNumberOfStrössel = 20;
    public int m_minNumberOfStrössel = 1;
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
        m_prefabs.Add(Resources.Load("Prefabs/water_poly", typeof(GameObject)) as GameObject);
        m_prefabs.Add(Resources.Load("Prefabs/Human", typeof(GameObject)) as GameObject);
    }

    // Update is called once per frame
    void Update()
    {
        m_slidervalue.text = m_valueslider.value.ToString();
        m_maxNumberOfStrössel = (int) m_valueslider.value;
        m_slidervalue2.text = m_valueslider2.value.ToString();
        m_minNumberOfStrössel = (int)m_valueslider2.value;
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
        for (int i = 0; i < Random.Range(m_minNumberOfStrössel, m_maxNumberOfStrössel); i++)
        {
             SpawnObject(strössel, new Vector3(this.transform.position.x + Random.Range(-5,5), this.transform.position.y + Random.Range(-5, 5), this.transform.position.z + Random.Range(-5, 5)));
        }
    }

    public void SpawnObject(GameObject objectToSPawn, Vector3 pos)
    {
        GameObject Object = Instantiate(objectToSPawn,pos, objectToSPawn.gameObject.transform.rotation);
        if (objectToSPawn == m_debugSpawnHuman)
        {
            Object.GetComponent<Objekt_Skit>().objtype = 3;
        }
        float scalemodifier = Random.Range(0.1f, 2);
        Object.transform.rotation = Quaternion.Euler(new Vector3(Object.transform.eulerAngles.x, Random.Range(-180.0f, 180.0f), Object.transform.eulerAngles.z));
        Object.transform.localScale = new Vector3(Object.transform.localScale.x * scalemodifier, Object.transform.localScale.y * scalemodifier, Object.transform.localScale.z * scalemodifier);
    }
}
