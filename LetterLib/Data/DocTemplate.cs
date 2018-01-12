using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using SharpHelper.Object;

namespace LetterLib.Data {
    public class DocTemplate : SharpObject {
        public DocTemplate() => IsListen = true;

        private string _outputFileNameTemplate;
        private string _templateFilePath;
        private ObservableCollection<FieldSlot> _fields;
        private ObservableCollection<ParaSlot> _paraSlots;
        private ObservableCollection<ParaResource> _resources;

        public string OutputFileNameTemplate {
            get => _outputFileNameTemplate;
            set {
                if (_outputFileNameTemplate == value) return;
                _outputFileNameTemplate = value;
                OnPropertyChanged();
            }
        }

        public string TemplateFilePath {
            get => _templateFilePath;
            set {
                if (_templateFilePath == value) return;
                _templateFilePath = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<FieldSlot> Fields {
            get => _fields;
            set {
                if (_fields == value) return;
                _fields = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<ParaSlot> ParaSlots {
            get => _paraSlots;
            set {
                if (_paraSlots == value) return;
                _paraSlots = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<ParaResource> Resources {
            get => _resources;
            set {
                if (_resources == value) return;
                _resources = value;
                OnPropertyChanged();
            }
        }

        public ReadOnlyDictionary<string, List<ParaResource>> ResourceList() =>
            new ReadOnlyDictionary<string, List<ParaResource>>(
                this.Resources.GroupBy(r => r.ParaName).ToDictionary(
                    g => g.Key, g => g.ToList()));
    }
}
