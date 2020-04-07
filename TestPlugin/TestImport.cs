using FPLedit.Shared;
using System.IO;
using System.Text;

namespace TestPlugin
{
    public class TestImport : IImport
    {
        public string Filter => "Fahrplanname als Textdatei (*.txt)|*.txt";

        public Timetable Import(Stream stream, IReducedPluginInterface pluginInterface, ILog replaceLog = null)
        {
            /*
             * Der Fahrplantyp (Linear/Netzwerk) muss hier explizit angegeben werden, kann aber ntürlich auf Basis der
             * importierten Datei gewählt werden.
             * 
             * Hinweis zur asynchronen Ausführung: siehe TestExport.cs
             */
            var tt = new Timetable(TimetableType.Linear);
            using (var streamReader = new StreamReader(stream, new UTF8Encoding(false), true, 1024, true))
                tt.TTName = streamReader.ReadToEnd();
            return tt;
        }
    }
}