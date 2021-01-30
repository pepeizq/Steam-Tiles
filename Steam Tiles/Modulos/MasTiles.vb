Imports System.Globalization
Imports System.Net
Imports Microsoft.Toolkit.Uwp.UI.Animations
Imports Microsoft.Toolkit.Uwp.UI.Controls
Imports Newtonsoft.Json
Imports Windows.Globalization.NumberFormatting
Imports Windows.System
Imports Windows.UI
Imports Windows.UI.Core

Module MasTiles

    Public Async Sub Cargar()

        Dim recursos As New Resources.ResourceLoader()

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        Dim boton As Button = pagina.FindName("botonMasTiles")
        boton.Background = New SolidColorBrush(Colors.Transparent)
        boton.BorderBrush = New SolidColorBrush(Colors.Transparent)
        boton.BorderThickness = New Thickness(0, 0, 0, 0)
        boton.Style = App.Current.Resources("ButtonRevealStyle")

        AddHandler boton.PointerEntered, AddressOf Interfaz.EfectosHover.Entra_Basico
        AddHandler boton.PointerExited, AddressOf Interfaz.EfectosHover.Sale_Basico

        Dim spBoton As New StackPanel With {
            .Orientation = Orientation.Horizontal
        }

        Dim icono As New FontAwesome5.FontAwesome With {
            .Foreground = New SolidColorBrush(App.Current.Resources("ColorPrimario")),
            .Icon = FontAwesome5.EFontAwesomeIcon.Solid_ThLarge,
            .Margin = New Thickness(0, 0, 8, 0),
            .FontSize = 15
        }

        spBoton.Children.Add(icono)

        Dim tb As New TextBlock With {
            .Foreground = New SolidColorBrush(App.Current.Resources("ColorPrimario")),
            .Text = recursos.GetString("MoreTiles")
        }

        spBoton.Children.Add(tb)
        boton.Content = spBoton

        '------------------------------------------

        Dim sp As StackPanel = pagina.FindName("spMasTiles")

        Try
            Dim idsConsultar As New List(Of String) From {
                "9NXV34SMGBHT", 'Amazon Juegos
                "9P8MS4J7F6V6", 'Amazon Videos
                "9NL9Z5CC0FKM", 'Bethesda
                "9NLKV74DDS0M", 'Blizzard
                "9MWSP8WMMWHX", 'Discord
                "9NZMQV0HB386", 'Epic Games
                "9NBLGGH52SWD", 'GOG
                "9MVGBL5QV1NR", 'Netflix
                "9P6TTBLTHP0L", 'Origin
                "9MZKKMC82W60", 'Spotify
                "9P4N7M24PQV4", 'Stadia
                "9NBLGGH51SB3", 'Steam
                "9NV7SS9FBV6L", 'Twitch
                "9NF9PH08FRSJ", 'Ubisoft
                "9N90JJC255ZL"  'Webs
            }

            Dim i As Integer = 0
            For Each id In idsConsultar
                If id = Trial.idTienda Then
                    Exit For
                End If
                i += 1
            Next
            idsConsultar.RemoveAt(i)

            Dim ids As String = String.Empty

            For Each id In idsConsultar
                ids = ids + id + ","
            Next

            If ids.Length > 0 Then
                ids = ids.Remove(ids.Length - 1)

                Dim idiomas As IReadOnlyList(Of String) = UserProfile.GlobalizationPreferences.Languages
                Dim idioma As String = String.Empty

                If idiomas.Count > 0 Then
                    idioma = idiomas(0)
                Else
                    idioma = "en-us"
                End If

                Dim pais As New Windows.Globalization.GeographicRegion
                Dim html As String = Await HttpClient(New Uri("https://displaycatalog.mp.microsoft.com/v7.0/products?bigIds=" + ids + "&market=" + pais.CodeTwoLetter + "&languages=" + idioma + "&MS-CV=DGU1mcuYo0WMMp+F.1"))

                If Not html = Nothing Then
                    Dim apps As MicrosoftStoreBBDDDetalles = JsonConvert.DeserializeObject(Of MicrosoftStoreBBDDDetalles)(html)

                    If Not apps Is Nothing Then
                        If apps.Apps.Count > 0 Then
                            Dim listaApps As New List(Of MasTileApp)

                            For Each app2 In apps.Apps
                                Dim titulo As String = WebUtility.HtmlDecode(app2.Detalles(0).Titulo)

                                Dim imagen As String = app2.Detalles(0).Imagenes(0).Enlace

                                If Not imagen.Contains("http:") Then
                                    imagen = "http:" + imagen
                                End If

                                Dim precio As String = String.Empty

                                Try
                                    Dim tempDouble As Double = Double.Parse(app2.Propiedades2(0).Disponible(0).Datos.Precio.PrecioRebajado, CultureInfo.InvariantCulture).ToString

                                    Dim moneda As String = app2.Propiedades2(0).Disponible(0).Datos.Precio.Divisa

                                    Dim formateador As New CurrencyFormatter(moneda) With {
                                        .Mode = CurrencyFormatterMode.UseSymbol
                                    }

                                    precio = formateador.Format(tempDouble)
                                Catch ex As Exception

                                End Try

                                listaApps.Add(New MasTileApp(titulo, imagen, app2.ID, precio))
                            Next

                            listaApps.Sort(Function(x, y) x.Titulo.CompareTo(y.Titulo))

                            For Each app2 In listaApps
                                Dim fondo As New SolidColorBrush With {
                                    .Opacity = 0.8,
                                    .Color = App.Current.Resources("ColorCuarto")
                                }

                                Dim gridApp As New Grid With {
                                    .Padding = New Thickness(10, 10, 10, 10),
                                    .BorderBrush = New SolidColorBrush(App.Current.Resources("ColorPrimario")),
                                    .BorderThickness = New Thickness(1.5, 1.5, 1.5, 1.5),
                                    .Background = fondo
                                }

                                Dim col1 As New ColumnDefinition
                                Dim col2 As New ColumnDefinition
                                Dim col3 As New ColumnDefinition

                                col1.Width = New GridLength(1, GridUnitType.Auto)
                                col2.Width = New GridLength(1, GridUnitType.Star)
                                col3.Width = New GridLength(1, GridUnitType.Auto)

                                gridApp.ColumnDefinitions.Add(col1)
                                gridApp.ColumnDefinitions.Add(col2)
                                gridApp.ColumnDefinitions.Add(col3)

                                Dim imagen As New ImageEx With {
                                    .IsCacheEnabled = True,
                                    .Width = 60,
                                    .Height = 60,
                                    .Source = app2.Imagen,
                                    .EnableLazyLoading = True
                                }

                                imagen.SetValue(Grid.ColumnProperty, 0)
                                gridApp.Children.Add(imagen)

                                Dim tbTitulo As New TextBlock With {
                                    .Foreground = New SolidColorBrush(Colors.White),
                                    .Text = app2.Titulo,
                                    .TextWrapping = TextWrapping.Wrap,
                                    .Margin = New Thickness(10, 0, 15, 0),
                                    .VerticalAlignment = VerticalAlignment.Center
                                }

                                tbTitulo.SetValue(Grid.ColumnProperty, 1)
                                gridApp.Children.Add(tbTitulo)

                                If Not app2.Precio = String.Empty Then
                                    Dim tbPrecio As New TextBlock With {
                                        .Foreground = New SolidColorBrush(Colors.White),
                                        .Text = app2.Precio,
                                        .VerticalAlignment = VerticalAlignment.Center
                                    }

                                    tbPrecio.SetValue(Grid.ColumnProperty, 2)
                                    gridApp.Children.Add(tbPrecio)
                                End If

                                Dim botonApp As New Button With {
                                    .Tag = app2.ID,
                                    .Content = gridApp,
                                    .Padding = New Thickness(0, 0, 0, 0),
                                    .BorderBrush = New SolidColorBrush(Colors.Transparent),
                                    .BorderThickness = New Thickness(0, 0, 0, 0),
                                    .Style = App.Current.Resources("ButtonRevealStyle"),
                                    .Margin = New Thickness(0, 0, 0, 15),
                                    .HorizontalAlignment = HorizontalAlignment.Stretch,
                                    .HorizontalContentAlignment = HorizontalAlignment.Stretch,
                                    .MaxWidth = 400
                                }

                                AddHandler botonApp.Click, AddressOf ComprarAppClick
                                AddHandler botonApp.PointerEntered, AddressOf Entra_Boton
                                AddHandler botonApp.PointerExited, AddressOf Sale_Boton

                                sp.Children.Add(botonApp)
                            Next
                        End If
                    End If
                End If

                If sp.Children.Count > 1 Then
                    Dim botonUltimo As Button = sp.Children(sp.Children.Count - 1)
                    botonUltimo.Margin = New Thickness(0, 0, 0, 0)
                End If
            End If
        Catch ex As Exception
            Dim tbError As New TextBlock With {
                .Foreground = New SolidColorBrush(Colors.White),
                .Text = recursos.GetString("MoreTilesError"),
                .HorizontalAlignment = HorizontalAlignment.Center
            }

            sp.Children.Add(tbError)
        End Try

    End Sub

    Private Async Sub ComprarAppClick(sender As Object, e As RoutedEventArgs)

        Dim boton As Button = sender
        Await Launcher.LaunchUriAsync(New Uri("ms-windows-store://pdp/?ProductId=" + boton.Tag))

    End Sub

    Private Sub Entra_Boton(sender As Object, e As PointerRoutedEventArgs)

        Dim boton As Button = sender
        Dim grid As Grid = boton.Content

        Dim fondo As New SolidColorBrush With {
            .Opacity = 1,
            .Color = App.Current.Resources("ColorCuarto")
        }

        grid.Background = fondo
        grid.Saturation(1).Scale(1.02, 1.02, grid.ActualWidth / 2, grid.ActualHeight / 2).Start()

        Window.Current.CoreWindow.PointerCursor = New CoreCursor(CoreCursorType.Hand, 1)

    End Sub

    Private Sub Sale_Boton(sender As Object, e As PointerRoutedEventArgs)

        Dim boton As Button = sender
        Dim grid As Grid = boton.Content

        Dim fondo As New SolidColorBrush With {
            .Opacity = 0.8,
            .Color = App.Current.Resources("ColorCuarto")
        }

        grid.Background = fondo
        grid.Saturation(1).Scale(1, 1, grid.ActualWidth / 2, grid.ActualHeight / 2).Start()

        Window.Current.CoreWindow.PointerCursor = New CoreCursor(CoreCursorType.Arrow, 1)

    End Sub

