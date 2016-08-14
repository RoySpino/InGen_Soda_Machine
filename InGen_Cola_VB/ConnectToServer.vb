rem    vbnc -r:/usr/lib/cli/MySql.Data-6.1/MySql.Data.dll connect.vb

Imports MySql.Data.MySqlClient
Imports MySql.Data
Imports System.Data
Imports System.Collections.Generic
Imports System.Collections

Public Class ConnectToServer
    Dim cnet As String
    Dim con As MySqlConnection

    Public Sub New()
         cnet = "Database=soda;Data Source=localhost;User Id=root;Password=toor"
         con = New MySqlConnection(cnet)
    End Sub

    Private Function open() As Boolean
        Try
            con.Open()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Function close() As Boolean
        Try
            con.Close()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Sub runquery(query As String)
		Dim cmd As MySqlCommand

        Try
            If open() = False Then
                Return
            End If

			cmd = New MySqlCommand(query, con)
            cmd.ExecuteNonQuery()

            close()
        Catch ex As Exception
            MsgBox("Unable to run query" & vbNewLine & ex.ToString())
        End Try
    End Sub

    Function showAsTable(query As String) As DataTable
        Dim tmp As DataTable
        Dim cmd As MySqlDataAdapter

        Try
            If open() = False Then
                Return Nothing
            End If

            cmd = New MySqlDataAdapter(query, con)
            tmp = New DataTable()
            cmd.Fill(tmp)
            
            close()
            Return tmp

        Catch ex As Exception
            Console.WriteLine(ex.ToString())
        End Try

        Return Nothing
    End Function

    Public Function showAsList(query As String) As List(Of String)
        Dim ret As List(Of String)
        Dim red As MySqlDataReader

        If open() = True Then
            Try
                Dim cmd As MySqlCommand
                cmd = New MySqlCommand(query, con)
                ret = New List(Of String)

                red = cmd.ExecuteReader()
                While red.Read()
                    ret.Add(red(0).ToString())
                End While
                red.Close()
                close()
                Return ret
            Catch ex As Exception
                Console.WriteLine(ex.ToString())
            End Try
            close()
        End If

        Return Nothing
    End Function

    Public Function getEl(table As String, col As String, constraint As String) As String
        Dim ret As String = ""
        Dim query As String
        Dim red As MySqlDataReader

        query = "SELECT " + col + "  FROM " + table + " WHERE " + constraint + ";"

        If open() = True Then
            Try
                Dim cmd As MySqlCommand = New MySqlCommand(query, con)
                red = cmd.ExecuteReader()

                While red.Read()
                    ret = red(0).ToString()
                End While

                red.Close()
                close()
                Return ret
            Catch ex As Exception
                Console.WriteLine(ex.ToString())
            End Try
        End If

        Return ""
    End Function
	
	Public Function insert(table as string, col as list(Of String), strin as list(Of String), douin as List(Of Double), intin as list(Of Integer), bolin as list(Of Boolean), datin as list(of DateTime)) As Boolean
		Dim cmd As New MySqlCommand()
		Dim query As String
		Dim colmns As String =""
		Dim ValLst as String =""
		
		For i as Integer=0 To col.Count-1
			colmns += col(i)
			
			if i < col.Count-1 then
				colmns += ","
			end if
		Next
		
		if not strin is Nothing then
			For i as integer =0 to strin.Count-1
				ValLst += "?s" & i
				
				if i < strin.Count-1 then
					ValLst += ","
				end if
			Next
		end if
		
		if not douin is Nothing then
			if ValLst.Length > 0 then
				ValLst += ","
			end if
		
			For i as integer =0 to douin.Count-1
				ValLst += "?d" & i
				
				if i < douin.Count-1 then
					ValLst += ","
				end if
			Next
		end if
		
		if not intin is Nothing then
			if ValLst.Length > 0 then
				ValLst += ","
			end if
		
			For i as integer =0 to intin.Count-1
				ValLst += "?i" & i
				
				if i < intin.Count-1 then
					ValLst += ","
				end if
			Next
		end if
		
		if not bolin is Nothing then
			if ValLst.Length > 0 then
				ValLst += ","
			end if
		
			For i as integer =0 to bolin.Count-1
				ValLst += "?b" & i
				
				if i < bolin.Count-1 then
					ValLst += ","
				end if
			Next
		end if
		
		if not datin is Nothing then
			if ValLst.Length > 0 then
				ValLst += ","
			end if
		
			For i as integer =0 to datin.Count-1
				ValLst += "?dt" & i
				
				if i < datin.Count-1 then
					ValLst += ","
				end if
			Next
		end if
		
		if open() = true then
			try
				cmd = new MySqlCommand()
				cmd.Connection = con
				
				query = "INSERT INTO " & table & " (" & colmns & ") VALUES(" & ValLst & ");"
				cmd.CommandText = query
				cmd.Prepare()
				
				if not strin is Nothing then
					For i as integer=0 to strin.Count-1
						cmd.Parameters.AddWithValue("?s" & i, strin(i))
					Next
				end if
				
				if not douin is Nothing then
					For i as integer=0 to douin.Count-1
						cmd.Parameters.AddWithValue("?d" & i, douin(i))
					Next
				end if
				
				if not intin is Nothing then
					For i as integer=0 to intin.Count-1
						cmd.Parameters.AddWithValue("?i" & i, intin(i))
					Next
				end if			
				
				if not bolin is Nothing then
					For i as integer=0 to bolin.Count-1
						cmd.Parameters.AddWithValue("?b" & i, bolin(i))
					Next
				end if

				if not datin is Nothing then
					For i as integer=0 to datin.Count-1
						cmd.Parameters.AddWithValue("?dt" & i, datin(i))
					Next
				end if
				
				cmd.ExecuteNonQuery()
				con.Close()
				return true
				
			Catch ex As MySqlException
				Console.WriteLine(query)
				Console.WriteLine(ex.ToString())
			End Try
		end if

		return false
    End Function
   
    Public Function update(table as string, col as list(Of String), strin as list(Of String), douin as List(Of Double), intin as list(Of Integer), bolin as list(Of Boolean), datin as list(of DateTime), constraint as string) As Boolean
		Dim cmd As New MySqlCommand()
		Dim query As String
		Dim colCnt as integer =0
		Dim ValLst as String =""
		
		try
			if not strin is Nothing then
				For i as integer =0 to strin.Count-1
					ValLst += col(colCnt) & "=?s" & i
					
					if i < strin.Count-1 then
						ValLst += ","
					end if
					
					colCnt = colCnt + 1
				Next
			end if
			
			if not douin is Nothing then
				if ValLst.Length > 0 then
					ValLst += ","
				end if
			
				For i as integer =0 to douin.Count-1
					ValLst += col(colCnt) & "=?d" & i
					
					if i < douin.Count-1 then
						ValLst += ","
					end if
					
					colCnt = colCnt + 1
				Next
			end if
			
			if not intin is Nothing then
				if ValLst.Length > 0 then
					ValLst += ","
				end if
			
				For i as integer =0 to intin.Count-1
					ValLst += col(colCnt) & "=?i" & i
					
					if i < intin.Count-1 then
						ValLst += ","
					end if
					
					colCnt = colCnt + 1
				Next
				
				colCnt = colCnt + 1
			end if
			
			if not bolin is Nothing then
				if ValLst.Length > 0 then
					ValLst += ","
				end if
			
				For i as integer =0 to bolin.Count-1
					ValLst += col(colCnt) & "=?b" & i
					
					if i < bolin.Count-1 then
						ValLst += ","
					end if
					
					colCnt = colCnt + 1
				Next
			end if
			
			if not datin is Nothing then
				if ValLst.Length > 0 then
					ValLst += ","
				end if
			
				For i as integer =0 to datin.Count-1
					ValLst += col(colCnt) & "=?dt" & i
					
					if i < datin.Count-1 then
						ValLst += ","
					end if
					
					colCnt = colCnt + 1
				Next
			end if
			
			if open() = true then
				try
					cmd = new MySqlCommand()
					cmd.Connection = con
					
					query = "UPDATE " & table & " SET " & ValLst & " WHERE " & constraint & ";"
					cmd.CommandText = query
					cmd.Prepare()
					
					if not strin is Nothing then
						For i as integer=0 to strin.Count-1
							cmd.Parameters.AddWithValue("?s" & i, strin(i))
						Next
					end if
					
					if not douin is Nothing then
						For i as integer=0 to douin.Count-1
							cmd.Parameters.AddWithValue("?d" & i, douin(i))
						Next
					end if
					
					if not intin is Nothing then
						For i as integer=0 to intin.Count-1
							cmd.Parameters.AddWithValue("?i" & i, intin(i))
						Next
					end if			
					
					if not bolin is Nothing then
						For i as integer=0 to bolin.Count-1
							cmd.Parameters.AddWithValue("?b" & i, bolin(i))
						Next
					end if

					if not datin is Nothing then
						For i as integer=0 to datin.Count-1
							cmd.Parameters.AddWithValue("?dt" & i, datin(i))
						Next
					end if
					
					cmd.ExecuteNonQuery()
					con.Close()
					return true
					
				Catch ex As MySqlException
					Console.WriteLine(query)
					Console.WriteLine(ex.ToString())
				End Try
			end if
			
		Catch el As MySqlException
			Console.WriteLine(el.ToString())
		End Try

		return false
	End Function

	public Function count(table as string) as integer
		dim query as string
		dim cnt as integer
		
		query = "SELECT COUNT(*) FROM " + table + ";"
		
		if open() = true then
			Try
                              Dim cmd as MySqlCommand = new MySqlCommand(query, con)

                              cnt = Convert.ToInt32(cmd.ExecuteScalar() + "")
                              close()
                              return cnt
			Catch ex As Exception
				Console.WriteLine(ex.ToString)
			End Try
		end if
		
		return -1
	end function
	
	public Function count(table as string, colmn as string, constraint as string) as integer
		dim query as string
		dim cnt as integer
		
		query = "SELECT COUNT(" & colmn & ") FROM " + table + " WHERE " & constraint & ";"
		
		if open() = true then
			Try
				Dim cmd as MySqlCommand = new MySqlCommand(query, con)

                cnt = Convert.ToInt32(cmd.ExecuteScalar() + "")
                close()
                return cnt
			Catch ex As Exception
				Console.WriteLine(ex.ToString)
			End Try
		end if
		
		return -1
	end function
	
	public Function showRowAsList( table as string, col as string, constraint as string) as List(of String)
		Dim ret as List(of String)
		Dim Red as MySqlDataReader 
		Dim query as string
		Dim lim as integer
		
		lim = countCol(table)
		query = "SELECT " + col + " FROM " + table + " WHERE " + constraint + ";"
		
		if open() = true then
			try
				dim cmd as MySqlCommand = new MySqlCommand(query, con)
				
				Red = cmd.ExecuteReader()
				
				' look through all rows and put their values into a list
                while (Red.Read())
                    for i as integer=0 to lim-1
                        ret.Add(Red(i).ToString())
					Next
				end while
			Catch ex As Exception
				Console.WriteLine(ex.ToString)
			End Try
		end if
	end Function
	
	public Function countCol(table as string) as integer
		Dim query as string
        Dim ret as integer
		
        query = "SELECT count(*) FROM INFORMATION_SCHEMA.COLUMNS where TABLE_NAME = '" + table + "';"
		
        if open() = true then
            try
                ' try to run the command
                Dim cmd as MySqlCommand = new MySqlCommand(query, con)

                ' convert value to intager and return
                ret = Convert.ToInt32(cmd.ExecuteScalar() + "")
                close()
                return ret
             
			Catch ex As Exception
				Console.WriteLine(ex.ToString)
			End Try
            close()
        end if
        
		' if nothing is found return 0
        return 0
	end function
End Class
