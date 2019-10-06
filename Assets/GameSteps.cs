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
  private SpawnSystem SpawnSystem;

  public GameObject m_objectToSpawn;
  public GameObject m_spawnPosition;

  public void initializeAction()
  {
    SpawnSystem = MonoBehaviour.FindObjectOfType<SpawnSystem>();
  }

  public void Spawn()
  {
    if (!m_available)
      return;

    if (m_objectToSpawn == null)
      return;

    if (m_action.ToLower() == "light")
    {
      m_objectToSpawn.GetComponent<InitiateWorld>().BigBang();
    }
    else
    {
      SpawnSystem.SpawnObject(m_objectToSpawn, m_spawnPosition.transform.position);
    }
  }
}

public class GameSteps : MonoBehaviour
{
  public PlayerActions[] m_playerActions;

  public void UpdateAction(int p_index, KeyCode p_key, bool p_available)
  {
    m_playerActions[p_index].m_key = p_key;
    m_playerActions[p_index].m_available = p_available;
    m_playerActions[p_index].Spawn();
  }

  public bool IsKeyMapped(KeyCode p_key)
  {
    foreach (PlayerActions action in m_playerActions)
    {
      if (action.m_key == p_key)
        return true;
    }
    return false;
  }

  public void OnGUI()
  {
    Event keyEvent = Event.current;

    if (keyEvent.isKey)
    {
      KeyCode keyInput = keyEvent.keyCode;
      if (IsKeyMapped(keyInput))
      {
        for (int actionIndex = 0; actionIndex < m_playerActions.Length; ++actionIndex)
        {
          PlayerActions action = m_playerActions[actionIndex];
          if (action.m_key == keyInput)
          {
            m_playerActions[actionIndex].Spawn();
            return;
          }
        }
      }
    }
  }
}

