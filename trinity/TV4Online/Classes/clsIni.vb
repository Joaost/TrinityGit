Imports System.Runtime.InteropServices
Imports System
Imports System.Text
Imports System.Collections
Imports Microsoft.VisualBasic

Namespace Trinity
    Public Class clsIni
        ' This class is a *.INI file reader object

        <DllImport("KERNEL32.DLL", EntryPoint:="GetPrivateProfileIntA", SetLastError:=False, CharSet:=CharSet.Ansi, ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)> _
        Private Shared Function GetPrivateProfileInt(ByVal lpApplicationName As String, ByVal lpKeyName As String, ByVal nDefault As Integer, ByVal lpFileName As String) As Integer
        End Function
        <DllImport("KERNEL32.DLL", EntryPoint:="WritePrivateProfileStringA", SetLastError:=False, CharSet:=CharSet.Ansi, ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)> _
        Private Shared Function WritePrivateProfileString(ByVal lpApplicationName As String, ByVal lpKeyName As String, ByVal lpString As String, ByVal lpFileName As String) As Integer
        End Function
        <DllImport("KERNEL32.DLL", EntryPoint:="GetPrivateProfileStringA", SetLastError:=False, CharSet:=CharSet.Ansi, ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)> _
        Private Shared Function GetPrivateProfileString(ByVal lpApplicationName As String, ByVal lpKeyName As String, ByVal lpDefault As String, ByVal lpReturnedString As StringBuilder, ByVal nSize As Integer, ByVal lpFileName As String) As Integer
        End Function
        <DllImport("KERNEL32.DLL", EntryPoint:="WritePrivateProfileStructA", SetLastError:=False, CharSet:=CharSet.Ansi, ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)> _
        Private Shared Function WritePrivateProfileStruct(ByVal lpszSection As String, ByVal lpszKey As String, ByVal lpStruct() As Byte, ByVal uSizeStruct As Integer, ByVal szFile As String) As Integer
        End Function
        <DllImport("KERNEL32.DLL", EntryPoint:="GetPrivateProfileStructA", SetLastError:=False, CharSet:=CharSet.Ansi, ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)> _
        Private Shared Function GetPrivateProfileStruct(ByVal lpszSection As String, ByVal lpszKey As String, ByVal lpStruct() As Byte, ByVal uSizeStruct As Integer, ByVal szFile As String) As Integer
        End Function
        <DllImport("KERNEL32.DLL", EntryPoint:="GetPrivateProfileSectionNamesA", SetLastError:=False, CharSet:=CharSet.Ansi, ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)> _
        Private Shared Function GetPrivateProfileSectionNames(ByVal lpszReturnBuffer() As Byte, ByVal nSize As Integer, ByVal lpFileName As String) As Integer
        End Function
        <DllImport("KERNEL32.DLL", EntryPoint:="WritePrivateProfileSectionA", SetLastError:=False, CharSet:=CharSet.Ansi, ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)> _
        Private Shared Function WritePrivateProfileSection(ByVal lpAppName As String, ByVal lpString As String, ByVal lpFileName As String) As Integer
        End Function
        ' 
        '/// Constructs a new INIReader object.
        '/// Specifies the INI file to use.
        Public Sub Create(ByVal File As String)
            Filename = File
        End Sub
        '/// Specifies the INI file to use.
        '/// A String representing the full path of the INI file.
        Public Property Filename() As String
            Get
                Return m_Filename
            End Get
            Set(ByVal Value As String)
                m_Filename = Value
            End Set
        End Property
        '/// Specifies the section you're working in. (aka 'the active section')
        '/// A String representing the section you're working in.
        Public Property Section() As String
            Get
                Return m_Section
            End Get
            Set(ByVal Value As String)
                m_Section = Value
            End Set
        End Property
        '/// Reads an Integer from the specified key of the specified section.
        '/// Specifies the section to search in.
        '/// Specifies the key from which to return the value.
        '/// Specifies the value to return if the specified key isn't found.
        '/// Returns the value of the specified Section/Key pair, or returns the default value if the specified Section/Key pair isn't found in the INI file.
        Public Overloads Function ReadInteger(ByVal Section As String, ByVal Key As String, ByVal DefVal As Integer) As Integer
            Try
                Return GetPrivateProfileInt(Section, Key, DefVal, Filename)
            Catch
                Return DefVal
            End Try
        End Function
        '/// Reads an Integer from the specified key of the specified section.
        '/// Specifies the section to search in.
        '/// Specifies the key from which to return the value.
        '/// Returns the value of the specified Section/Key pair, or returns 0 if the specified Section/Key pair isn't found in the INI file.
        Public Overloads Function ReadInteger(ByVal Section As String, ByVal Key As String) As Integer
            Return ReadInteger(Section, Key, 0)
        End Function
        '/// Reads an Integer from the specified key of the active section.
        '/// Specifies the key from which to return the value.
        '/// Specifies the section to search in.
        '/// Returns the value of the specified Key, or returns the default value if the specified Key isn't found in the active section of the INI file.
        Public Overloads Function ReadInteger(ByVal Key As String, ByVal DefVal As Integer) As Integer
            Return ReadInteger(Section, Key, DefVal)
        End Function
        '/// Reads an Integer from the specified key of the active section.
        '/// Specifies the key from which to return the value.
        '/// Returns the value of the specified Key, or returns 0 if the specified Key isn't found in the active section of the INI file.
        Public Overloads Function ReadInteger(ByVal Key As String) As Integer
            Return ReadInteger(Key, 0)
        End Function
        '/// Reads a String from the specified key of the specified section.
        '/// Specifies the section to search in.
        '/// Specifies the key from which to return the value.
        '/// Specifies the value to return if the specified key isn't found.
        '/// Returns the value of the specified Section/Key pair, or returns the default value if the specified Section/Key pair isn't found in the INI file.
        Public Overloads Function ReadString(ByVal Section As String, ByVal Key As String, ByVal DefVal As String) As String
            Try
                Dim sb As New StringBuilder(MAX_ENTRY)
                Dim Ret As Integer = GetPrivateProfileString(Section, Key, DefVal, sb, MAX_ENTRY, Filename)
                Return sb.ToString
            Catch
                Return DefVal
            End Try
        End Function
        '/// Reads a String from the specified key of the specified section.
        '/// Specifies the section to search in.
        '/// Specifies the key from which to return the value.
        '/// Returns the value of the specified Section/Key pair, or returns an empty String if the specified Section/Key pair isn't found in the INI file.
        Public Overloads Function ReadString(ByVal Section As String, ByVal Key As String) As String
            Return ReadString(Section, Key, "")
        End Function
        '/// Reads a String from the specified key of the active section.
        '/// Specifies the key from which to return the value.
        '/// Returns the value of the specified Key, or returns an empty String if the specified Key isn't found in the active section of the INI file.
        Public Overloads Function ReadString(ByVal Key As String) As String
            Return ReadString(Section, Key)
        End Function
        '/// Reads a Long from the specified key of the specified section.
        '/// Specifies the section to search in.
        '/// Specifies the key from which to return the value.
        '/// Specifies the value to return if the specified key isn't found.
        '/// Returns the value of the specified Section/Key pair, or returns the default value if the specified Section/Key pair isn't found in the INI file.
        Public Overloads Function ReadLong(ByVal Section As String, ByVal Key As String, ByVal DefVal As Long) As Long
            Return Long.Parse(ReadString(Section, Key, DefVal.ToString))
        End Function
        '/// Reads a Long from the specified key of the specified section.
        '/// Specifies the section to search in.
        '/// Specifies the key from which to return the value.
        '/// Returns the value of the specified Section/Key pair, or returns 0 if the specified Section/Key pair isn't found in the INI file.
        Public Overloads Function ReadLong(ByVal Section As String, ByVal Key As String) As Long
            Return ReadLong(Section, Key, 0)
        End Function
        '/// Reads a Long from the specified key of the active section.
        '/// Specifies the key from which to return the value.
        '/// Specifies the section to search in.
        '/// Returns the value of the specified Key, or returns the default value if the specified Key isn't found in the active section of the INI file.
        Public Overloads Function ReadLong(ByVal Key As String, ByVal DefVal As Long) As Long
            Return ReadLong(Section, Key, DefVal)
        End Function
        '/// Reads a Long from the specified key of the active section.
        '/// Specifies the key from which to return the value.
        '/// Returns the value of the specified Key, or returns 0 if the specified Key isn't found in the active section of the INI file.
        Public Overloads Function ReadLong(ByVal Key As String) As Long
            Return ReadLong(Key, 0)
        End Function
        '/// Reads a Byte array from the specified key of the specified section.
        '/// Specifies the section to search in.
        '/// Specifies the key from which to return the value.
        '/// Specifies the value to return if the specified key isn't found.
        '/// Returns the value of the specified Section/Key pair, or returns Nothing (Null in C#, C++.NET) if the specified Section/Key pair isn't found in the INI file.
        Public Function ReadByteArray(ByVal Section As String, ByVal Key As String, ByVal Length As Integer) As Byte()
            If Length > 0 Then
                Try
                    Dim Buffer(Length - 1) As Byte
                    If GetPrivateProfileStruct(Section, Key, Buffer, Buffer.Length, Filename) = 0 Then
                        Return Nothing
                    Else
                        Return Buffer
                    End If
                Catch
                    Return Nothing
                End Try
            Else
                Return Nothing
            End If
        End Function
        '/// Reads a Byte array from the specified key of the active section.
        '/// Specifies the key from which to return the value.
        '/// Specifies the value to return if the specified key isn't found.
        '/// Returns the value of the specified Key, or returns Nothing (Null in C#, C++.NET) if the specified Key pair isn't found in the active section of the INI file.
        Public Function ReadByteArray(ByVal Key As String, ByVal Length As Integer) As Byte()
            Return ReadByteArray(Section, Key, Length)
        End Function
        '/// Reads a Boolean from the specified key of the specified section.
        '/// Specifies the section to search in.
        '/// Specifies the key from which to return the value.
        '/// Specifies the value to return if the specified key isn't found.
        '/// Returns the value of the specified Section/Key pair, or returns the default value if the specified Section/Key pair isn't found in the INI file.
        Public Overloads Function ReadBoolean(ByVal Section As String, ByVal Key As String, ByVal DefVal As Boolean) As Boolean
            Return Boolean.Parse(ReadString(Section, Key, DefVal.ToString))
        End Function
        '/// Reads a Boolean from the specified key of the specified section.
        '/// Specifies the section to search in.
        '/// Specifies the key from which to return the value.
        '/// Returns the value of the specified Section/Key pair, or returns False if the specified Section/Key pair isn't found in the INI file.
        Public Overloads Function ReadBoolean(ByVal Section As String, ByVal Key As String) As Boolean
            Return ReadBoolean(Section, Key, False)
        End Function
        '/// Reads a Boolean from the specified key of the specified section.
        '/// Specifies the key from which to return the value.
        '/// Specifies the value to return if the specified key isn't found.
        '/// Returns the value of the specified Key pair, or returns the default value if the specified Key isn't found in the active section of the INI file.
        Public Overloads Function ReadBoolean(ByVal Key As String, ByVal DefVal As Boolean) As Boolean
            Return ReadBoolean(Section, Key, DefVal)
        End Function
        '/// Reads a Boolean from the specified key of the specified section.
        '/// Specifies the key from which to return the value.
        '/// Returns the value of the specified Key, or returns False if the specified Key isn't found in the active section of the INI file.
        Public Overloads Function ReadBoolean(ByVal Key As String) As Boolean
            Return ReadBoolean(Section, Key)
        End Function
        '/// Writes an Integer to the specified key in the specified section.
        '/// Specifies the section to write in.
        '/// Specifies the key to write to.
        '/// Specifies the value to write.
        '/// Returns True if the function succeeds, False otherwise.
        Public Overloads Function Write(ByVal Section As String, ByVal Key As String, ByVal Value As Integer) As Boolean
            Try
                Return (WritePrivateProfileString(Section, Key, Value.ToString, Filename) <> 0)
            Catch
                Return False
            End Try
        End Function
        '/// Writes an Integer to the specified key in the active section.
        '/// Specifies the key to write to.
        '/// Specifies the value to write.
        '/// Returns True if the function succeeds, False otherwise.
        Public Overloads Function Write(ByVal Key As String, ByVal Value As Integer) As Boolean
            Return Write(Section, Key, Value)
        End Function
        '/// Writes a String to the specified key in the specified section.
        '/// Specifies the section to write in.
        '/// Specifies the key to write to.
        '/// Specifies the value to write.
        '/// Returns True if the function succeeds, False otherwise.
        Public Overloads Function Write(ByVal Section As String, ByVal Key As String, ByVal Value As String) As Boolean
            Try
                Return (WritePrivateProfileString(Section, Key, Value, Filename) <> 0)
            Catch
                Return False
            End Try
        End Function
        '/// Writes a String to the specified key in the active section.
        '/// Specifies the key to write to.
        '/// Specifies the value to write.
        '/// Returns True if the function succeeds, False otherwise.
        Public Overloads Function Write(ByVal Key As String, ByVal Value As String) As Boolean
            Return Write(Section, Key, Value)
        End Function
        '/// Writes a Long to the specified key in the specified section.
        '/// Specifies the section to write in.
        '/// Specifies the key to write to.
        '/// Specifies the value to write.
        '/// Returns True if the function succeeds, False otherwise.
        Public Overloads Function Write(ByVal Section As String, ByVal Key As String, ByVal Value As Long) As Boolean
            Return Write(Section, Key, Value.ToString)
        End Function
        '/// Writes a Long to the specified key in the active section.
        '/// Specifies the key to write to.
        '/// Specifies the value to write.
        '/// Returns True if the function succeeds, False otherwise.
        Public Overloads Function Write(ByVal Key As String, ByVal Value As Long) As Boolean
            Return Write(Section, Key, Value)
        End Function
        '/// Writes a Byte array to the specified key in the specified section.
        '/// Specifies the section to write in.
        '/// Specifies the key to write to.
        '/// Specifies the value to write.
        '/// Returns True if the function succeeds, False otherwise.
        Public Overloads Function Write(ByVal Section As String, ByVal Key As String, ByVal Value() As Byte) As Boolean
            Try
                Return (WritePrivateProfileStruct(Section, Key, Value, Value.Length, Filename) <> 0)
            Catch
                Return False
            End Try
        End Function
        '/// Writes a Byte array to the specified key in the active section.
        '/// Specifies the key to write to.
        '/// Specifies the value to write.
        '/// Returns True if the function succeeds, False otherwise.
        Public Overloads Function Write(ByVal Key As String, ByVal Value() As Byte) As Boolean
            Return Write(Section, Key, Value)
        End Function
        '/// Writes a Boolean to the specified key in the specified section.
        '/// Specifies the section to write in.
        '/// Specifies the key to write to.
        '/// Specifies the value to write.
        '/// Returns True if the function succeeds, False otherwise.
        Public Overloads Function Write(ByVal Section As String, ByVal Key As String, ByVal Value As Boolean) As Boolean
            Return Write(Section, Key, Value.ToString)
        End Function
        '/// Writes a Boolean to the specified key in the active section.
        '/// Specifies the key to write to.
        '/// Specifies the value to write.
        '/// Returns True if the function succeeds, False otherwise.
        Public Overloads Function Write(ByVal Key As String, ByVal Value As Boolean) As Boolean
            Return Write(Section, Key, Value)
        End Function
        '/// Deletes a key from the specified section.
        '/// Specifies the section to delete from.
        '/// Specifies the key to delete.
        '/// Returns True if the function succeeds, False otherwise.
        Public Function DeleteKey(ByVal Section As String, ByVal Key As String) As Boolean
            Try
                Return (WritePrivateProfileString(Section, Key, Nothing, Filename) <> 0)
            Catch
                Return False
            End Try
        End Function
        '/// Deletes a key from the active section.
        '/// Specifies the key to delete.
        '/// Returns True if the function succeeds, False otherwise.
        Public Function DeleteKey(ByVal Key As String) As Boolean
            Try
                Return (WritePrivateProfileString(Section, Key, Nothing, Filename) <> 0)
            Catch
                Return False
            End Try
        End Function
        '/// Deletes a section from an INI file.
        '/// Specifies the section to delete.
        '/// Returns True if the function succeeds, False otherwise.
        Public Function DeleteSection(ByVal Section As String) As Boolean
            Try
                Return WritePrivateProfileSection(Section, Nothing, Filename) <> 0
            Catch
                Return False
            End Try
        End Function
        '/// Retrieves a list of all available sections in the INI file.
        '/// Returns an ArrayList with all available sections.
        Public Function GetSectionNames() As ArrayList
            GetSectionNames = New ArrayList
            Dim Buffer(MAX_ENTRY) As Byte
            Dim BuffStr As String
            Dim PrevPos As Integer = 0
            Dim Length As Integer
            Try
                Length = GetPrivateProfileSectionNames(Buffer, MAX_ENTRY, Filename)
            Catch
                Exit Function
            End Try
            Dim ASCII As New ASCIIEncoding
            If Length > 0 Then
                BuffStr = ASCII.GetString(Buffer)
                Length = 0
                PrevPos = -1
                Do
                    Length = BuffStr.IndexOf(ControlChars.NullChar, PrevPos + 1)
                    If Length - PrevPos = 1 OrElse Length = -1 Then Exit Do
                    Try
                        GetSectionNames.Add(BuffStr.Substring(PrevPos + 1, Length - PrevPos))
                    Catch
                    End Try
                    PrevPos = Length
                Loop
            End If
        End Function

        Public Property Text(ByVal sSection As String, ByVal sKey As String) As String
            Get
                Return ReadString(sSection, sKey, "")
            End Get
            Set(ByVal value As String)
                Write(sSection, sKey, value)
            End Set
        End Property

        Public Property Data(ByVal sSection As String, ByVal sKey As String) As Integer
            Get
                Return ReadInteger(sSection, sKey, -1)
            End Get
            Set(ByVal value As Integer)
                Write(sSection, sKey, value)
            End Set
        End Property

        'Private variables and constants
        Private m_Filename As String
        Private m_Section As String
        Private Const MAX_ENTRY As Integer = 32768
    End Class
End Namespace