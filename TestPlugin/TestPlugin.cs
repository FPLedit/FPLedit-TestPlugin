using Eto.Forms;
using FPLedit.Shared;
using FPLedit.Shared.UI;

namespace TestPlugin
{
    [Plugin("TestPlugin", "2.2.0", "2.2", Author = "Autorenname")]
    public class Plugin : IPlugin
    {
        public void Init(IPluginInterface info, IComponentRegistry registry)
        {
            // Code zur Initialisierung der Erweiterung
            registry.Register<IExport>(new TestExport());
            registry.Register<IImport>(new TestImport());

            info.Logger.Info("Das ist nur eine Meldung");
            info.Logger.Warning("Die Warnmeldung ist auf Windows-Systemen gelb");
            info.Logger.Error("Fehlermeldungen sind rot");
            info.Logger.Debug("Debug-Meldungen werden dem Benutzer nicht angezeigt");

            // Wert ermitteln:
            bool a = info.Settings.Get<bool>("test-bool", false);
            // Wert setzen:
            info.Settings.Set("test-bool", true);
            // Werte ändern sich nicht, sondern müssen neu abgefragt werden:
            bool b = info.Settings.Get<bool>("test-bool", false);
            info.Logger.Info("a == b: " + (a == b));
            // Sie lassen sich auch wieder entfernen:
            info.Settings.Remove("test-bool");
            
            /*
             * Hinweis: Im IReducesPluginInterface können Erweiterungen nur gelesen werden.
             */

            // Gibt den absoluten Pfad der Datei %TEMP%\fpledit\abc.xyz zurück:
            var fn = info.GetTemp("abc.xyz");
            info.Logger.Info(fn);
            
            var item = ((MenuBar)info.Menu).CreateItem("Test");

            // Einen Untereintrag hinzufügen:
            var subItem = item.CreateItem("Bla");
            subItem.Enabled = false;

            info.FileStateChanged += (s, e) => {
                // Nur aktivieren, wenn Datei geöffnet
                subItem.Enabled = e.FileState.Opened;
            };
        }

        private void FileHandling(IPluginInterface info)
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