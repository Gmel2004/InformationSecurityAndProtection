using System;
using System.IO;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private DES _des;
        private string _filePath;

        public Form1()
        {
            InitializeComponent();
            _des = new DES(); // Инициализация DES с автоматической генерацией ключа
        }

        // Кнопка "Прикрепить файл"
        private void btnAttachFile_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Все файлы (*.*)|*.*";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    _filePath = openFileDialog.FileName;
                    lblFilePath.Text = _filePath;
                }
            }
        }

        // Кнопка "Зашифровать"
        private void btnEncrypt_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_filePath))
            {
                MessageBox.Show("Файл не выбран.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                // Чтение данных из файла
                byte[] fileData = File.ReadAllBytes(_filePath);

                // Шифрование данных
                byte[] encryptedData = _des.Encrypt(fileData);

                // Отображение зашифрованных данных в TextBox
                txtEncryptedText.Text = BitConverter.ToString(encryptedData).Replace("-", "");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при шифровании: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Кнопка "Расшифровать"
        private void btnDecrypt_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_filePath))
            {
                MessageBox.Show("Файл не выбран.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrEmpty(txtEncryptedText.Text))
            {
                MessageBox.Show("Нет данных для расшифрования.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                // Преобразование текста из TextBox в байты
                string encryptedText = txtEncryptedText.Text;
                byte[] encryptedData = StringToByteArray(encryptedText);

                // Расшифрование данных
                byte[] decryptedData = _des.Decrypt(encryptedData);

                // Сохранение расшифрованных данных в файл
                File.WriteAllBytes(_filePath, decryptedData);

                MessageBox.Show("Файл успешно расшифрован и перезаписан.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при расшифровании: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Вспомогательный метод для преобразования строки в массив байт
        private byte[] StringToByteArray(string hex)
        {
            int length = hex.Length;
            byte[] bytes = new byte[length / 2];
            for (int i = 0; i < length; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            }
            return bytes;
        }
    }
}
