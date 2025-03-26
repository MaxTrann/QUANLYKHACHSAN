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
            // Enable visual styles & DevExpress skins
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            BonusSkins.Register();
            SkinManager.EnableFormSkins();
            DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle("DevExpress Style");

            // Kiểm tra sự tồn tại của file cấu hình kết nối
            if (File.Exists("connectdb.dba"))
            {
                try
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    using (FileStream fs = File.Open("connectdb.dba", FileMode.Open, FileAccess.Read))
                    {
                        // Deserialize đối tượng cấu hình (ví dụ: của lớp connect)
                        connect cp = (connect)bf.Deserialize(fs);

                        // Giải mã các chuỗi cấu hình sử dụng Encryptor.Decrypt
                        string servername = Encryptor.Decrypt(cp.servername, "qwertyuiop", true);
                        string username = Encryptor.Decrypt(cp.username, "qwertyuiop", true);
                        string pass = Encryptor.Decrypt(cp.passwd, "qwertyuiop", true);
                        string database = Encryptor.Decrypt(cp.database, "qwertyuiop", true);

                        // Xây dựng chuỗi kết nối SQL
                        ConnectionString = $"Data Source={servername};Initial Catalog={database};User ID={username};Password={pass};";
                        connoi = ConnectionString;

                        // Lưu các giá trị vào myFunctions (để sử dụng sau này)
                        myFunctions._srv = servername;
                        myFunctions._us = username;
                        myFunctions._pw = pass;
                        myFunctions._db = database;

                        // Thử mở kết nối để kiểm tra chuỗi kết nối
                        using (SqlConnection con = new SqlConnection(ConnectionString))
                        {
                            con.Open();
                            con.Close();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi đọc hoặc giải mã file connectdb.dba:\n" + ex.Message,
                                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                MessageBox.Show("Không tìm thấy file connectdb.dba. Vui lòng kiểm tra lại!",
                                "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Nếu không có lỗi, chạy form chính
            Application.Run(new frmMain());
        }
    }
}
