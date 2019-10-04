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

        Dim barra As ApplicationViewTitleBar = ApplicationView.GetForCurrentView().TitleBar
        barra.ButtonBackgroundColor = Colors.Transparent
        barra.ButtonForegroundColor = Colors.White
        barra.ButtonInactiveBackgroundColor = Colors.Transparent

        Dim iconoMasCosas As New FontAwesome.UWP.FontAwesome With {
            .Icon = FontAwesomeIcon.Cube,
            .Foreground = New SolidColorBrush(App.Current.Resources("ColorPrimario"))
        }

        Dim tbMasCosas As New TextBlock With {
            .Text = recursos.GetString("MoreThings"),
            .Foreground = New SolidColorBrush(App.Current.Resources("ColorPrimario")),
            .Margin = New Thickness(-5, 0, 10, 0)
        }

        Dim itemMasCosas As NavigationViewItem = pagina.FindName("itemMasCosas")
        itemMasCosas.Icon = iconoMasCosas
        itemMasCosas.Content = tbMasCosas
        itemMasCosas.Margin = New Thickness(0, 0, 4, 0)

        Dim menu As MenuFlyout = pagina.FindName("botonMasCosasMenu")
        menu.Placement = FlyoutPlacementMode.Top

        Dim iconoMasApps As New FontAwesome.UWP.FontAwesome With {
            .Icon = FontAwesomeIcon.Cube
        }

        Dim menuItemMasApps As New MenuFlyoutItem With {
            .Text = "pepeizqapps.com",
            .Icon = iconoMasApps
        }

        AddHandler menuItemMasApps.Click, AddressOf MenuItemMasAppsClick
        AddHandler menuItemMasApps.PointerEntered, AddressOf UsuarioEntraBoton
        AddHandler menuItemMasApps.PointerExited, AddressOf UsuarioSaleBoton

        menu.Items.Add(menuItemMasApps)

        Dim iconoDeals As New FontAwesome.UWP.FontAwesome With {
            .Icon = FontAwesomeIcon.Cube
        }

        Dim menuItemDeals As New MenuFlyoutItem With {
            .Text = "pepeizqdeals.com",
            .Icon = iconoDeals
        }

        AddHandler menuItemDeals.Click, AddressOf MenuItemDealsClick
        AddHandler menuItemDeals.PointerEntered, AddressOf UsuarioEntraBoton
        AddHandler menuItemDeals.PointerExited, AddressOf UsuarioSaleBoton

        menu.Items.Add(menuItemDeals)

        menu.Items.Add(New MenuFlyoutSeparator)

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

    End Sub

    Private Async Sub MenuItemVotarClick(sender As Object, e As RoutedEventArgs)

        Await Launcher.LaunchUriAsync(New Uri("ms-windows-store:REVIEW?PFN=" + Package.Current.Id.FamilyName))

    End Sub

    Private Async Sub MenuItemMasAppsClick(sender As Object, e As RoutedEventArgs)

        Await Launcher.LaunchUriAsync(New Uri("https://pepeizqapps.com/"))

    End Sub

    Private Async Sub MenuItemDealsClick(sender As Object, e As RoutedEventArgs)

        Await Launcher.LaunchUriAsync(New Uri("https://pepeizqdeals.com/"))

    End Sub

    Private Async Sub MenuItemContactoClick(sender As Object, e As RoutedEventArgs)

        Await Launcher.LaunchUriAsync(New Uri("https://pepeizqapps.com/contact/"))

    End Sub

    Private Async Sub MenuItemReportarClick(sender As Object, e As RoutedEventArgs)

        If StoreServicesFeedbackLauncher.IsSupported = True Then
            Dim ejecutador As StoreServicesFeedbackLauncher = StoreServicesFeedbackLauncher.GetDefault()
            Await ejecutador.LaunchAsync()
        Else
            Await Launcher.LaunchUriAsync(New Uri("https://pepeizqapps.com/contact/"))
        End If

    End Sub

    Private Async Sub MenuItemTraducirClick(sender As Object, e As RoutedEventArgs)

        Await Launcher.LaunchUriAsync(New Uri(traduccion))

    End Sub

    Private Async Sub MenuItemCodigoFuenteClick(sender As Object, e As RoutedEventArgs)

        Await Launcher.LaunchUriAsync(New Uri(codigoFuente))

    End Sub

    Private Sub UsuarioEntraBoton(sender As Object, e As PointerRoutedEventArgs)

        Window.Current.CoreWindow.PointerCursor = New CoreCursor(CoreCursorType.Hand, 1)

    End Sub

    Private Sub UsuarioSaleBoton(sender As Object, e As PointerRoutedEventArgs)

        Window.Current.CoreWindow.PointerCursor = New CoreCursor(CoreCursorType.Arrow, 1)

    End Sub

End Module