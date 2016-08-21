       program-id. Program1 as "Soda.Program1".

       data division.
       working-storage section.
           77 doloop               type Boolean.
           77 doSubLoop            type Boolean.
           77 sel                  type String.
           77 mdriver              type menuDriver.
       
       procedure division.
           move true to doloop.
           move new menuDriver to mdriver.
           perform mainloop
           until doloop is equal to false.
           goback.
       mainloop.
           perform menu.
           display ">>> " no advancing.
           accept sel.
           
           move true to doSubLoop.
           move sel::ToLower to sel.
           
           if sel = "exit" then
               move false to doloop
           else
               if sel = "new" then
                   invoke type Console::Clear()
                   move true to doSubLoop
                   perform newmenu
                   until doSubLoop = false
               else
                   if sel = "stats" then
                       invoke type Console::Clear()
                       move true to doSubLoop
                       perform statsMenuedisp
                       until dosubloop =false
                   else
                       if sel = "maint" then
                           invoke type Console::Clear()
                           move true to doSubLoop
                           perform maintMenueDisp
                           until doSubLoop = false
                       else
                           if sel = "current"
                               invoke type Console::Clear()
                               move true to doSubLoop
                               perform currMenueDisp
                               until doSubLoop = false
           end-if.
           
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
       newmenu.
           move mdriver::newMenueMain to doSubLoop.
               
       statsMenuedisp.
           move mdriver::statMenue to doSubLoop.
           
       maintMenueDisp.
           move mdriver::maintMenue to doSubLoop.
               
       currMenueDisp.
           move mdriver::currentMenue to doSubLoop.
       end program Program1.