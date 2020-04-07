using FPLedit.Shared;
using System.IO;
using System.Text;

namespace TestPlugin
{
    public class TestExport : IExport
    {
        public string Filter => "Fahrplanname als Textdatei (*.txt)|*.txt";

        public bool Export(Timetable tt, Stream filename, IReducedPluginInterface pluginInterface, string[] flags)
        {
            /*
             * Hinweis: Das IReducedPluginInterface ist hier extra angegeben, da Exporter möglicherweise
             * asynchron ausgeführt werden und daher nicht der volle Funkstionsumfang des IPluginInterfaces verwedent
             * werden kann.
             *
             * Besipiel: Wir exportieren den Fahrplannamen in eine Textdatei.
             * Das ist nicht sonderlich spektakulär. Hier ist aber beliebig viel Logik denkbar.
             * Auch binäre Daten können ohne Probleme exportiert werden.
             * */

            var content = tt.TTName; 
            using (var streamWriter = new StreamWriter(filename, new UTF8Encoding(false), 1024, true))
                streamWriter.Write(content);
            return true; // Gibt an, ob der Exportvorgang erfolgreich war.
        }
    }
}