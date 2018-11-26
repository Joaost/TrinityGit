Imports System.Xml

Namespace Trinity

    Public Class cColorSchemes

        Implements IEnumerable
        Private mCol As Collection

        Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
            Return mCol.GetEnumerator
        End Function

        Public Sub New()
            mCol = New Collection
            ReadColorSchemes()
        End Sub

        Public Sub Add(ByVal Scheme As cColorScheme)
            If Not mCol.Contains(Scheme.name) Then
                mCol.Add(Scheme, Scheme.name)
            End If
        End Sub

        Public Sub Remove(ByVal Scheme As cColorScheme)
            If mCol.Contains(Scheme.name) Then
                mCol.Remove(Scheme.name)
            End If
        End Sub

        Public Function ReadColorSchemes() As Boolean

            If Not System.IO.File.Exists(TrinitySettings.DataPath(cSettings.SettingsLocationEnum.locNetwork) & "colorschemes.xml") Then Return False

            Dim xmlSchemes As New Xml.XmlDocument
            xmlSchemes.Load(TrinitySettings.DataPath(cSettings.SettingsLocationEnum.locNetwork) & "colorschemes.xml")

            For Each colorscheme As XmlNode In xmlSchemes.SelectNodes("colorschemes/colorscheme")
                Dim attributes As XmlAttributeCollection = colorscheme.Attributes
                Dim tmpScheme As New cColorScheme

                tmpScheme.name = attributes("name").Value
                tmpScheme.textFont = attributes("font").Value
                tmpScheme.HeadlineColor = fromXML(attributes("headline").Value)
                tmpScheme.PanelBGColor = fromXML(attributes("panel").Value)
                tmpScheme.PanelFGColor = fromXML(attributes("text").Value)
                tmpScheme.TextBGColor = fromXML(attributes("text").Value)
                tmpScheme.pbc1 = fromXML(attributes("color1").Value)
                tmpScheme.pbc2 = fromXML(attributes("color2").Value)
                tmpScheme.pbc3 = fromXML(attributes("color3").Value)
                tmpScheme.pbc4 = fromXML(attributes("color4").Value)
                tmpScheme.pbc5 = fromXML(attributes("color5").Value)
                tmpScheme.pbc6 = fromXML(attributes("color6").Value)
                tmpScheme.pbc7 = fromXML(attributes("color7").Value)
                tmpScheme.pbc8 = fromXML(attributes("color8").Value)
                tmpScheme.pbc9 = fromXML(attributes("color9").Value)
                tmpScheme.pbc10 = fromXML(attributes("color10").Value)

                Add(tmpScheme)
            Next

            Return True

        End Function

        Public Function Count()
            Return mCol.Count
        End Function

        Public Function SaveColorSchemes() As Boolean

            If Not System.IO.File.Exists(TrinitySettings.DataPath(cSettings.SettingsLocationEnum.locNetwork) & "colorschemes.xml") Then Return False

            Dim xmlSchemes As New Xml.XmlDocument
            xmlSchemes.Load(TrinitySettings.DataPath(cSettings.SettingsLocationEnum.locNetwork) & "colorschemes.xml")

            For Each scheme As cColorScheme In mCol

                Dim schemeNode As XmlNode = xmlSchemes.SelectSingleNode("colorschemes/colorscheme[@name='" & scheme.name & "']")
                Dim attributes As XmlAttributeCollection = schemeNode.Attributes

                attributes("name").Value = scheme.name
                attributes("font").Value = scheme.textFont
                attributes("headline").Value = toXML(scheme.HeadlineColor)
                attributes("panel").Value = toXML(scheme.PanelBGColor)
                attributes("text").Value = toXML(scheme.TextBGColor)
                attributes("color1").Value = toXML(scheme.pbc1)
                attributes("color2").Value = toXML(scheme.pbc2)
                attributes("color3").Value = toXML(scheme.pbc3)
                attributes("color4").Value = toXML(scheme.pbc4)
                attributes("color5").Value = toXML(scheme.pbc5)
                attributes("color6").Value = toXML(scheme.pbc6)
                attributes("color7").Value = toXML(scheme.pbc7)
                attributes("color8").Value = toXML(scheme.pbc8)
                attributes("color9").Value = toXML(scheme.pbc9)
                attributes("color10").Value = toXML(scheme.pbc10)

            Next


            xmlSchemes.Save(TrinitySettings.DataPath(cSettings.SettingsLocationEnum.locNetwork) & "colorschemes.xml")

            Return True

        End Function

        Private Function toXML(ByVal Color As Color) As String

            Dim A As String = Hex(Color.A).PadLeft(2, "0")
            Dim R As String = Hex(Color.R).PadLeft(2, "0")
            Dim G As String = Hex(Color.G).PadLeft(2, "0")
            Dim B As String = Hex(Color.B).PadLeft(2, "0")


            Return A & R & G & B

        End Function

        Private Function fromXML(ByVal XML As String) As Color

            If XML.Length < 8 Then
                MsgBox(XML & " is not a valid color.")
                Exit Function
            End If

            Dim A As Integer = System.Convert.ToInt32(XML.Substring(0, 2), 16)
            Dim R As Integer = System.Convert.ToInt32(XML.Substring(2, 2), 16)
            Dim G As Integer = System.Convert.ToInt32(XML.Substring(4, 2), 16)
            Dim B As Integer = System.Convert.ToInt32(XML.Substring(6, 2), 16)

            Return Color.FromArgb(A, R, G, B)

        End Function

    End Class

End Namespace
