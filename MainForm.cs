using System;
using System.Windows.Forms;

namespace TableEditorMDI
{
    public class MainForm : Form
    {
        public MainForm()
        {
            IsMdiContainer = true;
            Text = "MDI Табличный редактор";
            Width = 800;
            Height = 600;

            var menu = new MenuStrip();
            var файл = new ToolStripMenuItem("Файл");
            var новаяТаблица = new ToolStripMenuItem("Новая таблица", null, (s, e) =>
            {
                var child = new TableForm { MdiParent = this };
                child.Show();
            });
            var выход = new ToolStripMenuItem("Выход", null, (s, e) => Close());

            файл.DropDownItems.AddRange(new[] { новаяТаблица, выход });
            menu.Items.Add(файл);
            MainMenuStrip = menu;
            Controls.Add(menu);
        }
    }
}
