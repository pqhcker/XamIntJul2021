using System;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using XamIntJul2021.iOS.Effects;

[assembly:ResolutionGroupName("Effects")]
[assembly:ExportEffect(typeof(EntryLineColorEffect),"EntryLineColorEffect")]
namespace XamIntJul2021.iOS.Effects
{
    
    public class EntryLineColorEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            var textField = Control as UITextField;
            textField.Layer.CornerRadius = 8.0f;
            textField.Layer.MasksToBounds = true;
            textField.Layer.BorderColor = Color.Red.ToCGColor();
            textField.Layer.BorderWidth = 1.0f;
        }

        protected override void OnDetached()
        {
            var textField = Control as UITextField;
            textField.Layer.CornerRadius = 8.0f;
            textField.Layer.MasksToBounds = true;
            textField.Layer.BorderColor = Color.Black.ToCGColor();
            textField.Layer.BorderWidth = 1.0f;
        }
    }
}
