Imports System.ComponentModel.Composition
Imports System.ComponentModel.Composition.Hosting
Imports System.Reflection
Imports TrinityPlugin

<Export(GetType(IPlugin))>
Public Class TeliaMediaPlan
    Implements IPlugin

    Private _mnu As New IPlugin.PluginMenu

    Public Function Menu() As IPlugin.PluginMenu Implements IPlugin.Menu
        Return _mnu
    End Function

    Public Sub New()
        _mnu.AddToMenu = ""
        _mnu.Caption = "Telia"

        Dim _itm As New IPlugin.PluginMenuItem
        _itm.Text = "Upload to Telia MediaPlan"
        _itm.OnClickFunction = AddressOf UploadPlan
        _mnu.Items.Add(_itm)

        Dim catalog As New AggregateCatalog()
        catalog.Catalogs.Add(New AssemblyCatalog(Assembly.GetEntryAssembly))
        Dim container As New CompositionContainer(catalog)
        container.SatisfyImportsOnce(Me)
    End Sub

    Sub UploadPlan()
        Dim _camp As Object = Application.ActiveCampaign

    End Sub

    <Import(GetType(ITrinityApplication))>
    Property Application() As ITrinityApplication

End Class
