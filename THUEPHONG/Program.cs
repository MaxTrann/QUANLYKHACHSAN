using DataLayer;
using DevExpress.Skins;
using DevExpress.UserSkins;
using System;
using System.Data.SqlClient;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace THUEPHONG
{
    static class Program
    {
        public static string ConnectionString; // Lưu chuỗi kết nối toàn cục
        public static string connoi; // Nếu cần lưu chuỗi kết nối riêng

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            BonusSkins.Register();
            SkinManager.EnableFormSkins();
            DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle("DevExpress Style");

            if (File.Exists("connectdb.dba"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream fs = File.Open("connectdb.dba", FileMode.Open, FileAccess.Read);

                connect cp = (connect)bf.Deserialize(fs);
                string servername = Encryptor.Decrypt(cp.servername, "qwertyuiop", true);
                string username = Encryptor.Decrypt(cp.username, "qwertyuiop", true);
                string pass = Encryptor.Decrypt(cp.passwd, "qwertyuiop", true);
                string database = Encryptor.Decrypt(cp.database, "qwertyuiop", true);

                string conStr = $"Data Source={servername};Initial Catalog={database};User ID={username};Password={pass};";
                ConnectionString = conStr;
                connoi = conStr;

                myFunctions._srv = servername;
                myFunctions._us = username;
                myFunctions._pw = pass;
                myFunctions._db = database;

                SqlConnection con = new SqlConnection(conStr);

                try
                {
                    con.Open();
                    //Application.Run(new frmMain());
                }
                catch 
                {
                    MessageBox.Show("Không thể kết nối CSDL.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    fs.Close();
                }
                con.Close();
                fs.Close();


                // Nếu bạn muốn login trước khi vào frmMain, có thể bật lại đoạn này
                if (File.Exists("sysparam.ini"))
                {
                    Application.Run(new frmLogin());

                }
                else
                {
                    Application.Run(new frmSetParam());

                }    
            }
            else
            {
                Application.Run(new frmKetNoiDB());
            }
        }
    }
}
