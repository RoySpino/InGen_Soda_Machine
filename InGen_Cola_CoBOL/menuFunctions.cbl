       $set ilusing "System.Data"
       class-id Soda.menuDriver.

       working-storage section.
           77 mact             type menuActions value new menuActions().
       
       method-id new final public.
       local-storage section.
       procedure division.
           goback.
       end method.
      
       method-id displayNewMenu final private.
       local-storage section.
       procedure division.
           display " ______________________________________________".
           display "| InGen Cola                 New Records       |".
           display "|----------------------------------------------|".
           display "| Profets         Enter dayley profets         |".
           display "| Purchase        Enter cost of soda spent     |".
           display "| CD              Back to Main Menu            |".
           display "|______________________________________________|".
       end method.
      * //////////////////////////////////////////////////////////////////////
       method-id displayStatsMenu final private.
       local-storage section.
       procedure division.
           display " ______________________________________________".
           display "| InGen Cola                 Statistics        |".
           display "|----------------------------------------------|".
           display "| add             Add this years profets       |".
           display "| avgg            Get avg. gross for the year  |".
           display "| avgn            Get avg. net for this year   |".
           display "| avgs            Get avg. spendings           |".
           display "| net             Calculate profets per-can    |".
           display "| sum             Calculate Net Profets        |".
           display "| weekest         Estemate weekly profets      |".
           display "| CD              Back to Main Menu            |".
           display "|______________________________________________|".
       end method.
      * //////////////////////////////////////////////////////////////////////
       
       method-id displayMaintMenu final private.
       local-storage section.
       procedure division.
           display " ______________________________________________".
           display "| InGen Cola                 Data Maintanince  |".
           display "|----------------------------------------------|".
           display "| upinv           Update inventory record      |".
           display "| upcsh           Update cash out records      |".
           display "| upspn           Update spending records      |".
           display "| delinv          Delete inventory record      |".
           display "| delcsh          Delete cash out records      |".
           display "| delspn          Delete spending records      |".
           display "| Addsda          Add new soda flavor          |".
           display "| CD              Back to Main Menu            |".
           display "|______________________________________________|".
       end method.
      * //////////////////////////////////////////////////////////////////////
       method-id displayCurrentMenu final private
       local-storage section.
       procedure division.
           display " ______________________________________________".
           display "| InGen Cola                 Current Records   |".
           display "|----------------------------------------------|".
           display "| CashOut         Show all profets collected   |".
           display "| Spendings       Show spending records        |".
           display "| inventory       Show weekly inventory        |".
           display "| CD              Back to Main Menu            |".
           display "|______________________________________________|".
       end method.
      * //////////////////////////////////////////////////////////////////////
       method-id newMenueMain.
       local-storage section.
           77 sel              type String.
           
       linkage section.
           77 doSubLoop        type Boolean.
       procedure division returning doSubLoop.
           invoke displayNewMenu.
           display ">>> " no advancing.
           accept sel.
           
           move sel::ToLower to sel.
      * chosen to exit 
           if sel = "cd" then
               invoke type Console::Clear()
               move false to doSubLoop
               goback
           end-if.
      * add new records
           if sel = "profets" then
               invoke mact::newProfets()
           else
               if sel = "purchase" then
                   invoke mact::newPurchase()
           end-if.
           move true to doSubLoop.
           goback.
       end method.
       
      * //////////////////////////////////////////////////////////////////////
       method-id statMenue.
       local-storage section.
           77 sel              type String.
           
       linkage section.
           77 doSubLoop        type Boolean.
       procedure division returning doSubLoop.
           invoke displayStatsMenu.
           display ">>> " no advancing.
           accept sel.
           
           move sel::ToLower to sel.
      * chosen to exit 
           if sel = "cd" then
               invoke type Console::Clear()
               move false to doSubLoop
               goback
           end-if.
      * perform statistics functions
           if sel = "net" then
               invoke mact::statNet()
           else
               if sel = "avgg" then
                   invoke mact::statAvgg()
               else
                   if sel = "avgn" then
                       invoke mact::statAvgn()
                   else
                       if sel = "avgs" then
                               invoke mact::statAvgs()
                       else
                           if sel = "add" then
                               invoke mact::statAdd()
                           else
                               if sel = "sum" then
                                   invoke mact::statSum()
                               else
                                   if sel = "weekest"
                                       invoke mact::statWeek()
           end-if.
           move true to doSubLoop.
       end method. 

      * //////////////////////////////////////////////////////////////////////
       method-id maintMenue.
       local-storage section.
           77 sel              type String.
           
       linkage section.
           77 doSubLoop        type Boolean.
       procedure division returning doSubLoop.
           invoke displayMaintMenu.
           display ">>> " no advancing.
           accept sel.
           
           move sel::ToLower to sel.
      * chosen to exit 
           if sel = "cd" then
               invoke type Console::Clear()
               move false to doSubLoop
               goback
           end-if.
      * perform statistics functions
           if sel = "addsda" then
               invoke mact::maintAddSoda()
           else
               if sel = "upinv" then
                   invoke mact::maintUpinv()
               else
                   if sel = "upcsh" then
                       invoke mact::maintupCashOut()
                   else
                       if sel = "upspn" then
                           invoke mact::upSpending()
                       else
                           if sel = "delinv" then
                               invoke mact::delInv()
                           else
                               if sel = "delcsh" then
                                   invoke mact::delCashOut()
                               else
                                   if sel = "delspn"
                                       invoke mact::delSpending()
           end-if.
           move true to doSubLoop.
       end method.  
       
      * //////////////////////////////////////////////////////////////////////
       method-id currentMenue.
       local-storage section.
           77 sel              type String.
           
       linkage section.
           77 doSubLoop        type Boolean.
       procedure division returning doSubLoop.
           invoke displayCurrentMenu.
           display ">>> " no advancing.
           accept sel.
           
           move sel::ToLower to sel.
      * chosen to exit 
           if sel = "cd" then
               invoke type Console::Clear()
               move false to doSubLoop
               goback
           end-if.
      * perform statistics functions
           if sel = "cashout" then
               invoke mact::curCashout
           else
               if sel = "Spendings" then
                   invoke mact::curSpendings
               else
                   if sel = "inventory" then
                       invoke mact::curInventory
           end-if.
           move true to doSubLoop.
       end method.   
       end class.