End Module

Public Class MasTileApp

    Public Titulo As String
    Public Imagen As String
    Public ID As String
    Public Precio As String

    Public Sub New(titulo As String, imagen As String, id As String, precio As String)
        Me.Titulo = titulo
        Me.Imagen = imagen
        Me.ID = id
        Me.Precio = precio
    End Sub

End Class

Public Class MicrosoftStoreBBDDDetalles

    <JsonProperty("Products")>
    Public Apps As List(Of MicrosoftStoreBBDDDetallesJuego)

End Class

Public Class MicrosoftStoreBBDDDetallesJuego

    <JsonProperty("LocalizedProperties")>
    Public Detalles As List(Of MicrosoftStoreBBDDDetallesJuego2)

    <JsonProperty("ProductId")>
    Public ID As String

    <JsonProperty("DisplaySkuAvailabilities")>
    Public Propiedades2 As List(Of MicrosoftStoreBBDDDetallesPropiedades2)

End Class

Public Class MicrosoftStoreBBDDDetallesJuego2

    <JsonProperty("ProductTitle")>
    Public Titulo As String

    <JsonProperty("ProductDescription")>
    Public Descripcion As String

    <JsonProperty("Images")>
    Public Imagenes As List(Of MicrosoftStoreBBDDDetallesJuego2Imagen)

