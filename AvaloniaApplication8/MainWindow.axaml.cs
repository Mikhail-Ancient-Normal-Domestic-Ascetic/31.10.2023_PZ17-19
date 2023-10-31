using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using Avalonia.Controls;
using Avalonia.Interactivity;
using AvaloniaApplication7.Models;
using MySqlConnector;

namespace AvaloniaApplication2;

public partial class MainWindow : Window
{
  
    public MainWindow()
    {
        InitializeComponent();
        
    }
    
    private void Button_OnClick(object? sender, RoutedEventArgs e)
    {
        
        new BookingWindow().ShowDialog(this);
    }

    private void Button_OnClick2(object? sender, RoutedEventArgs e)
    {
        
        new TypeWindows().ShowDialog(this);
    }

    private void Button_OnClick3(object? sender, RoutedEventArgs e)
    {
        
        new SetsWindows().ShowDialog(this);
    }

    private void Button_OnClick4(object? sender, RoutedEventArgs e)
    {
        
        new HumanWindows().ShowDialog(this);
    }
    
    private void Button_OnClick5(object? sender, RoutedEventArgs e)
    {
        
        new Rooms().ShowDialog(this);
    }
    
   
 
    
}