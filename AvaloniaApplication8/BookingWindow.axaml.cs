using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using AvaloniaApplication7.Models;
using MySqlConnector;

namespace AvaloniaApplication2;

public partial class BookingWindow : Window
{
    private List<booking> Bookings { get; set; }
    private MySqlConnectionStringBuilder _connectionSb;
    public BookingWindow()
    {
        InitializeComponent();
        Bookings = new List<booking>();
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
                command.CommandText = "SELECT *FROM booking ";
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Bookings.Add(new booking()
                        {
                            Id = reader.GetInt32("Id"),
                            human = reader.GetInt32("human"),
                            numePeople= reader.GetInt32("numPeople"),
                            numHotel = reader.GetInt32("numHotel"),
                            service = reader.GetInt32("service"),
                            price = reader.GetInt32("price")
                        });
                    }
                    
                }
            }
            connection.Close();
        }

        GroupsDataGrid11.ItemsSource = Bookings;
    }

   
}
