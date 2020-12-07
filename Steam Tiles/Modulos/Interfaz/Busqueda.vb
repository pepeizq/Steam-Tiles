Imports Microsoft.Toolkit.Uwp.Helpers
Imports Microsoft.Toolkit.Uwp.UI.Controls

Namespace Interfaz
    Module Busqueda

        Public Sub Cargar()

            Dim recursos As New Resources.ResourceLoader

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim tbBuscador As TextBox = pagina.FindName("tbBuscador")
            tbBuscador.PlaceholderText = recursos.GetString("SearchGame")

            AddHandler tbBuscador.TextChanged, AddressOf BuscadorTextChanged
            AddHandler tbBuscador.PointerEntered, AddressOf EfectosHover.Entra_Basico
            AddHandler tbBuscador.PointerExited, AddressOf EfectosHover.Sale_Basico

        End Sub

        Private Async Sub BuscadorTextChanged(sender As Object, e As TextChangedEventArgs)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim gvTiles As AdaptiveGridView = pagina.FindName("gvTiles")
            Dim tb As TextBox = sender

            Dim helper As New LocalObjectStorageHelper

            Dim listaJuegos As New List(Of Tile)

            If Await helper.FileExistsAsync("juegos") = True Then
                listaJuegos = Await helper.ReadFileAsync(Of List(Of Tile))("juegos")
            End If

            If Not listaJuegos Is Nothing Then
                gvTiles.Items.Clear()

                listaJuegos.Sort(Function(x, y) x.Titulo.CompareTo(y.Titulo))

                If tb.Text.Trim.Length > 0 Then
                    For Each juego In listaJuegos
                        Dim busqueda As String = tb.Text.Trim

                        If LimpiarBusqueda(juego.Titulo).ToString.Contains(LimpiarBusqueda(busqueda)) Then
                            BotonEstilo(juego, gvTiles)
                        End If
                    Next
                Else
                    For Each juego In listaJuegos
                        BotonEstilo(juego, gvTiles)
                    Next
                End If
            End If

        End Sub

        Private Function LimpiarBusqueda(texto As String)

            Dim listaCaracteres As New List(Of String) From {"Early Access", " ", "•", ">", "<", "¿", "?", "!", "¡", ":",
                ".", "_", "–", "-", ";", ",", "™", "®", "'", "’", "´", "`", "(", ")", "/", "\", "|", "&", "#", "=", ChrW(34),
                "@", "^", "[", "]", "ª", "«"}

            For Each item In listaCaracteres
                texto = texto.Replace(item, Nothing)
            Next

            texto = texto.ToLower
            texto = texto.Trim

            Return texto
        End Function

    End Module
End Namespace