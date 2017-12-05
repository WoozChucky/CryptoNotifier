using System;
using System.Collections.Generic;
using AppKit;
using CryptoNotifier.Mac.ViewModel;
using Foundation;

namespace CryptoNotifier.Mac.UI
{
    class AccountsTableDelegate : NSTableViewDelegate
    {
        List<AccountViewModel> accounts;

        public AccountsTableDelegate(List<AccountViewModel> accounts)
        {
            this.accounts = accounts;
        }

        // Returns the NSView for a given column/row. NSTableView is strange as unlike NSOutlineView 
        // it does not pass in the data for the given item (obtained from the DataSource) for the NSView APIs
        public override NSView GetViewForItem(NSTableView tableView, NSTableColumn tableColumn, nint row)
        {
            
            // This pattern allows you reuse existing views when they are no-longer in use.
            // If the returned view is null, you instance up a new view
            // If a non-null view is returned, you modify it enough to reflect the new data
            NSTextField view = (NSTextField)tableView.MakeView((NSString)accounts[(int)row].Id, this);
            if (view == null)
            {
                view = new NSTextField();
                view.Identifier = (NSString)accounts[(int)row].Id;
                view.Bordered = false;
                view.Selectable = false;
                view.Editable = false;
            }

            switch(tableColumn.Identifier)
            {
                case "Currency":
                    view.StringValue = (NSString)accounts[(int)row].Currency;
                    break;
                case "Amount":
                    view.StringValue = (NSString)accounts[(int)row].Amount;
                    break;
                case "Conversion":
                    view.StringValue = (NSString)accounts[(int)row].Conversion;
                    break;
                case "MarketValue":
                    view.StringValue = (NSString)accounts[(int)row].MarketValue;
                    break;
            }

            return view;
        }

        // An example of responding to user input 
        public override bool ShouldSelectRow(NSTableView tableView, nint row)
        {
            Console.WriteLine("ShouldSelectRow: {0}", row);
            return true;
        }
    }
}
