using Medic.DataSource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace Medic.Services
{
    class CreateDocFromDataGrid
    {
        public static FlowDocument GetDocument(DataGrid data) {
            var row = new TableRow();
            var group = new TableRowGroup();
            var table = new Table();
            var doc = new FlowDocument();
            var column = new TableColumn();


            foreach(var col in data.Columns) {
                row.Cells.Add(getCell(col.Header.ToString(),16));
            }
            group.Rows.Add(row);

            foreach (var i in data.Items) 
            {
                row = new TableRow();
                foreach(var col in data.Columns) 
                {
                    var item = (col.GetCellContent(i) as TextBlock);
                    if(item == null) 
                    {
                        data.ScrollIntoView(i,col);
                        var s = data.EnableRowVirtualization;
                        item = col.GetCellContent(i) as TextBlock;
                    }
                    row.Cells.Add(getCell(item.Text));
                }
                group.Rows.Add(row);
            }

            table.RowGroups.Add(group);
            doc.Blocks.Add(table);
            doc.ColumnWidth = 99999;
            return doc;
        }

        static private TableCell getCell(string text, int fontSize = 12) 
        {
            var paraggraph = new Paragraph(new Run(text)) { FontSize = fontSize };
            return new TableCell(paraggraph);
        }
    }
}
