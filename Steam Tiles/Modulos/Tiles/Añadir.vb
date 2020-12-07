Imports Microsoft.Toolkit.Uwp.Notifications
Imports Windows.Storage
Imports Windows.UI.Notifications
Imports Windows.UI.StartScreen

Namespace Tiles
    Module Añadir

        Public Async Sub Generar(tile As Tile)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim boton As Button = pagina.FindName("botonAñadirTile")
            boton.IsEnabled = False

            Dim gridTilePequeña As Grid = pagina.FindName("gridTilePequeña")
            Await Imagen.Generar(gridTilePequeña, tile.ID + "pequena.png", 71, 71)

            Dim gridTileMediana As Grid = pagina.FindName("gridTileMediana")
            Await Imagen.Generar(gridTileMediana, tile.ID + "mediana.png", 150, 150)

            Dim gridTileAncha As Grid = pagina.FindName("gridTileAncha")
            Await Imagen.Generar(gridTileAncha, tile.ID + "ancha.png", 310, 150)

            Dim gridTileGrande As Grid = pagina.FindName("gridTileGrande")
            Await Imagen.Generar(gridTileGrande, tile.ID + "grande.png", 310, 310)

            '-----------------------

            Dim nuevaTile As New SecondaryTile(tile.ID, tile.Titulo, tile.Enlace, New Uri("ms-appdata:///local/" + tile.ID + "ancha.png", UriKind.RelativeOrAbsolute), Windows.UI.StartScreen.TileSize.Wide310x150)

            nuevaTile.VisualElements.Square71x71Logo = New Uri("ms-appdata:///local/" + tile.ID + "pequena.png", UriKind.RelativeOrAbsolute)
            nuevaTile.VisualElements.Square150x150Logo = New Uri("ms-appdata:///local/" + tile.ID + "mediana.png", UriKind.RelativeOrAbsolute)
            nuevaTile.VisualElements.Wide310x150Logo = New Uri("ms-appdata:///local/" + tile.ID + "ancha.png", UriKind.RelativeOrAbsolute)
            nuevaTile.VisualElements.Square310x310Logo = New Uri("ms-appdata:///local/" + tile.ID + "grande.png", UriKind.RelativeOrAbsolute)

            If Not ApplicationData.Current.LocalSettings.Values("tile_ancha_titulo") Is Nothing Then
                If ApplicationData.Current.LocalSettings.Values("tile_ancha_titulo") = True Then
                    nuevaTile.VisualElements.ShowNameOnWide310x150Logo = True
                Else
                    nuevaTile.VisualElements.ShowNameOnWide310x150Logo = False
                End If
            Else
                nuevaTile.VisualElements.ShowNameOnWide310x150Logo = False
            End If

            If Not ApplicationData.Current.LocalSettings.Values("tile_grande_titulo") Is Nothing Then
                If ApplicationData.Current.LocalSettings.Values("tile_grande_titulo") = True Then
                    nuevaTile.VisualElements.ShowNameOnSquare310x310Logo = True
                Else
                    nuevaTile.VisualElements.ShowNameOnSquare310x310Logo = False
                End If
            Else
                nuevaTile.VisualElements.ShowNameOnSquare310x310Logo = False
            End If

            If Not ApplicationData.Current.LocalSettings.Values("tiles_color_titulo") Is Nothing Then
                If ApplicationData.Current.LocalSettings.Values("tiles_color_titulo") = 0 Then
                    nuevaTile.VisualElements.ForegroundText = ForegroundText.Light
                Else
                    nuevaTile.VisualElements.ForegroundText = ForegroundText.Dark
                End If
            Else
                nuevaTile.VisualElements.ForegroundText = ForegroundText.Light
            End If

            Await nuevaTile.RequestCreateAsync()

            '-----------------------

            Dim contenidoMediano As New TileBindingContentAdaptive

            Dim fondoImagenMediano As New TileBackgroundImage With {
                .Source = "ms-appdata:///local/" + tile.ID + "mediana.png",
                .HintCrop = AdaptiveImageCrop.Default
            }

            contenidoMediano = New TileBindingContentAdaptive With {
                .BackgroundImage = fondoImagenMediano
            }

            Dim tileMediano As New TileBinding With {
                .Content = contenidoMediano
            }

            '-----------------------

            Dim contenidoAncho As New TileBindingContentAdaptive

            Dim fondoImagenAncha As New TileBackgroundImage With {
                .Source = "ms-appdata:///local/" + tile.ID + "ancha.png",
                .HintCrop = AdaptiveImageCrop.Default
            }

            contenidoAncho = New TileBindingContentAdaptive With {
                .BackgroundImage = fondoImagenAncha
            }

            Dim tileAncha As New TileBinding With {
                .Content = contenidoAncho
            }

            '-----------------------

            Dim contenidoGrande As New TileBindingContentAdaptive

            Dim fondoImagenGrande As New TileBackgroundImage With {
                .Source = "ms-appdata:///local/" + tile.ID + "grande.png",
                .HintCrop = AdaptiveImageCrop.Default
            }

            contenidoGrande = New TileBindingContentAdaptive With {
                .BackgroundImage = fondoImagenGrande
            }

            Dim tileGrande As New TileBinding With {
                .Content = contenidoGrande
            }

            '-----------------------

            If Not ApplicationData.Current.LocalSettings.Values("tile_ancha_titulo") Is Nothing Then
                If ApplicationData.Current.LocalSettings.Values("tile_ancha_titulo") = True Then
                    tileAncha.Branding = TileBranding.Name
                End If
            End If

            If Not ApplicationData.Current.LocalSettings.Values("tile_grande_titulo") Is Nothing Then
                If ApplicationData.Current.LocalSettings.Values("tile_grande_titulo") = True Then
                    tileGrande.Branding = TileBranding.Name
                End If
            End If

            Dim visual As New TileVisual With {
                .TileMedium = tileMediano,
                .TileWide = tileAncha,
                .TileLarge = tileGrande
            }

            Dim contenido As New TileContent With {
                .Visual = visual
            }

            Dim notificacion As New TileNotification(contenido.GetXml)

            Try
                TileUpdateManager.CreateTileUpdaterForSecondaryTile(tile.ID).Update(notificacion)
            Catch ex As Exception

            End Try

            MasCosas.CalificarApp(True)
            Interfaz.AñadirTile.ResetearValores()

            boton.IsEnabled = True

        End Sub

    End Module
End Namespace

