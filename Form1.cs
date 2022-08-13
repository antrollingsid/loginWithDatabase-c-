using System.Data;
using System.Data.SqlClient;
namespace LoginWithDb
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtuname.Text.Trim().Length < 3)
            {
                erp.SetError(txtuname, "Username can not be less than 3 characters");
            }
            else
                erp.SetError(txtuname, "");

            if (txtpass.Text.Trim().Length < 3)
            {
                erp.SetError(txtpass, "Password can not be less than 3 characters");
            }
            else
                erp.SetError(txtpass, "");
        //////////////////// COnnection string should be modified when you download my source from github.
        //////////////////// AttachDbFilename=To_SOMEWHERE_Else 
            string cstr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\emreo\source\repos\LoginWithDb\DbSample.mdf;Integrated Security=True";
            using (SqlConnection cnn = new SqlConnection(cstr)) 
            {
                try
                {
                    cnn.Open();
                    if (cnn.State == ConnectionState.Open)
                    {
                        SqlCommand cmd = new SqlCommand();
                        cmd.Connection = cnn;
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "select count(*) as cnt from users where uname=@u and pwd=@p";

                        SqlParameter parmu = new SqlParameter();
                        parmu.ParameterName = "@u";
                        parmu.SqlDbType = SqlDbType.VarChar;
                        parmu.Value=txtuname.Text.Trim();
                        cmd.Parameters.Add(parmu);

                        cmd.Parameters.Add("@p",SqlDbType.VarChar).Value = txtpass.Text.Trim();

                        int result = Convert.ToInt32(cmd.ExecuteScalar());

                        cnn.Close();
                        if (result == 0) {
                            erp.SetError(txtuname, "Username is wrong");
                            erp.SetError(txtpass, "Password is wrong");
                        }
                        else { MessageBox.Show("Login Successfull"); }



                    }

                }
                catch(Exception xp)
                {
                    MessageBox.Show(xp.Message);
                }
                
            }
        }
    }
}