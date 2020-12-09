Namespace Interfaz
    Module Juegos

        Public Sub Cargar()

            Dim recursos As New Resources.ResourceLoader()

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim tbSeleccionarJuego As TextBlock = pagina.FindName("tbSeleccionarJuego")
            tbSeleccionarJuego.Text = recursos.GetString("SelectGame")

            Dim svJuegos As ScrollViewer = pagina.FindName("svJuegos")

            AddHandler svJuegos.ViewChanging, AddressOf JuegosScroll

            Dim botonSubirJuegos As Button = pagina.FindName("botonSubirJuegos")

            AddHandler botonSubirJuegos.Click, AddressOf SubirClick
            AddHandler botonSubirJuegos.PointerEntered, AddressOf EfectosHover.Entra_Boton_Icono
            AddHandler botonSubirJuegos.PointerExited, AddressOf EfectosHover.Sale_Boton_Icono

        End Sub

        Private Sub JuegosScroll(sender As Object, e As ScrollViewerViewChangingEventArgs)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim botonSubir As Button = pagina.FindName("botonSubirJuegos")

            Dim sv As ScrollViewer = sender

            If sv.VerticalOffset > 50 Then
                botonSubir.Visibility = Visibility.Visible
            Else
                botonSubir.Visibility = Visibility.Collapsed
            End If

        End Sub

        Private Sub SubirClick(sender As Object, e As RoutedEventArgs)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim svJuegos As ScrollViewer = pagina.FindName("svJuegos")
            svJuegos.ChangeView(Nothing, 0, Nothing)

            Dim botonSubir As Button = sender
            botonSubir.Visibility = Visibility.Collapsed

        End Sub

    End Module
End Namespace