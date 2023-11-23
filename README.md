## Show DataGridView in WindowsFormsHost on Page

Your question seems to lay attribution on the `WindowsFormsHost` and `DataGridView` but have you checked to make sure that _anything_ is visible (for example by setting the top-level grid to an identifiable background color)? I don't see `Content` being set and I'm wondering if it might be as simple as correcting that?

```
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        var frame = new Frame();
        FrameApp.SetCurrentMainFrame(frame);
        FrameApp.SetCurrentTopFrame(frame);
        FrameApp.FrameTop.Navigate(new ChooseDepPage());

        Content = frame; // << Check that content is being set "somewhere"
    }
}
```
___

**Minimal try (and fail to) reproduce issue with Page**

[![DataGridView shown in WindowsFormsHost][1]][1]

```

public MainWindow()
{
    InitializeComponent();
    var frame = new Frame();
    FrameApp.SetCurrentMainFrame(frame);
    FrameApp.SetCurrentTopFrame(frame);
    FrameApp.FrameTop.Navigate(new ChooseDepPage());

    Content = frame; // << Check that content is being set "somewhere"
}
```
___

```
class FrameApp
{
    public static Frame FrameMain { get; set; }
    public static Frame FrameTop { get; set; }
    public static void SetCurrentMainFrame(Frame frame) => FrameMain = frame;
    public static void SetCurrentTopFrame(Frame frame) => FrameTop = frame;
}
```
___

```
<Page 
    x:Class="show_data_grid_view.ChooseDepPage"
    xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
    xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006" 
    xmlns:d="http://schemas.microsoft.com/expression/blend/2008" 
    xmlns:local="clr-namespace:show_data_grid_view"
    xmlns:wf="clr-namespace:System.Windows.Forms;assembly=System.Windows.Forms"
    mc:Ignorable="d" 
    d:DesignHeight="450" d:DesignWidth="800"
    Title="SelectedDepPageVer2">
    <Grid 
        x:Name="grid1"
        Background="LightGreen">
        <Grid.RowDefinitions>
            <RowDefinition Height="0.10*"/>
            <RowDefinition Height="*"/>
            <RowDefinition Height="20"/>
        </Grid.RowDefinitions>
        <Grid.ColumnDefinitions>
            <ColumnDefinition Width="*"/>
            <ColumnDefinition Width="0.24*"/>
        </Grid.ColumnDefinitions>
        <Border 
        Grid.Row="1" 
        Grid.Column="0"
        BorderBrush="Azure" 
        BorderThickness="10">
            <WindowsFormsHost
            x:Name="windowsFormsHost">
                <wf:DataGridView 
                x:Name="StaffDGV" 
                RowHeadersVisible="False"
                ReadOnly="True" 
                SelectionMode="FullRowSelect" 
                AllowUserToResizeRows="False"
                AutoSizeColumnsMode="Fill"
                DoubleClick="StaffDGV_DoubleClick" 
                SelectionChanged="StaffDGV_SelectionChanged"
                AutoGenerateColumns="False"
                BackgroundColor="WhiteSmoke"
                Dock="Fill"/>
            </WindowsFormsHost>
        </Border>
    </Grid>
</Page>
```

___
```
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
```
___
```
class ChooseDepPageModel
{
    public BindingList<Record> Recordset { get; } = new BindingList<Record>();
}
```
___
```
class Record
{
    public int Index { get; set; }
    public string Description { get; set; }
}
```


  [1]: https://i.stack.imgur.com/3Sd44.png