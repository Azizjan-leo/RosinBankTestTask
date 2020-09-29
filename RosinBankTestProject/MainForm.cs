using System;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
using _Excel = Microsoft.Office.Interop.Excel;

namespace RosinBankTestProject
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            connstrTxt.Focus();
        }

        private void WriteLine(string data)
        {
            resultTxt.Text += resultTxt.Text == string.Empty ? data : Environment.NewLine + data;
        }

        private void ConnectBtn_Click(object sender, EventArgs e)
        {
            //If a user didn't enter the connection string we coudn't connect
            if (connstrTxt.Text == string.Empty)
            {
                MessageBox.Show("Please, enter the connection string");
                return;
            }

            //create instanace of database connection
            SqlConnection conn = new SqlConnection(connstrTxt.Text);

            try
            {
                WriteLine("Openning Connection ...");

                //open connection
                conn.Open();

                WriteLine("Connection successful!");

            }
            catch (Exception ex)
            {
                WriteLine("Error: " + ex.Message);
            }

            try
            {
                SqlCommand ifExistsCmd = new SqlCommand(@"IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES 
                       WHERE TABLE_NAME='Clients') SELECT 1 ELSE SELECT 0", conn);

                if (Convert.ToInt32(ifExistsCmd.ExecuteScalar()) == 1)
                {
                    WriteLine("The Clients table exists.");
                }
                else
                {
                    WriteLine("The Clients table does not exist. Creating the table...");

                    SqlCommand createTblCmd = new SqlCommand(@"CREATE TABLE Clients(ID integer NOT NULL, Name nvarchar(100) NOT NULL, BirthDate date NOT NULL, PhoneNumber varchar(13), Address nvarchar(25) NOT NULL, SocialNumber varchar(14) NOT NULL, PRIMARY KEY(ID));", conn);

                    createTblCmd.ExecuteNonQuery();

                    WriteLine("The table Clients created successfully!");

                    String insertQuery = "INSERT INTO dbo.Clients (ID,Name,BirthDate,PhoneNumber,Address,SocialNumber) VALUES " +
                    "(1,N'Тестовый клиент1','1991-03-08','123',N'г. Баткен',12345678901234)," +
                    "(2,N'Тестовый клиент2','1996-04-20','456',N'г. Бишкек',98765432101234)," +
                    "(3,N'Тестовый клиент3','1995-08-04','789',N'г. Нарын', 12345543211234)," +
                    "(4,N'Тестовый клиент4','1989-02-25','012',N'с. Комсомольское',12345671234567)";

                    using (SqlCommand command = new SqlCommand(insertQuery, conn))
                    {
                        int result = command.ExecuteNonQuery();

                        WriteLine("The values has been added.");
                    }


                }
            }
            catch (Exception ex)
            {
                WriteLine("Error: " + ex.Message);
            }

            String path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Template\example.xlsx");
            _Application excel = new _Excel.Application();
            Workbook wb = excel.Workbooks.Open(path);
            (int, string, DateTime, string, string, string) client;

            try
            {
                String getQuery = @"SELECT * FROM dbo.Clients WHERE SocialNumber = 12345543211234";

                using (SqlCommand command = new SqlCommand(getQuery, conn))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            WriteLine($"The client with social number 12345543211234 has been found.");
                           
                            Worksheet ws = wb.Worksheets[1];
                            
                            var formatRange = ws.get_Range("b8", "b8");
                            formatRange.NumberFormat = "##############";
                            formatRange = ws.get_Range("i4", "i4");
                            formatRange.NumberFormat = "##############";


                            ws.Cells[1, 2] = DateTime.Now;
                            ws.Cells[3, 2] = ws.Cells[4, 4] = reader.GetInt32(0);
                            ws.Cells[4, 2] = ws.Cells[4, 5] = reader.GetString(1);
                            ws.Cells[5, 2] = ws.Cells[4, 6] = reader.GetDateTime(2);
                            ws.Cells[6, 2] = ws.Cells[4, 7] = reader.GetString(3);
                            ws.Cells[7, 2] = ws.Cells[4, 8] = reader.GetString(4);
                            ws.Cells[8, 2] = ws.Cells[4, 9] = reader.GetString(5);
                        }
                    }
                }

              
                
               
                String dir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + @"Result");
                Directory.CreateDirectory(dir);
                wb.SaveAs(dir + @"\example.xlsx");
                wb.Close();
                conn.Close();

                WriteLine($"The file has been saved in {dir}\\");
                WriteLine("Work completed!");
            }
            catch (Exception ex)
            {
                wb.Close();
                conn.Close();
                WriteLine($"Error: {ex.Message}");
            }
        }
    
        private void ConnstrTxt_KeyPress(object sender, KeyPressEventArgs e)
        { 
            if (e.KeyChar == (char)Keys.Return)
            {
                ConnectBtn_Click(sender, e);
            }
        }
    }
}
