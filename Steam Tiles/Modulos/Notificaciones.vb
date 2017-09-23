Imports Windows.UI.Notifications
Imports Windows.UI.Popups

Module Notificaciones

    Public Async Sub MessageBox(contenido As String)

        Try
            Dim messageDialog = New MessageDialog(contenido)
            Await messageDialog.ShowAsync()
        Catch ex As Exception

        End Try

    End Sub

    Public Sub Toast(titulo As String, contenido As String)

        Dim notificador As ToastNotifier = ToastNotificationManager.CreateToastNotifier()
        Dim xml As Windows.Data.Xml.Dom.XmlDocument = ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastImageAndText02)

        Dim nodosimagen As Windows.Data.Xml.Dom.XmlNodeList = xml.GetElementsByTagName("image")
        nodosimagen(0).Attributes.GetNamedItem("src").NodeValue = "ms-appx:///Assets/steam_logo.png"

        Dim nodostexto As Windows.Data.Xml.Dom.XmlNodeList = xml.GetElementsByTagName("text")

        If Not titulo = String.Empty Then
            nodostexto.Item(0).AppendChild(xml.CreateTextNode(titulo))
        End If

        If Not contenido = String.Empty Then
            nodostexto.Item(1).AppendChild(xml.CreateTextNode(contenido))
        End If

        Dim toastnodo As Windows.Data.Xml.Dom.IXmlNode = xml.SelectSingleNode("/toast")
        Dim audio As Windows.Data.Xml.Dom.XmlElement = xml.CreateElement("audio")
        audio.SetAttribute("src", "ms-winsoundevent:Notification.SMS")

        Dim toast As ToastNotification = New ToastNotification(xml) With {
            .ExpirationTime = DateTime.Now.AddSeconds(10)
        }

        notificador.Show(toast)

    End Sub

End Module
