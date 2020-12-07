Imports Microsoft.Toolkit.Uwp.UI.Animations
Imports Windows.Services.Store
Imports Windows.Storage
Imports Windows.System
Imports Windows.UI
Imports Windows.UI.Core

Module MasCosas

    Dim codigoFuente As String = "https://github.com/pepeizq/Steam-Tiles"
    Dim traduccion As String = "https://poeditor.com/join/project/93E4lCQLWI"
    Dim calificar As Boolean = True
    Dim youtube As String = "https://www.youtube.com/watch?v=rsm54AbJZZk"
    Dim pepeizqapps As Boolean = True
    Dim pepeizqdeals As Boolean = True
    Dim twitter As String = "https://twitter.com/pepeizqu"

    Public Sub Cargar()

        Dim recursos As New Resources.ResourceLoader

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        Dim grid As Grid = pagina.FindName("gridMasCosas")

        Dim sv As New ScrollViewer With {
            .VerticalScrollBarVisibility = ScrollBarVisibility.Auto
        }

        Dim sp As New StackPanel With {
            .Orientation = Orientation.Vertical,
            .Padding = New Thickness(10, 10, 30, 10)
        }

        If Not codigoFuente = Nothing Then
            sp.Children.Add(GenerarCaja(recursos.GetString("MoreThingsSourceCode"), recursos.GetString("MoreThingsSourceCodeDescription"),
                                        codigoFuente, FontAwesome5.EFontAwesomeIcon.Brands_Github, False, "github.png"))
        End If

        If Not traduccion = Nothing Then
            sp.Children.Add(GenerarCaja(recursos.GetString("MoreThingsHelpTranslate"), recursos.GetString("MoreThingsHelpTranslateDescription"),
                                        traduccion, FontAwesome5.EFontAwesomeIcon.Solid_GlobeEurope, False, "traducir.png"))
        End If

        If calificar = True Then
            sp.Children.Add(GenerarCaja(recursos.GetString("MoreThingsRateApp"), recursos.GetString("MoreThingsRateAppDescription"),
                                        Nothing, FontAwesome5.EFontAwesomeIcon.Solid_ThumbsUp, True, Nothing))
        End If

        If Not youtube = Nothing Then
            sp.Children.Add(GenerarCaja(recursos.GetString("MoreThingsVideo"), recursos.GetString("MoreThingsVideoDescription"),
                                        youtube, FontAwesome5.EFontAwesomeIcon.Brands_Youtube, False, "youtube.png"))
        End If

        If pepeizqapps = True Then
            sp.Children.Add(GenerarCaja("pepeizqapps.com", recursos.GetString("MoreThingspepeizqappsDescription"),
                                        "https://pepeizqapps.com/", FontAwesome5.EFontAwesomeIcon.Solid_Cube, False, "pepeizqapps.png"))
        End If

        If pepeizqdeals = True Then
            sp.Children.Add(GenerarCaja("pepeizqdeals.com", recursos.GetString("MoreThingspepeizqdealsDescription"),
                                        "https://pepeizqdeals.com/", FontAwesome5.EFontAwesomeIcon.Solid_Cube, False, "pepeizqdeals.png"))
        End If

        If Not twitter = Nothing Then
            sp.Children.Add(GenerarCaja("@pepeizqu", recursos.GetString("MoreThingsMyTwitterDescription"),
                                        twitter, FontAwesome5.EFontAwesomeIcon.Brands_Twitter, False, "twitter.png"))
        End If

        If sp.Children.Count > 1 Then
            Dim botonUltimo As Button = sp.Children(sp.Children.Count - 1)
            botonUltimo.Margin = New Thickness(0, 0, 0, 0)
        End If

        sv.Content = sp
        grid.Children.Add(sv)

    End Sub

    Private Function GenerarCaja(titulo As String, descripcion As String, enlace As String, icono2 As FontAwesome5.EFontAwesomeIcon, calificar As Boolean, imagenFondo As String)

        Dim recursos As New Resources.ResourceLoader

        Dim colorFondo As New SolidColorBrush With {
            .Color = App.Current.Resources("ColorCuarto"),
            .Opacity = 0.5
        }

        Dim spBoton As New StackPanel With {
            .Orientation = Orientation.Vertical,
            .Padding = New Thickness(30, 30, 30, 30),
            .BorderBrush = New SolidColorBrush(App.Current.Resources("ColorPrimario")),
            .BorderThickness = New Thickness(1, 1, 1, 1),
            .Background = colorFondo
        }

        Dim spTitulo As New StackPanel With {
            .Orientation = Orientation.Horizontal
        }

        Dim icono As New FontAwesome5.FontAwesome With {
            .Icon = icono2,
            .Foreground = New SolidColorBrush(Colors.White),
            .VerticalAlignment = VerticalAlignment.Center
        }

        spTitulo.Children.Add(icono)

        Dim tbTitulo As New TextBlock With {
            .Text = titulo,
            .Margin = New Thickness(15, 0, 0, 0),
            .Foreground = New SolidColorBrush(Colors.White),
            .FontSize = 17,
            .VerticalAlignment = VerticalAlignment.Center
        }

        spTitulo.Children.Add(tbTitulo)

        spBoton.Children.Add(spTitulo)

        Dim tbDescripcion As New TextBlock With {
            .Text = descripcion,
            .Margin = New Thickness(0, 15, 0, 0),
            .Foreground = New SolidColorBrush(Colors.White),
            .FontSize = 15,
            .VerticalAlignment = VerticalAlignment.Center,
            .TextWrapping = TextWrapping.Wrap
        }

        spBoton.Children.Add(tbDescripcion)

        Dim boton As New Button With {
            .Content = spBoton,
            .Padding = New Thickness(0, 0, 0, 0),
            .BorderBrush = New SolidColorBrush(Colors.Transparent),
            .BorderThickness = New Thickness(0, 0, 0, 0),
            .Style = App.Current.Resources("ButtonRevealStyle"),
            .Margin = New Thickness(0, 0, 0, 30),
            .HorizontalAlignment = HorizontalAlignment.Stretch,
            .HorizontalContentAlignment = HorizontalAlignment.Stretch
        }

        AddHandler boton.PointerEntered, AddressOf Entra_Boton
        AddHandler boton.PointerExited, AddressOf Sale_Boton

        If calificar = False Then
            AddHandler boton.Click, AddressOf AbrirClick
        Else
            AddHandler boton.Click, AddressOf CalificarClick
        End If

        Dim tbToolTip As TextBlock = New TextBlock With {
            .FontSize = 16,
            .TextWrapping = TextWrapping.Wrap
        }

        If calificar = False Then
            tbToolTip.Text = recursos.GetString("MoreThingsOpen")
        Else
            tbToolTip.Text = recursos.GetString("MoreThingsRate")
        End If

        ToolTipService.SetToolTip(boton, tbToolTip)
        ToolTipService.SetPlacement(boton, PlacementMode.Mouse)

        If Not imagenFondo = Nothing Then
            Dim fondoBoton As New ImageBrush With {
                .ImageSource = New BitmapImage(New Uri("ms-appx:///Assets/MasCosas/" + imagenFondo)),
                .Stretch = Stretch.UniformToFill,
                .Opacity = 0.1,
                .AlignmentY = AlignmentY.Top
            }

            boton.Background = fondoBoton
        End If

        Return boton

    End Function

    Private Async Sub AbrirClick(sender As Object, e As RoutedEventArgs)

        Dim boton As Button = sender
        Dim enlace As String = boton.Tag

        Try
            Await Launcher.LaunchUriAsync(New Uri(enlace))
        Catch ex As Exception

        End Try

    End Sub

    Private Sub CalificarClick(sender As Object, e As RoutedEventArgs)

        CalificarApp(False)

    End Sub

    Public Async Sub CalificarApp(primeraVez As Boolean)

        Dim recursos As New Resources.ResourceLoader()

        Dim usuarios As IReadOnlyList(Of User) = Await User.FindAllAsync

        If Not usuarios Is Nothing Then
            If usuarios.Count > 0 Then
                Dim usuario As User = usuarios(0)

                Dim contexto As StoreContext = StoreContext.GetForUser(usuario)
                Dim config As ApplicationDataContainer = ApplicationData.Current.LocalSettings

                Dim sacarVentana As Boolean = True

                If primeraVez = True Then
                    If config.Values("Calificar_App") = 1 Then
                        sacarVentana = False
                    End If
                End If

                If sacarVentana = True Then
                    Dim review As StoreRateAndReviewResult = Await contexto.RequestRateAndReviewAppAsync

                    If review.Status = StoreRateAndReviewStatus.Succeeded Then
                        Notificaciones.Toast(recursos.GetString("MoreThingsRateAppThanks"), Nothing)
                        config.Values("Calificar_App") = 1
                    ElseIf review.Status = StoreRateAndReviewStatus.Error Then
                        Await Launcher.LaunchUriAsync(New Uri("ms-windows-store:REVIEW?PFN=" + Package.Current.Id.FamilyName))
                        config.Values("Calificar_App") = 1
                    Else
                        config.Values("Calificar_App") = 0
                    End If
                End If
            End If
        End If

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