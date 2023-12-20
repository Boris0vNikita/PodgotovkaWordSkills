//using Microsoft.Office.Interop.Word;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TestWSRus.Infractucture;
using TestWSRus.options;
using Microsoft.Office.Interop.Word;

namespace TestWSRus.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddPerson.xaml
    /// </summary>
    public partial class AddPerson 
    {
        public AddPerson()
        {
            InitializeComponent();
            cmbRole.ItemsSource = DbConn.db.Role.ToList();

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            string firstName = txbFistName.Text.Trim();
            string lastName = txbLastName.Text.Trim();
            string patronymic = txbPatronymic.Text.Trim();
            string phone = txbPhone.Text.Trim();
            string nameRole = cmbRole.Text;
            string password = txbPass.Text.Trim();
            string repeatPassword = txbPass1.Text.Trim();

            if (firstName != null && lastName != null && patronymic != null && phone != null && nameRole != null && password != null && repeatPassword != null)
            {

                HashPassword hash = new HashPassword();
                byte[] salt = hash.GeneralSalt();
                byte[] sha = hash.GenerationHash(password, salt);
                string hashPass = Convert.ToBase64String(sha);

                int idRole = DbConn.db.Role.Where(p => p.NameRole == nameRole).FirstOrDefault().IdRole;
                if (txbPass.Text == txbPass1.Text)
                {
                    People people = new People();
                    people.IdPeople = DbConn.db.People.Max(p => p.IdPeople) + 1;
                    people.FirstName = firstName;
                    people.LastName = lastName;
                    people.Patronymic = patronymic;

                    if (txbPhone.Text.Length == 11)
                    {
                        people.Phone = phone;
                        people.Login = txbLogin.Text;
                        people.Password = hashPass;
                        people.RoleId = idRole;
                        try
                        {
                            DbConn.db.People.Add(people);
                            DbConn.db.SaveChanges();
                            MessageBox.Show("Пользователь добавлен");

                        }
                        // проверка валидации - т.е покажет какое поле не добавлено при сохранении пользователя
                        catch (DbEntityValidationException ex)
                        {
                            foreach (DbEntityValidationResult validationError in ex.EntityValidationErrors)
                            {
                                lblEror.Content = string.Format("Object: " + validationError.Entry.Entity.ToString());
                                lblEror.Content = ("");
                                foreach (DbValidationError err in validationError.ValidationErrors)
                                {
                                    lblEror.Content = (err.ErrorMessage + "");
                                }
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Телефон должен содержать 11 цифр");
                    }
                }
                else
                {
                    MessageBox.Show("Пароли не совпадают");
                }

            }
            else
            {
                MessageBox.Show("заполните все поля!");
            }
        }

        private void btnListPerson_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GetNavigationService(this).Navigate(new Uri("/Pages/PagePerson.xaml", UriKind.RelativeOrAbsolute));
        }

        private void txbPhone_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !(Char.IsDigit(e.Text, 0));
        }

        private void btnAddPhoto_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image | *jpg;*png | All files | *.*";

            if (openFileDialog.ShowDialog() == true)
            {
                string allPath = openFileDialog.FileName;
                string filename = System.IO.Path.GetFileName(allPath);

                string endPoints = @"D:\1WS Russian 2023\Подготовка\TestWord\TestWord\Sourse\";

                string copyEndPoint = System.IO.Path.Combine(endPoints, filename);

                try
                {
                    if (File.Exists(allPath))
                    {
                        File.Copy(allPath, copyEndPoint);
                        MessageBox.Show("Успешно.");
                    }
                    else
                    {
                        MessageBox.Show("Файл не найден в исходной папке.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void btnCreateDoc_Click(object sender, RoutedEventArgs e)
        {
            CreateDocument();
        }

        private void CreateDocument()
        {
            Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();

            Document doc = wordApp.Documents.Add();

            doc.Content.Text = "jopa";

            object fileName = @"D:\1WS Russian 2023\Подготовка\TestWSRus\temp1.docx";

            doc.SaveAs2(fileName);
            doc.Close();
            wordApp.Quit();

            System.Diagnostics.Process.Start(fileName.ToString());
            // Создаем новый экземпляр приложения <link>Word</link>
            //Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();

            //object missing = System.Reflection.Missing.Value;
            // Создаем новый документ
            //Document doc = wordApp.Documents.Add(ref missing, ref missing, ref missing, ref missing);

            //foreach (Microsoft.Office.Interop.Word.Section section in doc.Sections)
            //{
            //    //Get the header range and add the header details.
            //    Microsoft.Office.Interop.Word.Range headerRange = section.Headers[Microsoft.Office.Interop.Word.WdHeaderFooterIndex.wdHeaderFooterPrimary].Range;
            //    headerRange.Fields.Add(headerRange, Microsoft.Office.Interop.Word.WdFieldType.wdFieldPage);
            //    headerRange.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;
            //    headerRange.Font.ColorIndex = Microsoft.Office.Interop.Word.WdColorIndex.wdBlue;
            //    headerRange.Font.Size = 10;
            //    headerRange.Text = "ТТИ НИЯУ МИФИ";
            //}

            //foreach (Microsoft.Office.Interop.Word.Section wordSection in doc.Sections)
            //{
            //    //Get the footer range and add the footer details.
            //    Microsoft.Office.Interop.Word.Range footerRange = wordSection.Footers[Microsoft.Office.Interop.Word.WdHeaderFooterIndex.wdHeaderFooterPrimary].Range;
            //    footerRange.Font.ColorIndex = Microsoft.Office.Interop.Word.WdColorIndex.wdDarkRed;
            //    footerRange.Font.Size = 10;
            //    footerRange.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;
            //    footerRange.Text = "Трехгорный 2023, через 19 дней новый год, а я тут к чемпионату готовлюсь";
            //}

            //// Добавляем текст в документ
            ////doc.Content.Text = "Привет, мир!";

            ////Add paragraph with Heading 1 style
            //Microsoft.Office.Interop.Word.Paragraph para1 = doc.Content.Paragraphs.Add(ref missing);
            //object styleHeading1 = "Заголовок 1";
            //para1.Range.set_Style(ref styleHeading1);
            //para1.Range.Text = "Para 1 text";
            //para1.Range.InsertParagraphAfter();

            //Microsoft.Office.Interop.Word.Table firstTable = doc.Tables.Add(para1.Range, 5, 5, ref missing, ref missing);

            //firstTable.Borders.Enable = 1;
            //foreach (Row row in firstTable.Rows)
            //{
            //    foreach (Cell cell in row.Cells)
            //    {
            //        //Header row
            //        if (cell.RowIndex == 1)
            //        {
            //            cell.Range.Text = "Column " + cell.ColumnIndex.ToString();
            //            cell.Range.Font.Bold = 1;
            //            //other format properties goes here
            //            cell.Range.Font.Name = "verdana";
            //            cell.Range.Font.Size = 10;
            //            //cell.Range.Font.ColorIndex = WdColorIndex.wdGray25;                            
            //            cell.Shading.BackgroundPatternColor = WdColor.wdColorGray25;
            //            //Center alignment for the Header cells
            //            cell.VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;
            //            cell.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;

            //        }
            //        //Data row
            //        else
            //        {
            //            cell.Range.Text = (cell.RowIndex - 2 + cell.ColumnIndex).ToString();
            //        }
            //    }
            //}



            //// Сохраняем документ
            //object filename = @"D:\1WS Russian 2023\Подготовка\TestWSRus\temp1.docx";
            //doc.SaveAs2(ref filename);

            //// Закрываем документ и приложение <link>Word</link>
            //doc.Close(ref missing, ref missing, ref missing);
            //doc = null;
            //wordApp.Quit(ref missing, ref missing, ref missing);
            //wordApp = null;

            //System.Diagnostics.Process.Start(filename.ToString());

            //wordApp.Quit();
        }

        private void btnCreateExel_Click(object sender, RoutedEventArgs e)
        {
            string fileName = @"D:\1WS Russian 2023\Подготовка\TestWSRus\excel1.xls";

            Excel.Application xlApp = new Excel.Application();
            if (xlApp == null)
            {
                MessageBox.Show("Excel is not properly installed!!");
                return;
            }
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;
            xlWorkBook = xlApp.Workbooks.Add(misValue);
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
            xlWorkSheet.Cells[1, 1] = "ID";
            xlWorkSheet.Cells[1, 2] = "Name";
            xlWorkSheet.Cells[2, 1] = "1";
            xlWorkSheet.Cells[2, 2] = "One";
            xlWorkSheet.Cells[3, 1] = "2";
            xlWorkSheet.Cells[3, 2] = "Two";
            xlWorkSheet.Cells[3, 3] = txbFistName.Text.Trim();
            xlWorkBook.SaveAs(fileName, Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
            xlWorkBook.Close(true, misValue, misValue);
            xlApp.Quit();
            Marshal.ReleaseComObject(xlWorkSheet);
            Marshal.ReleaseComObject(xlWorkBook);
            Marshal.ReleaseComObject(xlApp);
            MessageBox.Show("Excel file created");

            //object fileName = @"D:\1WS Russian 2023\Подготовка\TestWord\TestWord\testExcel2.xls";

            //Microsoft.Office.Interop.Excel.Application wordApp = new Microsoft.Office.Interop.Excel.Application();
            //Microsoft.Office.Interop.Excel.Workbook workbook = wordApp.Workbooks.Open(fileName.ToString());
            //Microsoft.Office.Interop.Excel.Worksheet worksheet = workbook.Sheets[1];

            //workbook.Close();
            //wordApp.Quit();

            //System.Diagnostics.Process.Start(fileName.ToString());
        }
    }
}
