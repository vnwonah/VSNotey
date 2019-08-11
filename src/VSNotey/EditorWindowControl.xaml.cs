using Microsoft;
using Microsoft.VisualStudio.Shell;
using System.Globalization;
using System.Windows.Input;

namespace VSNotey
{
    using EnvDTE;
    using EnvDTE80;
    using Microsoft.VisualStudio.Settings;
    using Microsoft.VisualStudio.Shell.Interop;
    using Microsoft.VisualStudio.Shell.Settings;
    using Microsoft.Win32;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Documents;
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
        /// 
        private Events _dteEvents;
        private SolutionEvents _slnEvents;
        private DTE _dte;
        public EditorWindowControl()
        {
            Dispatcher.VerifyAccess();
            this.InitializeComponent();


            string assemblyFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string add = Path.Combine(assemblyFolder, "Images", "add.png");
            string sub = Path.Combine(assemblyFolder, "Images", "subtract.png");
            string save = Path.Combine(assemblyFolder, "Images", "save.png");


            addimage.Source = new BitmapImage( new Uri(add));
            subimage.Source = new BitmapImage(new Uri(sub));
            saveimage.Source = new BitmapImage(new Uri(save));


            try
            {

                _dte = (DTE)ServiceProvider.GlobalProvider.GetService(typeof(DTE));

                _dteEvents = _dte.Events;
                _slnEvents = _dteEvents.SolutionEvents;
                _slnEvents.Opened += OnSolutionOpened;
                _slnEvents.BeforeClosing += OnSolutionClosing;

              
            }
            catch (Exception e)
            {

            }
          
        }

        private async void OnSolutionClosing()
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();
            try
            {
                var text = ConvertRichTextBoxContentsToString(txtBox);

                string solutionDir = Path.GetDirectoryName(_dte.Solution.FullName);

                using (StreamWriter outputFile = new StreamWriter(Path.Combine(solutionDir, "notes.notey")))
                {
                    await outputFile.WriteAsync(text);
                }
            }
            catch (Exception)
            {

            }
        }

        private void OnSolutionOpened()
        {
            try
            {
                Dispatcher.VerifyAccess();
                string solutionDir = Path.GetDirectoryName(_dte.Solution.FullName);
                txtBox.AppendText(File.ReadAllText(Path.Combine(solutionDir, "notes.notey")));
            }
            catch (Exception)
            {

            }
           
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
                string.Format(CultureInfo.CurrentUICulture, "Invoked '{0}'", this.ToString()),
                "EditorWindow");
        }

       
        private void addimage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtBox.FontSize += 1;
        }

        private void subimage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (txtBox.FontSize > 6)
                txtBox.FontSize -= 1;
        }

        private async void saveimage_MouseDown(object sebder, MouseButtonEventArgs e)
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();
            try
            {
                var text = ConvertRichTextBoxContentsToString(txtBox);

                string solutionDir = Path.GetDirectoryName(_dte.Solution.FullName);

                using (StreamWriter outputFile = new StreamWriter(Path.Combine(solutionDir, "notes.notey")))
                {
                    await outputFile.WriteAsync(text);
                }

                MessageBox.Show("Notes Saved");
            }
            catch (Exception ex)
            {
               
            }
           

        }

        private string ConvertRichTextBoxContentsToString(RichTextBox rtb)
        {
            System.Windows.Documents.TextRange textRange = new System.Windows.Documents.TextRange(rtb.Document.ContentStart, rtb.Document.ContentEnd);
            return textRange.Text;
        }

        public string GetImageFullPath(string filename)
        {
            var x =  Path.Combine(
                    //Get the location of your package dll
                    Assembly.GetExecutingAssembly().Location,
                    //reference your 'images' folder
                    "Images\\",
                    filename
                 );

            return x;
        }
    }
}