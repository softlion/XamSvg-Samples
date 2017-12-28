# Samples for XamSvg enterprise cross platform and full featured Svg image control
This repository holds the samples for the [XamSvg Xamarin component](https://components.xamarin.com/view/xamsvgforms), also available as a nuget on a private nuget server.

| Xamarin.Forms | Android | Android + iOS + Windows UWP+WinRT |
|:-------------:|:-------:|:---------------------------------:|
| [![NuGet][xamarinstore-img]][xamarinstore-linkforms] | [![NuGet][xamsvg-img]][xamsvg-link] | [![NuGet][xamarinstore-img]][xamarinstore-link]
| [![][xamsvglivedemo-img]][xamsvglivedemo-link] | [![][xamsvglivedemo-img]][xamsvglivedemo-link] | [![][xamsvglivedemo-img]][xamsvglivedemo-link]
| [![][formsdemo-img]][formsdemo-link] | [![][formsdemo-img]][droiddemo-link] | [![][formsdemo-img]][droiddemo-link] [![][formsdemo-img]][iosdemo-link] [![][formsdemo-img]][uwpdemo-link]

# Animating SVG images
The xamarin Forms project contains a code demonstrating an animated svg ring. This code works on all platforms: android, ios and universal windows and does not depend on any custom renderer. It is fully contained in the portable Forms project. Check the [RingProgress control](https://github.com/softlion/XamSvg-Samples/blob/master/Demos/XamSvg.XamFormsDemo/XamSvg.Demo/Controls/RingProgress.cs)

# Quick start for Xamarin Forms

1. Declare XamSvg extension to Xamarin Forms

Add `SvgImageRenderer.InitializeForms();` before `global::Xamarin.Forms.Forms.Init` on each platform. SvgImageRenderer is a class in namespace `XamSvg.XamForms.Uw` for windows, `XamSvg.XamForms.Ios` for iOS, and `XamSvg.XamForms.Droid` for Android. If you use Resharper those will be added automagically.

2. Tell XamSvg where to search for svg files

Svg files will be stored as embedded resources. To access an embedded resource the code needs the full assembly path of this resource. To make this process easier, you tell XamSvg in which assembly to search for the resources and you don't specify the full path of the svg file in the Svg control.

```csharp
public class App : Application
{
    public App()
    {
        XamSvg.Shared.Config.ResourceAssembly = typeof(App).GetTypeInfo().Assembly;
        ...
    }
...
```

Trick: you can add more than one assembly by using the ResourceAssemblies property instead.

3. Add your svg files  

Create a folder "images" at the root of your shared or PCL project (ie the project containing the App.cs file), put your svg files there. Make sure they have the .svg extension. And set their build action type to "embedded resource".

4. Add an svg image control 

Remark the xmlns:svg attribute on the ContentPage. If you use Resharper it will be added automagically.

```xml
<?xml version="1.0" encoding="utf-8" ?>
<ContentPage xmlns="http://xamarin.com/schemas/2014/forms"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             xmlns:svg="clr-namespace:XamSvg.XamForms;assembly=XamSvg.XamForms"
             x:Class="XamSvg.Demo.MainPage">
  <ContentView>
    <StackLayout Orientation="Vertical" VerticalOptions="Start">

        <svg:SvgImage Svg="res:images.logo" HorizontalOptions="Start" HeighRequest="32" />

    </StackLayout>
  </ContentView>
</ContentPage>
```

Remark the Svg property: the image name is prefixed with "res:", and the extension (.svg) is removed. 'images' is the folder in which you put the file. It is the full path of the svg file from the root of your PCL or shared project. You have other schemes like string: to load an inline svg.

5. Enjoy

# Common mistakes

If nothing appears, make sure your svg is displayed correctly by the windows explorer (after you installed this [extension](https://svgextension.codeplex.com/)). 

Common errors include
* forgetting to set the build action of the svg file to "Embedded resource".
* missing viewBox attribute at the root of the svg file (open it using a text editor).
* the svg color is the same as the background color, especially white or black. Use ColorMapping to change colors, or edit your svg file with [inkscape](http://www.inkscape.org/) or your preferred text editor.

# Other Receipes

**Icons on TabbedPage**

1. Create a TabbedPage, call it TabContainer for example.

```xml
<?xml version="1.0" encoding="utf-8" ?>
<TabbedPage xmlns="http://xamarin.com/schemas/2014/forms"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             xmlns:demo="clr-namespace:XamSvg.Demo;assembly=XamSvg.Demo"
             x:Class="XamSvg.Demo.Pages.TabContainer"
             Title="Vapolia.fr XamSvg Demo"
            >

    <demo:MainPage Title="Slideshow" x:Name="MainPage" />
    <demo:Page2 Title="Animation"  x:Name="AnimationPage" />
    
</TabbedPage>
```

2. In its codebehind, set the icons

```csharp
public TabContainer()
{
    InitializeComponent();

    MainPage.Icon = new SvgImageSource { Svg = "res:images.intertwingly", Height = 30 }.CreateFileImageSource();
    AnimationPage.Icon = new SvgImageSource { Svg = "res:images.0GoldMirror", Height = 30 }.CreateFileImageSource();
}
```

3. Specific instructions for Android

* Your android's MainActivity must inherit from FormsAppCompatActivity, not FormsApplicationActivity. Note that if you switch to FormsAppCompatActivity, your app must also use an appcompat theme. See this [xamarin guide](https://blog.xamarin.com/material-design-for-your-xamarin-forms-android-apps/).

* Add [SvgTabbedPageRenderer](https://gist.github.com/softlion/eac96a4aae416934c3fd5a9184a1d63b) to your android project

4. Enjoy icons on tabs, iOS + Android.

**Mvvmcross** 

Fully compatible with mvvmcross, including the bindings of image source, color mappings, and all other properties.  

**Android native**: make the svg image height the same height of a Button

```xml
<?xml version="1.0" encoding="utf-8"?>
<android.support.constraint.ConstraintLayout 
              xmlns:android="http://schemas.android.com/apk/res/android"
              xmlns:app="http://schemas.android.com/apk/res-auto"
              xmlns:tools="http://schemas.android.com/tools"
              android:layout_width="match_parent"
              android:layout_height="match_parent">
    <Button
        android:id="@+id/myinfo"
        app:layout_constraintTop_toTopOf="parent"
        app:layout_constraintLeft_toLeftOf="parent"
        app:layout_constraintRight_toRightOf="parent"
        android:layout_width="0dp"
        android:layout_height="wrap_content"
        android:text="@string/Menu_MyInfo"
        style="@style/Widget.AppCompat.Button.Borderless"
        app:MvxBind="Click SettingsCommand"
        />
    <XamSvg.SvgImageView
        app:layout_constraintTop_toTopOf="@+id/myinfo"
        app:layout_constraintBottom_toBottomOf="@+id/myinfo"
        app:layout_constraintLeft_toLeftOf="parent"
        android:layout_width="wrap_content"
        android:layout_height="0dp"
        tools:layout_width="30dp"
        app:colorMapping="000000=e01a1a"
        app:colorMappingSelected="000000=ff3030"
        app:colorMappingDisabled="000000=1a1a1a"
        app:fillMode="fit"
        app:svg="res:images.info" />
```

Note that as the svg has an intrinsic width computed from its height and its aspect ratio, the width displayed by the designer is incorrect. You can correct the designer by assigning a design time only value to layout_width using the tools prefix: `tools:layout_width="30dp"` which requires the xmlns:tools namespace declaration.

**Android native**: set back button toolbar icon

```csharp
var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
toolbar.NavigationIcon = SvgFactory.GetDrawable("res:images.webbrowser.backward", "000000=FFFFFF");
```

**iOS storyboard**: usage in an xcode storyboard

Using Visual Studio Mac, open your storyboard file using `Open with xcode`. Add an Image view (UIImageView), set its custcom `Class` property to `UISvgImageView`, and optionally add new `User Defined Runtime Attributes` as required:

![Attribute Inspector](https://image.ibb.co/e5N0uw/Prt_Scr_capture_11.jpg)

| Key Path | Type | Sample Value
| --------- | ----- | ----
| BundleName | String | res:images.info
| ColorMapping | String | 000000=e01a1a
| ColorMappingSelected | String | 000000=ff3030

To size your svg, set contraints on one dimension only. The other dimension will be set using the first dimension and the computed aspect ratio. If you set constraints on both dimensions, the svg will stretch. You can prevent this by setting FillMode to Fit (along with ContentMode to AspectFit):

| Key Path | Type | Sample Value
| --------- | ----- | ----
| FillMode | String | Fit

When only one dimension is constrained, the designer don't know how to set the other dimension and displays contraint errors. The solution is to set the `intrinsic size` to a manual value in the dimension which has no contraint (in the dimension property pane of the designer).

![Intrinsic Size](https://image.ibb.co/bzeDEw/Prt_Scr_capture_12.jpg)

1. Select the `UISvgImageView` view.
2. Show the size inspector (⌘Shift5).
3. Change the "Intrinsic Size" drop-down from "Default (System Defined)" to "Placeholder."
4. Enter reasonable guesses at your view's runtime width **or** height. Width if you set contraints on the height, height otherwise.

These constraints are removed at compile-time, meaning they will have no effect on your running app, and the layout engine will add constraints as appropriate at runtime to respect your view's intrinsicContentSize.

# Community

Join the svg community on our [slack channel](https://xamarinchat.slack.com/#xamsvg)


[xamsvg-img]: https://img.shields.io/badge/nuget-2.3.4.8-blue.svg
[xamsvg-link]: https://www.nuget.org/packages/Softlion.XamSvg.Free
[xamsvglivedemo-img]: https://img.shields.io/badge/live-demo-brightgreen.svg
[xamsvglivedemo-link]: https://appetize.io/embed/amyhugx1xzurnv45h8kyp5kam0?device=iphone7&scale=75&orientation=portrait&osVersion=10.3&xdocMsg=true&deviceColor=black
[xamarinstore-img]: https://img.shields.io/badge/Xamarin-Component%20Store-00FF7F.svg
[xamarinstore-linkforms]: https://components.xamarin.com/view/XamSvgForms
[xamarinstore-link]: https://components.xamarin.com/view/XamSvg

[formsdemo-img]: https://img.shields.io/badge/demo-source%20code-lightgrey.svg
[formsdemo-link]: https://github.com/softlion/XamSvg-Samples/tree/master/Demos/XamSvg.XamFormsDemo
[droiddemo-link]: https://github.com/softlion/XamSvg-Samples/tree/master/Demos/XamSvg.DroidTests
[iosdemo-link]: https://github.com/softlion/XamSvg-Samples/tree/master/Demos/XamSvg.Ios2Tests
[uwpdemo-link]: https://github.com/softlion/XamSvg-Samples/tree/master/Demos/XamSvg.UwDemo
