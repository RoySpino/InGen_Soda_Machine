       class-id sodaCOBOL.ConnectToServer.

       working-storage section.
           77 cnet          type String.
           77 con           type MySql.Data.MySqlClient.MySqlConnection.
          
      * ////////////////////////////////////////////////////////////////////////////////////////////////////
       method-id new final public.
       local-storage section.
       procedure division.
           move "Server=localhost; Database=soda; Uid=root; PASSWORD=vig/&*649/-TD10036em1271;" to cnet.
           move new type MySql.Data.MySqlClient.MySqlConnection to con.
           move cnet to con::ConnectionString.
           goback.
       end method.
      * ////////////////////////////////////////////////////////////////////////////////////////////////////
       method-id open_ final private.
       local-storage section.
           77 ex            type Exception.
       linkage section.
           01 ret           type Boolean.
       procedure division returning ret.
           try
               invoke con::Open
               move true to ret
               goback
           catch ex
               display "ConnectToServer, on Open " ex::Message
           end-try.
           
           move false to ret.
           goback.
       end method.
      * ////////////////////////////////////////////////////////////////////////////////////////////////////
       method-id close_ final private.
       local-storage section.
           77 ex            type Exception.
       linkage section.
            01 ret          type Boolean.
       procedure division returning ret.
           try
               invoke con::Close
               move true to ret
               goback
           catch ex
               display "ConnectToServer, on Close " ex::Message
           end-try.
           
           move false to ret.
           goback.
       end method.
      * ////////////////////////////////////////////////////////////////////////////////////////////////////
       method-id runQuery final public.
       local-storage section.
           77 cmd           type MySql.Data.MySqlClient.MySqlCommand.
       linkage section.
           01 query         type String.
           01 ret           type Boolean.
       procedure division using query returning ret.
           if open_() = true
               try
                   move type MySql.Data.MySqlClient.MySqlCommand::new(query, con) to cmd
                   invoke cmd::ExecuteNonQuery()
                   invoke close_()
                   move true to ret
                   goback
               catch
                   display "ConnectToServer, On runQuery where query= " query
                   move false to ret
               end-try
           end-if.
           goback.
       end method.
      * ////////////////////////////////////////////////////////////////////////////////////////////////////
       method-id getEl final public.
       local-storage section.
           77 ex            type Exception.
           77 cmd           type MySql.Data.MySqlClient.MySqlCommand.
           77 red           type MySql.Data.MySqlClient.MySqlDataReader.
           77 query         type String.
       linkage section.
           01 ret           type String.
           01 tabl          type String.
           01 colun         type String.
           01 constraint    type String.
       procedure division using tabl, colun, constraint returning ret.
           move type String::Format("SELECT {0} FROM {1} WHERE {2};", colun, tabl,constraint) to query.
           display query.
           if open_() = true
               try
                   move type MySql.Data.MySqlClient.MySqlCommand::new(query,con) to cmd
                   move cmd::ExecuteReader() to red
                   
                   perform readLoop
                   until red::Read() = false
                   invoke red::Close()
                   invoke close_()
                   goback
               catch ex
                   display "ConnectToServer, on getEl() where query= " query
                   move null to ret
               end-try
               invoke close_()
           end-if.
           
           goback.
       readLoop.
           move red[0]::ToString() to ret.
           
       end method.
      * ////////////////////////////////////////////////////////////////////////////////////////////////////
       method-id showAsTable final public.
       local-storage section.
           77 cmd           type MySql.Data.MySqlClient.MySqlDataAdapter.
           77 ex            type Exception.
       linkage section.
           01 query         type String.
           01 ret           type System.Data.DataTable value new type System.Data.DataTable.
       procedure division using query returning ret.
           if open_() = true
               try
                   move type MySql.Data.MySqlClient.MySqlDataAdapter::new(query, con) to cmd
                   invoke cmd::Fill(ret)
                   invoke close_()
                   goback
               catch ex
                   display "ConnectToServer, on showAsTable() where query= " query
               end-try
           end-if.
           
           goback.
       end method.
      * ////////////////////////////////////////////////////////////////////////////////////////////////////
       method-id showAsList final public.
       local-storage section.
           77 cmd           type MySql.Data.MySqlClient.MySqlCommand.
           77 red           type MySql.Data.MySqlClient.MySqlDataReader.
           77 ex            type Exception.
       linkage section.
           01 query         type String.
           01 ret           type List[type String] value new type List[type String].
       procedure division using query returning ret.
           if open_() is equal to true
               try
                   move type MySql.Data.MySqlClient.MySqlCommand::new(query,con) to cmd
                   move cmd::ExecuteReader to red
                   
                   perform fillLoop
                   until red::Read() = false
                   invoke red::Close()
                   invoke close_()
               catch ex
                   display "ConnectToServer, on showAsList() where query= " query
               end-try
           end-if

           goback.
       fillLoop.
           invoke ret::Add(red[0]::ToString)
       end method.
      * ////////////////////////////////////////////////////////////////////////////////////////////////////
       method-id insert final public.
       local-storage section.
           77 cmd            type MySql.Data.MySqlClient.MySqlCommand.
           77 query          type String.
           77 colns          type String.
           77 vallst         type String.
           77 indx           type Int32.
           77 lim            type Int32.
           77 ex             type Exception.
       linkage section.
           01 table_         type String.
           01 ret            type Boolean.
           01 strin          type List[type String].
           01 coln           type List[type String].
           01 intin          type List[type Int32].
           01 douin          type List[type Double].
           01 datin          type List[type DateTime].
           01 bolin          type List[type Boolean].
       procedure division using table_, coln, strin, douin, intin, bolin, datin returning ret.
           move type MySql.Data.MySqlClient.MySqlCommand::new() to cmd.
           move "" to colns.
           move "" to vallst.
           
           move coln::Count to lim
           move coln[0] to colns.
           perform varying indx from 1 by 1 until indx >= lim
               move type String::Format("{0},{1}", colns, coln[indx]) to colns
           end-perform.
           
           if strin::Count >0
               move strin::Count to lim
               perform varying indx from 0 by 1 until indx >= lim
                   move type String::Format("{0},?s_{1}",vallst, indx) to vallst
               end-perform
           end-if.
           if douin::Count > 0
               if vallst::Length >1
                   move type String::Format("{0},?d_0",vallst) to vallst
               else move "?d_0" to vallst
               end-if
               move douin::Count to lim
               perform varying indx from 1 by 1 until indx >= lim
                   move type String::Format("{0},?d_{1}",vallst, indx) to vallst
               end-perform
           end-if.
           if intin::Count > 0
               if vallst::Length >1
                   move type String::Format("{0},?i_0",vallst) to vallst
               else move "?i_0" to vallst
               end-if
               move intin::Count to lim
               perform varying indx from 1 by 1 until indx >= lim
                   move type String::Format("{0},?i_{1}",vallst, indx) to vallst
               end-perform
           end-if.
           if datin::Count > 0
               if vallst::Length >1
                   move type String::Format("{0},?da_0",vallst) to vallst
               else move "?da_0" to vallst
               end-if
               move datin::Count to lim
               perform varying indx from 1 by 1 until indx >= lim
                   move type String::Format("{0},?da_{1}",vallst, indx) to vallst
               end-perform
           end-if.
           if bolin::Count > 0
               if vallst::Length >1
                   move type String::Format("{0},?b_0",vallst) to vallst
               else move "?b_0" to vallst
               end-if
               move bolin::Count to lim
               perform varying indx from 1 by 1 until indx >= lim
                   move type String::Format("{0},?b_{1}",vallst, indx) to vallst
               end-perform
           end-if.

           move String::Format("INSERT INTO {0} ({1}) VALUE ({2});", table_, colns, vallst) to query.
           if open_() = true
               try
                   move con to cmd::Connection
                   move query to cmd::CommandText
                   invoke cmd::Prepare()
                   
                   if strin::Count > 0
                       move strin::Count to lim
                       perform varying indx from 0 by 1 until indx >= lim
                           invoke cmd::Parameters::AddWithValue(String::Format("?s_{0}", indx), strin[indx])
                       end-perform
                   end-if
                   if douin::Count > 0
                       move douin::Count to lim
                       perform varying indx from 0 by 1 until indx >= lim
                           invoke cmd::Parameters::AddWithValue(String::Format("?d_{0}", indx), douin[indx])
                       end-perform
                   end-if
                   if intin::Count > 0
                       move intin::Count to lim
                       perform varying indx from 0 by 1 until indx >= lim
                           invoke cmd::Parameters::AddWithValue(String::Format("?i_{0}", indx), intin[indx])
                       end-perform
                   end-if
                   if datin::Count > 0
                       move datin::Count to lim
                       perform varying indx from 0 by 1 until indx >= lim
                           invoke cmd::Parameters::AddWithValue(String::Format("?da_{0}", indx), datin[indx])
                       end-perform
                   end-if
                   if bolin::Count > 0
                       move bolin::Count to lim
                       perform varying indx from 0 by 1 until indx >= lim
                           invoke cmd::Parameters::AddWithValue(String::Format("?b_{0}", indx), bolin[indx])
                       end-perform
                       
                   end-if
                   invoke cmd::ExecuteNonQuery()
                   invoke close_()
                   move true to ret
               catch ex
                   display "ConnectToServer, on insert() where query= " query ex::Message
                   move false to ret
               end-try
           end-if.
           
           goback.
       end method.
      * ////////////////////////////////////////////////////////////////////////////////////////////////////
       method-id update_
       local-storage section.
           77 cmd              type SqlCommand.
           77 query            type String.
           77 colns            type String.
           77 ValLst           type String.
           77 ex               type Exception.
           77 i                pic 9(15).
           77 valCnt           pic 9(15).
       linkage section.
           77 ret              type Boolean.
           77 tbl              type String.
           77 col_             type List[type String].
           77 strin            type List[type String].
           77 douin            type List[type Double].
           77 intin            type List[type Int32].
           77 datin            type List[type DateTime].
           77 bolin            type List[type Boolean].
           77 constraint       type String.
       procedure division using tbl,col_,strin, douin,intin, datin, bolin, constraint returning ret.
           move "" to ValLst.
           move "" to colns.
           move 0 to valCnt.
           
           if strin <> null then
               perform varying i from 0 by 1 until i = strin::Count
                   if ValLst = "" then
                       move type String::Format("{0}=?s_{1}",col_[valCnt],i) to ValLst
                   else
                       move type String::Format("{0},{1}=?s_{2}",ValLst,col_[valCnt],i) to ValLst
                   end-if
                   add 1 to valCnt giving valCnt
               end-perform
           end-if.
           
           if douin <> null then
               perform varying i from 0 by 1 until i = douin::Count
                   if ValLst = "" then
                       move type String::Format("{0}=?d_{1}",col_[valCnt],i) to ValLst
                   else
                       move type String::Format("{0},{1}=?d_{2}",ValLst,col_[valCnt],i) to ValLst
                   end-if
                   add 1 to valCnt giving valCnt
               end-perform
           end-if.
           
           if intin <> null then
               perform varying i from 0 by 1 until i = intin::Count
                   if ValLst = "" then
                       move type String::Format("{0}=?i_{1}",col_[valCnt],i) to ValLst
                   else
                       move type String::Format("{0},{1}=?i_{2}",ValLst,col_[valCnt],i) to ValLst
                   end-if
                   add 1 to valCnt giving valCnt
               end-perform
           end-if.
           
           if datin <> null then
               perform varying i from 0 by 1 until i = datin::Count
                   if ValLst = "" then
                       move type String::Format("{0}=?dt_{1}",col_[valCnt],i) to ValLst
                   else
                       move type String::Format("{0},{1}=?dt_{2}",ValLst,col_[valCnt],i) to ValLst
                   end-if
                   add 1 to valCnt giving valCnt
               end-perform
           end-if.
           
           if bolin <> null then
               perform varying i from 0 by 1 until i = bolin::Count
               if ValLst = "" then
                       move type String::Format("{0}=?b_{1}",col_[valCnt],i) to ValLst
                   else
                       move type String::Format("{0},{1}=?b_{2}",ValLst,col_[valCnt],i) to ValLst
                   end-if
                   add 1 to valCnt giving valCnt
           end-if.
           
           move type String::Format("UPDATE {0} SET ({1}) WHERE {2}", tbl, ValLst, constraint) to query.
           display query.
           
           if open_ = true then
               try
                   move new SqlCommand() to cmd
                   move con to cmd::Connection
                   move query to cmd::CommandText
                   invoke cmd::Prepare()
               
                   if strin = null then
                       perform varying i from 0 by 1 until i = strin::Count
                           invoke cmd::Parameters::AddWithValue(type String::Format("?s_{0}", i), strin[i])
                       end-perform
                   end-if
                    if douin = null then
                       perform varying i from 0 by 1 until i = douin::Count
                           invoke cmd::Parameters::AddWithValue(type String::Format("?d_{0}", i), douin[i])
                       end-perform
                   end-if
                   if intin = null then
                       perform varying i from 0 by 1 until i = intin::Count
                           invoke cmd::Parameters::AddWithValue(type String::Format("?i_{0}", i), intin[i])
                       end-perform
                   end-if
                    if datin = null then
                       perform varying i from 0 by 1 until i = datin::Count
                           invoke cmd::Parameters::AddWithValue(type String::Format("?dt_{0}", i), datin[i])
                       end-perform
                   end-if
                   if bolin = null then
                       perform varying i from 0 by 1 until i = bolin::Count
                           invoke cmd::Parameters::AddWithValue(type String::Format("?b_{0}", i), bolin[i])
                       end-perform
                   end-if
               
                   invoke cmd::ExecuteNonQuery()
                   invoke close_()
                   move true to ret
                   goback
               catch ex
                   display ex::ToString()
                   move false to ret
                   goback
               end-try
           end-if.
           move false to ret.
           goback.
       end method.
       end class.
