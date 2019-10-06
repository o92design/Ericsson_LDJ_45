using UnityEngine;
using UnityEngine.UI;

public class ActionsUpdater : MonoBehaviour
{
  public Text m_ActionText;
  public Text m_EventText;
  public GameSteps m_gameSteps;
  public int m_flowStep = 0;

  public void UpdateAction(KeyCode p_key)
  {
    PlayerActions action = m_gameSteps.m_eventActions[++m_flowStep];
    string actionText = action.m_action;

    if (m_flowStep != 0)
      actionText += " of " + p_key.ToString() + ",";
    else
      actionText += ",";

    m_ActionText.text = actionText;
    m_gameSteps.UpdateAction(m_flowStep, p_key, true);
  }

  public void UpdateEventText()
  {
    m_EventText.text = "then " + m_gameSteps.m_eventActions[m_flowStep + 1].m_action.ToLower();
  }

  public void OnGUI()
  {
    Event keyEvent = Event.current;

    if(keyEvent.isKey)
    {
      if(m_flowStep == 0 ||
        (keyEvent.keyCode != m_gameSteps.m_eventActions[m_flowStep].m_key && Input.GetKeyDown(keyEvent.keyCode)))
      {
        UpdateAction(keyEvent.keyCode);
        UpdateEventText();
      }
    }
  }

}
