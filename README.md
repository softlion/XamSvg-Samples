# Samples for the Xamarin Svg enterprise cross platform and full featured Svg image control
This repository holds the samples for the XamSvg Xamarin control (Vapolia.Xamarin.Svg.Forms and Vapolia.Xamarin.Svg for UWP, Android and iOS).

| Xamarin.Forms (Android, iOS, UWP) | Xamarin Native (Android, iOS, UWP) |
|:-------------:|:-------:|
| [![NuGet][forms-img]][forms-link] ![Nuget](https://img.shields.io/nuget/dt/Vapolia.Xamarin.Svg.Forms) | [![NuGet][xamsvg-img]][xamsvg-link] ![Nuget](https://img.shields.io/nuget/dt/Vapolia.Xamarin.Svg)
| [![][xamsvglivedemo-img]][xamsvglivedemo-link] | [![][xamsvglivedemo-img]][xamsvglivedemo-link]
| [![][formsdemo-img]][formsdemo-link] | [![][formsdemo-img]][demo-link]



Xamarin Forms controls:  
`SvgImageSource`  
`SvgImage`

Xamarin Android controls:  
`SvgImageView`  
`SvgPictureDrawable`

Xamarin iOS controls:  
`UISvgImageView`

Native UWP controls:   
`Svg`

[More infos](https://vapolia.eu)

# Changes in v4.x:

* Simplification: the "res:" prefix is now the default protocol and is not needed anymore. Use "zzzz.svg" that's all !
* Simplification: Setting ResourceAssembly/ResourceAssemblies is now optional. Use "zzzz.svg" that's all !
* ColorMapping and ColorMappingSelected accept standard colors from styles, and can be set in a XAML collection.
* All source are now `SvgSource` and can handle urls. The `Svg` property has been renamed `Source`.

Breaking simplification:  
`SvgFactory.FromUri(SvgFactory.FromString("zz.svg"),w,h)` has been replaced by `SvgFactory.GetImage("zz.svg",w,h)`  
Everywhere you see `SvgSource` you can put a string, thanks to implicit conversion.

Upcoming demo:  
* New features: interaction on the SVG including tap (or any other gesture) zone detection. Upcoming demo with zone highlightning and TappedZoneId event.

# Quick start for Xamarin Forms

Note: there is no startup code needed! Anywhere!

1. Add some svg files  

Create a folder "images" at the root of your netstandard project (the project containing the `App.cs` file) and put your svg files there.   
Make sure they have the `.svg` extension. And set their build action type to `embedded resource` (important!) in the file property window.

2. Use the `SvgImage` control or the `SvgImageSource` control

```xml
<?xml version="1.0" encoding="utf-8" ?>
<ContentPage xmlns="http://xamarin.com/schemas/2014/forms"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             xmlns:svg="clr-namespace:XamSvg.XamForms;assembly=XamSvg.XamForms"
             x:Class="XamSvg.Demo.MainPage">
  <ContentPage.IconImageSource>
    <svg:SvgImageSource Source="myicon.svg" Height="50" />
  </ContentPage.IconImageSource>
  
  <ContentView>
    <StackLayout Orientation="Vertical" VerticalOptions="Start">

        <svg:SvgImage Source="logo.svg" HorizontalOptions="Start" HeighRequest="32" />

        <svg:SvgImage Source="logo.svg" HorizontalOptions="Start" HeighRequest="32"
                      ColorMapping="{Binding ColorMapping}" 
                      ColorMappingSelected="ffffff=>00ff00,000000=>0000FF" 
                      />
      
        <svg:SvgImage WidthRequest="100" Source="https://upload.wikimedia.org/wikipedia/commons/1/15/Svg.svg" />

        <svg:SvgImage WidthRequest="100" Source="data:image/svg+xml;base64,PD94bWwgdmVyc2lvbj0iMS4wIiBlb..." />
      
    </StackLayout>
  </ContentView>
</ContentPage>
```

Remarks:
* The `xmlns:svg` attribute is required on the `ContentPage`. If you use Resharper, it will be added automagically.
* The Source property value is the image name. It is searched in all embedded resources across loaded assemblies.
* All properties are bindable.

[![image.png](https://i.postimg.cc/Kzj346tM/image.png)](https://postimg.cc/7GvZWQsH)

3. Enjoy

trick: You can also use `<SvgImageSource Svg="...." Height="50" />` as the ImageSource for tab icons, button icons, navigation bar icons, ... But the Widht and/or Height is mandatory, as the Xamarin Forms controls infrastructure has a limitation: it has no way to dynamically give the target height to ImageSource objects.


# Color Mapping

XamSvg supports remapping color based on a change in the control's state, like selected or disabled.

To specify a color mapping, set the ColorMapping, ColorMappingSelected or ColorMappingDisabled properties to a string. This string contains a list of mapping separated by a semicolumn ";". A mapping has two parts, separated by the equal sign. The left part is the  color that should be replaced by the right part.

A color is specified using standard html coding: AARRGGBB, RRGGBB, or RGB. A is the transparency (alpha channel).

For example ffffff=00ff00;000000=0000FF means replace ffffff (white) by 00ff00 (green) and replace 000000 (black) by 0000FF (red).

# Xamarin Forms samples

Simple svg image
```xml
   <svg:SvgImage Svg="res:images.logo" HeightRequest="70" HorizontalOptions="Center" VerticalOptions="Center" />
```  

Svg image on a button

```xml
  <Button Text="Add Contact" ContentLayout="Right,20">
     <Button.ImageSource>
         <svg:SvgImageSource Svg="res:images.tabHome" Height="60" ColorMapping="000000=FF0000" />
     </Button.ImageSource>
  </Button>
```  

Svg image for the icon of a TabbedPage tab

```xml
  <NavigationPage Title="Home">
    <x:Arguments>
        <views:HomePage />
    </x:Arguments>
    <NavigationPage.IconImageSource>
        <svg:SvgImageSource Svg="res:images.tabHome" Height="50" />
    </NavigationPage.IconImageSource>
  </NavigationPage>
```

# Common mistakes

If nothing appears, make sure your svg is displayed correctly by the windows explorer (after you installed this [extension](https://github.com/maphew/svg-explorer-extension/releases)). 

Common errors include
* forgetting to set the build action of the svg file to "Embedded resource".
* missing viewBox attribute at the root of the svg file (open it using a text editor).
* the svg color is the same as the background color, especially white or black. Use ColorMapping to change colors, or edit your svg file with [inkscape](http://www.inkscape.org/) or your preferred text editor.

The assembly in which the svg resources are must have an `assembly Name` equal to its `Default namespace`, otherwise the svg files will not be found. If you are unable to do so, you can still display an svg but you will have to specify the full name of the resource like this:

```xml
    <xamForms:SvgImage
        Svg="res:YourDefaultNamespace.Images.getFDR_01_Ready.svg"
        x:Name="SvgIcon" HorizontalOptions="Fill" VerticalOptions="Start" Margin="8,0,8,0" BackgroundColor="Yellow"
        />
```

You can discover the full name of an embedded resource by opening your assembly (.dll in your bin folder) in the free tool `Telerik JustDecompile`.

[![image.png](https://i.postimg.cc/8cT3hbPM/image.png)](https://postimg.cc/jwkZTPfS)

# Other Receipes

**Icons on TabbedPage**

```xml
<?xml version="1.0" encoding="utf-8" ?>
<TabbedPage xmlns="http://xamarin.com/schemas/2014/forms"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             xmlns:views="clr-namespace:XamSvg.Demo;assembly=XamSvg.Demo"
             xmlns:svg="clr-namespace:XamSvg.XamForms;assembly=XamSvg.XamForms"
             x:Class="XamSvg.Demo.Pages.TabContainer"
             Title="Vapolia.fr XamSvg Demo"
            >

  <views:HomePage Title="Home">
    <views:HomePage.IconImageSource>
        <svg:SvgImageSource Svg="res:images.tabHome" />
    </views:HomePage.IconImageSource>
  </views:HomePage>
    
</TabbedPage>
```

**Mvvmcross** 

The library is fully compatible with mvvmcross bindings for all properties:  
image source, color mappings, and all others.  

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

Note that as the svg has an intrinsic width computed from its height and its aspect ratio, the width displayed by the designer is incorrect. You can correct the designer by assigning a design time only value to `layout_width` using the `tools` prefix: `tools:layout_width="30dp"` which requires the `xmlns:tools` namespace declaration.



**Android native**: set back button toolbar icon

```csharp
var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
toolbar.NavigationIcon = SvgFactory.GetDrawable("res:images.webbrowser.backward", "000000=FFFFFF");
```

**Android**: link all

This component is compatible with the "link all" linker option which reduces you app size by removing unused classes, properties and methods.  
If you set the android linker to "link all", add these lines to the  linker.xml file of your android application project:

```xml
  <assembly fullname="System.IdentityModel.Tokens.Jwt" preserve="all">
    <type fullname="System.IdentityModel.Tokens.JwtSecurityToken" preserve="all" />
  </assembly>
  <assembly fullname="Microsoft.IdentityModel.Tokens" preserve="all">
    <type fullname="Microsoft.IdentityModel.Tokens.JsonWebKey" preserve="all" />
  </assembly>
```

This is required because the xamarin tooling does not use linker.xml files from android libraries.

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

# Reference

## Xamarin Forms

Default namespace:
```xml
xmlns:svg="clr-namespace:XamSvg.XamForms;assembly=XamSvg.XamForms"
```

### SvgImage control
`SvgImage` displays a image in up to 2 states: normal, selected

```xml
<svg:SvgImage Svg="res:images.union" HeightRequest="70" HorizontalOptions="Center" VerticalOptions="Center" />
```

| Property | Type | Notes
| --------- | ----- | ---
Svg | string | svg to display. Don't forget the res: prefix if loading from embedded resources
ColorMapping | string | see color mapping reference. Default to none.
ColorMappingSelected | string | color mapping when IsSelected="True". Default to none.
IsSelected | bool | used to switch color mapping
IsSelectionEnabled | bool | True by default: the value of IsSelected is also inherited from the parent container
Command | ICommand | if set, execute this command on tap
CommandParameter | object | parameter to send when calling Command.Execute
Width | double | Optional. You can also specify the width only and height will be computed from the aspect ratio
Height | double | Optional
FillMode | FillMode | Fit, Fill, Crop. Useful only if both width and height are forced. Default to Fit to maintain the aspect ratio.
IsLoadAsync | bool | set to False to disable async image loading, making the image appear immediatly. Default to True.
IsHighlightEnabled | bool | if set, ColorMappingSelected is used while the image is pressed (until the finger is released)
ViewportTransform | IMatrix | transform the svg using any matrix before displaying it

### SvgImageSource class

`SvgImageSource` inherits from `ImageSource`, use it on any `ImageSource` property. For example `Page.IconImageSource`.
It can also be transformed into a `FileImageSource` by calling `CreateFileImageSource()`.

SvgImageSource can be used in Button.ImageSource, ToolbarItem.IconImageSource, ...

```xml
<svg:SvgImageSource Svg="res:images.tabHome" Height="50" />
```

| Property | Type | Notes
| --------- | ----- | ---
Svg | string | svg to display. Don't forget the res: prefix if loading from embedded resources.
Width | double | Optional. You can also specify the width only and height will be computed from the aspect ratio.
Height | double | Optional.
ColorMapping | string | Optional. See color mapping reference.
SvgFillMode | FillMode | Fit, Fill, Crop. Useful only if both width and height are forced. Default to Fit to maintain the aspect ratio.
PreventTintOnIos | bool | Default to false. Prevents tinting on iOS, thus always displaying the original image.

All properties are bindable, but Xamarin Forms does not support changing them after the control using this SvgImageSource is rendered.
Alternatively, you can bind the ImageSource property on the target control, and define SvgImageSource in styles.
Example:
```xml
    <svg:SvgImageSource x:Key="NormalIcon" Svg="res:resources.images.icon_normal" Height="80" />
    <svg:SvgImageSource x:Key="SelectedIcon" Svg="res:resources.images.icon_selected" Height="80" ColorMapping="FFF=000" />

    <Style x:Key="NormalIconStyle" TargetType="ImageButton">
      <Setter Property="Source" Value="{StaticResource NormalIcon}"/>
      <Setter Property="BackgroundColor" Value="Transparent"/>
    </Style>

    <Style x:Key="SelectedIconStyle" TargetType="ImageButton">
      <Setter Property="Source" Value="{StaticResource SelectedIcon}"/>
      <Setter Property="BackgroundColor" Value="Transparent"/>
   </Style>
```
And usage:
```xml
    <ImageButton Style="{Binding StyleKeyToUse}" />
```
```csharp
     public string StyleKeyToUse {get;set;} = "NormalIconStyle"; 
     //Don't forget to call OnPropertyChanged(nameof(StyleKeyToUse)) after each change.
```

## Android native

Layout properties:

| Tag | Type | Default value | Notes
| --------- | ----- | ---- | ---
app:svg | string or resource id | (required) | .net embedded resource file path and name, or android resource id
app:colorMapping | string | (null) | example: FF000000=FF808080
app:colorMappingSelected | string | (null) | example: FF000000=FFa0a0a0;FFFFFFFF=00000000
app:colorMappingDisabled | string | (null)
app:traceEnabled | bool | false
app:loadAsync | bool | true
app:fillMode | enum | fit | fit, fill of fit_crop (new v3.1.0). fit_crop: Scale the image uniformly (maintain the image's aspect ratio) so that both dimensions (width and height) of the image will be equal to or larger than the corresponding dimension of the view (minus padding). 
android:adjustViewBounds | bool | true | if true and fillMode is not Fill, the svg view will grow or shrink depending on the svg size.
android:autoMirrored | bool | false | true to mirror image in RTL languages

`android:padding` is respected, and included in the width/height measurement.  
`android:gravity` is respected, and included in the width/height measurement. If the svg is smaller than its view, this property controls its centering.

## iOS native

`UISvgImageView` inherits `UIImageView`, so it's easy to use it in an xcode storyboard: drag an `UIImageView` and set its custom class to `UISvgImageView`. To set specific svg properties, add `User Defined Runtime Attributes` in the same pane where you set the custom class.

Attributes (supported in `User Defined Runtime Attributes`):

| Key Path | Type | Default value | Notes
| --------- | ----- | ---- | ---
BundleName | string | (required) | Svg path. Example: res:images.logo
BundleString | string | (optional) | exclusive with BundleName. The svg content as a string.
ColorMapping | string | (null) | example: FF000000=FF808080
ColorMappingSelected | string | (null) | example: FF000000=FFa0a0a0;FFFFFFFF=00000000
TraceEnabled | bool | false
IsLoadAsync | bool | true | set to false to force the svg to appear immediatly, or if it disappears sometimes
AlignmentMode | string | TopLeft | TopLeft, CenterHorizontally, CenterVertically, Center. Can be combined (in code only).
FillMode | string | Fit | Fit, Fill, FitCrop.
FillWidth | number | 0 | The width the svg would like to have. 0 to let the OS decides using UI constraints or Frame value.
FillHeight | number | 0 | The height the svg would like to have. 0 to let the OS decides using UI constraints or Frame value.

`UIImageView.ContentMode` is forced by `UISvgImageView`, so it has no impact. Use `FillMode` instead.

[xamsvg-img]: https://img.shields.io/nuget/vpre/Vapolia.Xamarin.Svg
[xamsvg-link]: https://www.nuget.org/packages/Vapolia.Xamarin.Svg
[forms-img]: https://img.shields.io/nuget/vpre/Vapolia.Xamarin.Svg.Forms
[forms-link]: https://www.nuget.org/packages/Vapolia.Xamarin.Svg.Forms
[xamsvglivedemo-img]: https://img.shields.io/badge/live-demo-brightgreen.svg
[xamsvglivedemo-link]: https://appetize.io/embed/amyhugx1xzurnv45h8kyp5kam0?device=iphone7&scale=75&orientation=portrait&osVersion=10.3&xdocMsg=true&deviceColor=black

[formsdemo-img]: https://img.shields.io/badge/demo-source%20code-lightgrey.svg
[formsdemo-link]: https://github.com/softlion/XamSvg-Samples/tree/master/Demos/XamSvg.XamFormsDemo
[demo-link]: https://github.com/softlion/XamSvg-Samples/tree/master/Demos/


## Advanced configuration

The svg files that don't specify the full path of an assembly are searched in the embedded resources of all loaded assemblies automatically. If you have late loading assemblies that are not detected, or if you prefer to manually specify the assemblies, add this line:

```csharp
public class App : Application
{
    public App()
    {
        XamSvg.Shared.Config.ResourceAssembly = typeof(App).Assembly;
        ...
    }
...
```

Trick: you can add more than one assembly by using the `ResourceAssemblies` property instead.

## More infos

[More infos](https://vapolia.eu)

Commercial support available.
