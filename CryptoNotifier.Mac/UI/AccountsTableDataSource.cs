using System;
using AppKit;

namespace CryptoNotifier.Mac.UI
{
    class AccountsTableDataSource : NSTableViewDataSource
    {
        int rows = 0;

        public AccountsTableDataSource(int rows)
        {
            this.rows = rows;
        }

        public override nint GetRowCount(NSTableView tableView)
        {
            return rows;
        }
    }
}
