using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AilienAttacker : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public GameObject m_endTarget;
    public GameObject m_starttarget;
    public GameObject m_firetarget;
    public float speed = 1;
    private float time = 0f;
    private float time2 = 0f;
    float interpolationPeriod = 30f;
    float interpolationPeriod2 = 3f;
    public bool rotate = false;
    public bool move_to_end = false;
    public Vector3 desiredPosition;
    float radius = 10.0f;
    float radiusSpeed = 5f;
    float m_firerate = 3f;
    public bool m_shooting = false;
    public GameObject m_boulder;
    public bool m_exploded = false;
    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        m_boulder = Resources.Load< GameObject > ("Prefabs/rock_001");
    }

    // Update is called once per frame
    void Update()
    {
        

        if (transform.position.x >= m_starttarget.transform.position.x -10 && !rotate)
        {
            AudioManager.Instance.PlayAilien();
            StartCoroutine("RotateAriund");
            rotate = true;
        }
        else if (move_to_end)
        {
            float step = speed * Time.deltaTime; // calculate distance to move
            transform.position = Vector3.MoveTowards(transform.position, m_endTarget.transform.position, step);
        }
        else if(!move_to_end && !rotate)
        {
            float step = speed * Time.deltaTime; // calculate distance to move
            transform.position = Vector3.MoveTowards(transform.position, m_starttarget.transform.position, step);
        }

        if (transform.position == m_endTarget.transform.position)
        {
            Destroy(this.gameObject) ;
        }

    }

    IEnumerator RotateAriund()
    {

        while (time < interpolationPeriod)
        {
            time += Time.deltaTime;
            time2 += Time.deltaTime;
            transform.Rotate(Vector3.up * (10 * Time.deltaTime));
            transform.Rotate(Vector3.right * (10 * Time.deltaTime));
            transform.RotateAround(m_starttarget.transform.position, Vector3.up, speed * Time.deltaTime);
            desiredPosition = (transform.position - new Vector3(m_starttarget.transform.position.x, m_starttarget.transform.position.y, m_starttarget.transform.position.z)).normalized * radius + new Vector3 (m_starttarget.transform.position.x, m_starttarget.transform.position.y, m_starttarget.transform.position.z) ;
            transform.position = Vector3.MoveTowards(transform.position, desiredPosition, radiusSpeed * Time.deltaTime);


            if (m_firetarget == null && !m_shooting)
            {
                if (PlanetJointHandler.Instance.m_connected.Count > 0)
                {
                    float distance = 0;
                    foreach (GameObject game in PlanetJointHandler.Instance.m_connected)
                    {
                        if (game != null)
                        {
                            if (Vector3.Distance(this.transform.position, game.transform.position) < distance || distance == 0)
                            {
                                distance = Vector3.Distance(this.transform.position, game.transform.position);
                                m_firetarget = game;
                            }
                        }
                    }
                }
                else
                {
                    m_firetarget = PlanetJointHandler.Instance.gameObject;
                }
            }


            if (m_firetarget != null)
            {
                lineRenderer.SetPosition(0, this.gameObject.transform.position);
                lineRenderer.SetPosition(1, m_firetarget.transform.position);

                if (time2 > interpolationPeriod2)
                {
                    StartCoroutine(ResetLazer(lineRenderer));
                    time2 = 0;
                }
            }



            yield return null;
        }
        Debug.Log("End - ro");
        lineRenderer.enabled = false;
        move_to_end = true;
    }

    IEnumerator ResetLazer(LineRenderer lazer)
    {
        if (!m_exploded)
        {
            AudioManager.Instance.PlayLaser();
            yield return new WaitForSeconds(0.1f);
            m_shooting = true;
            lineRenderer.enabled = true;
            yield return new WaitForSeconds(0.5f);
            lazer.enabled = false;
        }
        if (m_firetarget == PlanetJointHandler.Instance.gameObject)
        {
            m_firetarget.transform.localScale = 0.8f * m_firetarget.transform.localScale;
            if (m_firetarget.transform.localScale.x < 1.5f && !m_exploded)
            {
                for (int i = 0; i < 50; i++)
                {

                    GameObject obj = Instantiate(m_boulder, m_firetarget.transform.position,Quaternion.identity);
                    obj.GetComponent<MeshCollider>().enabled = false;
                    obj.GetComponent<Rigidbody>().useGravity = false;
                    obj.transform.localScale = Random.Range(1, 7) * obj.transform.localScale;
                    transform.rotation = Random.rotation;
                    obj.GetComponent<Rigidbody>().velocity = -Random.rotation.eulerAngles / 15;
                    obj.GetComponent<Rigidbody>().AddTorque(obj.transform.up * 10);
                }
                // destroy bam
                m_firetarget.transform.localScale = Vector3.zero;
                AudioManager.Instance.PlayBoom();
                m_exploded = true;
            }
        }
        else
        {
            PlanetJointHandler.Instance.m_connected.Remove(m_firetarget);
            Destroy(m_firetarget);
        }
        m_shooting = false;
    }


}
