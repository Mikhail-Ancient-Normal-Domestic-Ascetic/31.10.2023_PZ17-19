using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using AvaloniaApplication7.Models;
using AvaloniaApplication8;
using MySqlConnector;

namespace AvaloniaApplication2;

public partial class SetsWindows : Window
{
    private List<sets> setss { get; set; }
    private MySqlConnectionStringBuilder _connectionSb;
    public SetsWindows()
    {
        InitializeComponent();
        setss = new List<sets>();
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
                command.CommandText = "SELECT * FROM sets";
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        setss.Add(new sets()
                        {
                            Id = reader.GetInt32("Id"),
                            services = reader.GetString("services")
                        });
                    }

                }
            }

            connection.Close();
        }
        GroupsDataGrid14.ItemsSource = setss;
    }

    private void Button_OnClick(object? sender, RoutedEventArgs e)
    {
   
        UpdateDataGrid();
    }
}