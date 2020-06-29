using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using OilfieldCalc2.CustomControls;
using OilfieldCalc2.iOS;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using CoreText;

[assembly: ExportRenderer(typeof(HighlightedEntry), typeof(HighlightedEntryRenderer))]
namespace OilfieldCalc2.iOS
{
    public class HighlightedEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                var nativeTextField = (UITextField)Control;
                nativeTextField.EditingDidBegin += (object sender, EventArgs eIos) =>
                {
                    nativeTextField.PerformSelector(new ObjCRuntime.Selector("selectAll"), null, 0.0f);
                };
            }
        }
    }
}