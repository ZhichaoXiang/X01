using BrunieBCL.Models;
using BrunieBCL.Models.Data;
using BrunieBCL.Models.DbUsage;
using BrunieBCL.Models.Platform;
using BrunieClient.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;


namespace BrunieClient.Models.Office {
    public sealed class DbExcel : ADisposable {
        readonly Excel.Application App;
        readonly DbArray ExcelDbArray;
        readonly DbArray AuswExcelDbArray;
        public DbExcel() {
            App = new Excel.Application();
            ExcelDbArray = new DbArray("^QTEMP", new string[] { "%JOB", "\"EXCEL\"" });
            AuswExcelDbArray = ExcelDbArray.AppendIndices("AUSW");
            //AuswExcelDbArray.AppendIndices("\"MAPPE\"", "%MAP");
        }
        //__EXECUTE(_T("D:$G(%RTN)'=\"\" @%RTN"));
        //public string FetchDbExcelSheetName() {
        //    return (AuswExcelDbArray.GetValue());
        //}
        public static DbExcel TryCreateInstance() {
            return (new DbExcel());
        }
        public Excel.Workbooks TryGetWorkbooks() {
            return (App.Workbooks);
        }
        public Excel.Workbook TryAddWorkbook(Excel.Workbooks workbooks) {
            return (workbooks.Add());
        }
        public Excel.Workbook TryOpenWorkbook(Excel.Workbooks workbooks, string p, bool isok) {
            return (workbooks.Open(p, isok));
        }
        public Excel.Worksheet TryGetActiveSheet(Excel.Workbook workbook) {
            return (workbook.ActiveSheet);
        }
        public Excel.Worksheet TryGetSheet(Excel.Workbook workbook, string name) {
            return (workbook.Sheets.Cast<Excel.Worksheet>().First(x => x.Name == name));
        }

