using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance = null;

    public KeyCode left;
    public KeyCode right;
    public KeyCode up;
    public KeyCode down;

    List<KeyCode> m_keys = new List<KeyCode>();

    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
                
        }
        else
        {
            Instance = this;
        }

        m_keys.Add(KeyCode.A);
        m_keys.Add(KeyCode.B);
        m_keys.Add(KeyCode.C);
        m_keys.Add(KeyCode.D);
        m_keys.Add(KeyCode.E);
        m_keys.Add(KeyCode.F);
        m_keys.Add(KeyCode.G);
        m_keys.Add(KeyCode.H);
        m_keys.Add(KeyCode.I);
        m_keys.Add(KeyCode.J);
        m_keys.Add(KeyCode.K);
        m_keys.Add(KeyCode.L);
        m_keys.Add(KeyCode.M);
        m_keys.Add(KeyCode.N);
        m_keys.Add(KeyCode.O);
        m_keys.Add(KeyCode.P);
        m_keys.Add(KeyCode.Q);
        m_keys.Add(KeyCode.R);
        m_keys.Add(KeyCode.S);
        m_keys.Add(KeyCode.T);
        m_keys.Add(KeyCode.U);
        m_keys.Add(KeyCode.V);
        m_keys.Add(KeyCode.W);
        m_keys.Add(KeyCode.X);
        m_keys.Add(KeyCode.Y);
        m_keys.Add(KeyCode.Z);

        left = KeyCode.None;
        right = KeyCode.None;
        up = KeyCode.None;
        down = KeyCode.None;
    }

    void SetKey(ref KeyCode func)
    {
        foreach (KeyCode key in m_keys)
        {
            if (Input.GetKeyDown(key))
            {
                func = key;
            }
        }
    }

    void Update()
    {
        if(left == KeyCode.None)
        {
            SetKey(ref left);
        }
        else if(down == KeyCode.None)
        {
            SetKey(ref down);
        }
        else if(up == KeyCode.None)
        {
            SetKey(ref up);
        }
        else if(right == KeyCode.None)
        {
            SetKey(ref right);
        }
    }
}
