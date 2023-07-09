using System.Reflection;

namespace TestTheme;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();
        this.LoadPredefinedTheme();
		MainPage = new AppShell();
	}

    private void LoadPredefinedTheme()
    {
        var themeName = $"TestTheme.Style.CustomTheme";

        if (Current.Resources.MergedDictionaries.Any(x => x.ToString() == themeName)) return;

        var assembly = IntrospectionExtensions.GetTypeInfo(typeof(App)).Assembly;
        ResourceDictionary resourceDictionary = null;
        try
        {
            var types = assembly.GetTypes();

            if (types.Any(x => x.FullName != null && x.FullName.Equals(themeName)))
                resourceDictionary = assembly.CreateInstance(themeName) as ResourceDictionary;

            if (resourceDictionary != null)
            {
                Current.Resources.MergedDictionaries.Add(resourceDictionary);
            }
        }
        catch (Exception)
        {
            //Ignore and pass null to get default theme
        }
    }
}
