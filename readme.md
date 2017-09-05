# Quicken QIF to QuickBooks QBO Conversion Command Line Utility

This utility performs a very simple job:

It converts a Quicken QIF file's <INTUI.BID> Id using a value of a known and supported instution so that Quickbooks can open the file. QIF and QBO files both are OFX formatted files, but the difference is that QBO is an Intuit authorized format that requires a bought and paid for Intuit ID. Many smaller banks don't have an Intuit ID and thus the QIF files can't be imported. 

This Command Line Utility does the following:

* Loads a QIF file
* Sets the <INTUI.BID> Id to a known value of a supported bank (default 2200)
* Writes the file out as a QBO file
* Opens the file in Quickbooks via Shell execution

The utility uses a single self-contained file:

* [QfxToQboConverter.exe](https://github.com/RickStrahl/QfxToQboConverter/raw/master/QfxToQboConverter.exe)
* requires .NET 4.5 or later

To use the utility:

```txt
Syntax:
-------
QfxToQbo  inputFile [/? Help] [-o outputFile]  [-n noAutoOpen] [-b bankId (default 2200)]

Arguments:
----------
inputfile  - the QFX input file. If not specified you'll be prompted.

-o QBO output file to create (not specified: same as .qif with .qbo extension)
-n Don't open in QuickBooks automatically
-b Fake Bank Id to assign - default is 2200 


Examples:
---------
QfxToQboConverter c:\temp\transactions.qfx 
QfxToQboConverter c:\temp\transactions.qfx -o c:\temp\qbtransactions.qbo -b 4530 -n
```

> #### Bank Displayed in QuickBooks
> Imported files will show up under the bank that the Bank Id is mapped to which is **not your bank's name** most likely. If you import QIF files for more than one bank make sure you use **separate** BankIds for each of the banks so the transactions don't get mixed up.

### Associating with QFX Files
It's also useful to associate the utility with QFX files so when you download QIF files you can just double click and open the file in QuickBooks. 

To associate in Windows 10:

* Right click the QIF file
* Select *Open With...*
* Select *Choose Another App...*
* Choose *More Apps* then down to *Look for another App on this PC*
* Check *Always use this App for QFX*

Now QFX files are associated with the utility. 

> #### Default Parameters Only for Associations
> Association only runs with the default parameters, which means you only can use the default bank id of 2200. If you have more than one bank you're importing from QIF make sure you use the command line utility and apply the appropriate bank id for each.

### 