using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace OilfieldCalc2.Models
{
    public class MyMenuItem : BindableBase
    {
        public string Title { get; set; }       //Name of the menu item
        public string Icon { get; set; }        //Icon image to display with the menu item
        public string PageName { get; set; }    //Name of the page to navigate to.

        private bool _isEnabled;
        public bool IsEnabled
        {
            get => _isEnabled;
            set => SetProperty(ref _isEnabled, value);
        }
    }
}
