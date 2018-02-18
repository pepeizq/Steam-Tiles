Imports Microsoft.Services.Store.Engagement
Imports Microsoft.Toolkit.Uwp.Helpers
Imports Microsoft.Toolkit.Uwp.UI.Controls
Imports Windows.System
Imports Windows.UI
Imports Windows.UI.Core

Module MasCosas

    Dim volver As Boolean = False
    Dim traduccion As String = "https://poeditor.com/join/project/aKmScyB4QT"
    Dim codigoFuente As String = "https://github.com/pepeizq/Steam-Tiles"
    Dim gridPrincipalNombre As String = "gridTiles"

    Public Sub Generar()

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        Dim gridCosas As Grid = pagina.FindName("gridMasCosas")
        Dim sv As New ScrollViewer

        gridCosas.Children.Clear()
        gridCosas.Children.Add(sv)

        Dim gridRelleno As New Grid

        Dim col1 As New ColumnDefinition
        Dim col2 As New ColumnDefinition

        col1.Width = New GridLength(1, GridUnitType.Auto)
        col2.Width = New GridLength(1, GridUnitType.Star)

        gridRelleno.ColumnDefinitions.Add(col1)
        gridRelleno.ColumnDefinitions.Add(col2)

        '-----------------------------

        Dim sp As New StackPanel With {
            .Orientation = Orientation.Vertical,
            .Margin = New Thickness(20, 20, 20, 20),
            .BorderBrush = New SolidColorBrush(App.Current.Resources("ColorSecundario")),
            .BorderThickness = New Thickness(1, 1, 1, 1),
            .Padding = New Thickness(15, 15, 15, 15),
            .VerticalAlignment = VerticalAlignment.Top
        }

        sp.SetValue(Grid.ColumnProperty, 0)

        Dim color1 As New GradientStop With {
            .Color = Microsoft.Toolkit.Uwp.Helpers.ColorHelper.ToColor("#e0e0e0"),
            .Offset = 0.5
        }

        Dim color2 As New GradientStop With {
            .Color = Microsoft.Toolkit.Uwp.Helpers.ColorHelper.ToColor("#d6d6d6"),
            .Offset = 1.0
        }

        Dim coleccion As New GradientStopCollection From {
            color1,
            color2
        }

        Dim brush As New LinearGradientBrush With {
            .StartPoint = New Point(0.5, 0),
            .EndPoint = New Point(0.5, 1),
            .GradientStops = coleccion
        }

        sp.Background = brush

        Dim lv As New ListView With {
            .IsItemClickEnabled = True
        }

        AddHandler lv.ItemClick, AddressOf LvItemClick

        Dim recursos As New Resources.ResourceLoader()

        If volver = True Then
            lv.Items.Add(GenerarLvItem(New Thickness(0, 0, 0, 5), 0, 59440, Nothing, recursos.GetString("MoreThings_Back")))
        End If

        lv.Items.Add(GenerarLvItem(New Thickness(0, 0, 0, 30), 1, 59617, Nothing, recursos.GetString("MoreThings_VoteApp")))
        lv.Items.Add(GenerarLvItem(New Thickness(0, 0, 0, 5), 2, Nothing, "Assets\MasCosas\pepeizq.png", recursos.GetString("MoreThings_MoreApps")))
        lv.Items.Add(GenerarLvItem(New Thickness(0, 0, 0, 5), 3, Nothing, "Assets\MasCosas\contacto.png", recursos.GetString("MoreThings_Contact")))
        lv.Items.Add(GenerarLvItem(New Thickness(0, 0, 0, 30), 4, Nothing, "Assets\MasCosas\feedback.png", recursos.GetString("MoreThings_ReportBug")))

        If Not traduccion = Nothing Then
            lv.Items.Add(GenerarLvItem(New Thickness(0, 0, 0, 5), 5, Nothing, "Assets\MasCosas\poeditor.png", recursos.GetString("MoreThings_HelpTranslate")))
        End If

        If Not codigoFuente = Nothing Then
            lv.Items.Add(GenerarLvItem(New Thickness(0, 0, 0, 0), 6, Nothing, "Assets\MasCosas\github.png", recursos.GetString("MoreThings_SourceCode")))
        End If

        sp.Children.Add(lv)

        gridRelleno.Children.Add(sp)

        '-----------------------------

        Dim gridWv As New Grid With {
            .Margin = New Thickness(20, 20, 20, 20)
        }

        gridWv.SetValue(Grid.ColumnProperty, 1)

        Dim row1 As New RowDefinition
        Dim row2 As New RowDefinition

        row1.Height = New GridLength(1, GridUnitType.Auto)
        row2.Height = New GridLength(1, GridUnitType.Star)

        gridWv.RowDefinitions.Add(row1)
        gridWv.RowDefinitions.Add(row2)

        Dim pb As New ProgressBar With {
            .Foreground = New SolidColorBrush(App.Current.Resources("ColorSecundario")),
            .Visibility = Visibility.Collapsed,
            .IsIndeterminate = True,
            .Height = 20
        }

        pb.SetValue(Grid.RowProperty, 0)
        gridWv.Children.Add(pb)

        Dim wv As New WebView
        wv.SetValue(Grid.RowProperty, 1)
        wv.Tag = pb
        lv.Tag = wv

        AddHandler wv.NavigationCompleted, AddressOf WvNavigationCompleted
        gridWv.Children.Add(wv)

        gridRelleno.Children.Add(gridWv)

        sv.Content = gridRelleno

    End Sub

    Private Function GenerarLvItem(marginThickness As Thickness, id As String, codigoSegoe As Integer, urlImagen As String, texto As String)

        Dim lvitem As New ListViewItem With {
            .Background = New SolidColorBrush(App.Current.Resources("ColorSecundario")),
            .Margin = marginThickness
        }

        Dim spContenido As New StackPanel With {
            .Orientation = Orientation.Horizontal,
            .Tag = id
        }

        If Not codigoSegoe = Nothing Then
            Dim tb As New TextBlock With {
                .FontFamily = New FontFamily("Segoe MDL2 Assets"),
                .Text = Char.ConvertFromUtf32(codigoSegoe),
                .Foreground = New SolidColorBrush(Colors.White),
                .Margin = New Thickness(0, 0, 10, 0),
                .VerticalAlignment = VerticalAlignment.Center
            }

            spContenido.Children.Add(tb)
        End If

        If Not urlImagen = Nothing Then
            Dim imagen As New ImageEx With {
                .Source = urlImagen,
                .Margin = New Thickness(0, 0, 10, 0),
                .VerticalAlignment = VerticalAlignment.Center,
                .Width = 15,
                .Height = 15,
                .IsCacheEnabled = True
            }

            spContenido.Children.Add(imagen)
        End If

        Dim tbTexto As New TextBlock With {
            .Foreground = New SolidColorBrush(Colors.White),
            .VerticalAlignment = VerticalAlignment.Center,
            .Text = texto
        }

        spContenido.Children.Add(tbTexto)

        lvitem.Content = spContenido

        AddHandler lvitem.PointerEntered, AddressOf UsuarioEntraBoton
        AddHandler lvitem.PointerExited, AddressOf UsuarioSaleBoton

        Return lvitem

    End Function

    Private Async Sub LvItemClick(sender As Object, args As ItemClickEventArgs)

        Dim lv As ListView = sender
        Dim sp As StackPanel = args.ClickedItem

        If sp.Tag.ToString = 0 Then

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim gridCosas As Grid = pagina.FindName("gridMasCosas")
            gridCosas.Visibility = Visibility.Collapsed

            Dim gridPrincipal As Grid = pagina.FindName(gridPrincipalNombre)
            gridPrincipal.Visibility = Visibility.Visible

        ElseIf sp.Tag.ToString = 1 Then

            Await Launcher.LaunchUriAsync(New Uri("ms-windows-store:REVIEW?PFN=" + Package.Current.Id.FamilyName))

        ElseIf sp.Tag.ToString = 2 Then

            NavegarMasCosas(lv, sp.Tag, "https://pepeizqapps.com/")

        ElseIf sp.Tag.ToString = 3 Then

            NavegarMasCosas(lv, sp.Tag, "https://pepeizqapps.com/contact/")

        ElseIf sp.Tag.ToString = 4 Then

            If StoreServicesFeedbackLauncher.IsSupported = True Then
                Dim ejecutador As StoreServicesFeedbackLauncher = StoreServicesFeedbackLauncher.GetDefault()
                Await ejecutador.LaunchAsync()
            Else
                NavegarMasCosas(lv, sp.Tag, "https://pepeizqapps.com/contact/")
            End If

        ElseIf sp.Tag.ToString = 5 Then

            NavegarMasCosas(lv, sp.Tag, traduccion)

        ElseIf sp.Tag.ToString = 6 Then

            NavegarMasCosas(lv, sp.Tag, codigoFuente)

        End If

    End Sub

    Public Sub NavegarMasCosas(lv As ListView, tag As String, url As String)

        For Each item As ListViewItem In lv.Items
            Dim sp As StackPanel = item.Content

            If sp.Tag = tag Then
                item.Background = New SolidColorBrush(App.Current.Resources("ColorPrimario"))
            Else
                item.Background = New SolidColorBrush(App.Current.Resources("ColorSecundario"))
            End If
        Next

        Dim wv As WebView = lv.Tag
        Dim pb As ProgressBar = wv.Tag

        pb.Visibility = Visibility.Visible
        wv.Navigate(New Uri(url))

    End Sub

    Private Sub WvNavigationCompleted(sender As WebView, args As WebViewNavigationCompletedEventArgs)

        Dim pb As ProgressBar = sender.Tag
        pb.Visibility = Visibility.Collapsed

    End Sub

    Private Sub UsuarioEntraBoton(sender As Object, e As PointerRoutedEventArgs)

        Window.Current.CoreWindow.PointerCursor = New CoreCursor(CoreCursorType.Hand, 1)

    End Sub

    Private Sub UsuarioSaleBoton(sender As Object, e As PointerRoutedEventArgs)

        Window.Current.CoreWindow.PointerCursor = New CoreCursor(CoreCursorType.Arrow, 1)

    End Sub

End Module