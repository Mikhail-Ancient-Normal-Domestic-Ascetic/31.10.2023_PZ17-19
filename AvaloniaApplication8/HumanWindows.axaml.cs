using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using AvaloniaApplication7.Models;
using MySqlConnector;

namespace AvaloniaApplication2;

public partial class HumanWindows : Window
{
    private List<human> Humans { get; set; }
    private MySqlConnectionStringBuilder _connectionSb;
    
    public HumanWindows()
    {
        InitializeComponent();
        Humans = new List<human>();
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
                command.CommandText = "SELECT *FROM human";
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Humans.Add(new human()
                        {
                            Id = reader.GetInt32("id"),
                            Name = reader.GetString("name"),
                            Surname = reader.GetString("surname"),
                            age = reader.GetInt32("age")
                        });
                    }

                }
            }

            connection.Close();
        }
        GroupsDataGrid12.ItemsSource = Humans;
    }

    private void TextSerch_OnTextChanged(object? sender, TextChangedEventArgs e)
    {
        var listSearh = Humans.Where(u => u.Name.Contains(TextSerch.Text.ToString())).ToList();
        GroupsDataGrid12.ItemsSource = listSearh;
    }
}