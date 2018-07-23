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
    using System.Windows;
    using System.Windows.Controls;

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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            RegistryKey key = Registry.LocalMachine.OpenSubKey("Software\\Microsoft\\VisualStudio\\15.0");

            if (key != null)
            {
                string[] values = key.GetValueNames();

                foreach (var item in values)
                {
                    txtBox.Text += "\n" + item;
                }
            }
        }
    }
}