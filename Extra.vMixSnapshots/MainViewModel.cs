using Extra.vMixApi;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Extra.vMixSnapshots
{
    public enum SnapshotType
    {
        Snapshot,
        SnapshotInput
    }

    public enum SnapshotFormat
    {
        Jpg,
        Png
    }

    class MainViewModel : ViewModelBase
    {
        private readonly vMixClient _vmix = null;

        private SnapshotType snapshotType = SnapshotType.Snapshot;

        public SnapshotType SnapshotType
        {
            get { return snapshotType; }
            set
            {
                snapshotType = value;
                RaisePropertyChanged();
                RaisePropertyChanged("IsSnapshotInput");
            }
        }

        private SnapshotFormat snapshotFormat = SnapshotFormat.Jpg;

        public SnapshotFormat SnapshotFormat
        {
            get { return snapshotFormat; }
            set
            {
                snapshotFormat = value;
                RaisePropertyChanged();
            }
        }

        public bool IsSnapshotInput
        {
            get { return this.SnapshotType == SnapshotType.SnapshotInput; }
        }

        private int _inputNumber = 1;

        public int InputNumber
        {
            get { return _inputNumber; }
            set { _inputNumber = value; RaisePropertyChanged(); }
        }

        private string _vMixStorageDirectory = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "vMixStorage");

        public string vMixStorageDirectory
        {
            get { return _vMixStorageDirectory; }
            set { _vMixStorageDirectory = value; RaisePropertyChanged(); }
        }

        private string _snapshotSaveDirectory = System.IO.Path.Combine( AppDomain.CurrentDomain.BaseDirectory, "Snapshots");

        public string SnapshotSaveDirectory
        {
            get { return _snapshotSaveDirectory; }
            set
            {
                _snapshotSaveDirectory = value;
                RaisePropertyChanged();
                EnsureSaveDirectory();
            }
        }

        private BitmapImage _snapshotBitmap;

        public BitmapImage SnapshotBitmap
        {
            get { return _snapshotBitmap; }
            set { _snapshotBitmap = value; RaisePropertyChanged(); }
        }

        public MainViewModel()
        {
            _vmix = new vMixClient(this.vMixStorageDirectory);

            EnsureSaveDirectory();
        }


        public async Task TakeSnapshotAsync(string name)
        {
            var newName = Path.GetFileNameWithoutExtension(name);
            newName = $"{newName}.{this.SnapshotFormat.ToString().ToLower()}";
            var type = this.SnapshotFormat == SnapshotFormat.Jpg ? ImageFormat.Jpg : ImageFormat.Png;

            string sourceFileName = null;
            if (this.SnapshotType == SnapshotType.Snapshot)
                sourceFileName = await _vmix.GetSnapshotAsync(newName, type);
            else if (this.SnapshotType == SnapshotType.SnapshotInput)
                sourceFileName = await _vmix.GetSnapshotInputAsync(this.InputNumber, newName, type);

            var destFileName = System.IO.Path.Combine(this.SnapshotSaveDirectory, newName);

            System.IO.File.Move(sourceFileName, destFileName);

            this.SnapshotBitmap = await ImageUtils.TryGetBitmapAsync(destFileName);
        }
        private void EnsureSaveDirectory()
        {
            if (!Directory.Exists(this.SnapshotSaveDirectory))
                Directory.CreateDirectory(this.SnapshotSaveDirectory);

        }
    }
}
