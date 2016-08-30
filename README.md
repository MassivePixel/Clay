# Clay - React inspired library for Xamarin.Forms

[![Build status](https://ci.appveyor.com/api/projects/status/j6ncctmr606xmtbo?svg=true)](https://ci.appveyor.com/project/tpetrina/clay)
[![NuGet Pre Release](https://img.shields.io/nuget/vpre/MassivePixel.Clay.svg?maxAge=2592000)]()
[![license](https://img.shields.io/github/license/mashape/apistatus.svg?maxAge=2592000)]()

## Motivational example

In our Xamarin app let's create a new page and instead of using XAML, use `ClayComponent`:

```csharp
MainPage = new ContentPage();
VirtualDOM.Render(new Counter(), MainPage);
```

The component itself is a special class that can rebuild it's UI:

```csharp
public class Counter : ClayComponent<int>
{
    public override Node Render()
        => h(new StackLayout
        {
            VerticalOptions = LayoutOptions.Center,
            Children =
            {
                new Label
                {
                    Text = $"Current value = {State}"
                },
                new Button
                {
                    Text = "+",
                    Command = new Command(inc)
                }
            }
        });

    void inc()
    {
        SetState(State + 1);
    }
}
```

The `h` function is responsible for maintaining virtual representation of our view. There are several different overrides accepting different kinds of inputs, but the one in this sample can take an existing `Xamarin.Forms.View` and recreate nodes from the real view.

On each button click a new state is set which triggers redrawing the entire UI. However, UI is not rebuilt from scratch. Instead, UI is simply updated where it needs to be keeping the list of changes to a minimum.

## status

> NOTE: The project is still work in progress.

## Build

Library is being developed inside Xamarin Studio 6.

## Roadmap

Our virtual DOM implementation is far from complete and currently simply redraws the entire UI. This needs tbe developed properly with XAML specific features in mind like attached properties, data virtualization in lists, attached collections (triggers and behaviors), static resources and many more.

## Contributions

All are welcome.

## License

Licensed under MIT licence.