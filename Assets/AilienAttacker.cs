using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AilienAttacker : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public GameObject m_endTarget;
    public GameObject m_starttarget;
    public float speed = 1;
    private float time = 0f;
    private float time2 = 0f;
    float interpolationPeriod = 30f;
    float interpolationPeriod2 = 0.5f;
    public bool rotate = false;
    public bool move_to_end = false;
    public Vector3 desiredPosition;
    float radius = 10.0f;
    float radiusSpeed = 5f;
    float m_firerate = 3f;
    public bool m_shooting = false;
    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
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
            desiredPosition = (transform.position - m_starttarget.transform.position).normalized * radius + m_starttarget.transform.position ;
            transform.position = Vector3.MoveTowards(transform.position, desiredPosition, radiusSpeed * Time.deltaTime);

            lineRenderer.SetPosition(0, this.gameObject.transform.position);
            lineRenderer.SetPosition(1, m_starttarget.transform.position);

            if (time2 > interpolationPeriod2)
            {
                StartCoroutine(ResetLazer(lineRenderer));
            }


            yield return null;
        }
        Debug.Log("End - ro");
        lineRenderer.enabled = false;
        move_to_end = true;
    }

    IEnumerator ResetLazer(LineRenderer lazer)
    {
        AudioManager.Instance.PlayLaser();
        m_shooting = true;
        lineRenderer.enabled = true;
        yield return new WaitForSeconds(0.5f);
        lazer.enabled = false;
        m_shooting = false;
        time2 = 0;
    }


}
