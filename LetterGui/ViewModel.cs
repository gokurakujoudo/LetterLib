using LetterLib.Data;
using SharpHelper.Object;

namespace LetterGui {
    class ViewModel : SharpObject {
        private DocTemplate _template;

        public ViewModel() {
            IsListen = true;
            this.Template = TemplateHelper.CurrenTemplate;
        }

        public DocTemplate Template {
            get => _template;
            set {
                if (_template == value) return;
                _template = value;
                OnPropertyChanged();
            }
        }
    }
}
