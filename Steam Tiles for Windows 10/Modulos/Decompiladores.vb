Imports Microsoft.Toolkit.Uwp
Imports Windows.Web.Http

Module Decompiladores

    Public Async Function HttpHelperResponse(url As Uri) As Task(Of String)

        Using request = New HttpHelperRequest(url, HttpMethod.Post)
            Using response = Await HttpHelper.Instance.SendRequestAsync(request)
                Return Await response.GetTextResultAsync()
            End Using
        End Using

    End Function

    Public Async Function HttpClient(url As Uri) As Task(Of String)

        Dim handler As New Net.Http.HttpClientHandler
        Dim cliente As New Net.Http.HttpClient(handler)
        Dim httpFinal As String = Nothing

        cliente.DefaultRequestHeaders.Add("user-agent", "Chrome/45.0.2454.93")

        Try
            Dim respuesta As New Net.Http.HttpResponseMessage
            respuesta = Await cliente.GetAsync(url)
            respuesta.EnsureSuccessStatusCode()

            httpFinal = TryCast(Await respuesta.Content.ReadAsStringAsync(), String)
        Catch ex As Exception

        End Try

        cliente.Dispose()
        Return httpFinal
    End Function

End Module
