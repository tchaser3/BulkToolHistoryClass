/* Title:           Bulk Tool History Class
 * Date:            11-20-18
 * Author:          Terry Holmes
 * 
 * Description:     This is used to set up the  Bulk Tool History Class */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewEventLogDLL;

namespace BulkToolHistoryDLL
{
    public class BulkToolHistoryClass
    {
        EventLogClass TheEventLogClass = new EventLogClass();

        BulkToolHistoryDataSet aBulkToolHistoryDataSet;
        BulkToolHistoryDataSetTableAdapters.bulktoolhistoryTableAdapter aBulkToolHistoryTableAdapter;

        InsertBulkTookHistoryEntryTableAdapters.QueriesTableAdapter aInsertBulkToolHistoryTableAdapter;

        public bool InsertBulkToolHistory(int intEmployeeID, int intWarehouseEmployeeID, int intQuantityEffected, int intBulkToolID, string strTransactionNotes)
        {
            bool blnFatalError = false;

            try
            {
                aInsertBulkToolHistoryTableAdapter = new InsertBulkTookHistoryEntryTableAdapters.QueriesTableAdapter();
                aInsertBulkToolHistoryTableAdapter.InsertBulkToolHistory(DateTime.Now, intEmployeeID, intWarehouseEmployeeID, intQuantityEffected, intBulkToolID, strTransactionNotes);
            }
            catch(Exception Ex)
            {
                TheEventLogClass.InsertEventLogEntry(DateTime.Now, "Bulk Tool History Class // Insert Bulk Tool History " + Ex.Message);

                blnFatalError = true;
            }

            return blnFatalError;
        }
        public BulkToolHistoryDataSet GetBulkToolHistoryInfo()
        {
            try
            {
                aBulkToolHistoryDataSet = new BulkToolHistoryDataSet();
                aBulkToolHistoryTableAdapter = new BulkToolHistoryDataSetTableAdapters.bulktoolhistoryTableAdapter();
                aBulkToolHistoryTableAdapter.Fill(aBulkToolHistoryDataSet.bulktoolhistory);
            }
            catch (Exception Ex)
            {
                TheEventLogClass.InsertEventLogEntry(DateTime.Now, "Bulk Tool History Class // Get Bulk Tool History Info " + Ex.Message);
            }

            return aBulkToolHistoryDataSet;
        }
        public void UpdateBulkToolHistoryDB(BulkToolHistoryDataSet aBulkToolHistoryDataSet)
        {
            try
            {
                aBulkToolHistoryTableAdapter = new BulkToolHistoryDataSetTableAdapters.bulktoolhistoryTableAdapter();
                aBulkToolHistoryTableAdapter.Update(aBulkToolHistoryDataSet.bulktoolhistory);
            }
            catch (Exception Ex)
            {
                TheEventLogClass.InsertEventLogEntry(DateTime.Now, "Bulk Tool History Class // Update Bulk Tool History DB " + Ex.Message);
            }
        }
    }
}
