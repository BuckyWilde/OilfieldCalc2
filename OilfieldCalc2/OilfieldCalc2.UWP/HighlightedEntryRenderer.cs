using OilfieldCalc2.CustomControls;
using OilfieldCalc2.UWP;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Media;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;

[assembly: ExportRenderer(typeof(HighlightedEntry), typeof(HighlightedEntryRenderer))]
namespace OilfieldCalc2.UWP
{
    public class HighlightedEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == nameof(Entry.IsFocused)
                && Control != null && Control.FocusState != Windows.UI.Xaml.FocusState.Unfocused)
                Control.SelectAll();
        }
    }
}
