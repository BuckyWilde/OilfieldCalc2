using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using OilfieldCalc2.CustomControls;
using OilfieldCalc2.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(HighlightedEntry), typeof(HighlightedEntryRenderer))]
namespace OilfieldCalc2.Droid
{
    public class HighlightedEntryRenderer : EntryRenderer
    {
        public HighlightedEntryRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {


                EditText editText = null;

                for (int i = 0; i < ChildCount; ++i)
                {
                    Android.Views.View view = (Android.Views.View)GetChildAt(i);
                    if (view is EditText) editText = (EditText)view;
                }

                editText?.SetSelectAllOnFocus(true);

            }
        }
    }
}