using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// GlobalVariables.cs
/// Clasa speciala in care stocam variabele globale pentru gestionarea lor in pagina
public static class GlobalVariables
{
    public static string ConnectionString = @"Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|\database.mdf;Integrated Security=True;User Instance=True";

    public static string[] keywords;
}