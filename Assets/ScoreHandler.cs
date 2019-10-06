using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ScoreHandler : MonoBehaviour
{
    public static ScoreHandler Instance;
    public TextMeshProUGUI m_scoretext;
    public TextMeshProUGUI m_score;
    public TextMeshProUGUI m_modifier;
    public GameObject m_planet;
    private float time = 0.0f;
    public float interpolationPeriod = 0.3f;
    public int m_oldscore = 0;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);

        }
        else
        {
            Instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (PlanetJointHandler.Instance != null)
        {
            m_score.text = PlanetJointHandler.Instance.m_connected.Count.ToString();
            time += Time.deltaTime;

            if (time >= interpolationPeriod)
            {
                if (PlanetJointHandler.Instance.m_connected.Count - m_oldscore != 0)
                {
                    if (PlanetJointHandler.Instance.m_connected.Count - m_oldscore > 0)
                    {
                        m_modifier.color = Color.green;
                        m_modifier.text = "+" + (PlanetJointHandler.Instance.m_connected.Count - m_oldscore).ToString();
                    }
                    else
                    {
                        m_modifier.color = Color.red;
                        m_modifier.text = (PlanetJointHandler.Instance.m_connected.Count - m_oldscore).ToString();
                    }
                }
                else
                {
                    m_modifier.text = "";
                }
                time = time - interpolationPeriod;
                m_oldscore = PlanetJointHandler.Instance.m_connected.Count;
            }
        }
    }

}
