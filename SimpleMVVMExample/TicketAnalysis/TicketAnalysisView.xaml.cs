using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Windows;
using System.Windows.Controls;
using SimpleMVVMExample.DB;
using MessageBox = System.Windows.MessageBox;
using UserControl = System.Windows.Controls.UserControl;

namespace SimpleMVVMExample.TicketAnalysis
{
    /// <summary>
    /// Interaction logic for TicketAnalysisView.xaml
    /// </summary>
    public partial class TicketAnalysisView : UserControl
    {
        private List<TicketAnalysisModel> analysis = new List<TicketAnalysisModel>();

        public TicketAnalysisView()
        {
            InitializeComponent();
        }

        // Executes Ticket Analysis generation.
        private void btnRunAnalysis_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Generating Ticket Analysis");

            GetAnalysisData();
        }

        // Query for Analysis Data
        private void GetAnalysisData()
        {
            dgTicketAnalysis.Items.Clear();

            using (var cmd = DC.GetOpenConnection().CreateCommand())
            {
                if (cmd.Connection.State != ConnectionState.Open) return;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"SELECT 
    (SELECT COUNT(*) FROM TBLTICKET WHERE BLNFINISHED = '1') AS FINISHEDTICKETS, 
    (SELECT COUNT(*) FROM TBLTICKET WHERE BLNFINISHED = '0') AS UNFINISHEDTICKETS,
    (SELECT query1.STRREQUESTBY FROM (SELECT TBLCUSTOMER.STRFORENAME || ' ' || TBLCUSTOMER.STRSURNAME AS STRREQUESTBY, Count(*) AS order_count
      FROM TBLTICKET
      INNER JOIN TBLCUSTOMER
        ON TBLCUSTOMER.INTCUSTOMERID = TBLTICKET.INTREQUESTBY
      GROUP BY TBLTICKET.INTREQUESTBY, TBLCUSTOMER.STRFORENAME, TBLCUSTOMER.STRSURNAME) query1,

     (SELECT max(query2.order_count) AS highest_count
      FROM (SELECT INTREQUESTBY, Count(*) AS order_count
            FROM TBLTICKET
            GROUP BY TBLTICKET.INTREQUESTBY) query2) query3
    WHERE query1.order_count = query3.highest_count) AS MOSTREQUESTS,
    (SELECT query1.STRASSIGNEDTO
    FROM (SELECT TBLSTAFF.STRFORENAME || ' ' || TBLSTAFF.STRSURNAME AS STRASSIGNEDTO, Count(*) AS order_count
          FROM TBLTICKET
          INNER JOIN TBLSTAFF
                ON TBLSTAFF.INTSTAFFID = TBLTICKET.INTASSIGNEDTO
          GROUP BY TBLTICKET.INTASSIGNEDTO, TBLSTAFF.STRFORENAME, TBLSTAFF.STRSURNAME) query1,
    
         (SELECT max(query2.order_count) AS highest_count
          FROM (SELECT INTASSIGNEDTO, Count(*) AS order_count
                FROM TBLTICKET
                GROUP BY TBLTICKET.INTASSIGNEDTO) query2) query3
    WHERE query1.order_count = query3.highest_count)
FROM 
    TBLTICKET";

                DbDataReader reader = cmd.ExecuteReader();
                // Populate DataGrid
                while (reader.Read())
                {
                    analysis.Add(new TicketAnalysisModel
                    {
                        Type = "Closed Tickets",
                        Value = reader.GetInt32(0).ToString()
                    });
                    analysis.Add(new TicketAnalysisModel {Type = "Open Tickets", Value = reader.GetInt32(1).ToString()});
                    analysis.Add(new TicketAnalysisModel
                    {
                        Type = "Customer - Most Requests",
                        Value = reader.GetString(2)
                    });
                    if (!reader.IsDBNull(3))
                    {
                        analysis.Add(new TicketAnalysisModel
                        {
                            Type = "Staff - Most Tickets",
                            Value = reader.GetString(3)
                        });
                    }
                    else
                    {
                        analysis.Add(new TicketAnalysisModel
                        {
                            Type = "Staff - Most Tickets",
                            Value = ""
                        });
                    }
                }
            }
            dgTicketAnalysis.ItemsSource = analysis;
        }

        // Printing DataGrid
        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            var Printdlg = new PrintDialog();
            if (Printdlg.ShowDialog().GetValueOrDefault())
            {
                Size pageSize = new Size(Printdlg.PrintableAreaWidth, Printdlg.PrintableAreaHeight);
                // sizing of the element.
                dgTicketAnalysis.Measure(pageSize);
                dgTicketAnalysis.Arrange(new Rect(5, 5, pageSize.Width, pageSize.Height));
                Printdlg.PrintVisual(dgTicketAnalysis, "Ticket Analysis");
            }
        }
    }

    public class TicketAnalysisModel
    {
        public string Type { get; set; }

        public string Value { get; set; }
    }
}
