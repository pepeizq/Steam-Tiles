Imports Microsoft.Toolkit.Uwp.Notifications
Imports Windows.Networking.BackgroundTransfer
Imports Windows.Storage
Imports Windows.UI.Notifications
Imports Windows.UI.StartScreen

Module Tiles

    Public Async Sub Generar(tile As Tile)

        Dim ficheroImagen As StorageFile = Await ApplicationData.Current.LocalFolder.CreateFileAsync("header.png", CreationCollisionOption.GenerateUniqueName)
        Dim downloader As BackgroundDownloader = New BackgroundDownloader()
        Dim descarga As DownloadOperation = downloader.CreateDownload(tile.Imagen, ficheroImagen)
        Await descarga.StartAsync

        Dim nuevaTile As SecondaryTile = New SecondaryTile(tile.ID, tile.Titulo, tile.Enlace.ToString, New Uri("ms-appdata:///local/" + ficheroImagen.Name, UriKind.RelativeOrAbsolute), TileSize.Wide310x150)

        nuevaTile.VisualElements.Wide310x150Logo = New Uri("ms-appdata:///local/" + ficheroImagen.Name, UriKind.RelativeOrAbsolute)
        nuevaTile.VisualElements.Square310x310Logo = New Uri("ms-appdata:///local/" + ficheroImagen.Name, UriKind.RelativeOrAbsolute)

        Await nuevaTile.RequestCreateAsync()

        Dim imagenDRM As AdaptiveImage = Nothing

        If ApplicationData.Current.LocalSettings.Values("logotile") = "on" Then
            imagenDRM = New AdaptiveImage With {
                .HintRemoveMargin = True,
                .HintAlign = AdaptiveImageAlign.Right
            }

            If ApplicationData.Current.LocalSettings.Values("logotile1") = "on" Then
                If tile.Cliente = "Steam" Then
                    imagenDRM.Source = "Assets/AppList/AppList.targetsize-16.png"
                ElseIf tile.Cliente = "Origin" Then
                    imagenDRM.Source = "Assets/origin_logo_mini.png"
                ElseIf tile.Cliente = "Battle.net" Then
                    imagenDRM.Source = "Assets/battlenet_logo_mini.png"
                ElseIf tile.Cliente = "GOG Galaxy" Then
                    imagenDRM.Source = "Assets/gog_logo_mini.png"
                End If
            End If

            If ApplicationData.Current.LocalSettings.Values("logotile2") = "on" Then
                If tile.Cliente = "Steam" Then
                    imagenDRM.Source = "Assets/steam_logo_mini2.png"
                ElseIf tile.Cliente = "Origin" Then
                    imagenDRM.Source = "Assets/origin_logo_mini2.png"
                ElseIf tile.Cliente = "Battle.net" Then
                    imagenDRM.Source = "Assets/battlenet_logo_mini2.png"
                ElseIf tile.Cliente = "GOG Galaxy" Then
                    imagenDRM.Source = "Assets/gog_logo_mini2.png"
                End If
            End If
        End If

        Dim imagen As AdaptiveImage = New AdaptiveImage With {
            .Source = "ms-appdata:///local/" + ficheroImagen.Name,
            .HintRemoveMargin = True,
            .HintAlign = AdaptiveImageAlign.Stretch
        }

        If ApplicationData.Current.LocalSettings.Values("circulotile") = "on" Then
            imagen.HintCrop = AdaptiveImageCrop.Circle
        Else
            imagen.HintCrop = AdaptiveImageCrop.Default
        End If

        Dim fondoImagen As TileBackgroundImage = New TileBackgroundImage With {
            .Source = "ms-appdata:///local/" + ficheroImagen.Name,
            .HintOverlay = Integer.Parse(ApplicationData.Current.LocalSettings.Values("overlaytile"))
        }

        If ApplicationData.Current.LocalSettings.Values("circulotile") = "on" Then
            fondoImagen.HintCrop = AdaptiveImageCrop.Circle
        Else
            fondoImagen.HintCrop = AdaptiveImageCrop.Default
        End If

        '-----------------------

        Dim contenidoWide As TileBindingContentAdaptive = New TileBindingContentAdaptive With {
            .BackgroundImage = fondoImagen
        }

        If Not imagenDRM Is Nothing Then
            contenidoWide.Children.Add(imagenDRM)
        End If

        Dim tileWide As TileBinding = New TileBinding With {
            .Content = contenidoWide
        }

        '-----------------------

        Dim contenidoSmall As TileBindingContentAdaptive = New TileBindingContentAdaptive
        contenidoSmall.Children.Add(imagen)

        If Not imagenDRM Is Nothing Then
            contenidoSmall.Children.Add(imagenDRM)
        End If

        Dim tileSmall As TileBinding = New TileBinding With {
            .Content = contenidoSmall
        }

        '-----------------------

        Dim contenidoMedium As TileBindingContentAdaptive = New TileBindingContentAdaptive
        contenidoMedium.Children.Add(imagen)

        If Not imagenDRM Is Nothing Then
            contenidoMedium.Children.Add(imagenDRM)
        End If

        Dim tileMedium As TileBinding = New TileBinding With {
            .Content = contenidoMedium
        }

        '-----------------------

        Dim contenidoLarge As TileBindingContentAdaptive = New TileBindingContentAdaptive
        contenidoLarge.Children.Add(imagen)

        If Not imagenDRM Is Nothing Then
            contenidoLarge.Children.Add(imagenDRM)
        End If

        Dim tileLarge As TileBinding = New TileBinding With {
            .Content = contenidoLarge
        }

        '-----------------------

        If ApplicationData.Current.LocalSettings.Values("titulotile") = "on" Then
            tileWide.Branding = TileBranding.Name
            tileSmall.Branding = TileBranding.Name
            tileMedium.Branding = TileBranding.Name
            tileLarge.Branding = TileBranding.Name
        End If

        Dim visual As TileVisual = New TileVisual With {
            .TileWide = tileWide,
            .TileSmall = tileSmall,
            .TileMedium = tileMedium,
            .TileLarge = tileLarge
        }

        Dim contenido As TileContent = New TileContent With {
            .Visual = visual
        }

        Dim notificacion As TileNotification = New TileNotification(contenido.GetXml)

        TileUpdateManager.CreateTileUpdaterForSecondaryTile(tile.ID).Update(notificacion)

    End Sub

End Module
