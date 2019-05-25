using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        SqlConnection sqlConnection;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string conn = @"Data Source=.\SQLEXPRESS;AttachDbFilename=C:\Users\dan12\Desktop\WindowsFormsApp1\WindowsFormsApp1\Database1.mdf;Integrated Security=True;User Instance=True";
            sqlConnection = new SqlConnection(conn);
            sqlConnection.Open();
            SqlDataReader sqlReader = null;
            SqlCommand command = new SqlCommand("SELECT * FROM [Students]", sqlConnection);

            try
            {
                sqlReader = command.ExecuteReader();
                while (sqlReader.Read())
                {
                    listBox3.Items.Add(Convert.ToString(sqlReader["Id"]) + "  " + Convert.ToString(sqlReader["Name"]) + "  " + Convert.ToString(sqlReader["SName"]) + "  " + Convert.ToString(sqlReader["Class"]) + "  " + Convert.ToString(sqlReader["Normative"]) + "  " + Convert.ToString(sqlReader["Mark"]));
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (sqlReader != null) sqlReader.Close();
            }
        }

        private void ВыходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
            {
                sqlConnection.Close();
            }
                 Close();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
                sqlConnection.Close();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text) && !string.IsNullOrWhiteSpace(textBox1.Text) &&
               !string.IsNullOrEmpty(textBox2.Text) && !string.IsNullOrWhiteSpace(textBox2.Text) &&
               !string.IsNullOrEmpty(textBox3.Text) && !string.IsNullOrWhiteSpace(textBox3.Text) &&
               !string.IsNullOrEmpty(textBox4.Text) && !string.IsNullOrWhiteSpace(textBox4.Text) &&
               !string.IsNullOrEmpty(textBox5.Text) && !string.IsNullOrWhiteSpace(textBox5.Text)
                )
            {
                MessageBox.Show("Ученик добавлен!");
                SqlCommand command = new SqlCommand("INSERT INTO [Students] (Name, SName, Class, Normative, Mark) VALUES(@Name, @SName, @Class, @Normative, @Mark)", sqlConnection);

                command.Parameters.AddWithValue("Name", textBox1.Text);
                command.Parameters.AddWithValue("SName", textBox2.Text);
                command.Parameters.AddWithValue("Class", textBox3.Text);
                command.Parameters.AddWithValue("Normative", textBox4.Text);
                command.Parameters.AddWithValue("Mark", textBox5.Text);
                command.ExecuteNonQuery();
                textBox1.Text = ""; textBox2.Text = ""; textBox3.Text = ""; textBox4.Text = ""; textBox5.Text = "";
            }
            else MessageBox.Show("Поля заполнены некоректно!");
        }

        private void ОбновитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listBox3.Items.Clear();

            SqlDataReader sqlReader = null;
            SqlCommand command = new SqlCommand("SELECT * FROM [Students]", sqlConnection);

            try
            {
                sqlReader = command.ExecuteReader();
                while (sqlReader.Read())
                {
                    listBox3.Items.Add(Convert.ToString(sqlReader["Id"]) + "  " + Convert.ToString(sqlReader["Name"]) + "  " + Convert.ToString(sqlReader["SName"]) + "  " + Convert.ToString(sqlReader["Class"]) + "  " + Convert.ToString(sqlReader["Normative"]) + "  " + Convert.ToString(sqlReader["Mark"]));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (sqlReader != null) sqlReader.Close();
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox6.Text) && !string.IsNullOrWhiteSpace(textBox6.Text))
            {
                string objectSearch = listBox1.SelectedItem.ToString();
                string textSearch = textBox6.Text;
                string data = "SELECT * FROM [Students] WHERE " + objectSearch + "=@text";

                listBox2.Items.Clear();

                SqlDataReader sqlReader = null;
                SqlCommand command = new SqlCommand(data, sqlConnection);
                command.Parameters.AddWithValue("text", textSearch);

                try
                {
                    sqlReader = command.ExecuteReader();
                    while (sqlReader.Read())
                    {
                        listBox2.Items.Add(Convert.ToString(sqlReader["Id"]) + "\t" + Convert.ToString(sqlReader["Name"]) + "\t" + Convert.ToString(sqlReader["SName"]) + "\t" + Convert.ToString(sqlReader["Class"]) + "\t" + Convert.ToString(sqlReader["Normative"]) + "\t" + Convert.ToString(sqlReader["Mark"]));
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (sqlReader != null) sqlReader.Close();
                }
                if (listBox2.Items.Count == 0) MessageBox.Show("Таких учеников нет!");
            }
            else MessageBox.Show("Заполните поле поиска!");
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox7.Text) && !string.IsNullOrWhiteSpace(textBox7.Text))
            {
                string data = "UPDATE [Students] SET";
                if (!string.IsNullOrEmpty(textBox8.Text) && !string.IsNullOrWhiteSpace(textBox8.Text)) {
                    data += " [Mark]=@Mark";
                }
                if (!string.IsNullOrEmpty(textBox9.Text) && !string.IsNullOrWhiteSpace(textBox9.Text)) {
                    if (!string.IsNullOrEmpty(textBox8.Text) && !string.IsNullOrWhiteSpace(textBox8.Text)) data += ", [Normative]=@Normative";
                    else data += " [Normative]=@Normative";
                }
                if (!string.IsNullOrEmpty(textBox10.Text) && !string.IsNullOrWhiteSpace(textBox10.Text)) {
                    if((!string.IsNullOrEmpty(textBox8.Text) && !string.IsNullOrWhiteSpace(textBox8.Text)) || (!string.IsNullOrEmpty(textBox9.Text) && !string.IsNullOrWhiteSpace(textBox9.Text))) data += ", [Class]=@Class";
                    else data += " [Class]=@Class";
                }
                if (!string.IsNullOrEmpty(textBox11.Text) && !string.IsNullOrWhiteSpace(textBox11.Text)) {
                    if ((!string.IsNullOrEmpty(textBox8.Text) && !string.IsNullOrWhiteSpace(textBox8.Text)) || (!string.IsNullOrEmpty(textBox9.Text) && !string.IsNullOrWhiteSpace(textBox9.Text)) || (!string.IsNullOrEmpty(textBox10.Text) && !string.IsNullOrWhiteSpace(textBox10.Text))) data += ", [SName]=@SName";
                    else data += " [SName]=@Sname";
                }
                if (!string.IsNullOrEmpty(textBox12.Text) && !string.IsNullOrWhiteSpace(textBox12.Text)) {
                    if ((!string.IsNullOrEmpty(textBox8.Text) && !string.IsNullOrWhiteSpace(textBox8.Text)) || (!string.IsNullOrEmpty(textBox9.Text) && !string.IsNullOrWhiteSpace(textBox9.Text)) || (!string.IsNullOrEmpty(textBox10.Text) && !string.IsNullOrWhiteSpace(textBox10.Text)) || (!string.IsNullOrEmpty(textBox11.Text) && !string.IsNullOrWhiteSpace(textBox11.Text))) data += ", [Name]=@Name";
                    else data += " [Name]=@Name";
                }

                if ((!string.IsNullOrEmpty(textBox8.Text) && !string.IsNullOrWhiteSpace(textBox8.Text)) || (!string.IsNullOrEmpty(textBox9.Text) && !string.IsNullOrWhiteSpace(textBox9.Text)) || (!string.IsNullOrEmpty(textBox10.Text) && !string.IsNullOrWhiteSpace(textBox10.Text)) || (!string.IsNullOrEmpty(textBox11.Text) && !string.IsNullOrWhiteSpace(textBox11.Text)) || (!string.IsNullOrEmpty(textBox12.Text) && !string.IsNullOrWhiteSpace(textBox12.Text)))
                    {
                    data += " WHERE [Id]=@Id";
                    SqlCommand command = new SqlCommand(data, sqlConnection);
                    command.Parameters.AddWithValue("Id", textBox7.Text);
                    command.Parameters.AddWithValue("Name", textBox12.Text);
                    command.Parameters.AddWithValue("SName", textBox11.Text);
                    command.Parameters.AddWithValue("Class", textBox10.Text);
                    command.Parameters.AddWithValue("Normative", textBox9.Text);
                    command.Parameters.AddWithValue("Mark", textBox8.Text);

                    MessageBox.Show("Информация обновлена!");
                    textBox7.Text = ""; textBox8.Text = ""; textBox9.Text = ""; textBox10.Text = ""; textBox11.Text = ""; textBox12.Text = "";
                    command.ExecuteNonQuery();
                }
                else MessageBox.Show("Заполните поля которые хотите изменить!");
            }
            else MessageBox.Show("Поле ID заполнено не корректно!");
        }
    }
}
