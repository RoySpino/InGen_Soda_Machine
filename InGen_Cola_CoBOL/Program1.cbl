       program-id. Program1 as "sodaCOBOL.Program1".

       data division.
       working-storage section.
           77 sel              type String.
           77 menfunc          type sodaCOBOL.menuFunctions value new type sodaCOBOL.menuFunctions.
           77 mainloop         type Boolean.
           77 subloop          type Boolean.
       procedure division.
           move true to mainloop.
           
           perform menu
           until mainloop is equal to false.
           
           goback.
       menu.
           
           display " ______________________________________________".
           display "| InGen Cola                                   |".
           display "|----------------------------------------------|".
           display "| Current         Show Previous entries        |".
           display "| New             Make new Entry               |".
           display "| Stats           Soda Statistics              |".
           display "| Maint           Database Maintanince         |".
           display "| EXIT            Quit Program                 |".
           display "|______________________________________________|".
           display ">>> " no advancing.
           
           accept sel.
           move sel::ToLower() to sel.
           
           if sel = "exit"
               move false to mainloop
               else if sel = "new"
                   move true to subloop
                   invoke type Console::Clear()
                   perform NewMenu
                   until subloop = false
                   else if sel = "stats"
                       move true to subloop
                       invoke type Console::Clear()
                       perform StatsMenu
                       until subloop = false
                       else if sel = "current"
                           move true to subloop
                           invoke type Console::Clear()
                           perform currMenu
                           until subloop =false
           end-if.
       NewMenu.
           display " ______________________________________________".
           display "| InGen Cola                 New Records       |".
           display "|----------------------------------------------|".
           display "| Profets         Enter dayley profets         |".
           display "| Purchase        Enter cost of soda spent     |".
           display "| CD              Back to Main Menu            |".
           display "|______________________________________________|".
           display ">>> " no advancing.
           
           accept sel.
           move sel::ToLower() to sel.
           
           if sel = "cd"
               invoke type Console::Clear()
               move false to subloop 
               else if sel = "purchase"
                   invoke menfunc::newPurchase
                   else if sel = "profets"
                       invoke menfunc::newProfets
           end-if.
       StatsMenu.
           display " ______________________________________________".
           display "| InGen Cola                 Statistics        |".
           display "|----------------------------------------------|".
           display "| add             Add this years profets       |".
           display "| avgg            Get avg. grose for the year  |".
           display "| avgn            Get avg. net for this year   |".
           display "| avgs            Get avg. spendings           |".
           display "| net             Calculate profets per-can    |".
           display "| sum             Calculate Net Profets        |".
           display "| weekest         Estemate weekly profets      |".
           display "| CD              Back to Main Menu            |".
           display "|______________________________________________|".
           display ">>> " no advancing.
           
           accept sel.
           move sel::ToLower() to sel.
           
           if sel = "cd"
               invoke type Console::Clear()
               move false to subloop
           else if sel = "add"
               invoke menfunc::statAdd()
               else if sel = "avgg"
                   invoke menfunc::statAvgg()
                   else if sel = "avgn"
                       invoke menfunc::statAvgn()
                       else if sel = "avgs"
                           invoke menfunc::statAvgs()
                           else if sel = "net"
                               invoke menfunc::statNet()
                               else if sel = "sum"
                                   invoke menfunc::statSum()
                                   else if sel = "weekest"
                                       invoke menfunc::statWeek()
           end-if.
       currMenu.
           display " ______________________________________________".
           display "| InGen Cola                 Current Records   |".
           display "|----------------------------------------------|".
           display "| CashOut         Show all profets collected   |"
           display "| Spendings       Show spending records        |".
           display "| inventory       Show weekly inventory        |".
           display "| Maint           Goto Maintanince Menu        |".
           display "| CD              Back to Main Menu            |".
           display "|______________________________________________|".
           display ">>> " no advancing.
           
           accept sel.
           move sel::ToLower() to sel.
           
           if sel = "cd"
               invoke type Console::Clear()
               move false to subloop
               else if sel = "cashout"
                   invoke menfunc::curCashout()
               else if sel = "spendings"
                   invoke menfunc::curSpendings()
                   else if sel = "inventory"
                       invoke menfunc::curInventory()
                       else if sel = "maint"
                           display ""
           end-if.
           
       end program Program1.
