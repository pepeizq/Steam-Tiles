Imports System.Globalization
Imports Microsoft.Toolkit.Uwp.UI.Animations
Imports Microsoft.Toolkit.Uwp.UI.Controls
Imports Newtonsoft.Json
Imports Windows.Globalization.NumberFormatting
Imports Windows.System
Imports Windows.UI
Imports Windows.UI.Core

Namespace Interfaz
    Module MasTiles

        Public Async Sub Cargar()

            Dim recursos As New Resources.ResourceLoader()

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim grid As Grid = pagina.FindName("gridMasTiles")
            grid.Children.Clear()

            Try
                Dim sv As New ScrollViewer With {
                    .VerticalScrollBarVisibility = ScrollBarVisibility.Auto
                }

                Dim sp As New StackPanel With {
                    .Orientation = Orientation.Vertical,
                    .Padding = New Thickness(10, 10, 30, 10)
                }

                sv.Content = sp
                grid.Children.Add(sv)

                Dim idsConsultar As New List(Of String) From {
                    "9NXV34SMGBHT", 'Amazon
                    "9NL9Z5CC0FKM", 'Bethesda
                    "9NLKV74DDS0M", 'Blizzard
                    "9MWSP8WMMWHX", 'Discord
                    "9NZMQV0HB386", 'Epic Games
                    "9NBLGGH52SWD", 'GOG
                    "9P6TTBLTHP0L", 'Origin
                    "9NBLGGH51SB3", 'Steam
                    "9NV7SS9FBV6L", 'Twitch
                    "9NF9PH08FRSJ"  'Ubisoft
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
                                For Each app2 In apps.Apps
                                    Dim fondo As New SolidColorBrush With {
                                        .Opacity = 0.5,
                                        .Color = App.Current.Resources("ColorCuarto")
                                    }

                                    Dim spApp As New StackPanel With {
                                        .Orientation = Orientation.Horizontal,
                                        .BorderBrush = New SolidColorBrush(App.Current.Resources("ColorPrimario")),
                                        .BorderThickness = New Thickness(1, 1, 1, 1),
                                        .Background = fondo,
                                        .Padding = New Thickness(25, 25, 25, 25)
                                    }

                                    Dim imagenS As String = app2.Detalles(0).Imagenes(0).Enlace

                                    If Not imagenS.Contains("http:") Then
                                        imagenS = "http:" + imagenS
                                    End If

                                    Dim imagen As New ImageEx With {
                                        .IsCacheEnabled = True,
                                        .Width = 100,
                                        .Height = 100,
                                        .Source = imagenS,
                                        .Opacity = 0.9
                                    }

                                    spApp.Children.Add(imagen)

                                    Dim spDatos As New StackPanel With {
                                        .Orientation = Orientation.Vertical,
                                        .Margin = New Thickness(30, 0, 0, 0),
                                        .VerticalAlignment = VerticalAlignment.Center
                                    }

                                    Dim spDatos2 As New StackPanel With {
                                        .Orientation = Orientation.Horizontal
                                    }

                                    Dim tbTitulo As New TextBlock With {
                                        .Foreground = New SolidColorBrush(Colors.White),
                                        .Text = app2.Detalles(0).Titulo,
                                        .FontSize = 19
                                    }

                                    spDatos2.Children.Add(tbTitulo)

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

                                    If Not precio = String.Empty Then
                                        Dim tbPrecio As New TextBlock With {
                                            .Foreground = New SolidColorBrush(Colors.White),
                                            .Text = precio,
                                            .FontSize = 15,
                                            .Margin = New Thickness(20, 0, 0, 0),
                                            .VerticalAlignment = VerticalAlignment.Center
                                        }

                                        spDatos2.Children.Add(tbPrecio)
                                    End If

                                    spDatos.Children.Add(spDatos2)

                                    Dim tbDescripcion As New TextBlock With {
                                        .Foreground = New SolidColorBrush(Colors.White),
                                        .Text = app2.Detalles(0).Descripcion,
                                        .FontSize = 16,
                                        .Margin = New Thickness(0, 20, 0, 0)
                                    }

                                    spDatos.Children.Add(tbDescripcion)

                                    spApp.Children.Add(spDatos)

                                    Dim boton As New Button With {
                                        .Tag = app2.ID,
                                        .Content = spApp,
                                        .Padding = New Thickness(0, 0, 0, 0),
                                        .BorderBrush = New SolidColorBrush(Colors.Transparent),
                                        .BorderThickness = New Thickness(0, 0, 0, 0),
                                        .Style = App.Current.Resources("ButtonRevealStyle"),
                                        .Margin = New Thickness(0, 0, 0, 30),
                                        .HorizontalAlignment = HorizontalAlignment.Stretch,
                                        .HorizontalContentAlignment = HorizontalAlignment.Stretch
                                    }

                                    AddHandler boton.Click, AddressOf ComprarAppClick
                                    AddHandler boton.PointerEntered, AddressOf Entra_Boton
                                    AddHandler boton.PointerExited, AddressOf Sale_Boton

                                    Dim imagenF As String = String.Empty

                                    For Each imagen2 In app2.Detalles(0).Imagenes
                                        If imagen2.Posicion = "Desktop/0" Then
                                            imagenF = imagen2.Enlace
                                        End If
                                    Next

                                    If Not imagenF = String.Empty Then
                                        If Not imagenF.Contains("http:") Then
                                            imagenF = "http:" + imagenF
                                        End If

                                        Dim fondoBoton As New ImageBrush With {
                                            .ImageSource = New BitmapImage(New Uri(imagenF)),
                                            .Stretch = Stretch.UniformToFill,
                                            .Opacity = 0.1
                                        }

                                        boton.Background = fondoBoton
                                    End If

                                    Dim tbToolTip As TextBlock = New TextBlock With {
                                        .FontSize = 16,
                                        .TextWrapping = TextWrapping.Wrap,
                                        .Text = recursos.GetString("MoreTilesOpen")
                                    }

                                    ToolTipService.SetToolTip(boton, tbToolTip)
                                    ToolTipService.SetPlacement(boton, PlacementMode.Mouse)

                                    sp.Children.Add(boton)
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
                GenerarError(grid)
            End Try

        End Sub

        Private Sub GenerarError(grid As Grid)

            grid.Children.Clear()

            Dim recursos As New Resources.ResourceLoader()

            Dim sp As New StackPanel With {
                .Orientation = Orientation.Vertical,
                .HorizontalAlignment = HorizontalAlignment.Center,
                .VerticalAlignment = VerticalAlignment.Center
            }

            Dim tbMensaje As New TextBlock With {
                .Text = recursos.GetString("MoreTilesError"),
                .Foreground = New SolidColorBrush(Colors.White),
                .TextWrapping = TextWrapping.Wrap
            }

            sp.Children.Add(tbMensaje)

            Dim tbBoton As New TextBlock With {
                .Text = recursos.GetString("OpenWeb"),
                .Foreground = New SolidColorBrush(Colors.White)
            }

            Dim boton As New Button With {
                .Content = tbBoton,
                .HorizontalAlignment = HorizontalAlignment.Center,
                .BorderBrush = New SolidColorBrush(Colors.Transparent),
                .BorderThickness = New Thickness(0, 0, 0, 0),
                .Style = App.Current.Resources("ButtonRevealStyle"),
                .Margin = New Thickness(0, 30, 0, 0),
                .Padding = New Thickness(15, 12, 15, 12)
            }

            AddHandler boton.Click, AddressOf AbrirWebClick
            AddHandler boton.PointerEntered, AddressOf Entra_Boton_Texto
            AddHandler boton.PointerExited, AddressOf Sale_Boton_Texto

            sp.Children.Add(boton)

            grid.Children.Add(sp)

        End Sub

        Private Async Sub ComprarAppClick(sender As Object, e As RoutedEventArgs)

            Dim boton As Button = sender
            Await Launcher.LaunchUriAsync(New Uri("ms-windows-store://pdp/?ProductId=" + boton.Tag))

        End Sub

        Private Async Sub AbrirWebClick(sender As Object, e As RoutedEventArgs)

            Await Launcher.LaunchUriAsync(New Uri("https://pepeizqapps.com/"))

        End Sub

        Private Sub Entra_Boton(sender As Object, e As PointerRoutedEventArgs)

            Dim boton As Button = sender
            Dim sp As StackPanel = boton.Content

            Dim fondo As New SolidColorBrush With {
                .Opacity = 0.7,
                .Color = App.Current.Resources("ColorCuarto")
            }

            sp.Background = fondo
            sp.Saturation(1).Scale(1.01, 1.01, sp.ActualWidth / 2, sp.ActualHeight / 2).Start()

            Window.Current.CoreWindow.PointerCursor = New CoreCursor(CoreCursorType.Hand, 1)

        End Sub

        Private Sub Sale_Boton(sender As Object, e As PointerRoutedEventArgs)

            Dim boton As Button = sender
            Dim sp As StackPanel = boton.Content

            Dim fondo As New SolidColorBrush With {
                .Opacity = 0.5,
                .Color = App.Current.Resources("ColorCuarto")
            }

            sp.Background = fondo
            sp.Saturation(1).Scale(1, 1, sp.ActualWidth / 2, sp.ActualHeight / 2).Start()

            Window.Current.CoreWindow.PointerCursor = New CoreCursor(CoreCursorType.Arrow, 1)

        End Sub

    End Module

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
End Namespace