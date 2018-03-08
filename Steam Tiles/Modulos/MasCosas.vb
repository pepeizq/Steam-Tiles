Imports FontAwesome.UWP
Imports Microsoft.Services.Store.Engagement
Imports Windows.ApplicationModel.Core
Imports Windows.System
Imports Windows.UI
Imports Windows.UI.Core

Module MasCosas

    Dim traduccion As String = "https://poeditor.com/join/project/aKmScyB4QT"
    Dim codigoFuente As String = "https://github.com/pepeizq/Steam-Tiles"

    Public Sub Generar()

        Dim recursos As New Resources.ResourceLoader()

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        Dim tbTitulo As TextBlock = pagina.FindName("tbTitulo")
        tbTitulo.Text = Package.Current.DisplayName

        Dim coreBarra As CoreApplicationViewTitleBar = CoreApplication.GetCurrentView.TitleBar
        coreBarra.ExtendViewIntoTitleBar = True
        Window.Current.SetTitleBar(tbTitulo)

        Dim barra As ApplicationViewTitleBar = ApplicationView.GetForCurrentView().TitleBar
        barra.ButtonBackgroundColor = Colors.Transparent
        barra.ButtonForegroundColor = Colors.White
        barra.ButtonInactiveBackgroundColor = Colors.Transparent

        Dim botonCosas As Button = pagina.FindName("botonMasCosas")
        botonCosas.Margin = New Thickness(0, 0, coreBarra.SystemOverlayRightInset + 200, 0)

        Dim menu As MenuFlyout = pagina.FindName("botonMasCosasMenu")

        Dim iconoVotar As New FontAwesome.UWP.FontAwesome With {
            .Icon = FontAwesomeIcon.ThumbsOutlineUp
        }

        Dim menuItemVotar As New MenuFlyoutItem With {
            .Text = recursos.GetString("MoreThings_VoteApp"),
            .Icon = iconoVotar
        }

        AddHandler menuItemVotar.Click, AddressOf MenuItemVotarClick
        AddHandler menuItemVotar.PointerEntered, AddressOf UsuarioEntraBoton
        AddHandler menuItemVotar.PointerExited, AddressOf UsuarioSaleBoton

        menu.Items.Add(menuItemVotar)
        menu.Items.Add(New MenuFlyoutSeparator)

        Dim iconoMasApps As New FontAwesome.UWP.FontAwesome With {
            .Icon = FontAwesomeIcon.PlusCircle
        }

        Dim menuItemMasApps As New MenuFlyoutItem With {
            .Text = recursos.GetString("MoreThings_MoreApps"),
            .Icon = iconoMasApps
        }

        AddHandler menuItemMasApps.Click, AddressOf MenuItemMasAppsClick
        AddHandler menuItemMasApps.PointerEntered, AddressOf UsuarioEntraBoton
        AddHandler menuItemMasApps.PointerExited, AddressOf UsuarioSaleBoton

        menu.Items.Add(menuItemMasApps)

        Dim iconoContacto As New FontAwesome.UWP.FontAwesome With {
            .Icon = FontAwesomeIcon.CommentOutline
        }

        Dim menuItemContacto As New MenuFlyoutItem With {
            .Text = recursos.GetString("MoreThings_Contact"),
            .Icon = iconoContacto
        }

        AddHandler menuItemContacto.Click, AddressOf MenuItemContactoClick
        AddHandler menuItemContacto.PointerEntered, AddressOf UsuarioEntraBoton
        AddHandler menuItemContacto.PointerExited, AddressOf UsuarioSaleBoton

        menu.Items.Add(menuItemContacto)

        Dim iconoReportar As New FontAwesome.UWP.FontAwesome With {
            .Icon = FontAwesomeIcon.Bug
        }

        Dim menuItemReportar As New MenuFlyoutItem With {
            .Text = recursos.GetString("MoreThings_ReportBug"),
            .Icon = iconoReportar
        }

        AddHandler menuItemReportar.Click, AddressOf MenuItemReportarClick
        AddHandler menuItemReportar.PointerEntered, AddressOf UsuarioEntraBoton
        AddHandler menuItemReportar.PointerExited, AddressOf UsuarioSaleBoton

        menu.Items.Add(menuItemReportar)

        If Not traduccion = Nothing Then
            Dim iconoTraducir As New FontAwesome.UWP.FontAwesome With {
                .Icon = FontAwesomeIcon.Globe
            }

            Dim menuItemTraducir As New MenuFlyoutItem With {
                .Text = recursos.GetString("MoreThings_HelpTranslate"),
                .Icon = iconoTraducir
            }

            AddHandler menuItemTraducir.Click, AddressOf MenuItemTraducirClick
            AddHandler menuItemTraducir.PointerEntered, AddressOf UsuarioEntraBoton
            AddHandler menuItemTraducir.PointerExited, AddressOf UsuarioSaleBoton

            menu.Items.Add(New MenuFlyoutSeparator)
            menu.Items.Add(menuItemTraducir)
        End If

        If Not codigoFuente = Nothing Then
            If traduccion = Nothing Then
                menu.Items.Add(New MenuFlyoutSeparator)
            End If

            Dim iconoCodigoFuente As New FontAwesome.UWP.FontAwesome With {
                .Icon = FontAwesomeIcon.Github
            }

            Dim menuItemCodigoFuente As New MenuFlyoutItem With {
                .Text = recursos.GetString("MoreThings_SourceCode"),
                .Icon = iconoCodigoFuente
            }

            AddHandler menuItemCodigoFuente.Click, AddressOf MenuItemCodigoFuenteClick
            AddHandler menuItemCodigoFuente.PointerEntered, AddressOf UsuarioEntraBoton
            AddHandler menuItemCodigoFuente.PointerExited, AddressOf UsuarioSaleBoton

            menu.Items.Add(menuItemCodigoFuente)
        End If

        '--------------------------------------

        Dim gridCosas As Grid = pagina.FindName("gridMasCosas")

        Dim row1 As New RowDefinition
        Dim row2 As New RowDefinition
        Dim row3 As New RowDefinition

        row1.Height = New GridLength(1, GridUnitType.Auto)
        row2.Height = New GridLength(1, GridUnitType.Star)
        row3.Height = New GridLength(1, GridUnitType.Auto)

        gridCosas.RowDefinitions.Add(row1)
        gridCosas.RowDefinitions.Add(row2)
        gridCosas.RowDefinitions.Add(row3)

        Dim gridSuperior As New Grid
        gridSuperior.SetValue(Grid.RowProperty, 0)
        gridSuperior.Padding = New Thickness(10, 10, 10, 10)

        Dim col1 As New ColumnDefinition
        Dim col2 As New ColumnDefinition

        col1.Width = New GridLength(1, GridUnitType.Auto)
        col2.Width = New GridLength(1, GridUnitType.Star)

        gridSuperior.ColumnDefinitions.Add(col1)
        gridSuperior.ColumnDefinitions.Add(col2)

        Dim simboloBotonCerrar As New SymbolIcon With {
            .Symbol = Symbol.Cancel,
            .Foreground = New SolidColorBrush(Colors.White)
        }

        Dim botonCerrar As New Button With {
            .Background = New SolidColorBrush(App.Current.Resources("ColorSecundario")),
            .Padding = New Thickness(10, 10, 10, 10),
            .Content = simboloBotonCerrar,
            .BorderBrush = New SolidColorBrush(App.Current.Resources("ColorPrimario")),
            .BorderThickness = New Thickness(1, 1, 1, 1),
            .Margin = New Thickness(0, 0, 10, 0)
        }

        AddHandler botonCerrar.Click, AddressOf BotonCerrarClick
        AddHandler botonCerrar.PointerEntered, AddressOf UsuarioEntraBoton
        AddHandler botonCerrar.PointerExited, AddressOf UsuarioSaleBoton

        botonCerrar.SetValue(Grid.ColumnProperty, 0)
        gridSuperior.Children.Add(botonCerrar)

        Dim tbUrl As New TextBox With {
            .Padding = New Thickness(10, 10, 10, 10),
            .Name = "tbMasCosasUrl",
            .BorderBrush = New SolidColorBrush(App.Current.Resources("ColorPrimario")),
            .BorderThickness = New Thickness(1, 1, 1, 1)
        }

        tbUrl.SetValue(Grid.ColumnProperty, 1)

        gridSuperior.Children.Add(tbUrl)

        gridCosas.Children.Add(gridSuperior)

        Dim gridPb As New Grid With {
            .Name = "gridPbMasCosas",
            .Background = New SolidColorBrush(Colors.LightGray),
            .Visibility = Visibility.Collapsed,
            .Height = 25,
            .HorizontalAlignment = HorizontalAlignment.Center,
            .Width = 300,
            .Margin = New Thickness(0, 0, 0, 10)
        }

        gridPb.SetValue(Grid.RowProperty, 2)

        Dim pb As New ProgressBar With {
           .Foreground = New SolidColorBrush(App.Current.Resources("ColorSecundario")),
           .IsIndeterminate = True,
           .VerticalAlignment = VerticalAlignment.Center
        }

        gridPb.Children.Add(pb)
        gridCosas.Children.Add(gridPb)

        Dim wv As New WebView With {
            .Name = "wvMasCosas",
            .Margin = New Thickness(10, 10, 10, 10)
        }

        wv.SetValue(Grid.RowProperty, 1)
        AddHandler wv.NavigationStarting, AddressOf WvNavigationStarting
        AddHandler wv.NavigationCompleted, AddressOf WvNavigationCompleted
        gridCosas.Children.Add(wv)

    End Sub

    Private Async Sub MenuItemVotarClick(sender As Object, e As RoutedEventArgs)

        Await Launcher.LaunchUriAsync(New Uri("ms-windows-store:REVIEW?PFN=" + Package.Current.Id.FamilyName))

    End Sub

    Private Sub MenuItemMasAppsClick(sender As Object, e As RoutedEventArgs)

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        Dim gridCosas As Grid = pagina.FindName("gridMasCosas")
        gridCosas.Visibility = Visibility.Visible

        Dim wvCosas As WebView = pagina.FindName("wvMasCosas")
        wvCosas.Navigate(New Uri("https://pepeizqapps.com/"))

    End Sub

    Private Sub MenuItemContactoClick(sender As Object, e As RoutedEventArgs)

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        Dim gridCosas As Grid = pagina.FindName("gridMasCosas")
        gridCosas.Visibility = Visibility.Visible

        Dim wvCosas As WebView = pagina.FindName("wvMasCosas")
        wvCosas.Navigate(New Uri("https://pepeizqapps.com/contact/"))

    End Sub

    Private Async Sub MenuItemReportarClick(sender As Object, e As RoutedEventArgs)

        If StoreServicesFeedbackLauncher.IsSupported = True Then
            Dim ejecutador As StoreServicesFeedbackLauncher = StoreServicesFeedbackLauncher.GetDefault()
            Await ejecutador.LaunchAsync()
        Else
            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim gridCosas As Grid = pagina.FindName("gridMasCosas")
            gridCosas.Visibility = Visibility.Visible

            Dim wvCosas As WebView = pagina.FindName("wvMasCosas")
            wvCosas.Navigate(New Uri("https://pepeizqapps.com/contact/"))
        End If

    End Sub

    Private Sub MenuItemTraducirClick(sender As Object, e As RoutedEventArgs)

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        Dim gridCosas As Grid = pagina.FindName("gridMasCosas")
        gridCosas.Visibility = Visibility.Visible

        Dim wvCosas As WebView = pagina.FindName("wvMasCosas")
        wvCosas.Navigate(New Uri(traduccion))

    End Sub

    Private Sub MenuItemCodigoFuenteClick(sender As Object, e As RoutedEventArgs)

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        Dim gridCosas As Grid = pagina.FindName("gridMasCosas")
        gridCosas.Visibility = Visibility.Visible

        Dim wvCosas As WebView = pagina.FindName("wvMasCosas")
        wvCosas.Navigate(New Uri(codigoFuente))

    End Sub

    Private Sub BotonCerrarClick(sender As Object, e As RoutedEventArgs)

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        Dim gridCosas As Grid = pagina.FindName("gridMasCosas")
        gridCosas.Visibility = Visibility.Collapsed

    End Sub

    Private Sub WvNavigationStarting(sender As WebView, args As WebViewNavigationStartingEventArgs)

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        Dim tb As TextBox = pagina.FindName("tbMasCosasUrl")
        tb.Text = args.Uri.OriginalString

        Dim gridPb As Grid = pagina.FindName("gridPbMasCosas")
        gridPb.Visibility = Visibility.Visible

    End Sub

    Private Sub WvNavigationCompleted(sender As WebView, args As WebViewNavigationCompletedEventArgs)

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        Dim gridPb As Grid = pagina.FindName("gridPbMasCosas")
        gridPb.Visibility = Visibility.Collapsed

    End Sub

    Private Sub UsuarioEntraBoton(sender As Object, e As PointerRoutedEventArgs)

        Window.Current.CoreWindow.PointerCursor = New CoreCursor(CoreCursorType.Hand, 1)

    End Sub

    Private Sub UsuarioSaleBoton(sender As Object, e As PointerRoutedEventArgs)

        Window.Current.CoreWindow.PointerCursor = New CoreCursor(CoreCursorType.Arrow, 1)

    End Sub

End Module