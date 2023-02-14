using Lab.Lab3.Entities;
using Lab.Lab3.Services;
using System.Collections.ObjectModel;

namespace Lab.Lab3;

public partial class DBView : ContentPage {
    private IDbService DbService { get; init; }
    public IList<Set> Sets { get; set; }
    public ObservableCollection<Sushi> Sushi { get; private set; }

    private void LoadSets(object? sender, EventArgs e) {
        Sets = DbService!.GetAllSets().ToList();
        GroupPicker.ItemsSource = null;
        GroupPicker.ItemsSource = (System.Collections.IList?)Sets;
    }

    private void OnSelectedItem(object sender, EventArgs e) {
        var picker = (Picker)sender;
        Set? selectedItem = (Set?)picker.SelectedItem;
        if (selectedItem is not null) {
            Sushi.Clear();
            foreach (var element in DbService.GetSetSushi(selectedItem.Id)) {
                Sushi.Add(element);
            }
        }
    }

    public DBView(IDbService dbService) {
        DbService = dbService;
        DbService.Init();
        InitializeComponent();
        Sets = new List<Set>();
        Sushi = new();
        BindingContext = this;
    }
}