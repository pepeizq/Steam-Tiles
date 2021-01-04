Imports Windows.UI.Xaml.Media.Animation

Namespace Interfaz
    Module Pestañas

        Public Sub Visibilidad(gridMostrar As Grid, tag As String, origen As Object)

            Dim recursos As New Resources.ResourceLoader()

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim tbTitulo As TextBlock = pagina.FindName("tbTitulo")
            tbTitulo.Text = recursos.GetString("Steam_Title") + " (" + Package.Current.Id.Version.Major.ToString + "." + Package.Current.Id.Version.Minor.ToString + "." + Package.Current.Id.Version.Build.ToString + "." + Package.Current.Id.Version.Revision.ToString + ")"

            If Not tag = Nothing Then
                tbTitulo.Text = tbTitulo.Text + " • " + tag
            End If

            Dim gridJuegos As Grid = pagina.FindName("gridJuegos")
            gridJuegos.Visibility = Visibility.Collapsed

            Dim gridApp As Grid = pagina.FindName("gridApp")
            gridApp.Visibility = Visibility.Collapsed

            Dim gridAñadirTile As Grid = pagina.FindName("gridAñadirTile")
            gridAñadirTile.Visibility = Visibility.Collapsed

            Dim gridProgreso As Grid = pagina.FindName("gridProgreso")
            gridProgreso.Visibility = Visibility.Collapsed

            Dim gridConfig As Grid = pagina.FindName("gridConfig")
            gridConfig.Visibility = Visibility.Collapsed

            Dim gridAvisoNoJuegos As Grid = pagina.FindName("gridAvisoNoJuegos")
            gridAvisoNoJuegos.Visibility = Visibility.Collapsed

            gridMostrar.Visibility = Visibility.Visible

            '--------------------------------------------------------

            If Not origen Is Nothing Then
                ConnectedAnimationService.GetForCurrentView().PrepareToAnimate("animacion", origen)
                Dim animacion As ConnectedAnimation = ConnectedAnimationService.GetForCurrentView().GetAnimation("animacion")

                If Not animacion Is Nothing Then
                    animacion.Configuration = New DirectConnectedAnimationConfiguration
                    animacion.TryStart(gridMostrar)
                End If
            End If

            '--------------------------------------------------------

            Dim spBuscador As StackPanel = pagina.FindName("spBuscador")

            If gridMostrar.Name = "gridJuegos" Then
                spBuscador.Visibility = Visibility.Visible
            Else
                spBuscador.Visibility = Visibility.Collapsed
            End If

        End Sub

    End Module
End Namespace

