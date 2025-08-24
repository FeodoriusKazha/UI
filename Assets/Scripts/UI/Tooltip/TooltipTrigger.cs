using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
  [SerializeField] private string header;
  [SerializeField] private string content;
  [SerializeField] private RectTransform tooltipBoundsWindow;
  private static LTDescr delay;
  private static TooltipTrigger currentTrigger;

  public void OnPointerEnter(PointerEventData eventData)
  {
    if (currentTrigger != null && currentTrigger != this && delay != null)
    {
      LeanTween.cancel(delay.uniqueId);
      TooltipSystem.Hide();
    }

    currentTrigger = this;

    delay = LeanTween.delayedCall(0.9f, () =>
    {
      TooltipSystem.Show(content, tooltipBoundsWindow, header);
    });
  }

  public void OnPointerExit(PointerEventData eventData)
  {
    if (delay != null)
    {
      LeanTween.cancel(delay.uniqueId);
    }

    TooltipSystem.Hide();

    if (currentTrigger == this)
    {
      currentTrigger = null;
    }
  }

  private void OnDisable()
  {
    if (delay != null)
    {
      LeanTween.cancel(delay.uniqueId);
    }

    TooltipSystem.Hide();

    if (currentTrigger == this)
    {
      currentTrigger = null;
    }
  }
}
