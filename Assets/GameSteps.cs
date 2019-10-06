using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct PlayerActions
{
  public string m_action;
  public KeyCode m_key;
  public bool m_available;
}

public class GameSteps : MonoBehaviour
{
  public PlayerActions[] m_eventActions;

  public void UpdateAction(int p_index, KeyCode p_key, bool p_available)
  {
    m_eventActions[p_index].m_key = p_key;
    m_eventActions[p_index].m_available = p_available;
  }
}

