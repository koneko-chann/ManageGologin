using ManageGologin.ManagePhysicalPath;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Text;
using System.Windows.Forms;

namespace ManageGologin
{
    public partial class Form1 : Form
    {
        private readonly IProfileManager _profileManager;
        List<IWebDriver> webDrivers = new List<IWebDriver>();
        public Form1(IProfileManager profileManager)
        {
            InitializeComponent();
            _profileManager = profileManager;
        }

        private async void PathInputBtn_Click(object sender, EventArgs e)
        {
            var tasks = new List<Task<IWebDriver>>();

            foreach (var profileName in _profileManager.GetProfiles().Select(x => x.ProfileName))
            {
                tasks.Add(Task.Run(() => _profileManager.OpenProfile(profileName)));
                Thread.Sleep(3000);
            }

            var drivers = await Task.WhenAll(tasks);

            foreach (var driver in drivers)
            {
                webDrivers.Add(driver);
            }

            MessageBox.Show("All profiles opened successfully!");

        }

        private void ExitProgramBtn_Click(object sender, EventArgs e)
        {
            _profileManager.CloseProfile(webDrivers[0] as ChromeDriver);
        }

        private void btnJsFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.Description = "Select a folder containing text files";

            // Hiển thị hộp thoại chọn thư mục và kiểm tra nếu người dùng chọn thư mục
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                // Lấy đường dẫn thư mục
                string folderPath = folderBrowserDialog.SelectedPath;

                // Lấy danh sách các file trong thư mục
                string[] files = Directory.GetFiles(folderPath, "*.txt");

                // Kiểm tra nếu thư mục có chứa file
                if (files.Length == 0)
                {
                    MessageBox.Show("No text files found in the selected folder.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Đường dẫn thư mục "JS" của hệ thống
                string jsFolderPath = Path.Combine(Environment.CurrentDirectory, "JS");

                // Tạo thư mục "JS" nếu chưa tồn tại
                if (!Directory.Exists(jsFolderPath))
                {
                    Directory.CreateDirectory(jsFolderPath);
                }

                // Clear thư mục "JS" trước khi sao chép các file mới
                ClearFolder(jsFolderPath);

                // Sao chép từng file sang thư mục "JS" và đổi phần mở rộng thành .text
                foreach (string file in files)
                {
                    string fileName = Path.GetFileNameWithoutExtension(file);
                    string newFilePath = Path.Combine(jsFolderPath, fileName + ".txt");

                    // Đọc nội dung file gốc
                    string fileContent = File.ReadAllText(file);

                    // Ghi nội dung vào file mới
                    File.WriteAllText(newFilePath, fileContent);
                }

                MessageBox.Show("Files have been successfully copied and renamed.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Web files (*.html;*.htm)|*.html;*.htm|All files (*.*)|*.*";
            openFileDialog.Title = "Select a web file";

            // Hiển thị hộp thoại chọn file và kiểm tra nếu người dùng chọn file
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Lấy đường dẫn file
                string filePath = openFileDialog.FileName;

                // Đường dẫn tới file navigationwebs.txt
                string outputFilePath = Path.Combine(Environment.CurrentDirectory, "navigationwebs.txt");

                // Clear nội dung file navigationwebs.txt nếu tồn tại
                if (File.Exists(outputFilePath))
                {
                    File.WriteAllText(outputFilePath, string.Empty);
                }

                // Đọc nội dung file đã chọn
                string fileContent = File.ReadAllText(filePath);

                // Ghi nội dung vào file navigationwebs.txt
                File.WriteAllText(outputFilePath, fileContent, Encoding.UTF8);

                MessageBox.Show("File content has been successfully copied to navigationwebs.txt.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void ClearFolder(string folderPath)
        {
            // Xóa tất cả các file trong thư mục
            foreach (string file in Directory.GetFiles(folderPath))
            {
                File.Delete(file);
            }

            // Xóa tất cả các thư mục con trong thư mục
            foreach (string subFolder in Directory.GetDirectories(folderPath))
            {
                Directory.Delete(subFolder, true);
            }
        }
    }
}
