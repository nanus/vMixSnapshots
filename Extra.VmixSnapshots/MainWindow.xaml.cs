using Extra.vMixApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Extra.vMixSnapshots
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly MainViewModel _vm = null;

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = _vm = new MainViewModel();
        }

        protected override void OnPreviewKeyUp(KeyEventArgs e)
        {
            base.OnPreviewKeyUp(e);

            if (e.Key == Key.F1)
            {
                this.HandleTakeSnapshot(null, null);
            }
        }

        private void HandleSelectStorageClick(object sender, RoutedEventArgs e)
        {
            var newPath = ShowFolderBrowserDialog(_vm.vMixStorageDirectory);
            if (!string.IsNullOrWhiteSpace(newPath) && System.IO.Directory.Exists(newPath))
                _vm.vMixStorageDirectory = newPath;
        }

        private void HandleSelectSaveDirClick(object sender, RoutedEventArgs e)
        {
            var newPath = ShowFolderBrowserDialog(_vm.SnapshotSaveDirectory);
            if (!string.IsNullOrWhiteSpace(newPath) && System.IO.Directory.Exists(newPath))
                _vm.SnapshotSaveDirectory = newPath;
        }

        private string ShowFolderBrowserDialog(string initialPath)
        {
            System.Windows.Forms.FolderBrowserDialog dlg = new System.Windows.Forms.FolderBrowserDialog();
            dlg.ShowNewFolderButton = false;
            dlg.SelectedPath = initialPath;
            System.Windows.Forms.DialogResult result = dlg.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                return dlg.SelectedPath;
            }

            return null;
        }

        private async void HandleTakeSnapshot(object sender, RoutedEventArgs e)
        {
            var dt = DateTime.Now.ToString("yyyyMMddHHmmss");
            await _vm.TakeSnapshotAsync($"Snapshot_{dt}.png");
        }

        private void HandleOpenStorageClick(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(_vm.vMixStorageDirectory);
        }

        private void HandleOpenSaveDirClick(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(_vm.SnapshotSaveDirectory);
        }
    }
}
