using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace App.UI
{
  public class MainViewController : ViewController
  {
    [SerializeField] private Button newWindowButton;
    public override void Init(IWindowStarter starter)
    {
      base.Init(starter);
      newWindowButton.onClick.AddListener(OpenWindow);
    }
    private void OpenWindow()
    {
      var starter = new GenericStarter("Main", "AddDeviceWindow");
      var window = WindowsManager.Instance.CreateWindow<ViewController>(starter);
      window?.Show();
    }
  }
}