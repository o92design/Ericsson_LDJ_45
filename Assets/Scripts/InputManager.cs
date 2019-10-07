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

        m_keys.Add(KeyCode.Return);
        m_keys.Add(KeyCode.Tab);
        m_keys.Add(KeyCode.Backspace);

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

        m_keys.Add(KeyCode.Keypad0);
        m_keys.Add(KeyCode.Keypad1);
        m_keys.Add(KeyCode.Keypad2);
        m_keys.Add(KeyCode.Keypad3);
        m_keys.Add(KeyCode.Keypad4);
        m_keys.Add(KeyCode.Keypad5);
        m_keys.Add(KeyCode.Keypad6);
        m_keys.Add(KeyCode.Keypad7);
        m_keys.Add(KeyCode.Keypad8);
        m_keys.Add(KeyCode.Keypad9);
        m_keys.Add(KeyCode.KeypadPeriod);
        m_keys.Add(KeyCode.KeypadDivide);
        m_keys.Add(KeyCode.KeypadMultiply);
        m_keys.Add(KeyCode.KeypadMinus);
        m_keys.Add(KeyCode.KeypadPlus);
        m_keys.Add(KeyCode.KeypadEnter);

        m_keys.Add(KeyCode.UpArrow);
        m_keys.Add(KeyCode.DownArrow);
        m_keys.Add(KeyCode.RightArrow);
        m_keys.Add(KeyCode.LeftArrow);
    }

    public void MapFunction(string functionName)
    {
        foreach (KeyCode key in m_keys)
        {
            if (Input.GetKeyDown(key))
            {
                keyMap[functionName] = key;
                Debug.Log("MAPPED " + functionName + " to " + keyMap[functionName]);
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

    public void ScrambleKeys()
    {
        Dictionary<string, KeyCode> newMap = new Dictionary<string, KeyCode>();
        foreach(KeyValuePair<string, KeyCode> item in keyMap)
        {
            newMap[item.Key] = m_keys[Random.Range(0,m_keys.Count)];
        }
        keyMap = newMap;
    }
}
