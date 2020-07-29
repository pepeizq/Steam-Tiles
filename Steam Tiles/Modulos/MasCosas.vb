Imports Microsoft.Services.Store.Engagement
Imports Microsoft.Toolkit.Uwp.UI.Animations
Imports Windows.System
Imports Windows.UI.Core

Module MasCosas

    Public Function Generar(codigoFuente As String, traduccion As String)

        Dim recursos As New Resources.ResourceLoader()

        Dim iconoMasCosas As New FontAwesome5.FontAwesome With {
            .Icon = FontAwesome5.EFontAwesomeIcon.Solid_CaretDown,
            .Foreground = New SolidColorBrush(App.Current.Resources("ColorPrimario"))
        }

        Dim tbMasCosas As New TextBlock With {
            .Text = recursos.GetString("MoreThings"),
            .Foreground = New SolidColorBrush(App.Current.Resources("ColorPrimario"))
        }

        Dim itemMasCosas As New NavigationViewItem With {
            .Icon = iconoMasCosas,
            .Content = tbMasCosas
        }

        Dim tbToolTip As TextBlock = New TextBlock With {
            .Text = recursos.GetString("MoreThings")
        }

        ToolTipService.SetToolTip(itemMasCosas, tbToolTip)
        ToolTipService.SetPlacement(itemMasCosas, PlacementMode.Mouse)

        AddHandler itemMasCosas.PointerEntered, AddressOf UsuarioEntraBotonNVItem
        AddHandler itemMasCosas.PointerExited, AddressOf UsuarioSaleBotonNVItem

        '---------------------------------------------

        Dim menu As New MenuFlyout With {
            .Placement = FlyoutPlacementMode.Top,
            .ShowMode = FlyoutShowMode.Transient
        }

        Dim iconoMasApps As New FontAwesome5.FontAwesome With {
            .Icon = FontAwesome5.EFontAwesomeIcon.Solid_Cube
        }

        Dim menuItemMasApps As New MenuFlyoutItem With {
            .Text = "pepeizqapps.com",
            .Icon = iconoMasApps
        }

        AddHandler menuItemMasApps.Click, AddressOf MenuItemMasAppsClick
        AddHandler menuItemMasApps.PointerEntered, AddressOf UsuarioEntraBotonMFItem
        AddHandler menuItemMasApps.PointerExited, AddressOf UsuarioSaleBotonMFItem

        menu.Items.Add(menuItemMasApps)

        Dim iconoDeals As New FontAwesome5.FontAwesome With {
            .Icon = FontAwesome5.EFontAwesomeIcon.Solid_Cube
        }

        Dim menuItemDeals As New MenuFlyoutItem With {
            .Text = "pepeizqdeals.com",
            .Icon = iconoDeals
        }

        AddHandler menuItemDeals.Click, AddressOf MenuItemDealsClick
        AddHandler menuItemDeals.PointerEntered, AddressOf UsuarioEntraBotonMFItem
        AddHandler menuItemDeals.PointerExited, AddressOf UsuarioSaleBotonMFItem

        menu.Items.Add(menuItemDeals)

        menu.Items.Add(New MenuFlyoutSeparator)

        Dim iconoVotar As New FontAwesome5.FontAwesome With {
            .Icon = FontAwesome5.EFontAwesomeIcon.Regular_ThumbsUp
        }

        Dim menuItemVotar As New MenuFlyoutItem With {
            .Text = recursos.GetString("MoreThings_VoteApp"),
            .Icon = iconoVotar
        }

        AddHandler menuItemVotar.Click, AddressOf MenuItemVotarClick
        AddHandler menuItemVotar.PointerEntered, AddressOf UsuarioEntraBotonMFItem
        AddHandler menuItemVotar.PointerExited, AddressOf UsuarioSaleBotonMFItem

        menu.Items.Add(menuItemVotar)

        Dim iconoContacto As New FontAwesome5.FontAwesome With {
            .Icon = FontAwesome5.EFontAwesomeIcon.Regular_Comment
        }

        Dim menuItemContacto As New MenuFlyoutItem With {
            .Text = recursos.GetString("MoreThings_Contact"),
            .Icon = iconoContacto
        }

        AddHandler menuItemContacto.Click, AddressOf MenuItemContactoClick
        AddHandler menuItemContacto.PointerEntered, AddressOf UsuarioEntraBotonMFItem
        AddHandler menuItemContacto.PointerExited, AddressOf UsuarioSaleBotonMFItem

        menu.Items.Add(menuItemContacto)

        Dim iconoReportar As New FontAwesome5.FontAwesome With {
            .Icon = FontAwesome5.EFontAwesomeIcon.Solid_Bug
        }

        Dim menuItemReportar As New MenuFlyoutItem With {
            .Text = recursos.GetString("MoreThings_ReportBug"),
            .Icon = iconoReportar
        }

        AddHandler menuItemReportar.Click, AddressOf MenuItemReportarClick
        AddHandler menuItemReportar.PointerEntered, AddressOf UsuarioEntraBotonMFItem
        AddHandler menuItemReportar.PointerExited, AddressOf UsuarioSaleBotonMFItem

        menu.Items.Add(menuItemReportar)

        If Not traduccion = Nothing Then
            Dim iconoTraducir As New FontAwesome5.FontAwesome With {
                .Icon = FontAwesome5.EFontAwesomeIcon.Solid_Globe
            }

            Dim menuItemTraducir As New MenuFlyoutItem With {
                .Text = recursos.GetString("MoreThings_HelpTranslate"),
                .Icon = iconoTraducir,
                .Tag = traduccion
            }

            AddHandler menuItemTraducir.Click, AddressOf MenuItemTraducirClick
            AddHandler menuItemTraducir.PointerEntered, AddressOf UsuarioEntraBotonMFItem
            AddHandler menuItemTraducir.PointerExited, AddressOf UsuarioSaleBotonMFItem

            menu.Items.Add(New MenuFlyoutSeparator)
            menu.Items.Add(menuItemTraducir)
        End If

        If Not codigoFuente = Nothing Then
            If traduccion = Nothing Then
                menu.Items.Add(New MenuFlyoutSeparator)
            End If

            Dim iconoCodigoFuente As New FontAwesome5.FontAwesome With {
                .Icon = FontAwesome5.EFontAwesomeIcon.Brands_Github
            }

            Dim menuItemCodigoFuente As New MenuFlyoutItem With {
                .Text = recursos.GetString("MoreThings_SourceCode"),
                .Icon = iconoCodigoFuente,
                .Tag = codigoFuente
            }

            AddHandler menuItemCodigoFuente.Click, AddressOf MenuItemCodigoFuenteClick
            AddHandler menuItemCodigoFuente.PointerEntered, AddressOf UsuarioEntraBotonMFItem
            AddHandler menuItemCodigoFuente.PointerExited, AddressOf UsuarioSaleBotonMFItem

            menu.Items.Add(menuItemCodigoFuente)
        End If

        FlyoutBase.SetAttachedFlyout(itemMasCosas, menu)

        Return itemMasCosas

    End Function

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

        Dim item As MenuFlyoutItem = sender
        Dim enlace As String = item.Tag

        If Not enlace = String.Empty Then
            Await Launcher.LaunchUriAsync(New Uri(enlace))
        End If

    End Sub

    Private Async Sub MenuItemCodigoFuenteClick(sender As Object, e As RoutedEventArgs)

        Dim item As MenuFlyoutItem = sender
        Dim enlace As String = item.Tag

        If Not enlace = String.Empty Then
            Await Launcher.LaunchUriAsync(New Uri(enlace))
        End If

    End Sub

    Private Sub UsuarioEntraBotonNVItem(sender As Object, e As PointerRoutedEventArgs)

        Dim item As NavigationViewItem = sender
        Dim icono As FontAwesome5.FontAwesome = item.Icon
        icono.Saturation(1).Scale(1.2, 1.2, icono.ActualWidth / 2, icono.ActualHeight / 2).Start()

        Window.Current.CoreWindow.PointerCursor = New CoreCursor(CoreCursorType.Hand, 1)

    End Sub

    Private Sub UsuarioSaleBotonNVItem(sender As Object, e As PointerRoutedEventArgs)

        Dim item As NavigationViewItem = sender
        Dim icono As FontAwesome5.FontAwesome = item.Icon
        icono.Saturation(1).Scale(1, 1, icono.ActualWidth / 2, icono.ActualHeight / 2).Start()

        Window.Current.CoreWindow.PointerCursor = New CoreCursor(CoreCursorType.Arrow, 1)

    End Sub

    Private Sub UsuarioEntraBotonMFItem(sender As Object, e As PointerRoutedEventArgs)

        Dim item As MenuFlyoutItem = sender
        Dim icono As FontAwesome5.FontAwesome = item.Icon
        icono.Saturation(1).Scale(1.1, 1.1, icono.ActualWidth / 2, icono.ActualHeight / 2).Start()

        Window.Current.CoreWindow.PointerCursor = New CoreCursor(CoreCursorType.Hand, 1)

    End Sub

    Private Sub UsuarioSaleBotonMFItem(sender As Object, e As PointerRoutedEventArgs)

        Dim item As MenuFlyoutItem = sender
        Dim icono As FontAwesome5.FontAwesome = item.Icon
        icono.Saturation(1).Scale(1, 1, icono.ActualWidth / 2, icono.ActualHeight / 2).Start()

        Window.Current.CoreWindow.PointerCursor = New CoreCursor(CoreCursorType.Arrow, 1)

    End Sub

End Module