using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XamIntJul2021.Droid.Effects;

[assembly: ResolutionGroupName("Effects")]
[assembly: ExportEffect(typeof(EntryLineColorEffect), "EntryLineColorEffect")]
namespace XamIntJul2021.Droid.Effects
{
    public class EntryLineColorEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            (Control as Android.Views.View)?.Background.SetColorFilter(
                new Android.Graphics.BlendModeColorFilter(
                Color.Red.ToAndroid(), Android.Graphics.BlendMode.SrcAtop));
        }
        protected override void OnDetached()
        {
            (Control as Android.Views.View)?.Background.SetColorFilter(
                new Android.Graphics.BlendModeColorFilter(
                Color.Black.ToAndroid(), Android.Graphics.BlendMode.SrcAtop));
        }
    }
}
