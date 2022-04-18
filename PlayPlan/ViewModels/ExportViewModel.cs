using PlayPlan.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using PlayPlan.DataModel;
using System.Windows;
using System.Collections.ObjectModel;
using ClosedXML.Excel;
using System.Windows.Forms;
using System.IO;

namespace PlayPlan.ViewModels
{
    public class ExportViewModel : ViewModelBase
    {
        private IDataService _ds;
        private bool _isLoading = false;
        public ExportViewModel(ObservableCollection<Person> persons, IDataService ds)
        {
            _ds = ds;
            _persons = persons;
            SaveBtnCmd = new DelegateCommand(obj => this.RunSaveBtnCmd());
            CancelBtnCmd = new DelegateCommand(obj => this.RunCancelBtnCmd());
            DateFrom = DateTime.Now;
            DateTo = DateTime.Now;
        }
        public event EventHandler OnRequestClose;
        public event EventHandler OnUpdateListView;
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public bool IsLoading
        {
            get
            {
                return _isLoading;
            }
            set
            {
                _isLoading = value;
                OnPropertyChanged(nameof(IsLoading));
                OnPropertyChanged(nameof(IsEnabled));
            }
        }


        public bool IsEnabled
        {
            get { return !_isLoading; }
        }

        private ObservableCollection<Person> _persons;

        public ObservableCollection<Person> Persons
        {
            get { return _persons; }
            set { _persons = value; }
        }


        public ICommand SaveBtnCmd { get; private set; }
        public ICommand CancelBtnCmd { get; private set; }

        private void RunSaveBtnCmd()
        {
            IsLoading = true;
            List<ExcelReport> comments = new List<ExcelReport>();
            string TempFile = "";
            //CancellationTokenSource ct = new CancellationTokenSource();
            //ct.CancelAfter(5000);
            Task.Run(async () =>
            {
                try
                {
                    List<ExcelReport> comments = await _ds.ExcelReportAsync(DateFrom, DateTo);

                    var workbook = new XLWorkbook();
                    workbook.AddWorksheet("PlayPlan report");
                    var ws = workbook.Worksheet("PlayPlan report");

                    if (comments.Count == 0)
                    {
                        System.Windows.MessageBox.Show("Записи за указанный период не найдены!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                        IsLoading = false;
                        return;
                    }
                    int row = 1;
                    ws.ColumnWidth = 20;
                    ws.Cell("A" + row.ToString()).Value = "Дата мероприятия";
                    ws.Cell("B" + row.ToString()).Value = "Организатор";
                    ws.Cell("C" + row.ToString()).Value = "Автор записи";
                    ws.Cell("D" + row.ToString()).Value = "Запись";
                    ws.Cell("E" + row.ToString()).Value = "Место";
                    ws.Cell("G" + row.ToString()).Value = "Участники";
                    ws.Cell("F" + row.ToString()).Value = "Дата записи";
                    row = 2;
                    foreach (ExcelReport item in comments)
                    {
                        if (item.CommentFrom == null) continue;
                        ws.Cell("A" + row.ToString()).Value = item.DateComment.ToShortDateString();
                        ws.Cell("B" + row.ToString()).Value = item.PersonName.ToString();
                        ws.Cell("C" + row.ToString()).Value = item.CommentFrom.ToString();
                        ws.Cell("D" + row.ToString()).Value = item.Comment.ToString();
                        ws.Cell("E" + row.ToString()).Value = item.TopicTitle.ToString();
                        ws.Cell("G" + row.ToString()).Value = item.Participant.ToString();
                        ws.Cell("F" + row.ToString()).Value = item.DateInput.ToString();
                        row++;
                    }

                    TempFile = Path.GetTempFileName() + ".xlsx";
                    workbook.SaveAs(TempFile);
                    workbook.Dispose();
                    IsLoading = false;
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show("RunSaveBtnCmd() - Error of saving file: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }


            }).ContinueWith(t =>
            {
                try
                {
                    if (File.Exists(TempFile))
                    {
                        SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                        saveFileDialog1.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
                        saveFileDialog1.FilterIndex = 1;
                        saveFileDialog1.RestoreDirectory = true;
                        saveFileDialog1.DefaultExt = "xlsx";
                        saveFileDialog1.FileName = "PlayplanReport";
                        if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                        {
                            File.Move(TempFile, saveFileDialog1.FileName, true);
                        }
                    }
                }
                catch (Exception ex)
                {
                    if (ex.HResult == -2147024891)
                    {
                        System.Windows.MessageBox.Show("Невозможно сохранить файл. Вероятно он открыт другим приложением. " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        System.Windows.MessageBox.Show("RunSaveBtnCmd() - Error of moving file: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }

            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        private void RunCancelBtnCmd()
        {
            OnRequestClose(this, new EventArgs());
        }
    }
}
