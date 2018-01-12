using System.Xml.Serialization;
using SharpHelper.Object;

namespace LetterLib.Data
{
    public class ParaSlot:SharpObject
    {
        public ParaSlot() => IsListen = true;
        private string _paraName;
        private string _paraValue;
        private int _paraSubName;

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
        public int ParaSubName {
            get => _paraSubName;
            set {
                if (_paraSubName == value) return;
                _paraSubName = value;
                OnPropertyChanged();
            }
        }
    }
}
