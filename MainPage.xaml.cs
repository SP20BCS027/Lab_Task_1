using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using DataAccessLibrary;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using System.Data;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Lab_Task_1
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }


        private void Save_Button_Click(object sender, RoutedEventArgs e)
        {
            AddData();
        }
        public void AddData()
        { 
            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "Research.db");
            Console.WriteLine(dbpath);
            using (SqliteConnection myDBConnection = new SqliteConnection($"Filename={dbpath}"))
            {
                myDBConnection.Open();
                SqliteCommand myInsertCommand = new SqliteCommand();
                myInsertCommand.Connection = myDBConnection;
                myInsertCommand.CommandType = CommandType.Text;
                myInsertCommand.CommandText = "INSERT INTO Research VALUES(@Title, @Abstract, @URI, @Notes, @BibTex)";
                myInsertCommand.Parameters.AddWithValue("@Title", TitleText.Text);
                myInsertCommand.Parameters.AddWithValue("@Abstract", Abstract_Text.Text);
                myInsertCommand.Parameters.AddWithValue("@URI", URI.Text);
                myInsertCommand.Parameters.AddWithValue("@Notes", Notes_Text.Text);
                myInsertCommand.Parameters.AddWithValue("@BibTex", Bib_Text_Text.Text);
                var resultoftheCommand = myInsertCommand.ExecuteReader();
                myDBConnection.Close();
            }
        }
        public async static void InitializeDatabase()
        {
            await ApplicationData.Current.LocalFolder.CreateFileAsync("Research.db", CreationCollisionOption.OpenIfExists);
            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "Research.db");
            try
            {
                using (SqliteConnection db =
                   new SqliteConnection($"Filename={dbpath}"))
                {
                    
                    db.Open();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}
