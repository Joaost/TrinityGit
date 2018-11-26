Public Class cDocument

    Public ID As String = Guid.NewGuid.ToString

    Private _docType As String
    Public Property DocType() As String
        Get
            Return _docType
        End Get
        Set(ByVal value As String)
            _docType = value
        End Set
    End Property

    Private _data As Object
    Public Property Data() As Object
        Get
            Return _data
        End Get
        Set(ByVal value As Object)
            _data = value
        End Set
    End Property

    Private _name As String
    Public Property Name() As String
        Get
            If _name Is Nothing Then _name = ""
            Return _name
        End Get
        Set(ByVal value As String)
            _name = value
        End Set
    End Property

    Private _description As String
    Public Property Description() As String
        Get
            If _description Is Nothing Then _description = ""
            Return _description
        End Get
        Set(ByVal value As String)
            _description = value
        End Set
    End Property

    Private _fileName As String
    Public Property FileName() As String
        Get
            Return _fileName
        End Get
        Set(ByVal value As String)
            _fileName = value
        End Set
    End Property

    Friend Function CreateXML(ByVal XMLDoc As Xml.XmlDocument) As Xml.XmlElement
        Dim TmpNode As Xml.XmlElement
        TmpNode = XMLDoc.CreateElement("Document")
        TmpNode.SetAttribute("ID", ID)
        TmpNode.SetAttribute("Name", _name)
        TmpNode.SetAttribute("Description", _description)
        TmpNode.SetAttribute("DocType", _docType)
        TmpNode.SetAttribute("Filename", _filename)
        Dim TmpString As String = ""
        TmpString = System.Text.Encoding.GetEncoding(System.Globalization.CultureInfo.CurrentCulture.TextInfo.ANSICodePage).GetString(_data)
        TmpNode.SetAttribute("Data", TmpString)
        Return TmpNode
    End Function

    Friend Sub CreateFromXML(ByVal Node As Xml.XmlElement)
        ID = Node.GetAttribute("ID")
        _name = Node.GetAttribute("Name")
        _description = Node.GetAttribute("Description")
        _docType = Node.GetAttribute("DocType")
        _fileName = Node.GetAttribute("Filename")
        Dim TmpString As String
        Dim TmpData() As Byte
        TmpString = Node.GetAttribute("Data")
        TmpData = System.Text.Encoding.GetEncoding(System.Globalization.CultureInfo.CurrentCulture.TextInfo.ANSICodePage).GetBytes(TmpString)
        _data = TmpData
    End Sub

    Public Sub New()

    End Sub

    Public Sub New(ByVal CreateFromNode As Xml.XmlElement)
        CreateFromXML(CreateFromNode)
    End Sub
End Class
