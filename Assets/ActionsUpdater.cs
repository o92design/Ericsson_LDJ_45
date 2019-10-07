using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class ActionsUpdater : MonoBehaviour
{
  public Text m_ActionText;
  public Text m_EventText;
  public GameSteps m_gameSteps;
  public int m_flowStep = 0;
  [Range(0, 30)]
  public float m_inputWaitingTime = 5;
  public bool m_canInput;

  public void UpdateAction(KeyCode p_key)
  {
    // check if we're on exploration step now
    if (m_flowStep >= m_gameSteps.m_playerActions.Length)
      return;

    PlayerActions action = m_gameSteps.m_playerActions[++m_flowStep];
    string actionText = action.m_action;

    if (action.m_action.ToLower() == "sound")
    {
      GetComponent<PlayableDirector>().Play();
    }

    if (m_flowStep != 0)
      actionText += " of " + p_key.ToString() + ",";
    else
      actionText += ",";

    m_ActionText.text = actionText;
    m_gameSteps.m_playerActions[m_flowStep].initializeAction();
    m_gameSteps.UpdateAction(m_flowStep, p_key, true);
  }

  public void UpdateEventText()
  {
    if (m_flowStep + 1 < m_gameSteps.m_playerActions.Length)
    {
      m_EventText.text = "then " + m_gameSteps.m_playerActions[m_flowStep + 1].m_action.ToLower();
    }
    else if (m_flowStep + 1 == m_gameSteps.m_playerActions.Length)
    {
      m_EventText.text = "then god must explore";
      Debug.Log("We're on Exploration step");
      Destroy(m_EventText.gameObject, 5);
      Destroy(m_ActionText.gameObject, 5);
    }
  }

  public void OnGUI()
  {
    Event keyEvent = Event.current;

    if (keyEvent.isKey && m_canInput)
    {
      // Update next step
      if (m_flowStep == 0 ||
        (!m_gameSteps.IsKeyMapped(keyEvent.keyCode) && Input.GetKeyDown(keyEvent.keyCode)))
      {
        StartCoroutine(WaitForSomeTime(keyEvent.keyCode, m_inputWaitingTime));
      }
    }
  }

  IEnumerator WaitForSomeTime(KeyCode p_keyCode, float p_seconds)
  {
    m_canInput = false;
    UpdateAction(p_keyCode);
    UpdateEventText();

    yield return new WaitForSeconds(p_seconds);

    Debug.Log("Now we can input again!");
    m_canInput = true;
  }
}
