# Samples for XamSvg cross platform and full featured Svg image control
This repository holds the samples for the [XamSvg Xamarin component](https://components.xamarin.com/view/xamsvgforms), also available as a nuget on a private nuget server.

Interact now with the [demo xamarin forms iOS app](https://appetize.io/embed/amyhugx1xzurnv45h8kyp5kam0?device=iphone7&scale=75&orientation=portrait&osVersion=10.3&xdocMsg=true&deviceColor=black) which have been uploaded to this live interactive streaming iOS 10.3 simulator powered by appetize.io !

# Animating SVG images
The xamarin Forms projects contain a code demonstrating an animated svg ring. This code works on all platform: android, ios and universal windows and does not depend on any custom renderer. It is fully contained in the portable Forms project.

Have a look at the [RingProgress control](https://github.com/softlion/XamSvg-Samples/blob/master/Demos/XamSvg.XamFormsDemo/XamSvg.Demo/Controls/RingProgress.cs)

# Resources

[XamSvg for Xamarin.Forms component](https://components.xamarin.com/view/xamsvgforms)  
[XamSvg for Xamarin native only component](https://components.xamarin.com/view/xamsvg)  
[Xamarin samples](https://github.com/xamarin/monotouch-samples)  
[MvvmCross samples](https://github.com/MvvmCross/MvvmCross-Samples)  

# Receipes

**Mvvmcross** At the root of the repo you'll find MvvmCross extensions to easily use XamSvg with mvvmcross. Also check the prerelease branch of this repo for updates.

**Android native**: set back button toolbar icon

```
var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
toolbar.NavigationIcon = SvgFactory.GetDrawable(this, "res:images.backward", CancellationToken.None, SvgColorMapperFactory.FromString("000000=FFFFFF"));
```

# License

This repository is licensed under the MIT licence. Some of the svg files are licensed under their owner's license.
