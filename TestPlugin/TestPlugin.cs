using FPLedit.Buchfahrplan;
using FPLedit.Shared;
using System.Windows.Forms;

namespace TestPlugin
{
    [Plugin("TestPlugin", Author = "Autorenname")]
    public class Plugin : IPlugin
    {
        public void Init(IInfo info)
        {
            // Code zur Initialisierung der Erweiterung
            info.Register<IExport>(new TestExport());
            info.Register<IImport>(new TestImport());

            info.Logger.Info("Das ist nur eine Meldung");
            info.Logger.Warning("Die Warnmeldung ist auf Windows-Systemen gelb");
            info.Logger.Error("Fehlermeldungen sind rot");

            // Wert ermitteln:
            bool a = info.Settings.Get<bool>("test-bool", false);
            // Wert setzen:
            info.Settings.Set("test-bool", true);
            // Werte ändern sich nicht, sondern müssen neu abgefragt werden:
            bool b = info.Settings.Get<bool>("test-bool", false);
            info.Logger.Info("a == b: " + (a == b));
            // Sie lassen sich auch wieder entfernen:
            info.Settings.Remove("test-bool");

            // Gibt den absoluten Pfad der Datei %TEMP%\fpledit\abc.xyz zurück:
            var fn = info.GetTemp("abc.xyz");
            info.Logger.Info(fn);

            var item = new ToolStripMenuItem("Test");
            info.Menu.Items.Add(item);

            // Einen Untereintrag hinzufügen:
            var subItem = item.DropDownItems.Add("Bla");
            subItem.Enabled = false;

            info.FileStateChanged += (s, e) => {
                // Nur aktivieren, wenn Datei geöffnet
                subItem.Enabled = e.FileState.Opened;
            };

            info.Register<IBfplTemplate>(new TestTemplate());
        }

        private void FileHandling(IInfo info)
        {
            // Öffnet den "Datei öffnen"-Dialog:
            info.Open();
            // Speichert die Datei normal (d.h. bei bekanntem Pfad ohne Dialog):
            info.Save(false);
            // Öffnet zwingend den "Speichern unter"-Dialog:
            info.Save(true);
            // Lädt die Datei am aktuellen Speicherort neu
            // z.B. nach Änderungen durch externe Programme verwendbar:
            info.Reload();
        }
    }
}