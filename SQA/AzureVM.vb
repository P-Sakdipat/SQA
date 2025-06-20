Imports System.Data.SqlClient
Imports System.Data
Imports System.Configuration


Public Class AzureVM
    Dim PV As String = "Server=20.239.48.184;uid=ADMIN_sa;pwd=C11@tCll@i;Encrypt=True;TrustServerCertificate=True;"
    Dim m_Database As String = "RCI_QCD"

    Public Strcon As String

    Dim cn As SqlConnection
    Dim m_AutoClose As Boolean = True
    Dim m_ErrorMessage As String
    Dim m_Identity As Integer
    Dim m_Transaction As Boolean = False
    Dim T1 As SqlTransaction

    Public ReadOnly Property Identity() As Integer
        Get
            Return m_Identity
        End Get
    End Property

    Public ReadOnly Property ErrorMessge() As String
        Get
            Return m_ErrorMessage
        End Get
    End Property

    Public Sub New()
        Strcon = PV & "database=" & m_Database
    End Sub
    Public Sub New(ByVal DBName As String)
        m_Database = DBName
        Strcon = PV & "database=" & m_Database
    End Sub

    Public Sub BeginTrans()
        Me.AutoClose = False
        If Me.ConnectionOpen = True Then
            T1 = cn.BeginTransaction
            m_Transaction = True
        End If
    End Sub

    Public Function CommitTrans() As Boolean
        m_ErrorMessage = ""
        Dim X As Boolean = False
        Try
            T1.Commit()
            T1.Dispose()
            T1 = Nothing
            Me.ConnectionClose()
            Me.AutoClose = True
            X = True
            m_Transaction = False
        Catch ex As Exception
            m_ErrorMessage = ex.Message
            X = False
        End Try
        Return X
    End Function

    Public Function Rollback() As Boolean
        m_ErrorMessage = ""
        Dim X As Boolean = False
        Try
            T1.Rollback()
            T1.Dispose()
            T1 = Nothing
            Me.ConnectionClose()
            Me.AutoClose = True
            X = True
            m_Transaction = False
        Catch ex As Exception
            m_ErrorMessage = ex.Message
            X = False
        End Try
        Return X
    End Function

    'เพิ่มคำสั่งนี้มาน่ะก๊าบบบ
    Public Property AutoClose() As Boolean
        Get
            Return m_AutoClose
        End Get
        Set(ByVal value As Boolean)
            m_AutoClose = value
        End Set
    End Property

    Public Property Database() As String
        Get
            Return m_Database
        End Get
        Set(ByVal Value As String)
            m_Database = Value
            Strcon = PV & "database=" & m_Database
        End Set
    End Property

    Public Function GetDataset(ByVal Strsql As String,
        Optional ByVal DatasetName As String = "Dataset1",
        Optional ByVal TableName As String = "Table") As DataSet
        m_ErrorMessage = ""
        Dim DA As New SqlDataAdapter(Strsql, Strcon)

        Dim DS As New DataSet(DatasetName)
        Try
            DA.Fill(DS, TableName)
        Catch x1 As Exception
            DS = Nothing
            m_ErrorMessage = x1.Message
        End Try
        Return DS
    End Function

    Public Function GetDataTable(ByVal Strsql As String,
         Optional ByVal TableName As String = "Table") As DataTable
        m_ErrorMessage = ""
        Strsql = Replace(Strsql, "--", "")
        Strsql = Replace(Strsql, ";", "")
        Dim DA As New SqlDataAdapter(Strsql, Strcon)
        Dim DT As New DataTable(TableName)
        Try
            DA.SelectCommand.CommandTimeout = 5000
            DA.Fill(DT)
        Catch x1 As Exception
            'Err.Raise(60002, , x1.Message)
            DT = Nothing
            m_ErrorMessage = x1.Message
        End Try
        Return DT
    End Function

    Public Function CreateCommand(ByVal Strsql As String) As SqlCommand
        Dim cmd As New SqlCommand(Strsql)
        cmd.CommandTimeout = 2000
        Return cmd
    End Function

    Public Function Execute(ByVal Strsql As String, Optional ByVal identityCheck As Boolean = False) As Integer
        Strsql = Replace(Strsql, "--", "")
        Strsql = Replace(Strsql, ";", ":")
        Dim cmd As New SqlCommand(Strsql)
        cmd.CommandTimeout = 2000
        Dim X As Integer = Me.Execute(cmd, identityCheck)
        Return X
    End Function

    Public Function Execute(ByRef Cmd As SqlCommand, Optional ByVal identityCheck As Boolean = False) As Integer
        If Me.ConnectionOpen = False Then
            Return -1
        End If
        m_ErrorMessage = ""

        Cmd.Connection = cn
        Dim X As Integer
        Try
            m_Identity = 0
            If m_Transaction = True Then
                Cmd.Transaction = T1
            End If
            X = Cmd.ExecuteNonQuery()
            If identityCheck = True Then
                Dim cmd2 As New SqlCommand("select @@identity", cn)
                m_Identity = cmd2.ExecuteScalar
            End If
        Catch ex As Exception

            m_ErrorMessage = ex.Message
            X = -1
        End Try
        If Me.AutoClose = True Then
            Me.ConnectionClose()
        End If
        Return X
    End Function

    Public Function CommandCreate(ByVal strsql As String, ByVal strType As String) As SqlCommand
        Dim cmd As SqlCommand = Me.CreateCommand(strsql)
        Me.CreateParam(cmd, strType)
        Return cmd
    End Function

    Public Sub CreateParam(ByRef Cmd As SqlCommand, ByVal StrType As String)
        'T:Text, M:Memo, Y:Currency, D:Datetime, I:Integer, S:Single, B:Boolean, P: Picture
        Dim i As Integer
        Dim j As String
        For i = 1 To Len(StrType)
            j = UCase(Mid(StrType, i, 1))
            Dim P1 As New SqlParameter
            P1.ParameterName = "@P" & i
            Select Case j
                Case "T"
                    P1.SqlDbType = SqlDbType.VarChar
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

    Public Function ConnectionOpen() As Boolean
        m_ErrorMessage = ""
        If cn Is Nothing Then
            cn = New SqlConnection(Strcon)

        End If
        If cn.State = ConnectionState.Closed Then
            Try
                cn.Open()
            Catch ex As Exception
                'Err.Raise(60002, , ex.Message)
                m_ErrorMessage = ex.Message
                Return False
            End Try

        End If
        Return True
    End Function

    Public Sub ConnectionClose()
        Try
            If cn IsNot Nothing Then
                cn.Close()
                cn.Dispose()
            End If
            cn = Nothing
        Catch ex As Exception

        End Try
    End Sub

    Protected Overrides Sub Finalize()
        Me.ConnectionClose()
        MyBase.Finalize()
    End Sub


End Class
