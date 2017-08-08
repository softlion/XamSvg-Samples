# Samples for XamSvg
This repository holds the samples with their complete source code for the XamSvg Xamarin component. The samples are licensed under the MIT licence. Some of the svg files are licensed under their owner's license.

At the root of the repo, you'll find MvvmCross extensions to easily use XamSvg with mvvmcross.

The ios demo app has been compiled and uploaded to a live ios simulator, thanks to appetize.io. Check it here: https://appetize.io/app/amyhugx1xzurnv45h8kyp5kam0?device=iphone5s&scale=75&orientation=portrait&osVersion=9.3

# Animated SVG
The xamarin Forms projects contain a code demonstrating an animated svg ring. This code works on all platform: android, ios, windows universal and windows phone, and does not depend on custom renderer, it is fully contained in the PCL Forms project.

Have a look at the RingProgress control here:  
https://github.com/softlion/XamSvg-Samples/blob/master/Demos/XamSvg.XamFormsDemo/XamSvg.Demo/Controls/RingProgress.cs

# Resources

The XamSvg Xamarin native component can be found there:
https://components.xamarin.com/view/xamsvg

The XamSvg for Xamarin.Forms component can be found there:
https://components.xamarin.com/view/xamsvgforms

MvvmCross samples:
https://github.com/MvvmCross/MvvmCross-Samples

Xamarin samples:
https://github.com/xamarin/monotouch-samples

[//]: # (comments)
[//]: # ([![Demo CountPages alpha](http://share.gifyoutube.com/KzB6Gb.gif)](https://www.youtube.com/watch?v=ek1j272iAmc))
[//]: # (end comments)

# Special notes

## UW

In release mode, UW projects won't detect the dependency services and renderers.  
So you'll have to add these lines:

    [assembly: ExportRenderer(typeof(SvgImage), typeof(SvgImageRenderer))]
    [assembly: Dependency(typeof(SvgLogger))]

The second line is optional as of version 2.3.3.4

# Receipes

Android native: set back button toolbar icon

    var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
    toolbar.NavigationIcon = SvgFactory.GetDrawable(this, "res:images.backward", CancellationToken.None, SvgColorMapperFactory.FromString("000000=FFFFFF"));
