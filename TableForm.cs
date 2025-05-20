using System;
using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace TableEditorMDI
{
    public class TableForm : Form
    {
        private DataGridView grid = new DataGridView { Dock = DockStyle.Fill };

        public TableForm()
        {
            Text = "Таблица (Продукты)";
            Width = 600;
            Height = 400;

            Controls.Add(grid);
            var menu = new MenuStrip();
            var файл = new ToolStripMenuItem("Файл");
            var сохранить = new ToolStripMenuItem("Сохранить", null, Save_Click);
            var открыть = new ToolStripMenuItem("Открыть", null, Open_Click);
            файл.DropDownItems.AddRange(new[] { сохранить, открыть });
            menu.Items.Add(файл);
            Controls.Add(menu);
            MainMenuStrip = menu;

            InitGrid();
        }

        private void InitGrid()
        {
            grid.Columns.Clear();
            grid.Columns.Add("Name", "Название");
            grid.Columns.Add("Price", "Цена");
            grid.Columns.Add("Qty", "Количество");
        }

        private void Save_Click(object sender, EventArgs e)
        {
            var sfd = new SaveFileDialog { Filter = "JSON (*.json)|*.json" };
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                var data = new string[grid.Rows.Count - 1][];
                for (int i = 0; i < grid.Rows.Count - 1; i++)
                {
                    data[i] = new string[grid.Columns.Count];
                    for (int j = 0; j < grid.Columns.Count; j++)
                        data[i][j] = grid.Rows[i].Cells[j].Value?.ToString() ?? "";
                }
                File.WriteAllText(sfd.FileName, JsonConvert.SerializeObject(data));
            }
        }

        private void Open_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog { Filter = "JSON (*.json)|*.json" };
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                InitGrid();
                var data = JsonConvert.DeserializeObject<string[][]>(File.ReadAllText(ofd.FileName));
                foreach (var row in data)
                    grid.Rows.Add(row);
            }
        }
    }
}
