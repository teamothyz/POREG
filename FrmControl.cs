using Polly;
using POREG.Models;
using POREG.OtherServices;
using System.Data;
using System.Diagnostics;

namespace POREG
{
    public partial class FrmControl : Form
    {
        private CancellationTokenSource WebDriverCancel;
        private CancellationTokenSource WorkerCancel;
        private int CurrentIndex;
        private List<POInfor> RegData;
        private Task[] Workers;
        private TinsoftKey Keys;
        private string _SimCodeKey;
        private string _2CaptchaKey;

        public FrmControl()
        {
            InitializeComponent();
            StopButton.Enabled = false;
            DataView.AutoGenerateColumns = false;
            API2CaptchaInput.Text = _2CaptchaService.GetKey();
            APISimCodeInput.Text = SimCodeService.GetKey();
        }

        private void LoadTable()
        {
            while (true)
            {              
                if (DataView.InvokeRequired)
                {
                    Invoke(() =>
                    {
                        DataView.DataSource = null;
                        DataView.DataSource = RegData;

                        ReadyAccountsInput.Text = RegData.Where(item => item.Status.Equals("NotStart") || item.Status.Equals("Running"))
                            .ToList().Count.ToString();
                        SuccessAccountsInput.Text = RegData.Where(item => item.Status.Equals("Success"))
                            .ToList().Count.ToString();
                        FailedAccountsInput.Text = RegData.Where(item => item.Status.Equals("Failed") || item.Status.Equals("Canceled"))
                            .ToList().Count.ToString();
                    });
                }
                else
                {
                    DataView.DataSource = null;
                    DataView.DataSource = RegData;

                    ReadyAccountsInput.Text = RegData.Where(item => item.Status.Equals("NotStart") || item.Status.Equals("Running"))
                            .ToList().Count.ToString();
                    SuccessAccountsInput.Text = RegData.Where(item => item.Status.Equals("Success"))
                        .ToList().Count.ToString();
                    FailedAccountsInput.Text = RegData.Where(item => item.Status.Equals("Failed") || item.Status.Equals("Canceled"))
                        .ToList().Count.ToString();
                }

                var isAutoFinished = WorkerCancel == null || WorkerCancel.Token.IsCancellationRequested;
                if (isAutoFinished)
                {
                    return;
                }
                Thread.Sleep(1000);
            }
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            if (int.Parse(ProxiesInput.Text) == 0)
            {
                MessageBox.Show("Error: Please Input Proxies!");
                return;
            }
            if (int.Parse(ProxiesInput.Text) < (int)ThreadsNumber.Value)
            {
                MessageBox.Show("Warning: Threads Number is higher than Proxies!");
            }
            if (API2CaptchaInput.Text.Trim().Equals(""))
            {
                MessageBox.Show("Error: Please Input 2Captcha Key!");
                return;
            }
            if (APISimCodeInput.Text.Trim().Equals(""))
            {
                MessageBox.Show("Error: Please Input Chothuesimcode Key!");
                return;
            }

            _SimCodeKey = APISimCodeInput.Text.Trim();
            SimCodeService.SaveKey(_SimCodeKey);

            _2CaptchaKey = API2CaptchaInput.Text.Trim();
            _2CaptchaService.SaveKey(_2CaptchaKey);
            _2CaptchaService.ReWriteConfigFile(_2CaptchaKey);

            StartButton.Enabled = false;
            APISimCodeInput.Enabled = false;
            API2CaptchaInput.Enabled = false;

            WebDriverCancel = new();
            WorkerCancel = new();

            Workers = new Task[(int)ThreadsNumber.Value];
            new Task(Run).Start();
            new Task(LoadTable).Start();
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            StopButton.Enabled = false;

            WorkerCancel.Cancel();
            WebDriverCancel.Cancel();
        }

