namespace VSNotey
{
    using Microsoft.VisualStudio.Settings;
    using Microsoft.VisualStudio.Shell.Interop;
    using Microsoft.VisualStudio.Shell.Settings;
    using Microsoft.Win32;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Reflection;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media.Imaging;

    /// <summary>
    /// Interaction logic for EditorWindowControl.
    /// </summary>
    public partial class EditorWindowControl : UserControl
    {
        private object uiShell;

        /// <summary>
        /// Initializes a new instance of the <see cref="EditorWindowControl"/> class.
        /// </summary>
        public EditorWindowControl()
        {
            this.InitializeComponent();


            string assemblyFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string add = Path.Combine(assemblyFolder, "Images", "add.png");
            string sub = Path.Combine(assemblyFolder, "Images", "subtract.png");


            addimage.Source = new BitmapImage( new Uri(add));
            subimage.Source = new BitmapImage(new Uri(sub));


        }

       

        /// <summary>
        /// Handles click on the button by displaying a message box.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event args.</param>
        [SuppressMessage("Microsoft.Globalization", "CA1300:SpecifyMessageBoxOptions", Justification = "Sample code")]
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Default event handler naming pattern")]
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(
                string.Format(System.Globalization.CultureInfo.CurrentUICulture, "Invoked '{0}'", this.ToString()),
                "EditorWindow");
        }

       
        private void addimage_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            txtBox.FontSize += 1;
        }

        private void subimage_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (txtBox.FontSize > 6)
                txtBox.FontSize -= 1;
        }

        public string GetImageFullPath(string filename)
        {
            var x =  Path.Combine(
                    //Get the location of your package dll
                    System.Reflection.Assembly.GetExecutingAssembly().Location,
                    //reference your 'images' folder
                    "Images\\",
                    filename
                 );

            return x;
        }
    }
}