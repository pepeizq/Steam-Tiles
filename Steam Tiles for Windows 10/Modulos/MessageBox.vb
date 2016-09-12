Imports Windows.UI.Popups

Module MessageBox

    Public Async Sub EnseñarMensaje(contenido As String)

        Try
            Dim messageDialog = New MessageDialog(contenido)
            Await messageDialog.ShowAsync()
        Catch ex As Exception

        End Try

    End Sub

End Module
