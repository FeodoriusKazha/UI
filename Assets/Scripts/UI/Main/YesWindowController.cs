using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using App.UI;

public class YesWindowController : ViewController
{
  [SerializeField] private Button submitButton;
  [SerializeField] private Button cancelButton;

  public override void Init(IWindowStarter starter)
  {
    base.Init(starter);
    submitButton.onClick.AddListener(OnSubmit);
    cancelButton.onClick.AddListener(Close);
  }
  public override void Close()
  {
    base.Close();
  }

  private void OnSubmit()
  {
    if (Parent is AddDeviceWindowController adc)
      adc.MarkClosingByChain();

    WindowsManager.Instance.CloseChain(Parent);
    base.Close();
  }
}
