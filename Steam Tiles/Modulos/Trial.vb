Imports Windows.Services.Store
Imports Windows.Storage
Imports Windows.System
Imports Windows.UI.StartScreen

Module Trial

    Public idTienda As String = "9NBLGGH51SB3"

    Public Async Sub Detectar()

        Dim config As ApplicationDataContainer = ApplicationData.Current.LocalSettings

        Dim usuarios As IReadOnlyList(Of User) = Await User.FindAllAsync

        If Not usuarios Is Nothing Then
            If usuarios.Count > 0 Then
                Dim usuario As User = usuarios(0)

                Dim contexto As StoreContext = StoreContext.GetForUser(usuario)
                Dim licencia As StoreAppLicense = Await contexto.GetAppLicenseAsync

                If licencia.IsActive = True And licencia.IsTrial = False Then
                    config.Values("Estado_App") = 1
                Else
                    config.Values("Estado_App") = 0
                End If
            End If
        End If

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        Dim gridMensaje As Grid = pagina.FindName("gridMensajeTrialAñadirTile")

        If config.Values("Estado_App") = 1 Then
            gridMensaje.Visibility = Visibility.Collapsed
        Else
            gridMensaje.Visibility = Visibility.Visible

            Dim tiles As IReadOnlyList(Of SecondaryTile) = Await SecondaryTile.FindAllAsync()

            Dim botonAñadirTile As Button = pagina.FindName("botonAñadirTile")
            Dim botonComprarApp As Button = pagina.FindName("botonComprarApp")

            If tiles.Count = 0 Then
                botonAñadirTile.Visibility = Visibility.Visible
                botonComprarApp.Visibility = Visibility.Collapsed
            ElseIf tiles.Count > 0 Then
                botonAñadirTile.Visibility = Visibility.Collapsed
                botonComprarApp.Visibility = Visibility.Visible
            End If
        End If

    End Sub

    Public Async Sub ComprarAppClick(sender As Object, e As RoutedEventArgs)

        Dim usuarios As IReadOnlyList(Of User) = Await User.FindAllAsync

        If Not usuarios Is Nothing Then
            If usuarios.Count > 0 Then
                Dim usuario As User = usuarios(0)

                Dim contexto As StoreContext = StoreContext.GetForUser(usuario)
                Await contexto.RequestPurchaseAsync(idTienda)
            End If
        End If

    End Sub

End Module
