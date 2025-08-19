public class GenericStarter : IWindowStarter
{
  private readonly string group;
  private readonly string name;

  public GenericStarter(string group, string name)
  {
    this.group = group;
    this.name = name;
  }

  public string GetGroup() => group;
  public string GetName() => name;

  public void SetupModels(ViewController viewController)
  {
    
  }
}