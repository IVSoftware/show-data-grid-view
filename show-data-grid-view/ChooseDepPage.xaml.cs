using System.ComponentModel;
using System.Windows.Controls;

namespace show_data_grid_view
{
    public partial class ChooseDepPage : Page
    {
        public ChooseDepPage()
        {
            base.DataContext = new ChooseDepPageModel();
            InitializeComponent();

            Loaded += async (sender, e) =>
            {
                // Turn ON for minimal test
                StaffDGV.AutoGenerateColumns = true;
                StaffDGV.DataSource = DataContext.Recordset;
                StaffDGV.Font = new Font("Segoe UI", 16 );
                StaffDGV.ColumnHeadersHeight = 60;
                StaffDGV.RowTemplate.Height = 60;
                StaffDGV.AllowUserToAddRows = false;
                StaffDGV.Columns[nameof(Record.Index)].HeaderText = string.Empty;
                StaffDGV.Columns[nameof(Record.Index)].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                StaffDGV.Columns[nameof(Record.Index)].Width = 60;
                DataContext.Recordset.Add(new Record { Index = 1, Description = "Record A" });
                DataContext.Recordset.Add(new Record { Index = 2, Description = "Record B" });
                DataContext.Recordset.Add(new Record { Index = 3, Description = "Record C" });
                await Task.Delay(100);
                StaffDGV.CurrentCell = null;
            };
        }
        ChooseDepPageModel DataContext => (ChooseDepPageModel)base.DataContext;
        private void StaffDGV_DoubleClick(object sender, EventArgs e) { }
        private void StaffDGV_SelectionChanged(object sender, EventArgs e) { }
    }
    class ChooseDepPageModel
    {
        public BindingList<Record> Recordset { get; } = new BindingList<Record>();
    }
    class Record
    {
        public int Index { get; set; }
        public string Description { get; set; }
    }
}
