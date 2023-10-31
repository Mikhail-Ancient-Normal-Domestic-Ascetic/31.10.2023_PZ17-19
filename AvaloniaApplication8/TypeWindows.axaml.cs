using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using AvaloniaApplication7.Models;
using MySqlConnector;

namespace AvaloniaApplication2;

public partial class TypeWindows : Window
{
    private List<type> types { get; set; }
    private MySqlConnectionStringBuilder _connectionSb;

    public TypeWindows()
    {
        InitializeComponent();
        types = new List<type>();
        ComboBoxType.Items.Add("на 1");
        ComboBoxType.Items.Add("на 6");
        _connectionSb = new MySqlConnectionStringBuilder
        {
            Server = "10.10.1.24",
            Database = "pro26",
            UserID = "user_01",
            Password = "user01pro"

        };
        UpdateDataGrid();
    }

    private void UpdateDataGrid()
    {
        using (var connection = new MySqlConnection(_connectionSb.ConnectionString))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT *FROM type";
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        types.Add(new type()
                        {
                            Id = reader.GetInt32("id"),
                            Name = reader.GetString("name")
                        });
                    }

                }
            }

            connection.Close();
        }
        GroupsDataGrid15.ItemsSource = types;
    }

    private void ComboBoxType_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        var listSearh2 = types.Where(u => u.Name.Contains(ComboBoxType.SelectedValue.ToString())).ToList();
        GroupsDataGrid15.ItemsSource = listSearh2;
    }
}