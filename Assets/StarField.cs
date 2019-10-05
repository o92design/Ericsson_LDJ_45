using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarField : MonoBehaviour
{
    private ParticleSystem particleSystem;

    [SerializeField]
    public List<Color> m_colors;

    public List<ParticleSystem.Particle> points;
    public float starDistanceSqr;
    public float starClipDistanceSqr;
    public int starMaxPerCOlor = 300;
    public float starSize = 0.35f;
    public float starDistance = 60f;
    public float starClipDistance = 15f;

    // Start is called before the first frame update
    void Start()
    {
        starDistanceSqr = starDistance * starDistance;
        starClipDistanceSqr = starClipDistance * starClipDistance;
        CreateStars();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CreateStars()
    {
        points = new List<ParticleSystem.Particle>();
        foreach (Color col in m_colors)
        {
            ParticleSystem.Particle[] col_points = new ParticleSystem.Particle[starMaxPerCOlor];
            for (int i = 0; i < starMaxPerCOlor; i++)
            {
                col_points[i].position = Random.insideUnitSphere * starDistance + this.transform.position;
                col_points[i].color = new Color(col.r, col.g, col.b, col.a);
                col_points[i].size = starSize;
 
            }

            points.AddRange(col_points);
        }

        particleSystem = gameObject.GetComponent<ParticleSystem>();

        particleSystem.SetParticles(points.ToArray(), points.ToArray().Length);

        Debug.Log(points.ToArray().Length);
    }
}
