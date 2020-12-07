Imports Windows.ApplicationModel.Core
Imports Windows.System
Imports Windows.UI

NotInheritable Class App
    Inherits Application

    Protected Overrides Async Sub OnLaunched(e As LaunchActivatedEventArgs)
        Dim iniciarApp As Boolean = False
        Dim arguments As String = e.Arguments

        Try
            iniciarApp = Await Launcher.LaunchUriAsync(New Uri(arguments))
        Catch ex As Exception

        End Try

        Try
            If iniciarApp = False Then
                iniciarApp = Await Launcher.LaunchFileAsync(New Uri(arguments))
            End If
        Catch ex As Exception

        End Try

        If iniciarApp = False Then
            Dim rootFrame As Frame = TryCast(Window.Current.Content, Frame)

            If rootFrame Is Nothing Then
                rootFrame = New Frame()

                AddHandler rootFrame.NavigationFailed, AddressOf OnNavigationFailed

                If e.PreviousExecutionState = ApplicationExecutionState.Terminated Then

                End If

                Window.Current.Content = rootFrame
            End If

            If rootFrame.Content Is Nothing Then
                rootFrame.Navigate(GetType(MainPage), e.Arguments)
            End If

            Window.Current.Activate()

            BarraAcrilica()
        Else
            Try
                Application.Current.Exit()
            Catch ex As Exception

            End Try

            Try
                CoreApplication.Exit()
            Catch ex As Exception

            End Try
        End If
    End Sub

    Private Sub OnNavigationFailed(sender As Object, e As NavigationFailedEventArgs)
        Throw New Exception("Failed to load Page " + e.SourcePageType.FullName)
    End Sub

    Private Sub OnSuspending(sender As Object, e As SuspendingEventArgs) Handles Me.Suspending
        Dim deferral As SuspendingDeferral = e.SuspendingOperation.GetDeferral()
        deferral.Complete()
    End Sub

    Private Sub BarraAcrilica()

        CoreApplication.GetCurrentView().TitleBar.ExtendViewIntoTitleBar = True
        Dim barra As ApplicationViewTitleBar = ApplicationView.GetForCurrentView().TitleBar
        barra.ButtonBackgroundColor = Colors.Transparent
        barra.ButtonInactiveBackgroundColor = Colors.Transparent

    End Sub

End Class
