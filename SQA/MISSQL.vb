Imports System.Data.SqlClient

Public Class MISSQL
    Dim PV As String = "Server=20.239.48.184;uid=ADMIN_sa;pwd=C11@tCll@i;"
    Dim m_Database As String = "testDB"
    Public Strcon As String

    Public Sub New()
        Strcon = PV & "database=" & m_Database
    End Sub

    Public Sub New(ByVal DBName As String)
        m_Database = DBName
        Strcon = PV & "database=" & m_Database
    End Sub

    Public Property Database() As String
        Get
            Return m_Database
        End Get
        Set(ByVal Value As String)
            m_Database = Value
            Strcon = PV & "database=" & m_Database
        End Set
    End Property

    Public Function GetDataset(ByVal Strsql As String, Optional ByVal DatasetName As String = "Dataset1", Optional ByVal TableName As String = "Table") As DataSet
        Dim DA As New SqlDataAdapter(Strsql, Strcon)
        Dim DS As New DataSet(DatasetName)
        Try
            DA.Fill(DS, TableName)
        Catch x1 As Exception
            Err.Raise(60002, , x1.Message)
        End Try
        Return DS
    End Function

    Public Function GetDataTable(ByVal Strsql As String, Optional ByVal TableName As String = "Table1") As DataTable
        Dim DA As New SqlDataAdapter(Strsql, Strcon)
        Dim DT As New DataTable(TableName)
        Try
            DA.Fill(DT)
        Catch x1 As Exception
            Err.Raise(60002, , x1.Message)
        End Try
        Return DT
    End Function

    Public Function CreateCommand(ByVal Strsql As String) As SqlCommand
        Dim cmd As New SqlCommand(Strsql)
        Return cmd
    End Function

    Public Function Execute(ByRef Cmd As SqlCommand) As Integer
        Dim Cn As New SqlConnection(Strcon)
        Cmd.Connection = Cn
        Dim X As Integer
        Try
            Cn.Open()
            X = Cmd.ExecuteNonQuery()
        Catch ex As Exception
            X = -1
        Finally
            Cn.Close()
        End Try
        Return X
    End Function

    Public Sub CreateParam(ByRef Cmd As SqlCommand, ByVal StrType As String)
        Dim i As Integer
        Dim j As String
        For i = 1 To Len(StrType)
            j = UCase(Mid(StrType, i, 1))
            Dim P1 As New SqlParameter
            P1.ParameterName = "@P" & i
            Select Case j
                Case "T"
                    P1.SqlDbType = SqlDbType.NVarChar
                Case "M"
                    P1.SqlDbType = SqlDbType.Text
                Case "Y"
                    P1.SqlDbType = SqlDbType.Money
                Case "D"
                    P1.SqlDbType = SqlDbType.DateTime
                Case "I"
                    P1.SqlDbType = SqlDbType.Int
                Case "S"
                    P1.SqlDbType = SqlDbType.Decimal
                Case "B"
                    P1.SqlDbType = SqlDbType.Bit
                Case "P"
                    P1.SqlDbType = SqlDbType.Image
            End Select
            Cmd.Parameters.Add(P1)
        Next
    End Sub
End Class
