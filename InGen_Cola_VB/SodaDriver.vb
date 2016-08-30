Imports System.Data
Imports System.Collections.Generic

Public Class SodaDriver
    Dim db As ConnectToServer

    Public Sub New()
        db = New ConnectToServer()
    End Sub

    Public Sub Menu()
        Dim sel As String
        Console.Clear()
        While (True)
            Console.Write(" ______________________________________________" & vbNewline & _
                      "| InGen Cola                                   |" & vbNewline & _
                      "|----------------------------------------------|" & vbNewline & _
                      "| Current         Show Previous entries        |" & vbNewline & _
                      "| New             Make new Entry               |" & vbNewline & _
                      "| Stats           Soda Statistics              |" & vbNewline & _
                      "| Maint           Database Maintanince         |" & vbNewline & _
                      "| EXIT            Quit Program                 |" & vbNewline & _
                      "|______________________________________________|" & vbNewline & _
                      ">>> ")
            sel = Console.ReadLine()
            sel = sel.toLower()

            Select Case sel
                Case "current"
                    CurMenu()
                Case "new"
                    NewMenu()
                Case "stats"
                    StatMenu()
                Case "maint"
                    Maintanince()
                Case "exit"
                    Exit While

            End Select
        End While
        Console.Clear()
    End Sub

    '=======================================================================================================================
    Sub CurMenu()
        Dim sel As String

        Console.Clear()
        While (True)
            Console.Write(" ______________________________________________" & vbNewline & _
                      "| InGen Cola                 Current Records   |" & vbNewline & _
                      "|----------------------------------------------|" & vbNewline & _
                      "| CashOut         Show all profets collected   |" & vbNewline & _
                      "| Spendings       Show spending records        |" & vbNewline & _
                      "| inventory       Show weekly inventory        |" & vbNewline & _
                      "| Maint           Goto Maintanince Menu        |" & vbNewline & _
                      "| CD              Back to Main Menu            |" & vbNewline & _
                      "|______________________________________________|" & vbNewline & _
                      ">>> ")
            sel = Console.ReadLine()
            sel = sel.ToLower().Trim()

            Select Case sel
                Case "cashout"
                    curCashout()
                Case "spendings"
                    curSpendings()
                Case "inventory"
                    curInventory()
                Case "maint"
                    Maintanince()
                Case "cd"
                    Exit While

            End Select
        End While
        Console.Clear()
    End Sub

    '=======================================================================================================================
    Sub NewMenu()
        Dim sel As String

        Console.Clear()
        While (True)
            Console.Write(" ______________________________________________" & vbNewline & _
                      "| InGen Cola                 New Records       |" & vbNewline & _
                      "|----------------------------------------------|" & vbNewline & _
                      "| Profets         Enter dayley profets         |" & vbNewline & _
                      "| Purchase        Enter cost of soda spent     |" & vbNewline & _
                      "| CD              Back to Main Menu            |" & vbNewline & _
                      "|______________________________________________|" & vbNewline & _
                      ">>> ")
            sel = Console.ReadLine()
            sel = sel.ToLower().Trim()

            Select Case sel
                Case "profets"
                    newProfets()
                Case "purchase"
                    newPurchase()
                Case "cd"
                    Exit While

            End Select
        End While
        Console.Clear()
    End Sub

    '=======================================================================================================================
    Sub StatMenu()
        Dim sel As String

        Console.Clear()
        While (True)
            Console.Write(" ______________________________________________" & vbNewline & _
                      "| InGen Cola                 Statistics        |" & vbNewline & _
                      "|----------------------------------------------|" & vbNewline & _
                      "| add             Add this years profets       |" & vbNewline & _
                      "| adds            Add this years spendings     |" & vbNewline & _
                      "| avgg            Get avg. grose for the year  |" & vbNewline & _
                      "| avgn            Get avg. net for this year   |" & vbNewline & _
                      "| avgs            Get avg. spendings           |" & vbNewline & _
                      "| net             Calculate profets per-can    |" & vbNewline & _
                      "| sum             Calculate Net Profets        |" & vbNewline & _
                      "| stddiv          Calculate standard deviation |" & vbNewline & _
                      "| weekest         Estemate weekly profets      |" & vbNewline & _
                      "| CD              Back to Main Menu            |" & vbNewline & _
                      "|______________________________________________|" & vbNewline & _
                      ">>> ")
            sel = Console.ReadLine()
            sel = sel.ToLower().Trim()

            Select Case sel
                Case "add"
                    statAdd()
                Case "adds"
                    statAddSpend()
                Case "avgg"
                    statAvgg()
                Case "avgn"
                    statAvgn()
                Case "avgs"
                    statAvgs()
                Case "net"
                    statNet()
                Case "sum"
                    statSum()
                Case "weekest"
                    statWeek()
                Case "stddiv"
                    statStanDiv()
                Case "cd"
                    Exit While

            End Select
        End While
        Console.Clear()
    End Sub

    '=======================================================================================================================
    Sub Maintanince()
        Dim sel As String

        Console.Clear()
        While (True)
            Console.Write(" ______________________________________________" & vbNewline & _
                      "| InGen Cola                 Data Maintanince  |" & vbNewline & _
                      "|----------------------------------------------|" & vbNewline & _
                      "| upinv           Update inventory record      |" & vbNewline & _
                      "| upcsh           Update cash out records      |" & vbNewline & _
                      "| upspn           Update spending records      |" & vbNewline & _
                      "| delinv          Delete inventory record      |" & vbNewline & _
                      "| delcsh          Delete cash out records      |" & vbNewline & _
                      "| delspn          Delete spending records      |" & vbNewline & _
                      "| Addsda          Add new soda flavor          |" & vbNewline & _
                      "| CD              Back to Main Menu            |" & vbNewline & _
                      "|______________________________________________|" & vbNewline & _
                      ">>> ")
            sel = Console.ReadLine()
            sel = sel.ToLower().Trim()

            Select Case sel
                Case "upinv"
                    upinv()
                Case "upcsh"
                    upCashOut()
                Case "upspn"
                    upSpending()
                Case "delinv"
                    delInv()
                Case "delcsh"
                    delCashOut()
                Case "delspn"
                    delSpending()
                Case "addsda"
                    addSoda()
                Case "cd"
                    Exit While

            End Select
        End While
        Console.Clear()
    End Sub

    '=======================================================================================================================
    Sub statSum()
        Dim spending As List(Of String)
        Dim earning As List(Of String)
        Dim ern As Double = 0
        Dim spn As Double = 0

        spending = db.showAsList("SELECT price From Spendings WHERE YEAR(date) = YEAR(NOW());")
        earning = db.showAsList("SELECT ernings FROM CashOut WHERE YEAR(date) = YEAR(NOW());")

        Console.Clear()

        spn = getSum(spending)
        ern = getSum(earning)

        Console.WriteLine(vbTab & " _________________________________" & vbNewLine & _
                  vbTab & "/ Current Total Profets: " & (ern - spn).ToString("C"))
    End Sub
    '=======================================================================================================================
    Sub statAvgg()
        Dim earning As List(Of String)
        Dim ern As Double = 0

        earning = db.showAsList("SELECT ernings FROM CashOut WHERE YEAR(date) = YEAR(NOW());")

        Console.Clear()
        ern = getAvrage(earning)

        Console.WriteLine(vbTab & " _________________________________" & vbNewLine & _
                  vbTab & "/ Average Grose: " & ern.ToString("C"))
    End Sub
    '=======================================================================================================================
    Sub statAvgn()
        Dim spending As List(Of String)
        Dim earning As List(Of String)
        dim avgPer  As Double
        Dim ern As Double = 0
        Dim spn As Double = 0

        spending = db.showAsList("SELECT price From Spendings WHERE YEAR(date) = YEAR(NOW());")
        earning = db.showAsList("SELECT ernings FROM CashOut WHERE YEAR(date) = YEAR(NOW());")

        Console.Clear()
        spn = getSum(spending)
        ern = getSum(earning)

        avgPer = (spending.Count + earning.Count) / 2

        Console.WriteLine(vbTab & " _________________________________" & vbNewLine & _
                  vbTab & "/ Avrage Net Profets: " & ((ern - spn) / avgPer).ToString("C"))
    End Sub
    '=======================================================================================================================
    Sub statAvgs()
        Dim spending As List(Of String)
        Dim avg As Double

        spending = db.showAsList("SELECT price From Spendings WHERE YEAR(date) = YEAR(NOW());")

        Console.Clear()
        avg = getAvrage(spending)

        Console.WriteLine(vbTab & " _________________________________" & vbNewLine & _
                  vbTab & "/ Average Spendings: " & avg.ToString("C"))
    End Sub
    '=======================================================================================================================
    Sub statNet()
        Dim dt As DataTable
        Dim tblRow As String = ""
        Dim rowlim As Integer = 0

        dt = db.showAsTable("SELECT SodaName, (Price + crv), cancount, ((Price + crv) / cancount), " & _
                                    "Worth, (Worth - (Price + crv)) FROM Inventory " & _
                            "INNER JOIN Sodas ON Sodas.SodaID = Inventory.sodaID " & _
                            "WHERE YEAR(date) = YEAR(NOW()) ORDER BY Inventory.id;")

        Console.Clear()
        For i As Integer = 0 To dt.Rows.Count()
            Console.WriteLine("___________________________________________________________________________________________" & vbNewLine & _
                      "    NAME      |    COSTS     |  # OF CANS   | PRICE PER CAN |   SALES       |      NET     " & vbNewLine & _
                      "-------------------------------------------------------------------------------------------")
            If i + 19 > dt.Rows.Count() Then
                rowlim = dt.Rows.Count() - 1
            Else
                rowlim = i + 19
            End If

            For r As Integer = i To rowlim
                For c As Integer = 0 To 5
                    If c = 1 Or c >= 3 Then
		        tblRow &= String.Format("{0,14} ", Convert.ToDouble(dt.Rows(r)(c).ToString()).ToString("C"))
		    Else
                    	tblRow &= String.Format("{0,14} ", dt.Rows(r)(c).ToString() & " ")
		    End If
                Next
                Console.WriteLine(tblRow)
                tblRow = ""
            Next
            i += 19
        Next
    End Sub
    '=======================================================================================================================
    Sub statAdd()
        Dim earning As List(Of String)
        Dim ern As Double = 0

        earning = db.showAsList("SELECT ernings FROM CashOut WHERE YEAR(date) = YEAR(NOW());")

        Console.Clear()
        ern = getSum(earning)

        Console.WriteLine(vbTab & " _________________________________" & vbNewLine & _
                  vbTab & "/ Total Grose: " & (ern.ToString("C")))
    End Sub
    '=======================================================================================================================
    Sub statAddSpend()
        Dim spending As List(Of String)
        Dim spn As Double = 0

        spending = db.showAsList("SELECT price From Spendings WHERE YEAR(date) = YEAR(NOW());")

        Console.Clear()
        spn = getSum(spending)

        Console.WriteLine(vbTab & " _________________________________" & vbNewLine & _
                  vbTab & "/ Total Grose: " & (spn.ToString("C")))
    End Sub
    '=======================================================================================================================
    Sub statWeek()
        Dim weekSodaEarn As Double = 0
        Dim cancount As Integer = 0
        Dim dt As DataTable

        dt = db.showAsTable("select SUM(cancount), SUM(worth) FROM Inventory WHERE YEAR(date) = YEAR(NOW()) GROUP BY date;")

        Console.Clear()
        For i As Integer = 0 To dt.Rows.Count() - 1
            Console.WriteLine(String.Format("Total Number of cans: {0,-10}", dt.Rows(i)(0).ToString()) & vbNewLine & _
                      String.Format("Estimated profets:    ${0,-10}", dt.Rows(i)(1).ToString()) & vbNewLine)
        Next
    End Sub
    '=======================================================================================================================
    Sub statStanDiv()
        Dim spending As List(Of String)
        Dim earning As List(Of String)
        Dim sqrEar As List(Of String)
        Dim sqrSpn As List(Of String)
        Dim dt, dt2 As DataTable
        Dim avgPer  As Double
        Dim ern As Double = 0
        Dim spn As Double = 0
        Dim Aern As Double = 0
        Dim Aspn As Double = 0
        Dim a,b As Integer

        spending = db.showAsList("SELECT price From Spendings WHERE YEAR(date) = YEAR(NOW());")
        earning = db.showAsList("SELECT ernings FROM CashOut WHERE YEAR(date) = YEAR(NOW());")
        a = spending.Count
        b = earning.Count

        If a > b Then
            b = a
        End If

        ern = getAvrage(earning)
        spn = getAvrage(spending)

        sqrEar = db.ShowAsList("SELECT ((ernings - " & ern & ") * (ernings-" & ern & ")) AS sqr FROM CashOut WHERE YEAR(date) = YEAR(NOW());")
        sqrSpn = db.ShowAsList("SELECT ((price - " & spn & ") * (price-" & spn & ")) AS sqr FROM Spendings WHERE YEAR(date) = YEAR(NOW());")
        Aern = getAvrage(sqrEar)
        Aspn = getAvrage(sqrSpn)

        dt = db.showAsTable("SELECT date, price, ((price-" & spn & ") / " & Math.Sqrt(Aspn) & ") AS stdDiv FROM Spendings WHERE YEAR(date) = YEAR(NOW());")
        dt2 = db.showAsTable("SELECT date, ernings, ((ernings-" & ern & ") / " & Math.Sqrt(Aern) & ") AS stdDiv FROM CashOut WHERE YEAR(date) = YEAR(NOW());")

        Console.Clear()
        For i As Integer=0 To b -1
            If (i mod 20) = 0 Then
                Console.WriteLine(String.Format("{0,-30}   {1,-30}   ", "Spendings","Earnings"))
                Console.WriteLine("________________________________________________________________")
                Console.WriteLine(String.Format("{0,-15}{1,-8}{2,-7}   |{3,-15}{4,-8}{5,-7}   ", "Date","Total","stdDiv","Date", "Total","stdDiv"))
            End If
            if i < dt.Rows.Count Then
                Console.Write(String.Format("{0,-15}{1,-8}{2,-7}   |", _
	                               Convert.ToDateTime(dt.Rows(i)(0).ToString()).ToString("M/d/yyyy"), _
                                    Convert.ToDouble(dt.rows(i)(1).ToString()).ToString("C"), _
                                    Convert.ToDouble(dt.Rows(i)(2).ToString()).ToString("N1")))
            Else
                Console.Write(String.Format("{0,-15}{1,-8}{2,-7}|", "","",""))
            End If
            if i < dt2.Rows.Count Then
                Console.WriteLine(String.Format("{0,-15}{1,-8}{2,-7}", _
	                               Convert.ToDateTime(dt2.Rows(i)(0).ToString()).ToString("M/d/yyyy"), _
                                    Convert.ToDouble(dt2.rows(i)(1).ToString()).ToString("C"), _
                                    Convert.ToDouble(dt2.Rows(i)(2).ToString()).ToString("N1")))
            Else
                Console.WriteLine("")
            End If
        Next


        Console.WriteLine(vbTab & " _________________________________" & vbNewLine & _
                  vbTab & "| Standard Deviation Of Earnings:  " & Math.Sqrt(Aern).ToString("C") & vbNewLine & _
                  vbTab & "| Standard Deviation Of Spendings: " & Math.Sqrt(Aspn).ToString("C"))
    End Sub
    '=======================================================================================================================
    Sub newProfets()
        Dim cash As String
        Dim douin As List(Of Double) = New List(Of Double)
        Dim col As List(Of String) = New List(Of String)
        Dim tdat As List(Of DateTime) = New List(Of DateTime)

        Console.Clear()
        col.Add("Ernings")
        col.Add("date")


        Console.Write(" ______________________________________________" & vbNewline & _
                  "| InGen Cola                 New Records       |" & vbNewline & _
                  "|----------------------------------------------|" & vbNewline & _
                  "| Enter the total amount collected: ")
        cash = Console.ReadLine()
        If cash = "back" Then
            Return
        End If

        If isNumeric(cash) = True Then
            ' setup data
            douin.Add(Convert.ToDouble(cash))
            tdat.Add(DateTime.Now())
        Else
            ' if Numeric input is bad prompt user and exit
            Console.WriteLine("Not A valid Number. Please try again")
            Return
        End If

        '' insert data to database
        db.insert("CashOut", col, Nothing, douin, Nothing, Nothing, tdat)
    End Sub
    '=======================================================================================================================
    Sub newPurchase()
        Dim Lim As Integer
        Dim Price As Double
        Dim spent As Double = 0
        Dim raw As String
        Dim lofSup As List(Of String)
        Dim douin As List(Of Double) = New List(Of Double)
        Dim douin2 As List(Of Double) = New List(Of Double)
        Dim intin As List(Of Integer) = New List(Of Integer)
        Dim col As List(Of String) = New List(Of String)
        Dim col2 As List(Of String) = New List(Of String)
        Dim tdat As List(Of DateTime) = New List(Of DateTime)

        Console.Clear()
        lofSup = db.showAsList("SELECT SodaName FROM Sodas ORDER BY SodaID;")

        col.Add("Price")
        col.Add("crv")
        col.Add("worth")
        col.Add("net")
        col.Add("sodaId")
        col.Add("cancount")
        col.Add("date")

        col2.Add("price")
        col2.Add("date")

        Console.Write(" ______________________________________________" & vbNewline & _
                  "| InGen Cola                 New Records       |" & vbNewline & _
                  "|----------------------------------------------|" & vbNewLine & _
                  "| Enter the number of units purchased: ")

        raw = Console.ReadLine().Trim()

        If raw = "back" Then
            Return
        End If
        ' conferm that the limit is a integer
	While true
	    Try
                If isNumeric(raw) = True Then
                    Lim = Convert.ToInt32(raw)
                    Exit While
                Else
                    Console.WriteLine("Invalid Input, Please Try again.")
                    Return
                End If
            Catch ex as Exception
                Console.Write("|" &vbNewLine & "| Invalid Input <Entering dencimal value>, " & vbNewLine & "| do you want to continue?(Y/n)" & vbNewLine & "| >>> ")
		raw = Console.ReadLine()
	        raw = raw.ToLower()

	        If raw = "y" Or raw = "yes" Then
                    Console.Write("|" &vbNewLine & "| Enter the number of cans purchased: ")
	            raw = Console.ReadLine()
	        Else
	    	    Return
	        End If
	    End Try
	End While

        ' print out a list of sodas and their indexes
        listSodas()

        For i As Integer = 0 To Lim - 1
            douin.Clear()
            intin.Clear()
            tdat.Clear()

            ' Get Soda id
            While True
                Try
                    Console.Write("| Soda Id: ")
                    intin.Add(Convert.ToInt32(Console.ReadLine().Trim()))
                    Exit While
                Catch xt As Exception
                    Console.WriteLine("| <ERROR: Invalid Input Try Again.>")
                End Try
            End While

            ' get price per unit
            While True
                Try
                    Console.Write("| Soda Price: ")
                    price = Convert.ToDouble(Console.ReadLine().Trim())
                    douin.Add(price)
                    spent += price

                    If price > 5 Then
                        douin.Add(1.2)
                        douin.Add(12)
                        douin.Add(12 - (price + 1.2))
                        intin.Add(24)
                    Else
                        douin.Add(0.6)
                        douin.Add(6)
                        douin.Add(6 - (price + 0.6))
                        intin.Add(12)
                    End If

                    tdat.Add(DateTime.Now())
                    Exit While
                Catch xt As Exception
                    Console.WriteLine("| <ERROR: Invalid Input Try Again.>")
                End Try
            End While

            db.insert("Inventory", col, Nothing, douin, intin, Nothing, tdat)
        Next
        
        douin2.add(spent)
	db.insert("Spendings", col2, Nothing, douin2, Nothing, Nothing, tdat)
        Console.WriteLine("")
    End Sub
    '=======================================================================================================================
    Sub curCashout()
        Dim table As DataTable
        Dim recCnt,YearFrom, YearTo As Integer
        Dim sel As String
        Dim tot As Integer
        Dim mult As Double

        YearTo = -1
        Console.Clear()
        Console.Write("Enter the year to search from: ")
        sel = Console.ReadLine()
        If IsNumeric(sel) = true Then
            YearFrom = Convert.ToInt32(sel)
            If YearFrom = Convert.ToDouble(DateTime.Now.ToString("yyyy")) then
                YearTo = YearFrom
            End If
        Else
            Console.WriteLine("<ERROR: Invalid Input " & sel & " is not a year>")
        End If
        
        If YearTo <= 0 then
            Console.Write("Enter the year to search to:   ")
            sel = Console.ReadLine()
            If IsNumeric(sel) = true Then
                YearTo = Convert.ToInt32(sel)
            Else
                Console.WriteLine("<ERROR: Invalid Input " & sel & " is not a year>")
            End If
        End If

        mult = YearTo - YearFrom
        if mult = 0 then
            mult = 1
        End If

        Console.WriteLine(vbNewLine & "{0,13}{1,13}", "Earnings", "Date")
        Console.WriteLine("-----------------------------")
        table = db.showAsTable("SELECT Ernings, date FROM CashOut WHERE YEAR(date) >= " & YearFrom & " AND YEAR(DATE) <= " & YearTo & ";")

        For r As Integer = 0 To table.Rows.Count - 1
            recCnt = (r + 1)
            Console.Write(String.Format("{0,13}",Convert.ToDouble(table.Rows(r)(0).ToString()).ToString("C")))
	       Console.WriteLine(String.Format("{0,13}",Convert.ToDateTime(table.Rows(r)(1).ToString()).ToString("M/d/yyyy")))
        Next

        tot = Convert.ToDouble(db.getEl("CashOut","SUM(Ernings)", "YEAR(date) >= " & YearFrom & " AND YEAR(DATE) <= " & YearTo & ";"))
        Console.WriteLine(vbNewLine & "        Total: " & tot.ToString("c"))
        Console.WriteLine(" Record Count: " & recCnt)
        Console.WriteLine("    Avg./moth: " & (tot / (12.0 * mult)).ToString("c"))
        Console.WriteLine("")
    End Sub

    '=======================================================================================================================
    Sub curSpendings()
        Dim table As DataTable
        Dim recCnt, YearFrom, YearTo As Integer
        Dim mult As Double
        Dim sel As String
        Dim tot As Integer

        YearTo = -1
        Console.Clear()
        Console.Write("Enter the year to search from: ")
        sel = Console.ReadLine()
        If IsNumeric(sel) = true Then
            YearFrom = Convert.ToInt32(sel)
            If YearFrom = Convert.ToDouble(DateTime.Now.ToString("yyyy")) then
                YearTo = YearFrom
            End If
        Else
            Console.WriteLine("<ERROR: Invalid Input " & sel & " is not a year>")
        End If
        
        If yearTo <= 0 Then
            Console.Write("Enter the year to search to:   ")
            sel = Console.ReadLine()
            If IsNumeric(sel) = true Then
                YearTo = Convert.ToInt32(sel)
            Else
                Console.WriteLine("<ERROR: Invalid Input " & sel & " is not a year>")
            End If
        End If

        mult = yearTo - yearFrom
        if mult = 0 then
            mult = 1
        End If

        Console.WriteLine(vbNewLine & "{0,13}{1,13}", "Spendings", "Date")
        Console.WriteLine("-----------------------------")
        table = db.showAsTable("SELECT price, date FROM Spendings WHERE YEAR(date) >= " & YearFrom & " AND YEAR(date) <= " & YearTo & ";")

        For r As Integer = 0 To table.Rows.Count - 1
            recCnt = (r + 1)
            Console.WriteLine("{0,13}{1,13}", _
                            Convert.ToDouble(table.Rows(r)(0).ToString()).Tostring("C"), _
                            Convert.ToDateTime(table.Rows(r)(1).ToString()).ToString("M/d/yyyy"))
        Next
        tot = Convert.ToDouble(db.getEl("Spendings","SUM(price)", "YEAR(date) >= " & YearFrom & " AND YEAR(DATE) <= " & YearTo & ";"))
        Console.WriteLine(vbNewLine & "        Total: " & tot.ToString("c"))
        Console.WriteLine(" Record Count: " & recCnt)
        Console.WriteLine("    Avg./moth: " & (tot / 12.0).toString("c"))
        Console.WriteLine("")
        Console.WriteLine("")
    End Sub

    '=======================================================================================================================
    Sub curInventory()
        Dim table As DataTable
        Dim lfromt As List(Of String)

        Console.Clear()
        lfromt = New List(Of String)

        table = db.showAsTable("SELECT SodaName, cancount, price+crv AS 'Price', worth " & _
                "FROM Inventory INNER JOIN Sodas ON Sodas.SodaID = Inventory.SodaID WHERE date > ADDDATE(NOW(), -31);")


        Console.WriteLine("Inventory as of " & DateTime.Now.ToString("MMM/yyyy"))
        Console.WriteLine("{0,-15}{1,-10}{2,-11}{3,-11}", "Name", "#Can", "Price", "Expect" & vbNewLine & _
                "------------------------------------------------")
        For r As Integer = 0 To table.Rows.Count - 1
            For c As Integer = 0 To 3
	        If c >= 2 then
		    lfromt.Add(Convert.ToDouble(table.Rows(r)(c).ToString()).ToString("C"))
	        Else
                    lfromt.Add(table.Rows(r)(c).ToString())
	        End If
            Next
            Console.WriteLine("{0,-15}{1,-10}{2,-11}{3,-11}", lfromt(0), lfromt(1), lfromt(2), lfromt(3))
            lfromt.Clear()
        Next
        Console.WriteLine("")
    End Sub
    '=======================================================================================================================
    Sub upInv()
        Dim dt As DataTable
        Dim tblRow As String = ""
        Dim raw As String = ""
        Dim raw2 As String = ""
        Dim rowidLl As Integer = 0
        Dim rowidUl As Integer = 0
        Dim rowlim As Integer = 0
        Dim rowId As Integer = 0
        Dim rowstring As String
        Dim nworth As Double
        Dim col As List(Of String) = New List(Of String)
        Dim col2 As List(Of String) = New List(Of String)
        Dim intin As List(Of Integer) = New List(Of Integer)
        Dim douin As List(Of Double) = New List(Of Double)


        raw = db.getEl("Inventory", "MIN(id)", "YEAR(date) = YEAR(NOW()) ORDER BY date DESC LIMIT 20;")
        rowidLl = Convert.ToInt32(raw)
        raw = db.getEl("Inventory", "MAX(id)", "YEAR(date) = YEAR(NOW()) ORDER BY date DESC LIMIT 20;")
        rowidUl = Convert.ToInt32(raw)

        col.Add("price")
        col.Add("crv")
        col.Add("sodaID")
        col.Add("cancount")

        Console.Clear()
        If showTop20Inventory() = False Then
            Return
        End If

        ' get index to update
        rowID = getRecID(rowidLl, rowidUl)

        ' print out a list of sodas and their indexes
        listSodas()

        ' loop through all colums to update
        Console.WriteLine("Enter New Values, use * to keep current values")
        For i As Integer = 0 To 3
            Console.Write(col(i) & ": ")
            raw = Console.ReadLine()

            ' make sure that values are numbers
            While True
                If raw <> "*" Then
                    If isNumeric(raw) = True Then
                        If i >= 2 Then
                            Try
                                intin.Add(Convert.ToInt32(raw))
                            Catch ex As Exception
                                Continue While
                            End Try
                            col2.Add(col(i))
                        Else
                            douin.Add(Convert.ToDouble(raw))
                            col2.Add(col(i))
                        End If
                        Exit While
                    End If
                Else
                    Exit While
                End If
            End While
        Next

        ' update worth
        If intin.Count() > 0 Then
            raw = intin(intin.Count -1).ToString()
        Else
            raw = db.getEl("Inventory", "cancount", "id = " & rowId)
        End If

        If Col2(0) = "price" Then
            raw2 = douin(0).ToString()
        Else
            raw2 = db.getEl("Inventory", "price", "id = " & rowId)
        End If

        nworth = Convert.ToDouble(raw) * 0.5
        douin.Insert(0, nworth)
        col2.Insert(0, "worth")

        ' update net
        If Col2(0) = "price" Then
            raw2 = douin(0).ToString()
        Else
            raw2 = db.getEl("Inventory", "price", "id = " & rowId)
        End If
        douin.Insert(0, (nworth - Convert.ToDouble(raw2)))
        col2.Insert(0, "net")

        If intin.Count = 0 Then
            intin = Nothing
        End If
        ' update inventory table
        db.update("Inventory", col2, Nothing, douin, intin, Nothing, Nothing, "id = " & rowId)
    End Sub
    '===========================================================================================================================
    Sub upCashOut()
        Dim col As List(Of String) = New List(Of String)
        Dim douin As List(Of Double) = New List(Of Double)
        Dim datin As List(Of DateTime) = New List(Of DateTime)
        Dim recUl As Integer
        Dim recLl As Integer
        Dim recid As Integer
        Dim raw As String

        Console.Clear()
        If showTop20CashOut() = False Then
            Return
        End If

        col.Add("ernings")
        col.Add("date")

        raw = db.getEl("CashOut", "MIN(id)", "YEAR(date) = YEAR(NOW()) ORDER BY date DESC LIMIT 20;")
        recLl = Convert.ToInt32(raw)
        raw = db.getEl("CashOut", "MAX(id)", "YEAR(date) = YEAR(NOW()) ORDER BY date DESC LIMIT 20;")
        recUl = Convert.ToInt32(raw)

        ' get record id
        recid = getRecID(recLl, recUl)
        Console.WriteLine("Enter New Values, use * to keep current values")

        ' get new earning values
        While True
            Console.Write("Earnings: ")
            raw = Console.ReadLine()
            If raw <> "*" Then
                If isNumeric(raw) = True Then
                    douin.Add(Convert.ToDouble(raw))
                    Exit While
                Else
                    Console.WriteLine("<<Error: numeric value is invalid>>")
                End If
            Else
                col.RemoveAt(0)
                douin = Nothing
		Exit While
            End If
        End While

        ' get new date values
        While True
            Console.Write("Date: ")
            raw = Console.ReadLine()
            If raw <> "*" Then
                Try
                    datin.Add(Convert.ToDateTime(raw))
                    Exit While
                Catch xt As Exception
                    Console.WriteLine("<<Error: date value is invalid>>")
                End Try
            Else
                If col.Count > 1 Then
                    col.removeAt(1)
                Else
                    col.removeAt(0)
                End If
                datin = Nothing
		Exit While
            End If
        End While

        ' update the record
        If col.Count > 0 Then
            db.update("CashOut", col, Nothing, douin, Nothing, Nothing, datin, "id=" & recid)
        End If
    End Sub

    '===========================================================================================================================
    Sub upSpending()
        Dim col As List(Of String) = New List(Of String)
        Dim douin As List(Of Double) = New List(Of Double)
        Dim datin As List(Of DateTime) = New List(Of DateTime)
        Dim raw As String
        Dim rowID As Integer
        Dim rowidLl As Integer
        Dim rowidUl As Integer

        Console.Clear()
        If showTop20Spending() = False Then
            Return
        End If

        raw = db.getEl("Spendings", "MIN(id)", "YEAR(date) = YEAR(NOW()) ORDER BY date DESC LIMIT 20;")
        rowidLl = Convert.ToInt32(raw)
        raw = db.getEl("Spendings", "MAX(id)", "YEAR(date) = YEAR(NOW()) ORDER BY date DESC LIMIT 20;")
        rowidUl = Convert.ToInt32(raw)

        col.Add("price")
        col.Add("date")

        ' get record id
        rowID = getRecID(rowidLl, rowidUl)

        ' get new earning values
        While True
            Console.Write("Earnings: ")
            raw = Console.ReadLine()
            If raw <> "*" Then
                If isNumeric(raw) = True Then
                    douin.Add(Convert.ToDouble(raw))
                    Exit While
                Else
                    Console.WriteLine("<<Error: numeric value is invalid>>")
                End If
            Else
                col.RemoveAt(0)
                douin = Nothing
            End If
        End While

        ' get new date values
        While True
            Console.Write("Date: ")
            raw = Console.ReadLine()
            If raw <> "*" Then
                Try
                    datin.Add(Convert.ToDateTime(raw))
                    Exit While
                Catch xt As Exception
                    Console.WriteLine("<<Error: date value is invalid>>")
                End Try
            Else
                If col.Count > 1 Then
                    col.RemoveAt(1)
                Else
                    col.RemoveAt(0)
                End If
                datin = Nothing
            End If
        End While

        If col.Count > 0 Then
            db.update("Spendings", col, Nothing, douin, Nothing, Nothing, datin, "id =" & rowID)
        End If
    End Sub

    '===========================================================================================================================
    Sub delInv()
        Dim raw As String
        Dim recLl As Integer
        Dim recUl As Integer
        Dim rowID As Integer

        Console.Clear()
        If showTop20Inventory() = False Then
            Return
        End If

        raw = db.getEl("Inventory", "MIN(id)", "YEAR(date) = YEAR(NOW()) ORDER BY date DESC LIMIT 20;")
        recLl = Convert.ToInt32(raw)
        raw = db.getEl("Inventory", "MAX(id)", "YEAR(date) = YEAR(NOW()) ORDER BY date DESC LIMIT 20;")
        recUl = Convert.ToInt32(raw)

        ' get record id
        rowID = getRecID(recLl, recUl)

        Console.Write("Are you sure you want to delete record: " & rowID & " (y/n): ")
        raw = Console.ReadLine()
        raw = raw.ToLower()

        If raw = "y" Or raw = "yes" Then
            db.runQuery("DELETE FROM Inventory WHERE id=" & rowID & ";")
        End If
    End Sub
    '===========================================================================================================================
    Sub delCashOut()
        Dim raw As String
        Dim recLl As Integer
        Dim recUl As Integer
        Dim rowID As Integer

        Console.Clear()
        If showTop20CashOut() = False Then
            Return
        End If

        raw = db.getEl("CashOut", "MIN(id)", "YEAR(date) = YEAR(NOW()) ORDER BY date DESC LIMIT 20;")
        recLl = Convert.ToInt32(raw)
        raw = db.getEl("CashOut", "MAX(id)", "YEAR(date) = YEAR(NOW()) ORDER BY date DESC LIMIT 20;")
        recUl = Convert.ToInt32(raw)

        ' get record id
        rowID = getRecID(recLl, recUl)

        Console.Write("Are you sure you want to delete record: " & rowID & " (y/n): ")
        raw = Console.ReadLine()
        raw = raw.ToLower()

        If raw = "y" Or raw = "yes" Then
            db.runQuery("DELETE FROM CashOut WHERE id=" & rowID & ";")
        End If
    End Sub
    '===========================================================================================================================
    Sub delSpending()
        Dim raw As String
        Dim recid As Integer
        Dim rowidLl As Integer
        Dim rowidUl As Integer

        Console.Clear()
        If showTop20Spending() = False Then
            Return
        End If

        raw = db.getEl("Spendings", "MIN(id)", "YEAR(date) = YEAR(NOW()) ORDER BY date DESC LIMIT 20;")
        rowidLl = Convert.ToInt32(raw)
        raw = db.getEl("Spendings", "MAX(id)", "YEAR(date) = YEAR(NOW()) ORDER BY date DESC LIMIT 20;")
        rowidUl = Convert.ToInt32(raw)

        ' get record id
        recid = getRecID(rowidLl, rowidUl)

        Console.Write("Are you sure you want to delete record: " & recid & " (y/n): ")
        raw = Console.ReadLine()
        raw = raw.ToLower()

        If raw = "y" Or raw = "yes" Then
            db.runQuery("DELETE FROM Spendings WHERE id=" & recid & ";")
        End If
    End Sub

    '===========================================================================================================================
    Sub addSoda()
        Dim col As List(Of String) = New List(Of String)
        Dim strin As List(Of String) = New List(Of String)
        Dim sodlist As List(Of String)
        Dim raw As String
        Dim tmp As String
        Dim isCounted As Boolean = False
        Dim ans As String

        sodlist = db.showAsList("SELECT LOWER(SodaName) FROM Sodas;")

        col.Add("SodaName")
        col.Add("purchasFrom")
        col.Add("Note")

        For i As Integer = 0 To col.Count - 1
            Console.Write(col(i) & ": ")
            raw = Console.ReadLine

            If col(i) = "SpdaName" Then
                For u As Integer = 0 To sodList.count - 1
                    If sodlist(i) = raw.ToLower() Then
                        isCounted = True
                        Exit For
                    End If
                Next
                If isCounted = True Then
                    Console.WriteLine("Soda already in list, do you want to ignore? (y/n)")
                    ans = Console.ReadLine
                    ans = ans.toLower()
                    If ans = "y" Or ans = "yes" Then
                        Return
                    Else
                        i -= 1
                    End If
                End If
            End If

            If i >= 0 Then
                strin.Add(raw)
            End If
        Next

        db.Insert("Sodas", col, strin, Nothing, Nothing, Nothing, Nothing)
        Console.WriteLine(strin(0) & " is added to the system.")
    End Sub
    '===========================================================================================================================
    Function getRecID(lowlim As Integer, uplim As Integer) As Integer
        Dim rowId As Integer
        Dim raw As String

        ' get record id
        While True
            Console.WriteLine("Enter the record id: ")
            raw = Console.ReadLine()
            If isNumeric(raw) = True Then
                rowID = Convert.ToInt32(raw)
                If rowID >= lowLim And rowID <= upLim Then
                    Return CInt(rowId)
                Else
                    Console.WriteLine("<<Error: not a valid input>>")
                End If
            Else
                Console.WriteLine("<<Error: not a valid input>>")
            End If
        End While
    End Function
    '===========================================================================================================================
    Function showTop20CashOut() As Boolean
        Dim dt As DataTable
        Dim tmp As String = ""

        dt = db.showAsTable("SELECT * FROM CashOut ORDER BY id DESC LIMIT 20;")

        If dt.Rows.Count > 0 Then
            Console.WriteLine(" _________________________________________" & vbNewLine & _
                      "|     ID      |  EARNINGS   |    DATE     |" & vbNewLine & _
                      "|-----------------------------------------|")

            For r As Integer = 0 To dt.Rows.Count - 1
                tmp = "|"

		tmp &= String.Format("{0,13}",dt.Rows(r)(0).ToString())
		tmp &= String.Format("{0,14}",Convert.ToDouble(dt.Rows(r)(1).ToString()).ToString("C"))
		tmp &= String.Format("{0,14}",Convert.ToDateTime(dt.Rows(r)(2).ToString()).ToString("M/d/yyyy"))

                tmp &= "|"
                Console.WriteLine(tmp)
            Next
        Else
            Console.WriteLine("<<Error: No records to display>>")
            Return False
        End If

        Return True
    End Function

    '===========================================================================================================================
    Function showTop20Inventory() As Boolean
        Dim dt As DataTable
        Dim rowlim As Integer
        Dim tblRow As String = ""

        dt = db.showAsTable("SELECT id, sodaName, cancount, price, crv, worth, net, date FROM Inventory INNER JOIN Sodas ON Inventory.sodaID=Sodas.SodaID WHERE YEAR(date) = YEAR(NOW()) ORDER BY date DESC LIMIT 20;")

        If dt.Rows.Count > 0 Then
            Console.WriteLine("_________________________________________________________________________________________________________________________" & vbNewLine & _
                      "      ID      |    NAME      |  # OF CANS   |    PRICE     |      CRV      |    WORTH     |      NET     |      DATE   " & vbNewLine & _
                      "-------------------------------------------------------------------------------------------------------------------------")

            If dt.Rows.Count() < 20 Then
                rowlim = dt.Rows.Count() - 1
            Else
                rowlim = 19
            End If

            For r As Integer = 0 To rowlim
                For c As Integer = 0 To 7
                    Try
                        If isNumeric(dt.Rows(r)(c).ToString()) = False And c <> 1 Then
                            tblRow &= String.Format("{0,16}", Convert.ToDateTime(dt.Rows(r)(c).ToString()).ToString("M/d/yyyy") & " ")
                        Else
                            tblRow &= String.Format("{0,15}", dt.Rows(r)(c).ToString() & " ")
                        End If
                    Catch xt As Exception
                        tblRow &= String.Format("{0,15}", dt.Rows(r)(c).ToString() & " ")
                    End Try

                Next
                Console.WriteLine(tblRow)
                tblRow = ""
            Next
        Else
            Console.WriteLine("<<Error: No records to display>>")
            Return False
        End If

        Return True
    End Function

    '===========================================================================================================================
    Function showTop20Spending() As Boolean
        Dim dt As DataTable
        Dim dblRow As String
        Dim tblRow As String = ""

        dt = db.showAsTable("SELECT * FROM Spendings ORDER BY id DESC LIMIT 20;")

        If dt.Rows.Count > 0 Then
            Console.WriteLine(" __________________________________________________" & vbNewLine & _
                      "|       ID       |      PRICE     |      DATE      |" & vbNewLine & _
                      "|--------------------------------------------------|")

            For r As Integer = 0 To dt.Rows.Count-1
                tblRow = "|"
                For c As Integer = 0 To 2
                    If isNumeric(dt.Rows(r)(c).ToString()) = False And c <> 1 Then
                        tblRow &= String.Format("{0,17} ", Convert.ToDateTime(dt.Rows(r)(c).ToString()).ToString("M/d/yyyy H:mm") & " ")
                    Else
                        tblRow &= String.Format("{0,15} ", dt.Rows(r)(c).ToString() & " ")
                    End If
                Next
                tblRow &= "|"
                Console.WriteLine(tblRow)
            Next
        Else
            Console.WriteLine("<<Error: No records to display>>")
            Return False
        End If

        Return True
    End Function
    '===========================================================================================================================
    Sub listSodas()
        Dim lofSup As List(Of String)

        lofSup = db.showAsList("SELECT SodaName FROM Sodas ORDER BY SodaID;")

        ' print out a list of sodas and their indexes
        Console.WriteLine(" _____________________________________________" & vbNewline & _
                  "|  ID|  NAME                                  |" & vbNewLine & _
                  "|---------------------------------------------|")
        For i As Integer = 0 To lofSup.Count - 1
            Console.Write("| {0,3}) {1,-25}              |" & vbNewline, (i + 1), lofSup(i))
        Next
        Console.WriteLine("|_____________________________________________|")
    End Sub
    '===========================================================================================================================
    Function isNumeric(raw As String) As Boolean
        Try
            Convert.ToDouble(raw)
            Return True
        Catch xt As Exception
            Return False
        End Try
    End Function
    '===========================================================================================================================
    Function getAvrage(lst As List(Of String)) As Double
        Dim Sum As Double = 0

        For i As Integer=0 To lst.Count-1
            Try
                Sum += Convert.ToDouble(lst(i))
            Catch xt As Exception
                Sum += 0
            End Try
        Next

        Return (Sum / lst.Count)
    End Function
    '===========================================================================================================================
    Function getSum(lst As List(Of String)) As Double
        Dim Sum As Double = 0

        For i As Integer=0 To lst.Count-1
            Try
                Sum += Convert.ToDouble(lst(i))
            Catch xt As Exception
                Sum += 0
            End Try
        Next

        Return Sum
    End Function
End Class