        public void Build(VmChildDialog vm, bool onlyOpenSheet, bool createSheet, string excelFormDirectory) {
            DbArray excelDbArray = new DbArray("^QTEMP", new string[] { "%JOB", "\"EXCEL\"" });
            //if(!onlyOpenSheet && !createSheet && !excelDbArray.IsDefined())) {
            if(!(onlyOpenSheet || createSheet || excelDbArray.IsDefined())) {
                return;
            }
            Excel.Application excelApp = new Excel.Application();

            excelApp.Quit();
            DbArray auswExcelDbArray = excelDbArray.AppendIndices("AUSW");
            if(!(onlyOpenSheet || createSheet)) {
                AApp.Current.Du.QueryDb("S AUSW=1");
            }
        }
        public static void TrySendToExcel(bool onlyOpenSheet, bool createSheet, string excelFormDirectory) {
            Ex.Try(() => {
                if(onlyOpenSheet) {
                    createSheet = false;
                }
                //new ExcelApp().Build(onlyOpenSheet, createSheet, excelFormDirectory);
            }, AApp.Current.MsgBox);
        }
        public void BuildWorkSheet() {
            Ex.Try(() => {
                Excel.Application excelApp = new Excel.Application();
                excelApp.Visible = false;
                excelApp.Workbooks.Add();
                var aworkbook = excelApp.ActiveWorkbook;
                var asheet = aworkbook.ActiveSheet;
                var sheets = aworkbook.Sheets;
                //excelApp.Workbooks.Open();
                Excel._Worksheet workSheet = (Excel.Worksheet)excelApp.ActiveSheet;
                //workSheet.EnableCalculation = false;
                //workSheet.EnableFormatConditionsCalculation = false;
                //workSheet.DisplayPageBreaks = false;
                //excelApp.ScreenUpdating = false;
                //excelApp.EnableEvents = false;
                //excelApp.DisplayStatusBar = false;
                //excelApp.Calculation = Excel.XlCalculation.xlCalculationManual;
                //excelApp.CalculateBeforeSave = false;
                int i = 0;
                //Timing t1 = new Timing("t1");
                //t1.Enter();
                int testcount = 548576;
                //int testcount = 10;
                object[,] saNames = new object[testcount, 26];
                //saNames[1, 1] = "test11" ;
                //saNames[1, 0] = "test12" ;
                //saNames[2, 1] = "test21" ;
                //saNames[4, 1] = "test41" ;
                //workSheet.Range[workSheet.Cells[3, "A"], workSheet.Cells[12, "B"]] = saNames;
                while(i < testcount) {
                    saNames[i, 0] = "test a" + i;
                    saNames[i, 1] = (double)i;
                    saNames[i, 24] = "test c" + i;
                    saNames[i, 25] = (double)i;
                    ++i;
                }
                workSheet.Range[workSheet.Cells[1, "A"], workSheet.Cells[testcount, "Z"]] = saNames;
                excelApp.Visible = true;
            }, AApp.Current.MsgBox);
        }
        protected override void OnDisposing(bool isDisposingManagedResources) {
        }
        void ExcelOpenFileDialog() {
            OpenFileDialog dlgFo = new OpenFileDialog();

        }
        BrunieDbUsage Du => AApp.Current.Du;
        public void CreateSheet(Excel.Worksheet excelWorkSheet) {
            Du.QueryDb("W $G(%MEMO)");
            excelWorkSheet.Name = "";
            object[,] saNames = new object[100, 26];
            //saNames[1, 1] = "test11" ;
            //saNames[1, 0] = "test12" ;
            //saNames[2, 1] = "test21" ;
            //saNames[4, 1] = "test41" ;
            excelWorkSheet.Range[excelWorkSheet.Cells[1, "A"], excelWorkSheet.Cells[100, "Z"]] = saNames;
            Excel.Range s = excelWorkSheet.Range[excelWorkSheet.Cells[1, "A"], excelWorkSheet.Cells[100, "Z"]];
            //s.FormulaR1C1 ;
        }
        public void Build(bool onlyOpenSheet, bool createSheet, string excelFormDirectory) {
            DbArray excelDbArray = new DbArray("^QTEMP", new string[] { "%JOB", "\"EXCEL\"" });
            DbArray auswExcelDbArray = excelDbArray.AppendIndices("AUSW");
            if(auswExcelDbArray.IsDefined()) {
            }
            if(createSheet) {
            }

            List<string> additinalGetting = new List<string>();
            additinalGetting.Add("\"F\"");
            //eg. KUR->KB->EXCEL
            DbGlobalItemsSource dbItemSource = new DbGlobalItemsSource("^QTEMP", "%JOB,\"EXCEL\"", 1, additinalGetting, null);
            while(dbItemSource.FetchMore(8000)) {
                object[,] saNames = new object[dbItemSource.Count, 10];
                int i = 0;
                while(i < dbItemSource.Count) {
                    string var = dbItemSource[i][0]; //A1
                    string x = dbItemSource[i][1].Trim();
                    string f = dbItemSource[i][2]; // 1: float
                    int row = 0;
                    int col = 0;
                    if("1" == f) {
                        saNames[row, col] = var;
                    } else {
                        saNames[row, col] = var.ToNumber() ?? 0;
                    }
                    ++i;
                }
                dbItemSource.Clear();
            }
        }
    }
    public class DbExcelCellItem {
        public int Row {
            get;
        }
        public int Column {
            get;
        }
        public object Value {
            get;
        }
        public DbExcelCellItem(int row, int column, object value) {
            Row = row;
            Column = column;
        }
        public static ICollection<DbExcelCellItem> Getaa() {
            //new DbArray
            //int i3 = x.ConvertFromBaseNumber(1, new Range<char>('A', 'Z'));
            return (null);
        }
    }
    public sealed class DbExcelItemsSource : DbGlobalItemsSource<DbExcelCellItem> {
        public const string GlobalQTEMP = "^QTEMP";
        public readonly DbArray ExcelDbArray = new DbArray(GlobalQTEMP, new string[] { "%JOB", "\"EXCEL\"" });
        public DbExcelItemsSource() : base(null, GlobalQTEMP, "", 3, "F".Yield(), null) {
        }
        protected override DbExcelCellItem NewItem(int index, string s, string splitter) {
            return (new DbExcelCellItem(0, 0, s));
        }
    }
}
