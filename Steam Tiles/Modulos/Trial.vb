Imports Microsoft.Toolkit.Uwp.UI.Controls
Imports Windows.Services.Store
Imports Windows.Storage
Imports Windows.System
Imports Windows.UI.StartScreen

Module Trial

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

            Dim panelAñadirTile As DropShadowPanel = pagina.FindName("panelAñadirTile")
            Dim panelComprarApp As DropShadowPanel = pagina.FindName("panelComprarApp")

            If tiles.Count = 0 Then
                panelAñadirTile.Visibility = Visibility.Visible
                panelComprarApp.Visibility = Visibility.Collapsed
            ElseIf tiles.Count > 0 Then
                panelAñadirTile.Visibility = Visibility.Collapsed
                panelComprarApp.Visibility = Visibility.Visible
            End If
        End If

    End Sub

End Module