End Class

Public Class MicrosoftStoreBBDDDetallesJuego2Imagen

    <JsonProperty("ImagePurpose")>
    Public Proposito As String

    <JsonProperty("Uri")>
    Public Enlace As String

    <JsonProperty("ImagePositionInfo")>
    Public Posicion As String

End Class

Public Class MicrosoftStoreBBDDDetallesPropiedades2

    <JsonProperty("Availabilities")>
    Public Disponible As List(Of MicrosoftStoreBBDDDetallesPropiedades2Disponibilidad)

End Class

Public Class MicrosoftStoreBBDDDetallesPropiedades2Disponibilidad

    <JsonProperty("OrderManagementData")>
    Public Datos As MicrosoftStoreBBDDDetallesPropiedades2DisponibilidadDatos

End Class

Public Class MicrosoftStoreBBDDDetallesPropiedades2DisponibilidadDatos

    <JsonProperty("Price")>
    Public Precio As MicrosoftStoreBBDDDetallesPropiedades2DisponibilidadDatosPrecio

End Class

Public Class MicrosoftStoreBBDDDetallesPropiedades2DisponibilidadDatosPrecio

    <JsonProperty("ListPrice")>
    Public PrecioRebajado As String

    <JsonProperty("MSRP")>
    Public PrecioBase As String

    <JsonProperty("CurrencyCode")>
    Public Divisa As String

End Class
