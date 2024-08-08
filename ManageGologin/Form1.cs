using ManageGologin.Helper;
using ManageGologin.ManagePhysicalPath;
using ManageGologin.Models;
using ManageGologin.Pagination;
using ManageGologin.Services;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Collections.Concurrent;
using System.Text;
using System.Windows.Forms;

namespace ManageGologin
{
    public partial class Form1 : Form
    {
        private readonly IProfileManager _profileManager;
        List<IWebDriver> webDrivers = new List<IWebDriver>();
        private readonly GeolocationService _geolocationService;
        public Form1(IProfileManager profileManager, GeolocationService geolocationService)
        {
            InitializeComponent();
            _profileManager = profileManager;
            _geolocationService = geolocationService;   
        }

        private async void PathInputBtn_Click(object sender, EventArgs e)
        {
            var tasks = new List<Task<IWebDriver>>();

            foreach (var profile in ((ProfileManager)_profileManager).Profiles)
            {
                tasks.Add(Task.Run(() => _profileManager.OpenProfile(profile)));
                await Task.Delay(3000); // Sử dụng Task.Delay để không chặn luồng hiện tại
            }

            try
            {
                var drivers = await Task.WhenAll(tasks);

                foreach (var driver in drivers)
                {
                    webDrivers.Add(driver);
                }

                MessageBox.Show("All profiles opened successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
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

        private void useScriptCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            btnJsFolder.Enabled = useScriptCheckbox.Checked;
            btnJsFolder.Visible = useScriptCheckbox.Checked;
            button1.Enabled = useScriptCheckbox.Checked;
            button1.Visible = useScriptCheckbox.Checked;
        }

        private void GologinProfiles_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            profilesBindingSource.DataSource = _profileManager.GetProfiles();
        }

        private void proxyInsertBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text files (*.txt)|*.txt";
            openFileDialog.Title = "Select a text file containing proxies";
            openFileDialog.CheckFileExists = true;
            openFileDialog.CheckPathExists = true;
            // Hiển thị hộp thoại chọn file và kiểm tra nếu người dùng chọn file
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Lấy đường dẫn file
                string filePath = openFileDialog.FileName;

                // Đường dẫn tới file proxy.txt
                string outputFilePath = Path.Combine(Environment.CurrentDirectory, Resources.ProxyFile);

                // Clear nội dung file proxy.txt nếu tồn tại
                if (File.Exists(outputFilePath))
                {
                    File.WriteAllText(outputFilePath, string.Empty);
                }

                // Đọc nội dung file đã chọn
                string fileContent = File.ReadAllText(filePath);

                //clear file proxy
                //    File.WriteAllText(outputFilePath, string.Empty);

                //đọc từng dòng proxy và check
                string[] lines = File.ReadAllLines(filePath);
                foreach (string line in lines)
                {
                    if (!CustomProxy.CheckFormat(line))
                    {
                        throw new ArgumentException("Proxy must be in the format {address}:{port}:{username}:{password}");
                    }
                }


                // Ghi nội dung vào file proxy.txt
                File.WriteAllText(outputFilePath, fileContent, Encoding.UTF8);

                MessageBox.Show("File content has been successfully copied to proxy.txt.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
        private async Task CheckProxyAsync(Profiles profile)
        {
            if (!await profile.Proxy.IsProxyAliveAsync())
            {
                profile.Proxy.ProxyStatus = "Dead";
            }
            else
            {
                profile.Proxy.ProxyStatus = "Alive";
            }
        }

        private async void checkProxyBtn_Click(object sender, EventArgs e)
        {
            var profilesList = profilesBindingSource.DataSource as List<Profiles>;

            if (profilesList != null)
            {
                // Cập nhật tất cả trạng thái thành "Checking"
                foreach (var profile in profilesList)
                {
                    profile.Proxy.ProxyStatus = "Checking";
                }
                 profilesBindingSource.ResetBindings(false); // Cập nhật DataGridView
                var tasks = new ConcurrentBag<Task>();

                // Chạy tác vụ kiểm tra proxy song song
                await Task.Run(() =>
                {
                    Parallel.ForEach(profilesList, profile =>
                    {
                        tasks.Add(CheckProxyAsync(profile));
                    });
                });

                await Task.WhenAll(tasks);

                // Thông báo cho BindingSource về thay đổi
                profilesBindingSource.ResetBindings(false);
            }
        }

        private void GologinProfiles_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (GologinProfiles.Columns[e.ColumnIndex].Name == "Status")
            {
                string status = e.Value as string;
                if (status == "Alive")
                {
                    e.CellStyle.BackColor = Color.Green;
                    e.CellStyle.ForeColor = Color.White;
                }
                else if (status == "Dead")
                {
                    e.CellStyle.BackColor = Color.Red;
                    e.CellStyle.ForeColor = Color.White;
                }
                else if (status == "Checking")
                {
                    e.CellStyle.BackColor = Color.Yellow;
                    e.CellStyle.ForeColor = Color.Black;
                }
            }
        }

        private void selectAllBtn_CheckedChanged(object sender, EventArgs e)
        {
            foreach (var row in GologinProfiles.Rows)
            {
                DataGridViewRow r = row as DataGridViewRow;
                r.Cells[0].Value = selectAllBtn.Checked;
            }
        }

        private void itemsPerPageCbx_SelectedIndexChanged(object sender, EventArgs e)
        {
            profilesBindingSource.ResetBindings(false);
            GologinProfiles.Refresh();
        }
    }
}
