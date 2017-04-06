using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace client_server_app
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void Write(string mess)
        {
            listBox2.Items.Add((listBox2.Items.Count + 1) + ". " + mess);
            listBox2.SelectedIndex = listBox2.Items.Count - 1;
            //listBox2.SelectedIndex = -1;
        }
        private MySqlConnection GetConnection()
        {
            string connString = "server=" + textBox1.Text + ";port=" + textBox2.Text + ";database=" + textBox3.Text + ";user=" + textBox4.Text + ";password=" + textBox5.Text + ";";
            Write("Строка подключения: " + connString);
            MySqlConnection connection = new MySqlConnection(connString);

            try
            {
                Write("Создание подключения к " + textBox1.Text + ":" + textBox2.Text + "...");
                connection.Open();
                Write("Подключение успешно создано!");
            }
            catch (Exception ex)
            {
                Write("Не удалось создать подключение!");
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
            return connection;
        }

        private void SqlRefresh()
        {
            listBox1.Items.Clear();
            List<GPU> gpuList = GetGpuList();
            for (int i = 0; i < gpuList.Count(); i++)
            {
                listBox1.Items.Add("" + (listBox1.Items.Count + 1) + ". ID: " + gpuList[i].Id + ", Model: " + gpuList[i].Model + ". Perofrmace: " + gpuList[i].Performance + ". Price: " + gpuList[i].Price);
            }
        }

        private List<GPU> GetGpuList()
        {
            List<GPU> gpuList = new List<GPU>();
            MySqlConnection connection = GetConnection();
            MySqlCommand command = new MySqlCommand("SELECT * FROM gpu", connection);
            MySqlDataReader reader = null;
            try
            {
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    gpuList.Add(new GPU(Convert.ToInt32(reader["Id"]), Convert.ToString(reader["Model"]), Convert.ToInt32(reader["Performance"]), Convert.ToInt32(reader["Price"])));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
            return gpuList;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GetConnection();
            //SqlRefresh();
        }


        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        private void обновитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SqlRefresh();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MySqlConnection connection = GetConnection();
            string commandString = "INSERT INTO gpu (model, performance, price) VALUES ('" + textBox6.Text + "', '" + textBox7.Text + "', '" + textBox8.Text + "');";
            Write("Командная строка: " + commandString);
            MySqlCommand command = new MySqlCommand(commandString, connection);
            try
            {
                command.ExecuteNonQuery();
                Write("Кажется получилось!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Write("Ошибочка!");
            }
            SqlRefresh();
        }

        private void очиститьКонсольToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listBox2.Items.Clear();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void textBox13_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            string commandString = "DELETE FROM gpu WHERE id = '" + textBox13.Text + "'";
            MySqlConnection connection = GetConnection();
            MySqlCommand command = new MySqlCommand(commandString, connection);
            command.ExecuteNonQuery();
            SqlRefresh();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string commandString = "UPDATE gpu SET model='" + textBox9.Text + "', performance='" + textBox10.Text + "', price='" + textBox11.Text + "' WHERE id = '" + textBox12.Text + "'";
            MySqlConnection connection = GetConnection();
            MySqlCommand command = new MySqlCommand(commandString, connection);
            try
            {
                command.ExecuteNonQuery();
                SqlRefresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

    }
}
