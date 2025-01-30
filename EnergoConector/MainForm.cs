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
        public Main()
        {
            InitializeComponent();
            comboBox1.KeyPress += (sender, e) => e.Handled = true;
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
                    InfoLabel.Text = "";
                    InfoLabel.Text += "Нет такого мака в списке\r\n";
                }
                else
                {

                    InfoLabel.Text = "";
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