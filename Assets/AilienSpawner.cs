using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AilienSpawner : MonoBehaviour
{

    public GameObject m_spawnplace;
    public GameObject m_endtarget;
    public GameObject m_start;
    public GameObject m_end;
    public GameObject m_ailienPrefab;

    // Start is called before the first frame update
    void Start()
    {
        m_start = Instantiate(m_spawnplace, new Vector3(this.transform.position.x - 100, this.transform.position.x, this.transform.position.x), Quaternion.identity);
        m_end = Instantiate(m_endtarget, new Vector3(this.transform.position.x + 100, this.transform.position.x, this.transform.position.x), Quaternion.identity);
        InvokeRepeating("SpawnAilienEvent",120,60);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightShift))
        {
            SpawnAilienEvent();
        }
    }

    public void SpawnAilienEvent()
    {
       GameObject ailien =  Instantiate(m_ailienPrefab, m_start.transform.position, Quaternion.identity);
        ailien.GetComponent<AilienAttacker>().m_starttarget = this.gameObject;
        ailien.GetComponent<AilienAttacker>().m_endTarget = m_end;

    }
}
