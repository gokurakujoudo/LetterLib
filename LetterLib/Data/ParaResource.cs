using System.Xml.Serialization;
using SharpHelper.Object;

namespace LetterLib.Data {
    public class ParaResource : SharpObject {
        public ParaResource() => IsListen = true;
        private string _paraName;
        private string _paraValue;
        private string _resourceName;

        [XmlAttribute]
        public string ParaName {
            get => _paraName;
            set {
                if (_paraName == value) return;
                _paraName = value;
                OnPropertyChanged();
            }
        }

        [XmlElement]
        public string ParaValue {
            get => _paraValue;
            set {
                if (_paraValue == value) return;
                _paraValue = value;
                OnPropertyChanged();
            }
        }

        [XmlAttribute]
        public string ResourceName {
            get => _resourceName;
            set {
                if (_resourceName == value) return;
                _resourceName = value;
                OnPropertyChanged();
            }
        }
    }
}
