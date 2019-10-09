using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class missile : MonoBehaviour
{

    public GameObject m_target;
    public float speed = 100;
    public float turn = 1;
    public GameObject m_boulder;

    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        m_boulder = Resources.Load<GameObject>("Prefabs/rock_001");
        rb = GetComponent<Rigidbody>();
        Invoke("StartCollide", 3);
    }

    // Update is called once per frame
    void Update()
    {
        if (m_target != null)
        {
            Vector3 relativePos = m_target.transform.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(relativePos);

            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime / turn);
            transform.Translate(0, 0, speed * Time.deltaTime, Space.Self);
        }

    }

    private void OnCollisionEnter(Collision collision)
    {



        for (int i = 0; i < 5; i++)
        {

            GameObject obj = Instantiate(m_boulder, collision.GetContact(0).point, Quaternion.identity);
            obj.GetComponent<MeshCollider>().enabled = false;
            obj.GetComponent<Rigidbody>().useGravity = false;
            obj.transform.localScale = Random.Range(1, 3) * obj.transform.localScale;
            obj.GetComponent<Rigidbody>().velocity =  new Vector3(collision.GetContact(0).point.x + Random.Range(-1.0f, 1.0f), collision.GetContact(0).point.y +  Random.Range(-1.0f, 1.0f), collision.GetContact(0).point.z + Random.Range(-1.0f, 1.0f));
            obj.GetComponent<Rigidbody>().AddTorque(new Vector3(Random.Range(-5.0f, 5.0f), Random.Range(-5.0f, 5.0f), Random.Range(-5.0f, 5.0f)));
            obj.GetComponent<Rigidbody>().AddTorque(new Vector3(Random.Range(-5.0f, 5.0f), Random.Range(-5.0f, 5.0f), Random.Range(-5.0f, 5.0f)));
        }
        AudioManager.Instance.PlayBoomLow();
        PlanetJointHandler.Instance.m_connected.Remove(collision.gameObject);
        Destroy(this.gameObject);
    }

    public void StartCollide()
    {
        Debug.Log("enabled");
        this.GetComponent<CapsuleCollider>().enabled = true;
    }
}