        #region Woker
        private void Run()
        {
            ButtonsSetting(true);
            var tokenWorker = WorkerCancel.Token;
            var tokenWebDriver = WebDriverCancel.Token;
            try
            {
                while (CurrentIndex < RegData.Count)
                {
                    if (tokenWorker.IsCancellationRequested)
                    {
                        tokenWorker.ThrowIfCancellationRequested();
                    }

                    var index = ScanTaskWoker();
                    if (index == -1)
                    {
                        break;
                    }

                    var key = Keys.GetKey();
                    var info = RegData[CurrentIndex++];
                    info.Status = "Running";

                    var webDriver = new WebDriverWorker(ref info, index, key, tokenWebDriver);
                    var task = new Task(() => webDriver.Run(ref info, _SimCodeKey, tokenWebDriver));
                    Workers[index] = task;
                    Workers[index].Start();
                }
            }
            catch (OperationCanceledException)
            {
                MessageBox.Show("Warning: Stopped All Process!");
            }
            catch (Exception)
            {
                MessageBox.Show("Error: Error When Register!");
            }
            finally
            {
                var workersWait = Workers.Where(worker => worker != null && !worker.IsCompleted).ToArray();
                if (workersWait.Any())
                {
                    Task.WaitAll(workersWait);
                }

                foreach (var process in Process.GetProcessesByName("chromedriver"))
                {
                    process.Kill();
                }
                ButtonsSetting(false);
                LoadTable();
                MessageBox.Show("Info: Register Has Finished!");
            }
        }

        private int ScanTaskWoker()
        {
            var index = -1;
            var tokenWorker = WorkerCancel.Token;
            while (CurrentIndex < RegData.Count)
            {
                for (int i = 0; i < Workers.Length; i++)
                {
                    tokenWorker.ThrowIfCancellationRequested();
                    var isWorkerDone = Workers[i] == null || Workers[i].IsCompleted;
                    if (isWorkerDone)
                    {
                        return i;
                    }
                }
            }
            return index;
        }
        #endregion

        #region Init Data
        private void ButtonsSetting(bool isRunning)
        {
            if (StartButton.InvokeRequired)
            {
                Invoke(() =>
                {
                    StartButton.Enabled = !isRunning;
                    StopButton.Enabled = isRunning;

                    LoadDataButton.Enabled = !isRunning;
                    LoadProxyButton.Enabled = !isRunning;

                    API2CaptchaInput.Enabled = !isRunning;
                    APISimCodeInput.Enabled = !isRunning;
                    ThreadsNumber.Enabled = !isRunning;
                });
            }
            else
            {
                StartButton.Enabled = !isRunning;
                StopButton.Enabled = isRunning;

                LoadDataButton.Enabled = !isRunning;
                LoadProxyButton.Enabled = !isRunning;

                API2CaptchaInput.Enabled = !isRunning;
                APISimCodeInput.Enabled = !isRunning;
                ThreadsNumber.Enabled = !isRunning;
            }
        }

        private void LoadDataButton_Click(object sender, EventArgs e)
        {
            try
            {
                var openFileDialog = new OpenFileDialog
                {
                    Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*",
                    FilterIndex = 1,
                    Multiselect = false,
                    RestoreDirectory = true
                };

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    RegData = new();
                    CurrentIndex = 0;

                    using var fileStream = openFileDialog.OpenFile();
                    using var reader = new StreamReader(fileStream);
                    while (reader.Peek() != -1)
                    {
                        DataHandler(reader.ReadLine());
                    }

                    LoadTable();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error: Reading Data File Error!");
            }
        }

        private void DataHandler(string line)
        {
            try
            {
                if (line == null || line.Trim().Equals(""))
                    return;

                var infor = line.Trim().Split("|");
                if (infor.Length != 7)
                {
                    throw new Exception("Wrong Data Format!");
                }
                var singleData = new POInfor
                {
                    Email = infor[0],
                    Password = infor[1],
                    Name = infor[2],
                    Birthday = infor[3],
                    IDCard = infor[4],
                    Address = infor[5],
                    ZipCode = infor[6],
                    ID = RegData.Count + 1
                };
                RegData.Add(singleData);
            }
            catch (Exception)
            {
                ExportDataService.ExportErrorData(line);
            }
        }

        private void LoadProxyButton_Click(object sender, EventArgs e)
        {
            try
            {
                var openFileDialog = new OpenFileDialog
                {
                    Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*",
                    Multiselect = false,
                    FilterIndex = 1,
                    RestoreDirectory = true
                };

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    Keys = new TinsoftKey(openFileDialog.FileName);
                    ProxiesInput.Text = Keys.KeyCount.ToString();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error: Reading Proxies File Error!");
            }
        }
        #endregion

        private void ShowProxiesButton_Click(object sender, EventArgs e)
        {
            if (Keys == null)
            {
                MessageBox.Show("Empty Proxies List");
            }
            else
            {
                MessageBox.Show(Keys.ShowKey());
            }
        }
    }
}
