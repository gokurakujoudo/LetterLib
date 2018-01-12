using System.Xml.Serialization;
using SharpHelper.Object;

namespace LetterLib.Data {
    public class FieldSlot : SharpObject {
        public FieldSlot() => IsListen = true;
        private string _fieldName;
        private string _fieldValue;

        [XmlAttribute]
        public string FieldName {
            get => _fieldName;
            set {
                if (_fieldName == value) return;
                _fieldName = value;
                OnPropertyChanged();
            }
        }

        [XmlElement]
        public string FieldValue {
            get => _fieldValue;
            set {
                if (_fieldValue == value) return;
                _fieldValue = value;
                OnPropertyChanged();
            }
        }
    }
}
