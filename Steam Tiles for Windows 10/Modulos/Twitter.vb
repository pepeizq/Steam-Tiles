Imports Windows.Storage

Module Twitter

    Dim listaTweets As List(Of Tweet)
    Dim WithEvents bw As BackgroundWorker

    Public Sub Generar()

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        Dim grid As Grid = pagina.FindName("gridTwitter")
        Dim cb As CheckBox = pagina.FindName("cbTwitter")

        If ApplicationData.Current.LocalSettings.Values("twitter") = Nothing Then
            cb.IsChecked = True
            grid.Visibility = Visibility.Visible
        Else
            If ApplicationData.Current.LocalSettings.Values("twitter") = "on" Then
                cb.IsChecked = True
                grid.Visibility = Visibility.Visible
            Else
                cb.IsChecked = False
                grid.Visibility = Visibility.Collapsed
            End If
        End If

        listaTweets = New List(Of Tweet)

        bw = New BackgroundWorker
        bw.WorkerReportsProgress = True
        bw.WorkerSupportsCancellation = True

        If bw.IsBusy = False Then
            bw.RunWorkerAsync()
        End If

    End Sub

    Private Sub bw_DoWork(ByVal sender As Object, ByVal e As DoWorkEventArgs) Handles bw.DoWork

        Dim html_ As Task(Of String) = Decompiladores.HttpClient(New Uri("https://twitrss.me/twitter_user_to_rss/?user=pepeizqapps"))
        Dim html As String = html_.Result

        If Not html = Nothing Then
            Dim i As Integer = 0
            While i < 3
                If html.Contains("<item>") Then
                    Dim temp, temp2 As String
                    Dim int, int2 As Integer

                    int = html.IndexOf("<item>")
                    temp = html.Remove(0, int + 4)

                    html = temp

                    int2 = temp.IndexOf("</item>")
                    temp2 = temp.Remove(int2, temp.Length - int2)

                    Dim temp3, temp4 As String
                    Dim int3, int4 As Integer

                    int3 = temp2.IndexOf("<description>")
                    temp3 = temp2.Remove(0, int3)

                    int4 = temp3.IndexOf("</description>")
                    temp4 = temp3.Remove(int4, temp3.Length - int4)

                    temp4 = temp4.Replace("<description>", Nothing)
                    temp4 = temp4.Replace("<![CDATA[", Nothing)
                    temp4 = temp4.Replace("</p>]]>", Nothing)
                    temp4 = temp4.Replace("&nbsp;", Nothing)
                    temp4 = temp4.Replace("&hellip;", Nothing)

                    If temp4.Contains("<p") Then
                        int3 = temp4.IndexOf("<p")
                        int4 = temp4.IndexOf(">")
                        temp4 = temp4.Remove(int3, (int4 + 1) - int3)
                    End If

                    Dim contenido As String = temp4.Trim

                    Dim temp5, temp6 As String
                    Dim int5, int6 As Integer

                    If contenido.Contains("<a href=") Then
                        int5 = contenido.IndexOf("<a href=")
                        temp5 = contenido.Remove(0, int5 + 9)

                        int6 = temp5.IndexOf(ChrW(34))
                        temp6 = temp5.Remove(int6, temp5.Length - int6)

                        int5 = contenido.IndexOf("<a")
                        int6 = contenido.IndexOf(">")
                        contenido = contenido.Remove(int5, (int6 + 1) - int5)
                        contenido = contenido.Replace("</a>", Nothing)
                    Else
                        int5 = temp2.IndexOf("<link>")
                        temp5 = temp2.Remove(0, int5)

                        int6 = temp5.IndexOf("</link>")
                        temp6 = temp5.Remove(int6, temp5.Length - int6)

                        temp6 = temp6.Replace("<link>", Nothing)
                    End If

                    Dim enlace As Uri = New Uri(temp6.Trim)

                    Dim temp7, temp8 As String
                    Dim int7, int8 As Integer

                    int7 = temp2.IndexOf("<pubDate>")
                    temp7 = temp2.Remove(0, int7)

                    int8 = temp7.IndexOf("</pubDate>")
                    temp8 = temp7.Remove(int8, temp7.Length - int8)

                    temp8 = temp8.Replace("<pubDate>", Nothing)

                    Dim fecha As Date = Date.Parse(temp8.Trim)

                    Dim tweet As New Tweet(contenido, enlace, fecha)
                    listaTweets.Add(tweet)

                End If
                i += 1
            End While
        End If

    End Sub

    Private Async Sub bw_RunWorkerCompleted(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs) Handles bw.RunWorkerCompleted

        If listaTweets.Count > 0 Then
            Dim hoy As Date = Date.Today

            Dim i As Integer = 0
            While i < listaTweets.Count
                If hoy < listaTweets(i).Fecha.AddDays(7) Then
                    Dim frame As Frame = Window.Current.Content
                    Dim pagina As Page = frame.Content

                    Dim grid As Grid = pagina.FindName("gridTwitter")

                    If ApplicationData.Current.LocalSettings.Values("twitter") = "on" Then
                        grid.Visibility = Visibility.Visible
                    End If

                    Dim boton As Button = pagina.FindName("buttonTwitter")
                    boton.Tag = listaTweets(i).Enlace

                    Dim tb As TextBlock = pagina.FindName("tbTwitter")
                    tb.Text = listaTweets(i).Contenido

                    Await Task.Delay(15000)
                End If

                If i = (listaTweets.Count - 1) Then
                    i = -1
                End If
                i += 1
            End While
        End If

    End Sub

End Module
