Imports Microsoft.Toolkit.Uwp.Notifications
Imports Microsoft.Toolkit.Uwp.UI.Controls
Imports Windows.Networking.BackgroundTransfer
Imports Windows.Storage
Imports Windows.UI.Notifications
Imports Windows.UI.StartScreen

Module Tiles

    Public Async Sub Generar(tile As Tile)

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        Dim boton As Button = pagina.FindName("botonAñadirTile")
        boton.IsEnabled = False

        Dim nuevaTile As New SecondaryTile(tile.ID, tile.Titulo, tile.Enlace.ToString, New Uri("ms-appdata:///local/" + tile.ID + "ancha.png", UriKind.RelativeOrAbsolute), TileSize.Wide310x150)

        nuevaTile.VisualElements.Wide310x150Logo = New Uri("ms-appdata:///local/" + tile.ID + "ancha.png", UriKind.RelativeOrAbsolute)
        nuevaTile.VisualElements.Square310x310Logo = New Uri("ms-appdata:///local/" + tile.ID + "grande.png", UriKind.RelativeOrAbsolute)

        Await nuevaTile.RequestCreateAsync()

        '-----------------------

        Dim imagenDRM As AdaptiveImage = Nothing

        If ApplicationData.Current.LocalSettings.Values("logotile") = "on" Then
            imagenDRM = New AdaptiveImage With {
                .HintRemoveMargin = True,
                .HintAlign = AdaptiveImageAlign.Right
            }

            Dim cbIconosLista As ComboBox = pagina.FindName("cbTilesIconosLista")
            Dim imagenIcono As ImageEx = cbIconosLista.SelectedItem
            imagenDRM.Source = imagenIcono.Source
        End If

        '-----------------------

        Dim imagenPequeña As ImageEx = pagina.FindName("imagenTilePequeña")
        Dim boolImagenPequeña As Boolean = Await DescargaImagen(imagenPequeña, tile.ID + "pequena")

        Dim imagenMediana As ImageEx = pagina.FindName("imagenTileMediana")
        Dim boolImagenMediana As Boolean = Await DescargaImagen(imagenMediana, tile.ID + "mediana")

        Dim imagenAncha As ImageEx = pagina.FindName("imagenTileAncha")
        Dim boolImagenAncha As Boolean = Await DescargaImagen(imagenAncha, tile.ID + "ancha")

        Dim imagenGrande As ImageEx = pagina.FindName("imagenTileGrande")
        Dim boolImagenGrande As Boolean = Await DescargaImagen(imagenGrande, tile.ID + "grande")

        '-----------------------

        Dim contenidoPequeño As New TileBindingContentAdaptive

        If boolImagenPequeña = True Then
            Dim fondoImagenPequeña As New TileBackgroundImage With {
                .Source = "ms-appdata:///local/" + tile.ID + "pequena.png",
                .HintCrop = AdaptiveImageCrop.Default
            }

            contenidoPequeño = New TileBindingContentAdaptive With {
                .BackgroundImage = fondoImagenPequeña
            }
        End If

        'If Not imagenDRM Is Nothing Then
        '    contenidoSmall.Children.Add(imagenDRM)
        'End If

        Dim tilePequeño As New TileBinding With {
            .Content = contenidoPequeño
        }

        '-----------------------

        Dim contenidoMediano As New TileBindingContentAdaptive

        If boolImagenMediana = True Then
            Dim fondoImagenMediano As New TileBackgroundImage With {
                .Source = "ms-appdata:///local/" + tile.ID + "mediana.png",
                .HintCrop = AdaptiveImageCrop.Default
            }

            contenidoMediano = New TileBindingContentAdaptive With {
                .BackgroundImage = fondoImagenMediano
            }
        End If

        If Not imagenDRM Is Nothing Then
            contenidoMediano.Children.Add(imagenDRM)
        End If

        Dim tileMediano As New TileBinding With {
            .Content = contenidoMediano
        }

        '-----------------------

        Dim contenidoAncho As New TileBindingContentAdaptive

        If boolImagenAncha = True Then
            Dim fondoImagenAncha As New TileBackgroundImage With {
                .Source = "ms-appdata:///local/" + tile.ID + "ancha.png",
                .HintCrop = AdaptiveImageCrop.Default
            }

            contenidoAncho = New TileBindingContentAdaptive With {
                .BackgroundImage = fondoImagenAncha
            }
        End If

        If Not imagenDRM Is Nothing Then
            contenidoAncho.Children.Add(imagenDRM)
        End If

        Dim tileAncha As New TileBinding With {
            .Content = contenidoAncho
        }

        '-----------------------

        Dim contenidoGrande As New TileBindingContentAdaptive

        If boolImagenGrande = True Then
            Dim fondoImagenGrande As New TileBackgroundImage With {
                .Source = "ms-appdata:///local/" + tile.ID + "grande.png",
                .HintCrop = AdaptiveImageCrop.Default
            }

            contenidoGrande = New TileBindingContentAdaptive With {
                .BackgroundImage = fondoImagenGrande
            }
        End If

        If Not imagenDRM Is Nothing Then
            contenidoGrande.Children.Add(imagenDRM)
        End If

        Dim tileGrande As New TileBinding With {
            .Content = contenidoGrande
        }

        '-----------------------

        If ApplicationData.Current.LocalSettings.Values("titulotile") = "on" Then
            tileAncha.Branding = TileBranding.Name
            tilePequeño.Branding = TileBranding.Name
            tileMediano.Branding = TileBranding.Name
            tileGrande.Branding = TileBranding.Name
        End If

        Dim visual As New TileVisual With {
            .TileWide = tileAncha,
            .TileSmall = tilePequeño,
            .TileMedium = tileMediano,
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

        boton.IsEnabled = True

    End Sub

    Public Async Function DescargaImagen(imagen As ImageEx, clave As String) As Task(Of Boolean)

        Dim descargaFinalizada As Boolean = False

        Dim fuente As Object = imagen.Source

        If TypeOf fuente Is Uri Then
            Dim ficheroImagen As StorageFile = Await ApplicationData.Current.LocalFolder.CreateFileAsync(clave + ".png", CreationCollisionOption.ReplaceExisting)
            Dim descargador As New BackgroundDownloader

            Try
                Dim descarga As DownloadOperation = descargador.CreateDownload(fuente, ficheroImagen)
                Await descarga.StartAsync
                descargaFinalizada = True
            Catch ex As Exception

            End Try
        End If

        If TypeOf fuente Is BitmapImage Then
            Dim ficheroOrigen As StorageFile = imagen.Tag
            Dim ficheroNuevo As StorageFile = Await ApplicationData.Current.LocalFolder.CreateFileAsync(clave + ".png", CreationCollisionOption.ReplaceExisting)

            Await ficheroOrigen.CopyAndReplaceAsync(ficheroNuevo)
            descargaFinalizada = True
        End If

        Return descargaFinalizada
    End Function

End Module
