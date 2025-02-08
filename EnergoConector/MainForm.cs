using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace EnergoConector
{

    public partial class Main : Form
    {
        private string _filePath = FileHandler.DefaultFilePath; // Путь к CSV-файл
        public Main()
        {
            InitializeComponent();
            ComPortChoice.KeyPress += (sender, e) => e.Handled = true;
            listBoxResults.Visible = false; // Скрываем ListBox по умолчанию
        }

        private void DefaultFilePathBtn_Click(object sender, EventArgs e)
        {
            _filePath = FileHandler.DefaultFilePath;
            labelFilePath.Text = _filePath; // Отображаем путь к файлу
        }

        private void ButtonBrowse_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                inputMacField.Clear();
                listBoxResults.Items.Clear();
                listBoxResults.Visible = false;
                openFileDialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
                openFileDialog.Title = "Выберите CSV-файл";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    _filePath = openFileDialog.FileName;
                    labelFilePath.Text = _filePath; // Отображаем путь к файлу
                }
            }
        }

        private void TextBoxSearch_TextChanged(object sender, EventArgs e)
        {
            if (!File.Exists(_filePath))
            {
                MessageBox.Show("Файл не найден", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string searchDigits = inputMacField.Text.Trim();

            if (string.IsNullOrEmpty(searchDigits))
            {
                listBoxResults.Visible = false; // Скрываем ListBox, если поле пустое
                return;
            }

            List<string> results = SearchByDigits(searchDigits);

            listBoxResults.Items.Clear();

            if (results.Count > 0)
            {
                foreach (var result in results)
                {
                    listBoxResults.Items.Add(result);
                }
                listBoxResults.Visible = true; // Показываем ListBox с результатами
                listBoxResults.Top = inputMacField.Bottom; // Позиционируем ListBox под TextBox
                listBoxResults.Left = inputMacField.Left;
                listBoxResults.Width = inputMacField.Width;
            }
            else
            {
                listBoxResults.Visible = false; // Скрываем ListBox, если результатов нет
            }
        }

        

        private void ListBoxResults_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // Если нажат Enter в ListBox
                if (listBoxResults.SelectedIndex != -1)
                {
                    inputMacField.Text = listBoxResults.SelectedItem.ToString();
                    listBoxResults.Visible = false; // Скрываем ListBox после выбора
                }
            }
            else if (e.KeyCode == Keys.Up && listBoxResults.SelectedIndex == 0)
            {
                // Если достигнут верхний элемент, возвращаем фокус в TextBox
                inputMacField.Focus();
            }
        }

        private List<string> SearchByDigits(string digits)
        {
            List<string> matches = new List<string>();

            try
            {
                using (StreamReader reader = new StreamReader(_filePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] parts = line.Split(';');
                        if (parts.Length > 0 && parts[0].StartsWith(digits))
                        {
                            string firstNineDigits = parts[0].Length >= 9 ? parts[0].Substring(0, 9) : parts[0];
                            matches.Add(firstNineDigits);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при чтении файла: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return matches;
        }

        private void TextBoxSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (listBoxResults.SelectedIndex != -1)
                {
                    string selectedItem = listBoxResults.SelectedItem.ToString();
                    string fullLine = FindFullLine(selectedItem);
                    inputMacField.Text = fullLine;
                    listBoxResults.Visible = false;
                }
            }
            else if (e.KeyCode == Keys.Down)
            {
                if (listBoxResults.Items.Count > 0)
                {
                    listBoxResults.Focus();
                    listBoxResults.SelectedIndex = 0;
                }
            }
        }

        private string FindFullLine(string searchTerm)
        {
            try
            {
                using (StreamReader reader = new StreamReader(_filePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (line.StartsWith(searchTerm))
                        {
                            return line;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при чтении файла: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return string.Empty;
        }
        private void MainForm_Click(object sender, EventArgs e)
        {
            listBoxResults.Visible = false;
        }



        private string send_set(SerialPort s_port, byte[] bytes)
        {
            RichTextBox richTextBox1_1 = this.richTextBox1;
            richTextBox1_1.Text = richTextBox1_1.Text + $" >>  {Encoding.ASCII.GetString(bytes)}  \r\n";
            s_port.Write(bytes, 0, bytes.Length);
            Thread.Sleep(1000);
            string source = s_port.ReadExisting();
            RichTextBox richTextBox1_2 = richTextBox1;
            richTextBox1_2.Text = richTextBox1_2.Text + $" <<   source  \t  {string.Concat(source.Select<char, string>((Func<char, string>)(x => ((int)x).ToString(nameof(x)))))} \r\n";
            return source;
        }


        // Входящий поток
        public static byte GetBCC(byte[] inputStream)
        {
            byte maxValue = byte.MaxValue;
            if (inputStream != null && inputStream.Length != 0)
            {
                for (int index = 0; index < inputStream.Length; ++index)
                    maxValue += inputStream[index];
            }
            return maxValue;
        }


        public byte[] W1_IEC(string command)
        {
            byte[] bytes = Encoding.ASCII.GetBytes("\u0001W1\u0002" + command + "\u0003\0");
            bytes[bytes.Length - 1] = Main.GetBCC(((IEnumerable<byte>)bytes).Take<byte>(bytes.Length - 1).ToArray<byte>());
            return bytes;
        }

        public void SET_BLE(string com_port, string mac, string pass)
        {
            SerialPort s_port = new SerialPort();
            s_port.PortName = com_port;
            s_port.BaudRate = 9600;
            s_port.Parity = Parity.Even;
            s_port.DataBits = 7;
            s_port.StopBits = StopBits.One;
            s_port.ReadTimeout = 1000;
            s_port.Open();
            Thread.Sleep(40);
            send_set(s_port, Encoding.ASCII.GetBytes("set_program_mode"));
            send_set(s_port, Encoding.ASCII.GetBytes("/?!"));
            send_set(s_port, Encoding.ASCII.GetBytes("\u0006051"));
            send_set(s_port, W1_IEC("IDPAS(" + mac + ")"));
            send_set(s_port, W1_IEC("CONPS(" + pass + ")"));
            send_set(s_port, W1_IEC("APPLY()"));
            Thread.Sleep(40);
            s_port.Close();
        }

        public BLE_String FindPairMacPass(string finding_mac)
        {
            try
            {
                List<BLE_String> bleRowList = new List<BLE_String>();
                using (StreamReader streamReader = new StreamReader(_filePath))
                {
                    while (!streamReader.EndOfStream)
                    {
                        BLE_String bleRow = new BLE_String();
                        string[] strArray = streamReader.ReadLine().Split(';');
                        bleRow.Mac = strArray[0];
                        bleRow.Pass = strArray[1];
                        bleRowList.Add(bleRow);
                    }
                }
                BLE_String pairMacPass = bleRowList.Find((Predicate<BLE_String>)(x => x.Mac == finding_mac));
                if (pairMacPass == null)
                {
                    InfoLabel.Text = string.Empty;
                    InfoLabel.Text += "Нет такого мака в списке\r\n";
                }
                else
                {

                    InfoLabel.Text = string.Empty;
                    InfoLabel.Text = $"Найдена связка: {pairMacPass.Mac} : {pairMacPass.Pass}  \r\n";
                }
                return pairMacPass;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }



        private void FindBtn_Click(object sender, EventArgs e)
        {
            BLE_String pairMacPass = FindPairMacPass(inputMacField.Text);
            if (pairMacPass == null)
                return;
            inputPassField.Text = pairMacPass.Pass;
        }



        private void AuthBtn_Click(object sender, EventArgs e)
        {
            // Здесь буде код для авторизации с прибором учета

            MessageBox.Show("Функционал кнопки пока  в разработке");
        }

        private void Main_Load(object sender, EventArgs e)
        {

        }

        private void InputMacField_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8) // цифры и клавиша BackSpace
            {
                e.Handled = true;
            }
        }


        private void InputPassField_KeyPress(object sender, KeyPressEventArgs e)
        {


            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8) // цифры и клавиша BackSpace
            {
                e.Handled = true;
            }



        }

        private void InputPassField_KeyUp(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                SendBtn.Focus();

            }

        }

        private void SendBtn_Click(object sender, EventArgs e)
        {
            InfoLabel.Text = string.Empty;
            KeyUps();
        }

        private void SendBtn_KeyUp(object sender, KeyEventArgs e)
        {
            InfoLabel.Text = string.Empty;
            KeyUps();
        }

        private void KeyUps()
        {
            if (ComPortChoice.Text != string.Empty)
                SET_BLE(ComPortChoice.Text, inputMacField.Text, inputPassField.Text);
            else
            {
                InfoLabel.Text = string.Empty;
                InfoLabel.Text += "Не выбран COM порт\r\n";
            }
        }

        private void ComPortChose_DropDown(object sender, EventArgs e)
        {
            ComPortChoice.Items.Clear();
            foreach (object portName in SerialPort.GetPortNames())
                ComPortChoice.Items.Add(portName);
        }

        private void inputMacField_KeyUp(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                inputPassField.Focus();

            }

        }

        private void listBoxResults_Click(object sender, EventArgs e)
        {
            if (listBoxResults.SelectedIndex != -1)
            {
                inputMacField.Text = listBoxResults.SelectedItem.ToString();
                listBoxResults.Visible = false; // Скрываем ListBox после выбора
            }

            else return;
        }
    
    }
}