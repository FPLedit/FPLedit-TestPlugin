using FPLedit.Shared;
using System.IO;

namespace TestPlugin
{
    public class TestExport : IExport
    {
        public string Filter => "Fahrplanname als Textdatei (*.txt)|*.txt";

        public bool Export(Timetable tt, string filename, IInfo info)
        {
            var content = tt.TTName;
            File.WriteAllText(filename, content);
            return true; // Gibt an, ob der Exportvorgang erfolgreich war.
        }
    }
}