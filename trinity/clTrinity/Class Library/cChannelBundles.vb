Imports System.Xml

Namespace Trinity

    Public Class cChannelBundles
        Implements IEnumerable

        Private mCol As New Dictionary(Of String, List(Of Trinity.cPricelistTarget))
        Private _main As cKampanj

        Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
            Return mCol.GetEnumerator()
        End Function

        Public Function Contains(ByVal BundleName As String) As Boolean
            Return mCol.ContainsKey(BundleName)
        End Function

        Public Function GetBundle(ByVal BundleName As String) As List(Of cPricelistTarget)
            If mCol.ContainsKey(BundleName) Then
                Return mCol(BundleName)
            Else
                Return Nothing
            End If
        End Function

        Public Sub Add(ByVal BundleName As String, ByVal Bundle As List(Of Trinity.cPricelistTarget))
            mCol.Add(BundleName, Bundle)
        End Sub
        Public Sub Remove(ByVal BundleName As String)
            mCol.Remove(BundleName)
        End Sub

        Public Sub Read()

            ' Dim _ChannelBundles As New Dictionary(Of String, Collection)
            Dim _ChannelBundle As List(Of cPricelistTarget)

            'Read common
            If System.IO.File.Exists(TrinitySettings.DataPath & "bundledchannels.xml") Then
                Dim _Bundles As New XmlDocument
                Try
                    _Bundles.Load(TrinitySettings.DataPath & "bundledchannels.xml")
                    For Each tmpBundle As XmlElement In _Bundles.SelectNodes("/bundles/bundle")
                        _ChannelBundle = New List(Of cPricelistTarget)
                        For Each tmpChannel As XmlElement In tmpBundle.SelectNodes("channels/channel")
                            If _main.Channels(tmpChannel.GetAttribute("name")) IsNot Nothing AndAlso _main.Channels(tmpChannel.GetAttribute("name")).BookingTypes(tmpChannel.GetAttribute("bookingtype")) IsNot Nothing AndAlso _main.Channels(tmpChannel.GetAttribute("name")).BookingTypes(tmpChannel.GetAttribute("bookingtype")).Pricelist.Targets(tmpChannel.GetAttribute("target")) IsNot Nothing Then
                                _ChannelBundle.Add(_main.Channels(tmpChannel.GetAttribute("name")).BookingTypes(tmpChannel.GetAttribute("bookingtype")).Pricelist.Targets(tmpChannel.GetAttribute("target")))
                            End If
                        Next
                        If Not mCol.ContainsKey(tmpBundle.GetAttribute("name")) Then
                            mCol.Add(tmpBundle.GetAttribute("name"), _ChannelBundle)
                        End If
                    Next

                Catch

                End Try
            Else
                Exit Sub
            End If

            If System.IO.File.Exists(Trinity.Helper.GetSpecialFolder(Trinity.Helper.CSIDLEnum.UserProfile) & "Trinity 4.0\bundledchannels.xml") Then
                Dim _Bundles As New XmlDocument
                Try
                    _Bundles.Load(TrinitySettings.DataPath & "bundledchannels.xml")
                    For Each tmpBundle As XmlElement In _Bundles.SelectNodes("/bundles/bundle")
                        _ChannelBundle = New List(Of cPricelistTarget)
                        For Each tmpChannel As XmlElement In tmpBundle.SelectNodes("channels/channel")
                            If Not _main IsNot Nothing AndAlso
                                _main.Channels(tmpChannel.GetAttribute("name")) IsNot Nothing AndAlso
                                _main.Channels(tmpChannel.GetAttribute("name")).BookingTypes(tmpChannel.GetAttribute("bookingtype")) IsNot Nothing AndAlso
                                _main.Channels(tmpChannel.GetAttribute("name")).BookingTypes(tmpChannel.GetAttribute("bookingtype")).Pricelist.Targets(tmpChannel.GetAttribute("target")) IsNot Nothing AndAlso
                                _ChannelBundle.Contains(_main.Channels(tmpChannel.GetAttribute("name")).BookingTypes(tmpChannel.GetAttribute("bookingtype")).Pricelist.Targets(tmpChannel.GetAttribute("target"))) Then

                                _ChannelBundle.Add(_main.Channels(tmpChannel.GetAttribute("name")).BookingTypes(tmpChannel.GetAttribute("bookingtype")).Pricelist.Targets(tmpChannel.GetAttribute("target")))
                            End If
                        Next
                        If Not mCol.ContainsKey(tmpBundle.GetAttribute("name")) Then mCol.Add(tmpBundle.GetAttribute("name"), _ChannelBundle)
                    Next

                Catch

                End Try
            Else
                Exit Sub
            End If

        End Sub

        Public Sub Save(ByVal ForEveryone As Boolean)

            Dim _Bundles As New XmlDocument

            Dim xmlBundles As XmlElement
            Dim xmlBundle As XmlElement
            Dim xmlChannels As XmlElement
            Dim xmlChannel As XmlElement
            Dim xmlPerson As XmlElement


            Dim Node As Object


            Node = _Bundles.CreateProcessingInstruction("xml", "version='1.0'")
            _Bundles.AppendChild(Node)

            xmlBundles = _Bundles.CreateElement("bundles")
            xmlBundles.SetAttribute("lasteditedby", TrinitySettings.UserName)
            xmlBundles.SetAttribute("lasteditedon", Now.ToShortDateString)
            _Bundles.AppendChild(xmlBundles)


            For Each Bundle As Object In mCol
                xmlBundle = _Bundles.CreateElement("bundle")
                xmlBundle.SetAttribute("name", Bundle.Key)
                xmlChannels = _Bundles.CreateElement("channels")

                For Each target As Trinity.cPricelistTarget In Bundle.value
                    xmlChannel = _Bundles.CreateElement("channel")
                    xmlChannel.SetAttribute("name", target.Bookingtype.ParentChannel.ChannelName)
                    xmlChannel.SetAttribute("bookingtype", target.Bookingtype.Name)
                    xmlChannel.SetAttribute("target", target.TargetName)
                    xmlChannels.AppendChild(xmlChannel)
                Next
                xmlBundle.AppendChild(xmlChannels)
                'xmlChannels.AppendChild(xmlBundle)
                xmlBundles.AppendChild(xmlBundle)
            Next
            _Bundles.AppendChild(xmlBundles)


            Try
                _Bundles.Save(TrinitySettings.DataPath & "bundledchannels.xml")
            Catch ex As Exception
                Windows.Forms.MessageBox.Show("Could not save. The file could be opened by someone else at the moment.", "T R I N I T Y")
            End Try

        End Sub

        Public Sub New(ByVal Campaign As cKampanj)
            'Read()
            _main = Campaign
        End Sub
    End Class

End Namespace
