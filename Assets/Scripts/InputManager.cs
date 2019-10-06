using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance = null;

    Dictionary<string, KeyCode> keyMap = new Dictionary<string, KeyCode>();
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
    }

    public void MapFunction(string functionName)
    {
        foreach (KeyCode key in m_keys)
        {
            if (Input.GetKeyDown(key))
            {
                keyMap[functionName] = key;
            }
        }
    }

    public KeyCode GetKey(string functionName)
    {
        if(keyMap.ContainsKey(functionName)) return keyMap[functionName];

        else return KeyCode.None;
    }

    public string GetName(string functionName)
    {
        return string.Format("{0} of {1}", functionName, keyMap[functionName].ToString());
    }

    void Update()
    {
        // Example code for adding keys
        if(!keyMap.ContainsKey("left"))
        {
            MapFunction("left");
        }
        else if(!keyMap.ContainsKey("down"))
        {
            MapFunction("down");
        }
        else if(!keyMap.ContainsKey("up"))
        {
            MapFunction("up");
        }
        else if(!keyMap.ContainsKey("right"))
        {
            MapFunction("right");
        }
    }
}
