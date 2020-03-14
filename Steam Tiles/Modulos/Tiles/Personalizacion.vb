Imports Microsoft.Toolkit.Uwp.UI.Controls
Imports Windows.Storage

Namespace Tiles
    Module Personalizacion

        Public Async Sub Cargar(grid As Grid, tipo As Integer)

            Dim anchoExterior As Integer = 0
            Dim altoExterior As Integer = 0

            Dim anchoInterior As Integer = 0
            Dim altoInterior As Integer = 0

            If tipo = 0 Then
                anchoExterior = 142
                altoExterior = 142

                anchoInterior = 71
                altoInterior = 71
            ElseIf tipo = 1 Then
                anchoExterior = 300
                altoExterior = 300

                anchoInterior = 150
                altoInterior = 150
            ElseIf tipo = 2 Then
                anchoExterior = 620
                altoExterior = 300

                anchoInterior = 310
                altoInterior = 150
            ElseIf tipo = 3 Then
                anchoExterior = 620
                altoExterior = 620

                anchoInterior = 310
                altoInterior = 310
            End If

            Dim id As Integer = 0

            Dim carpeta As StorageFolder = ApplicationData.Current.LocalFolder
            Dim ficheros As IReadOnlyList(Of StorageFile) = Await carpeta.GetFilesAsync()

            For Each fichero In ficheros
                id = id + 1
            Next

            Await Tiles.Imagen.Generar(grid, "personalizacion" + id.ToString + ".png", anchoInterior, altoInterior)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim botonVolver As Button = pagina.FindName("botonVolverPersonalizacion")
            botonVolver.Tag = tipo

            RemoveHandler botonVolver.Click, AddressOf Volver
            AddHandler botonVolver.Click, AddressOf Volver

            Dim gridPersonalizar As Grid = pagina.FindName("gridPersonalizarTiles")
            gridPersonalizar.Visibility = Visibility.Visible

            Dim gridExterior As Grid = pagina.FindName("gridPersonalizacionExterior")
            gridExterior.Width = anchoExterior
            gridExterior.Height = altoExterior

            Dim gridInterior As Grid = pagina.FindName("gridPersonalizacionInterior")
            gridInterior.Width = anchoInterior
            gridInterior.Height = altoInterior

            Dim imagen As New ImageEx With {
                .Source = "ms-appdata:///local/personalizacion" + id.ToString + ".png"
            }

            gridInterior.Children.Add(imagen)

        End Sub

        Public Sub Volver(sender As Object, e As RoutedEventArgs)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim gridAñadirTile As Grid = pagina.FindName("gridAñadirTile")
            gridAñadirTile.Visibility = Visibility.Visible

            Dim gridPersonalizarTiles As Grid = pagina.FindName("gridPersonalizarTiles")
            gridPersonalizarTiles.Visibility = Visibility.Collapsed

        End Sub

    End Module
End Namespace

