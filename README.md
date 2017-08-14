# Samples for XamSvg enterprise cross platform and full featured Svg image control
This repository holds the samples for the [XamSvg Xamarin component](https://components.xamarin.com/view/xamsvgforms), also available as a nuget on a private nuget server.

| Xamarin.Forms | Android | Android + iOS + Windows UWP+WinRT |
|:-------------:|:-------:|:---------------------------------:|
| [![NuGet][xamarinstore-img]][xamarinstore-linkforms] | [![NuGet][xamsvg-img]][xamsvg-link] | [![NuGet][xamarinstore-img]][xamarinstore-link]
| [![][xamsvglivedemo-img]][xamsvglivedemo-link] | [![][xamsvglivedemo-img]][xamsvglivedemo-link] | [![][xamsvglivedemo-img]][xamsvglivedemo-link]
| [![][formsdemo-img]][formsdemo-link] | [![][formsdemo-img]][droiddemo-link] | [![][formsdemo-img]][droiddemo-link] [![][formsdemo-img]][iosdemo-link] [![][formsdemo-img]][uwpdemo-link]

# Animating SVG images
The xamarin Forms projects contain a code demonstrating an animated svg ring. This code works on all platform: android, ios and universal windows and does not depend on any custom renderer. It is fully contained in the portable Forms project. Check the [RingProgress control](https://github.com/softlion/XamSvg-Samples/blob/master/Demos/XamSvg.XamFormsDemo/XamSvg.Demo/Controls/RingProgress.cs)

# Receipes

**Mvvmcross** 

Fully compatible with mvvmcross, including the bindings of image source, color mappings, and all other properties.  

**Android native**: make the svg image height the same height of a Button

```xml
<?xml version="1.0" encoding="utf-8"?>
<android.support.constraint.ConstraintLayout xmlns:android="http://schemas.android.com/apk/res/android"
              xmlns:app="http://schemas.android.com/apk/res-auto"
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
        app:colorMapping="000000=e01a1a"
        app:fillMode="fit"
        app:svg="res:images.info" />
```

**Android native**: set back button toolbar icon

```csharp
var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
toolbar.NavigationIcon = SvgFactory.GetDrawable(this, "res:images.backward", CancellationToken.None, SvgColorMapperFactory.FromString("000000=FFFFFF"));
```

# License

This repository is licensed under the MIT licence. Some of the svg files are licensed under their owner's license.

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
