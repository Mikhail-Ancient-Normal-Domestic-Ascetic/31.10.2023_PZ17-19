using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using AvaloniaApplication7.Models;
using MySqlConnector;

namespace AvaloniaApplication8;

public partial class AddRooms : Window
{
    public room newrooms { get; set; }
    private List<room> rooms { get; set; }
    private List<sets> Sets { get; set; } = new();
    private MySqlConnectionStringBuilder _connectionSb;
    public AddRooms()
    {
        InitializeComponent();
        _connectionSb = new MySqlConnectionStringBuilder
                {
                    Server = "10.10.1.24",
                    Database = "pro26",
                    UserID = "user_01",
                    Password = "user01pro"
        
                };
        newrooms = new room();
        rooms = new List<room>();
        ComboBoxN = this.Find<ComboBox>("ComboBoxN");
        
        UpdateComboBox();
        
        #if DEBUG
                this.AttachDevTools();
        #endif
        
        DataContext = newrooms;
        
    }
    private void UpdateComboBox()
    {
        Sets.Clear();
        using (var connection = new MySqlConnection(_connectionSb.ConnectionString))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT *FROM sets";
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Sets.Add(new sets()
                        {
                            Id = reader.GetInt32("Id"),
                            services = reader.GetString("services")
                        });
                    }

                }
            }

            connection.Close();
        }
        ComboBoxN.ItemsSource = Sets;
    }
    

    private void Button_OnClick(object? sender, RoutedEventArgs e)
    {
        
        using (var connection = new MySqlConnection(_connectionSb.ConnectionString))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "INSERT INTO room (Id,Type)" +
                                      "VALUES (@Id,@Type);";
                command.Parameters.AddWithValue("@Id", newrooms.Id);
                command.Parameters.AddWithValue("Type", ((ComboBoxN.SelectedItem) as sets).Id);

                var rowsCount = command.ExecuteNonQuery();
               
                
            }

            connection.Close();
        }
        UpdateComboBox();
        this.Close();
    }
    
}