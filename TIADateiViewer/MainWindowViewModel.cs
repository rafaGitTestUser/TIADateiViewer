using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Xml;
using System.Xml.Linq;
using TIADateiViewer;

namespace TIADateiViewer
{
    class MainWindowViewModel : INotifyPropertyChanged, IClose
    {
        private string filename;
        public string FileName
        {
            get => filename;
            set
            {
                if (filename != value)
                {
                    filename = value;
                    RaisePropertyChange(nameof(WindowTitle));
                }
            }
        }

        private List<NodeType> nodeTypes;
        public List<NodeType> NodeTypes
        {
            get => nodeTypes;
            set
            {
                if (nodeTypes != value)
                {
                    nodeTypes = value;
                    RaisePropertyChange(nameof(NodeTypes));
                }
            }
        }

        //private XmlDataProvider provider;
        //public XmlDataProvider XmlProvider
        //{
        //    get => provider;
        //    set
        //    {
        //        if (provider != value)
        //        {
        //            provider = value;
        //            RaisePropertyChange(nameof(WindowTitle));
        //        }
        //    }
        //}

        public string WindowTitle => $"TIA Selection Tool - Datei Viewer - {FileName}";
        public DelegateCommand FileOpenCmd { get; set; }
        public DelegateCommand CloseWindowCmd { get; set; }
        public DelegateCommand ReadXMLCmd { get; set; }
        public DelegateCommand CreateNodeTypeButton { get; set; }
        public DelegateCommand GetPropertiesCmd { get; set; }
        public Action Close{ get; set; }
        public Action CreateButton { get; set; }
        public ICollection<IButton> Buttons { get; }

        public MainWindowViewModel()
        {
            FileOpenCmd = new DelegateCommand((o) => OpenFileDialog());
            CloseWindowCmd = new DelegateCommand((o) => CloseWindow());
            GetPropertiesCmd = new DelegateCommand((o) => GetProperties());

            CreateNodeTypeButton = new DelegateCommand((o) => CreateButton());

            Buttons = new ObservableCollection<IButton>();

        }

        private void GetProperties()
        {
            throw new NotImplementedException();
        }

        private void CloseWindow()
        {
            Close?.Invoke();
        }

        private void OpenFileDialog()
        {
            OpenFileDialog fileDialog = new OpenFileDialog
            {
                Multiselect = false,
                Filter = "TIA Datei (*.tia)|*.tia",
                FilterIndex = 2,
                RestoreDirectory = true
            };

            if (fileDialog.ShowDialog() == true)
            {
                FileInfo fi = new FileInfo(fileDialog.FileName);
                FileName = fi.Name;
                try
                {
                    NodeTypes = XmlModel.GetNodeTypes(fi.FullName);
                }
                catch (Exception)
                {
                    MessageBox.Show("The XML file is invalid");
                    return;
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChange([CallerMemberName] string propertyName = "")
        {
            if (!string.IsNullOrEmpty(propertyName))
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(WindowTitle)));
        }
    }
}
