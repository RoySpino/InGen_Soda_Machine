       $set ilusing "System.Data"
       $set ilusing "System."
       class-id Soda.menuActions.

       working-storage section.
           77 db           type ConnectToServer value new type ConnectToServer.
           77 coln         type List[type String].
           77 strin        type List[type String].
           77 douin        type List[type Double].
           77 intin        type List[type Int32].
           77 bolin        type List[type Boolean].
           77 datin        type List[type DateTime].
        
      * ////////////////////////////////////////////////////////////////////////////// InstanceMethod
       method-id InstanceMethod final public.
       local-storage section.
       procedure division.
           move new type ConnectToServer to db.
           goback.
       end method.
      * ////////////////////////////////////////////////////////////////////////////// 
       method-id isNumeric final public.
       local-storage section.
           77 ex            type Exception.
       linkage section.
           01 raw           type String.
           01 ret           type Boolean.
           
       procedure division using raw returning ret.
           try 
               invoke type Convert::ToDouble(raw)
               move true to ret
               goback
           catch ex
               move false to ret
           end-try.
           goback.
       end method.
      * ////////////////////////////////////////////////////////////////////////////// 
       method-id getSum final public.
       local-storage section.
           77 i             pic 9(15).
           77 lim           pic 9(15).
           77 ex            type Exception.
       linkage section.
           01 tmp           type List[string].
           01 ret           type Double.
           
       procedure division using tmp returning ret.
           subtract 1 from tmp::Count giving lim.
           move 0 to ret.
           perform varying i from 0 by 1 until i > lim 
               try 
                   add type Convert::ToDouble(tmp[i]) to ret giving ret
               catch ex
               end-try
           End-perform.
           goback.
       end method. 
       
      * ////////////////////////////////////////////////////////////////////////////// 
       method-id getAvg final public.
       local-storage section.
       linkage section.
           01 tmp           type List[string].
           01 ret           type Double.
           
       procedure division using tmp returning ret.
           move getSum(tmp) to ret.
           compute ret = ret / tmp::Count.
           goback.
       end method.
      * ////////////////////////////////////////////////////////////////////////////// 
       method-id ClearAllLists final public.
       local-storage section.
       procedure division.
           move new Type List[type String] to strin.
           move new Type List[type Int32] to intin.
           move new Type List[type Double] to douin.
           move new Type List[type Boolean] to bolin.
           move new Type List[type DateTime] to datin.
           move new Type List[type String] to coln.
           goback.
       end method. 
      * //////////////////////////////////////////////////////////////////////////////
       method-id showTop20Inventory final private
       local-storage section.
           77 dt            type System.Data.DataTable value new type System.Data.DataTable.
           77 rowLim        pic 9(9).
           77 r             pic 999.
       linkage section.
           01 ret           type Boolean value false.
       procedure division returning ret.
           move db::showAsTable("SELECT id, sodaName, cancount, price, crv, worth, net, date FROM Inventory INNER JOIN Sodas ON Inventory.sodaID=Sodas.SodaID WHERE YEAR(date) = YEAR(GetDate()) ORDER BY date DESC LIMIT 20;") to dt.
           display "_________________________________________________________________________________________________________________________".
           display "      ID      |    NAME      |  # OF CANS   |    PRICE     |      CRV      |    WORTH     |      NET     |      DATE   " .
           display "-------------------------------------------------------------------------------------------------------------------------".
           if dt::Rows::Count >0
               If dt::Rows::Count < 20
                    subtract 1 from dt::Rows::Count giving rowlim
               Else
                    move 19 to rowlim
               End-If
               perform varying r from 0 by 1 until r >= rowLim
                       display type String::Format("{0,15}{1,15}{2,15}{3,15}{4,15}{5,15}{6,15}{7,16}",
                       dt::Rows[r][0]::ToString(), dt::Rows[r][1]::ToString(), dt::Rows[r][2]::ToString(),
                       Type Convert::ToDouble(dt::Rows[r][3]::ToString())::ToString("C"), dt::Rows[r][4]::ToString(), dt::Rows[r][5]::ToString(),
                       dt::Rows[r][6]::ToString(), dt::Rows[r][7]::ToString()) 
               end-perform
           else
               goback
           end-if.
           move true to ret.
       end method.
      * //////////////////////////////////////////////////////////////////////////////
       method-id listSodas final private.
       local-storage section.
           77 lofSup        type List[type String] value new type List[type String].
           77 indx          pic 9(9).
           77 ids           type Int32.
           77 outs          type String.
       procedure division.
           move db::showAsList("SELECT SodaName FROM Sodas ORDER BY SodaID;") to lofSup.
           display " _____________________________________________".
           display "|  ID|  NAME                                  |".
           display "|---------------------------------------------|".
           perform varying indx from 0 by 1 until indx >= lofSup::Count
               add 1 to indx giving ids
               move type String::Format("| {0,3}) {1,-25}              |", ids::ToString("N0"), lofSup[indx]) to outs
               display outs
           end-perform.
           goback.
       end method.
       
      * ////////////////////////////////////////////////////////////////////////////// 
       method-id newPurchase final public.
       local-storage section.
           77 raw            type String.
           77 lim            pic 9(10).
           77 indx           pic 9(10).
           77 tdat           type List[type DateTime] value new type List[type DateTime].
           77 douin2         type List[type Double] value new type List[type Double].
           77 col2           type List[type String] value new type List[type String].
           77 continAdd      type Boolean.
           77 spent          type Double.
           77 ex             type Exception.
           77 price          type Double.
       procedure division.
           invoke type Console::Clear().
           invoke col2::Add("price").
           invoke col2::Add("date").
           invoke tdat::Clear.
           display " ______________________________________________".
           display "| InGen Cola                 New Records       |".
           display "|----------------------------------------------|".
           display "| Enter the number of cans purchased: " no advancing.
           
           accept raw.
           move raw::Trim to raw.
           
           if isNumeric(raw) = true
               move type Convert::ToInt32(raw) to lim
           else
               display "Invalid Input, Please Try again."
               goback
           end-if
           
           invoke listSodas.
           perform varying indx from 0 by 1 until indx >= lim
               invoke ClearAllLists
               invoke coln::Add("Price")
               invoke coln::Add("crv")
               invoke coln::Add("worth")
               invoke coln::Add("net")
               invoke coln::Add("sodaId")
               invoke coln::Add("cancount")
               invoke coln::Add("date")
               move true to continAdd
               perform getSodas
               until continAdd = false
               move true to continAdd 
               perform getPPunit
               until continAdd = false
           end-perform.
           invoke ClearAllLists
           invoke douin2::Add(spent).
           invoke db::insert("Spendings", col2, strin, douin2, intin, tdat, bolin).
           goback.
       getSodas.
           try
               display "| Soda Id: " no advancing
               accept raw
               invoke intin::Add(type Convert::ToInt32(raw))
               move false to continAdd
           catch ex
               display "| <ERROR: Invalid Input Try Again.>"
           end-try.
       getPPunit.
           try
               display "| Soda Price: " no advancing
               accept raw
               invoke tdat::Clear()
               move type Convert::ToDouble(raw) to price
               add price to spent
               invoke douin::Add(price)
               if price > 5
                   invoke douin::Add(1.2)              *> CRV
                   invoke douin::Add(12)               *> expected returns
                   compute price = 12 - (Price + 1.2)  *> Net Returns
                   invoke douin::Add(price)            *> costs
                   invoke intin::Add(24)               *> number of cans
               else
                   invoke douin::Add(0.6)              *> CRV
                   invoke douin::Add(6)                *> expected returns
                   compute price = 6 - (Price + 0.6)   *> Net Returns
                   invoke douin::Add(price)            *> costs
                   invoke intin::Add(12)               *> number of cans
               end-if
               invoke tdat::Add(type DateTime::Now)
               invoke db::insert("Inventory", coln, strin, douin, intin,tdat, bolin)
               move false to continAdd
           catch ex
               display "| <ERROR: Invalid Input Try Again.>"
           end-try.
       end method.
      * ////////////////////////////////////////////////////////////////////////////// 
       method-id newProfets final public.
       local-storage section.
           77 cash              type Double.
           77 inpt              type String.
       procedure division.
           invoke type Console::Clear().
           invoke ClearAllLists
           invoke coln::Add("Ernings").
           invoke coln::Add("date").
           display " ______________________________________________".
           display "| InGen Cola                 New Records       |".
           display "|----------------------------------------------|".
           display "| Enter the total amount collected: " no advancing.
           
           accept inpt.
           if isNumeric(inpt) = true
               move type Convert::ToDouble(inpt) to cash
               invoke douin::Add(cash)
               invoke datin::Add(type DateTime::Now)
           else
               display "Not A valid Number. Please try again"
               goback
           end-if.
           invoke db::insert("CashOut", coln, strin, douin, intin,datin, bolin)
           goback.
       end method.
      * ///////////////////////////////////////////////////////////////////////////////
       method-id curCashout final public.
       local-storage section.
           77 tabl              type System.Data.DataTable value new type System.Data.DataTable.
           77 indx              pic 9(19).
           77 lim               pic 9(19).
       procedure division.
           invoke type Console::Clear().
           display type String::Format("{0,13}{1,13}", "Earnings", "Date").
           display "-----------------------------".
           move db::showAsTable("SELECT Ernings, date FROM CashOut WHERE YEAR(date) = YEAR(GetDate());") to tabl.
           
           if tabl <> null
               move tabl::Rows::Count to lim
               perform varying indx from 0 by 1 until indx >= lim
                   display type String::Format("{0,13}{1,13}", 
                                               Type Convert::ToDouble( tabl::Rows[indx][0]::ToString())::ToString("C"), 
                                               Type Convert::ToDateTime( tabl::Rows[indx][1]::ToString())::ToString("M/d/yyyy"))
               end-perform
           end-if.
           display "".
       end method.
      * ///////////////////////////////////////////////////////////////////////////////
       method-id curSpendings final public.
       local-storage section.
           77 tabl              type System.Data.DataTable value new type System.Data.DataTable.
           77 indx              pic 9(19).
           77 lim               pic 9(19).
       procedure division.
           invoke type Console::Clear().
           display type String::Format("{0,13}{1,13}", "Spendings", "Date").
           display "-----------------------------".
           move db::showAsTable("SELECT price, date FROM Spendings WHERE YEAR(date) = YEAR(GetDate());") to tabl.
           
           if tabl <> null
               move tabl::Rows::Count to lim
               perform varying indx from 0 by 1 until indx >= lim
                   display type String::Format("{0,13}{1,13}", 
                                               Type Convert::ToDouble( tabl::Rows[indx][0]::ToString())::ToString("C"), 
                                               Type Convert::ToDateTime( tabl::Rows[indx][1]::ToString())::ToString("M/d/yyyy"))
               end-perform
           end-if.
           display "".
       end method. 
      * ///////////////////////////////////////////////////////////////////////////////
       method-id curInventory final public.
       local-storage section.
           77 tabl              type System.Data.DataTable value new type System.Data.DataTable.
           77 indx              pic 9(19).
           77 lim               pic 9(19).
       procedure division.
           invoke type Console::Clear().
           display type String::Format("{0,15}{1,10}{2,11}{3,11}", "Name", "#Can", "Price", "Expect").
           display "------------------------------------------------"
           move db::showAsTable("SELECT SodaName, cancount, (price+crv) AS 'Price', worth FROM Inventory INNER JOIN Sodas ON Sodas.SodaID = Inventory.SodaID WHERE date > ADDDATE(GetDate(), -31);") to tabl.
           
           if tabl <> null
               move tabl::Rows::Count to lim
               perform varying indx from 0 by 1 until indx >= lim
                   display type String::Format("{0,15}{1,10}{2,11}{3,11}", 
                                               tabl::Rows[indx][0]::ToString(),
                                               Type Convert::ToDouble(tabl::Rows[indx][1]::ToString())::ToString("N0"),
                                               Type Convert::ToDouble( tabl::Rows[indx][2]::ToString())::ToString("C"), 
                                               Type Convert::ToDouble( tabl::Rows[indx][3]::ToString())::ToString("C"))
               end-perform
           end-if.
           display "".
       end method.  
      * ///////////////////////////////////////////////////////////////////////////////
       method-id statNet final public.
       local-storage section.
           77 tabl              type System.Data.DataTable value new type System.Data.DataTable.
           77 indx              pic 9(19).
           77 subindx           pic 9(19).
           77 rowlim            pic 9(19).
       procedure division.
           invoke type Console::Clear().
           
           move db::showAsTable
           ("SELECT SodaName, (Price + crv), cancount, ((Price + crv) / cancount), Worth, (Worth - (Price + crv)) FROM Inventory INNER JOIN Sodas ON Sodas.SodaID = Inventory.sodaID WHERE YEAR(date) = YEAR(GetDate()) ORDER BY Inventory.id DESC;") 
           to tabl.
           
           perform varying indx from 0 by 1 until indx >= tabl::Rows::Count
               display "___________________________________________________________________________________________"
               display "    NAME      |    COSTS     |  # OF CANS   | PRICE PER CAN |   SALES       |      NET     "
               display "-------------------------------------------------------------------------------------------"
               
               if indx + 19 < tabl::Rows::Count
                   move 20 to rowlim
               else
                   compute rowlim = tabl::Rows::Count - 1
               end-if
               
               perform varying subindx from 0 by 1 until subindx >= rowlim
                   display type String::Format("{0,14}{1,15}{2,15}{3,16}{4,16}{5,16}", 
                                           tabl::Rows[subindx][0]::ToString(),
                                           Type Convert::ToDouble(tabl::Rows[subindx][1]::ToString())::ToString("C"),
                                           Type Convert::ToDouble( tabl::Rows[subindx][2]::ToString())::ToString("N0"), 
                                           Type Convert::ToDouble( tabl::Rows[subindx][3]::ToString())::ToString("C"),
                                           Type Convert::ToDouble( tabl::Rows[subindx][4]::ToString())::ToString("C"),
                                           Type Convert::ToDouble( tabl::Rows[subindx][5]::ToString())::ToString("C"))
               end-perform
               add rowlim to indx
           end-perform.
           display "".
       end method. 
      * //////////////////////////////////////////////////////////////////////////////////////////////////////
       method-id statAdd final public
       local-storage section.
           77 spending          type List[type String] value new type List[type String].
           77 ern               type Double value 0.
       procedure division.
           move db::showAsList("SELECT ernings FROM CashOut WHERE YEAR(date) = YEAR(GetDate());") to spending.
           invoke type Console::Clear().
           move getSum(spending) to ern.
           display "     _________________________________".
           display type String::Format("    / Average Grose: {0}", (ern::ToString("C"))).
       end method.
      * /////////////////////////////////////////////////////////////////////////////////////////////////////////////
       method-id statAvgg final public
       local-storage section.
           77 spending          type List[type String] value new type List[type String].
           77 ern               type Double value 0.
       procedure division.
           move db::showAsList("SELECT ernings FROM CashOut WHERE YEAR(date) = YEAR(GetDate());") to spending.
           invoke type Console::Clear().
           move getAvg(spending) to ern.
           display "     _________________________________".
           display type String::Format("    / Average Grose: {0}", (ern::ToString("C"))).
       end method.
      * ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
       method-id statAvgn final public
       local-storage section.
           77 spending           type List[type String] value new type List[type String].
           77 earnings           type List[type String] value new type List[type String].
           77 ern                type Double value 0.
           77 spn                type Double value 0.
           77 ex                type Exception.
           77 idx                pic 9(13) value 0.
       procedure division.
           move db::showAsList("SELECT price From Spendings WHERE YEAR(date) = YEAR(GetDate());") to spending.
           move db::showAsList("SELECT ernings FROM CashOut WHERE YEAR(date) = YEAR(GetDate());") to earnings.
           invoke type Console::Clear().
           move getSum(earnings) to ern.
           move getSum(spending) to spn.
           compute ern = (ern + spn) / ((spending::Count + earnings::Count) / 2).
           display "     _________________________________".
           display type String::Format("    / Current Net Profets: {0}", (ern::ToString("C"))).
       end method.
      * ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
       method-id statAvgs final public
       local-storage section.
           77 spending          type List[type String] value new type List[type String].
           77 ern               type Double value 0.
       procedure division.
           invoke type Console::Clear().
           move db::showAsList("SELECT price From Spendings WHERE YEAR(date) = YEAR(GetDate());") to spending.
           move getAvg(spending) to ern.
           display "     _________________________________".
           display type String::Format("    / Average Grose: {0}", (ern::ToString("C"))).
       end method.
      * ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
       method-id statSum final public
       local-storage section.
           77 spending           type List[type String] value new type List[type String].
           77 earnings           type List[type String] value new type List[type String].
           77 ern                type Double value 0.
           77 spn                type Double value 0.
       procedure division.
           move db::showAsList("SELECT price From Spendings WHERE YEAR(date) = YEAR(GetDate());") to spending.
           move db::showAsList("SELECT ernings FROM CashOut WHERE YEAR(date) = YEAR(GetDate());") to earnings.
           invoke type Console::Clear().
           move getSum(earnings) to ern.
           move getSum(spending) to spn.
           subtract spn from ern giving ern.
           display "     _________________________________".
           display type String::Format("    / Current Total Profets:  {0}", (ern::ToString("C"))).
       end method. 
      * /////////////////////////////////////////////////////////////////////////////////////////////////////////////
        method-id statWeek final public
        local-storage section.
           77 tb                   type System.Data.DataTable value new System.Data.DataTable.
           77 idx                  pic 9(13).
        procedure division.
           invoke type Console::Clear().
           move db::showAsTable("select SUM(cancount), SUM(worth) FROM Inventory WHERE YEAR(date) = YEAR(GetDate()) GROUP BY date;") to tb.
           if tb <> null
               perform varying idx from 0 by 1 until idx is greater than or equal to tb::Rows::Count
                   display type String::Format("Total Number of cans: {0,-10}", tb::Rows[idx][0]::ToString())
                   display type String::Format("Estimated profets:    ${0,-10}", tb::Rows[idx][1]::ToString())
                   display ""
               end-perform
           end-if.
        end method.
      * ///////////////////////////////////////////////////////////////////////////////////////////////////////////
       method-id maintAddSoda final public
       local-storage section.
           77 sodlist              type List[string].
           77 raw                  type String.
           77 isCounted            type Boolean.
           77 ans                  type String.
           77 i                    pic 9(15).
           77 u                    pic 9(15).
       procedure division.
           invoke ClearAllLists.
           invoke type Console::Clear.
           move db::showAsList("SELECT LOWER(SodaName) FROM Sodas;") to sodlist.
           invoke coln::Add("SodaName").
           invoke coln::Add("purchasFrom").
           invoke coln::Add("Note").
           
           perform varying i from 0 by 1 until i >= coln::Count
               display coln[i]  ": " no advancing
               accept raw 
               if coln[i] = "SpdaName"
                   perform varying u from 0 by 1 until u >= sodlist::Count
                       if sodlist[u] = raw::ToLower()
                           move true to isCounted
                           move sodlist::Count to u
                       End-if
                   end-perform
                   if isCounted = true
                       display "Soda already in list, do you want to ignore? (y/n) " no advancing
                       accept ans
                       move ans::ToLower() to ans
                       if ans = "y" Or ans = "yes"
                           move coln::Count to i
                       else
                           subtract 1 from i giving i
                       end-if
                   End-if
               end-if
               if i is greater than or equal to 0
                   invoke strin::Add(raw)
               end-if
           End-perform.
           invoke db::insert("Sodas", coln, strin, douin, intin, datin, bolin).
           display strin[0] " is added to the system.".
           goback.
       end method.
      * ///////////////////////////////////////////////////////////////////////////////////////////////////////////
       method-id maintUpinv final public
       local-storage section.
           77 recUl                pic 9(15).
           77 recLl                pic 9(15).
           77 recid                pic 9(15).
           77 raw                  type String.
           77 i                    pic 9(15).
           77 u                    pic 9(15).
       procedure division.
           invoke ClearAllLists().
           invoke coln::Add("ernings").
           invoke coln::Add("date").
           
           goback.
       end method.
      * ///////////////////////////////////////////////////////////////////////////////////////////////////////////
       method-id upinv final public
       local-storage section.
           77 tblRow               type String value "".
           77 raw                  type String value "".
           77 raw2                 type String value "".
           77 rowidLl              pic 9(7).
           77 rowidUl              pic 9(7).
           77 rowlim               pic 9(10).
           77 row_Id               pic 9(10).
           77 idx                  pic 9(10)
           77 nworth               pic 9(5)v99.
           77 col1                 type List[type String] value new type List[type String].
           77 col2                 type List[type String] value new type List[type String].
           77 ex                   type Exception.
       procedure division.
           invoke type Console::Clear().
           move db::getEl("Inventory", "MIN(id)", "YEAR(date) = YEAR(GetDate()) ORDER BY date DESC LIMIT 20;") to raw.
           move type Convert::ToInt32(raw) to rowidLl.
           move db::getEl("Inventory", "MAX(id)", "YEAR(date) = YEAR(GetDate()) ORDER BY date DESC LIMIT 20;") to raw.
           move type Convert::ToInt32(raw) to rowidUl.
           
           invoke col1::Add("price").
           invoke col1::Add("crv").
           invoke col1::Add("sodaID").
           invoke col1::Add("cancount").
           
           if showTop20Inventory = false
               goback
           end-if.
           display "Enter New Values, use * to keep current values".
           perform varying idx from 0 by 1 until idx >=3
               display col1[idx] & ": "
               accept raw
      *        perform subloop
      *        until raw is not equal to "*"
           end-perform.
      *update worth    
           if intin::Count() > 1
               move intin[1]::ToString() to raw
           else
               move db::getEl("Inventory", "cancount", "id = " & row_Id) to raw
           end-if.
           if col2[0] = "price"
               move douin[0]::ToString() to raw2
           else
               move db::getEl("Inventory", "price", "id = " & row_Id) to raw2
           end-if.
           display "raw: " & raw & "  raw2: " & raw2.
           multiply type Convert::ToDouble(raw) by 0.5 giving nworth.
           invoke douin::Insert(0, nworth).
           invoke col2::Insert(0, "worth").
           if col2[0] = "price"
               move douin[0]::ToString() to raw2
           else
               move db::getEl("Inventory", "price", "id = " & row_Id) to raw2
           end-if.
           
           subtract type Convert::ToDouble(raw2) from nworth giving nworth.
           invoke douin::Insert(0, nworth)
           invoke col2::Insert(0, "net")
           If intin::Count = 0 Then
               move new List[type Int32] to intin
           End-If
      * update inventory table
      *     invoke db::update("Inventory", col2, strin, douin, intin, bolin, datin, "id = " & row_Id).
      *subloop.
           if isNumeric(raw) = true 
               if idx >= 2
                   try
                       invoke intin::Add(type Convert::ToInt32(raw))
                   catch ex
                   end-try
                   invoke col2::Add(col1[idx])
               else
                   invoke douin::Add(type Convert::ToDouble(raw))
                   invoke col2::Add(col1[idx])
               end-if
               move "*" to raw
           end-if.
       end method.
       end class.
