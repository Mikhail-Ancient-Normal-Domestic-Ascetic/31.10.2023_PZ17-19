using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using AvaloniaApplication7.Models;
using AvaloniaApplication8;
using MySqlConnector;

namespace AvaloniaApplication2;

public partial class Rooms : Window
{
    private List<room> rooms { get; set; }
    private MySqlConnectionStringBuilder _connectionSb;
    public Rooms()
    {
        InitializeComponent();
        rooms = new List<room>();
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
                command.CommandText = "SELECT *FROM room";
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        rooms.Add(new room()
                        {
                            Id = reader.GetInt32("id"),
                            Type = reader.GetInt32("type")
                        });
                    }

                }
            }

            connection.Close();
        }
        GroupsDataGrid13.ItemsSource = rooms;
    }

    private void Button_OnClick(object? sender, RoutedEventArgs e)
    {
        new AddRooms().ShowDialog(this);
        UpdateDataGrid();
    }

   

    private void Button_OnClick2(object? sender, RoutedEventArgs e)
    {
        new Rooms().ShowDialog(this);
        UpdateDataGrid();
    }
}