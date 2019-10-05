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

    List<KeyCode> keys = new List<KeyCode>();

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

            keys.Add(KeyCode.A);
            keys.Add(KeyCode.B);
            keys.Add(KeyCode.C);
            keys.Add(KeyCode.D);
            keys.Add(KeyCode.E);
            keys.Add(KeyCode.F);
            keys.Add(KeyCode.G);
            keys.Add(KeyCode.H);
            keys.Add(KeyCode.I);
            keys.Add(KeyCode.J);
            keys.Add(KeyCode.K);
            keys.Add(KeyCode.L);
            keys.Add(KeyCode.M);
            keys.Add(KeyCode.N);
            keys.Add(KeyCode.O);
            keys.Add(KeyCode.P);
            keys.Add(KeyCode.Q);
            keys.Add(KeyCode.R);
            keys.Add(KeyCode.S);
            keys.Add(KeyCode.T);
            keys.Add(KeyCode.U);
            keys.Add(KeyCode.V);
            keys.Add(KeyCode.W);
            keys.Add(KeyCode.X);
            keys.Add(KeyCode.Y);
            keys.Add(KeyCode.Z);

            ScrambleInput();
        }

    void ScrambleInput()
        {
            left = keys[Random.Range(0, keys.Count)];
            right = keys[Random.Range(0, keys.Count)];
            up = keys[Random.Range(0, keys.Count)];
            down = keys[Random.Range(0, keys.Count)];
        }
}
