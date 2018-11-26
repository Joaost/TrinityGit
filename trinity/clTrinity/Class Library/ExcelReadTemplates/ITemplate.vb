Imports System.Xml.Linq

Namespace ExcelReadTemplates
    Public Interface ITemplate

        Enum ValidationResult
            Success
            SheetNotFound
            IdentificationFailed
            TargetNotFound
            ContractNoNotFound
        End Enum

        Function UseSheet(Schedule As cDocument) As String

        Function Validate(Schedule As cDocument, Optional BreakOnFail As Boolean = False) As ValidationResult

        Sub Parse(xml As XDocument)

        Function GetXML() As XElement

    End Interface
End Namespace

