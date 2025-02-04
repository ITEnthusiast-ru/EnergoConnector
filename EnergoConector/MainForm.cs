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
        private string _filePath = "list_ble.csv"; // Укажите путь к вашему CSV-файлу
        public Main()
        {
            InitializeComponent();
            comboBox1.KeyPress += (sender, e) => e.Handled = true;
            inputMacField.TextChanged += TextBoxSearch_TextChanged; // Событие при изменении текста
            inputMacField.KeyDown += TextBoxSearch_KeyDown; // Событие при нажатии клавиш
            listBoxResults.Visible = false; // Скрываем ListBox по умолчанию
        }


        private void TextBoxSearch_TextChanged(object sender, EventArgs e)
        {
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

        private void TextBoxSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // Если нажат Enter и есть выбранный элемент в ListBox
                if (listBoxResults.SelectedIndex != -1)
                {
                    inputMacField.Text = listBoxResults.SelectedItem.ToString();
                    listBoxResults.Visible = false; // Скрываем ListBox после выбора
                }
            }
            else if (e.KeyCode == Keys.Down)
            {
                // Перемещаем фокус на ListBox и выбираем первый элемент
                if (listBoxResults.Items.Count > 0)
                {
                    listBoxResults.Focus();
                    listBoxResults.SelectedIndex = 0;
                }
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
                        if (line.StartsWith(digits))
                        {
                            matches.Add(line);
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

        private void MainForm_Click(object sender, EventArgs e)
        {
            listBoxResults.Visible = false;
        }
    


        private async Task<string> SendSetAsync(SerialPort sPort, byte[] bytes)
        {
            richTextBox1.AppendText($" >>  {Encoding.ASCII.GetString(bytes)}  \r\n");
            sPort.Write(bytes, 0, bytes.Length);
            await Task.Delay(200); // Асинхронная задержка
            string source = sPort.ReadExisting();
            richTextBox1.AppendText($" <<   source  \t  {string.Concat(source.Select(x => ((int)x).ToString("x")))} \r\n");
            return source;
        }

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
            bytes[bytes.Length - 1] = GetBCC(bytes.Take(bytes.Length - 1).ToArray());
            return bytes;
        }

        public async Task SetBleAsync(string comPort, string mac, string pass)
        {
            using (SerialPort sPort = new SerialPort(comPort, 9600, Parity.Even, 7, StopBits.One))
            {
                sPort.ReadTimeout = 1000;
                sPort.Open();
                await SendSetAsync(sPort, Encoding.ASCII.GetBytes("set_program_mode"));
                await SendSetAsync(sPort, Encoding.ASCII.GetBytes("/?!"));
                await SendSetAsync(sPort, Encoding.ASCII.GetBytes("\u0006051"));
                await SendSetAsync(sPort, W1_IEC("IDPAS(" + mac + ")"));
                await SendSetAsync(sPort, W1_IEC("CONPS(" + pass + ")"));
                await SendSetAsync(sPort, W1_IEC("APPLY()"));
                await Task.Delay(40);
            }
        }
        private BLE_String FindPairMacPass(string findiMac)
        {
            try
            {
                List<BLE_String> bleRowList = new List<BLE_String>();
                using (StreamReader streamReader = new StreamReader("list_ble.csv"))
                {
                    while (!streamReader.EndOfStream)
                    {
                        BLE_String bleString = new BLE_String();
                        string[] strArray = streamReader.ReadLine().Split(';');
                        bleString.pass = strArray[0];
                        bleString.pass = strArray[1];
                        bleRowList.Add(bleString);
                    }
                }
                BLE_String pairMacPass = bleRowList.Find((Predicate<BLE_String>)(x => x.pass == findiMac));
                if (pairMacPass == null)
                {
                    InfoLabel.Text =string.Empty;
                    InfoLabel.Text += "Нет такого мака в списке\r\n";
                }
                else
                {

                    InfoLabel.Text = string.Empty;
                    InfoLabel.Text = $"Найдена связка: {pairMacPass.pass} : {pairMacPass.pass}  \r\n";
                }
                return pairMacPass;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }



        private void FindBtnClick(object sender, EventArgs e)
        {
            BLE_String pairMacPass = FindPairMacPass(inputMacField.Text);
            if (pairMacPass == null)
                return;
            inputPassField.Text = pairMacPass.pass;
        }

        private async void SendBtnClick(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(comboBox1.Text))
            {
                InfoLabel.Text = "Не выбран COM порт\r\n";
                return;
            }

            try
            {
                await SetBleAsync(comboBox1.Text, inputMacField.Text, inputPassField.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }
    }
}