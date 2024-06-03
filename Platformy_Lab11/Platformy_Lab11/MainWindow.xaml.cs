using System;
using System.ComponentModel;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using MessageBox = System.Windows.MessageBox;

namespace Platformy_Lab11
{
    public partial class MainWindow : Window
    {
        private BackgroundWorker backgroundWorker;

        public MainWindow()
        {
            InitializeComponent();
            InitializeBackgroundWorker();
        }

        private void InitializeBackgroundWorker()
        {
            backgroundWorker = new BackgroundWorker();
            backgroundWorker.WorkerReportsProgress = true;
            backgroundWorker.DoWork += new DoWorkEventHandler(BackgroundWorker_DoWork);
            backgroundWorker.ProgressChanged += new ProgressChangedEventHandler(BackgroundWorker_ProgressChanged);
            backgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BackgroundWorker_RunWorkerCompleted);
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(InputN.Text, out int n) && n > 0)
            {
                if (!backgroundWorker.IsBusy)
                {
                    ProgressBar.Value = 0;
                    ResultTextBox.Clear();
                    backgroundWorker.RunWorkerAsync(n);
                }
            }
            else
            {
                MessageBox.Show("Please enter a valid positive integer.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            int n = (int)e.Argument;
            long result = CalculateFibonacci(n, sender as BackgroundWorker);
            e.Result = result;
        }

        private long CalculateFibonacci(int n, BackgroundWorker worker)
        {
            if (n <= 1)
                return n;

            long[] fib = new long[n + 1];
            fib[0] = 0;
            fib[1] = 1;

            for (int i = 2; i <= n; i++)
            {
                fib[i] = fib[i - 1] + fib[i - 2];
                Thread.Sleep(5);
                worker.ReportProgress((int)((i / (float)n) * 100));
            }

            return fib[n];
        }

        private void BackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            ProgressBar.Value = e.ProgressPercentage;
        }

        private void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ResultTextBox.Text = e.Result.ToString();
        }

        private long Factorial(int n)
        {
            long result = 1;
            for (int i = 1; i <= n; i++)
            {
                result *= i;
            }
            return result;
        }

        private long NewtonSymbolTask(int n, int k)
        {
            Task<long> numeratorTask = Task.Run(() => Factorial(n));
            Task<long> denominatorTask = Task.Run(() => Factorial(k) * Factorial(n - k));

            Task.WaitAll(numeratorTask, denominatorTask);

            return numeratorTask.Result / denominatorTask.Result;
        }

        private long NewtonSymbolDelegate(int n, int k)
        {
            Func<int, long> factorialDelegate = new Func<int, long>(Factorial);

            IAsyncResult numeratorResult = factorialDelegate.BeginInvoke(n, null, null);
            IAsyncResult denominatorResultK = factorialDelegate.BeginInvoke(k, null, null);
            IAsyncResult denominatorResultNMinusK = factorialDelegate.BeginInvoke(n - k, null, null);

            long numerator = factorialDelegate.EndInvoke(numeratorResult);
            long denominator = factorialDelegate.EndInvoke(denominatorResultK) * factorialDelegate.EndInvoke(denominatorResultNMinusK);

            return numerator / denominator;
        }

        private async Task<long> NewtonSymbolAsync(int n, int k)
        {
            Task<long> numeratorTask = Task.Run(() => Factorial(n));
            Task<long> denominatorKTask = Task.Run(() => Factorial(k));
            Task<long> denominatorNMinusKTask = Task.Run(() => Factorial(n - k));

            await Task.WhenAll(numeratorTask, denominatorKTask, denominatorNMinusKTask);

            long numerator = numeratorTask.Result;
            long denominator = denominatorKTask.Result * denominatorNMinusKTask.Result;

            return numerator / denominator;
        }

        private async void TaskButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(InputN.Text, out int n) && int.TryParse(InputK.Text, out int k))
            {
                if (n >= k && k >= 0)
                {
                    ProgressBar.Value = 50;
                    long result = await Task.Run(() => NewtonSymbolTask(n, k));
                    ProgressBar.Value = 100;

                    ResultTextBox.Text = result.ToString();
                }
                else
                {
                    MessageBox.Show("Please enter valid values: N >= K and K >= 0.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Please enter valid integer values.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DelegateButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(InputN.Text, out int n) && int.TryParse(InputK.Text, out int k))
            {
                if (n >= k && k >= 0)
                {
                    ProgressBar.Value = 50;
                    long result = NewtonSymbolDelegate(n, k);
                    ProgressBar.Value = 100;

                    ResultTextBox.Text = $"Result: {result}";
                }
                else
                {
                    MessageBox.Show("Please enter valid values: N >= K and K >= 0.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Please enter valid integer values.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void AsyncAwaitButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(InputN.Text, out int n) && int.TryParse(InputK.Text, out int k))
            {
                if (n >= k && k >= 0)
                {
                    ProgressBar.Value = 50;
                    long result = await NewtonSymbolAsync(n, k);
                    ProgressBar.Value = 100;

                    ResultTextBox.Text = result.ToString();
                }
                else
                {
                    MessageBox.Show("Please enter valid values: N >= K and K >= 0.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Please enter valid integer values.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CompressButton_Click(object sender, RoutedEventArgs e)
        {
            using (var folderDialog = new FolderBrowserDialog())
            {
                if (folderDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    var selectedPath = folderDialog.SelectedPath;
                    Task.Run(() => CompressFiles(selectedPath));
                }
            }
        }

        private void DecompressButton_Click(object sender, RoutedEventArgs e)
        {
            using (var folderDialog = new FolderBrowserDialog())
            {
                if (folderDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    var selectedPath = folderDialog.SelectedPath;
                    Task.Run(() => DecompressFiles(selectedPath));
                }
            }
        }

        private void CompressFiles(string folderPath)
        {
            var files = Directory.GetFiles(folderPath);
            Parallel.ForEach(files, file =>
            {
                using (var originalFileStream = new FileStream(file, FileMode.Open, FileAccess.Read))
                using (var compressedFileStream = new FileStream(file + ".gz", FileMode.Create))
                using (var compressionStream = new GZipStream(compressedFileStream, CompressionMode.Compress))
                {
                    originalFileStream.CopyTo(compressionStream);
                }
            });
            MessageBox.Show("Compression completed.", "Compression", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void DecompressFiles(string folderPath)
        {
            var files = Directory.GetFiles(folderPath, "*.gz");
            Parallel.ForEach(files, file =>
            {
                var decompressedFileName = Path.GetFileNameWithoutExtension(file);
                using (var compressedFileStream = new FileStream(file, FileMode.Open, FileAccess.Read))
                using (var decompressedFileStream = new FileStream(Path.Combine(folderPath, decompressedFileName), FileMode.Create))
                using (var decompressionStream = new GZipStream(compressedFileStream, CompressionMode.Decompress))
                {
                    decompressionStream.CopyTo(decompressedFileStream);
                }
            });
            MessageBox.Show("Decompression completed.", "Decompression", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}