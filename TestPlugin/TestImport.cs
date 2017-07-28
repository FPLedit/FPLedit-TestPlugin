using FPLedit.Shared;
using System.IO;

namespace TestPlugin
{
    public class TestImport : IImport
    {
        public string Filter => "Fahrplanname als Textdatei (*.txt)|*.txt";

        public Timetable Import(string filename, ILog logger)
        {
            var tt = new Timetable();
            tt.TTName = File.ReadAllText(filename);
            return tt;
        }
    }
}