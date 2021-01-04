Namespace Interfaz
    Module App2

        Public Sub Cargar()

            Dim recursos As New Resources.ResourceLoader()

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim tbAppMensaje As TextBlock = pagina.FindName("tbAppMensaje")
            tbAppMensaje.Text = recursos.GetString("Steam_AppMessage")

            Dim appBlanco As New Tile("Steam", "app", "steam://open/main", "Assets/App/blanco_pequeña.png", "Assets/App/blanco_mediana.png", "Assets/App/blanco_ancha.png", "Assets/App/blanco_grande.png")

            Dim botonAppModoBlanco As Button = pagina.FindName("botonAppModoBlanco")
            botonAppModoBlanco.Tag = appBlanco

            AddHandler botonAppModoBlanco.Click, AddressOf BotonTile_Click
            AddHandler botonAppModoBlanco.PointerEntered, AddressOf EfectosHover.Entra_Boton_ImagenTexto
            AddHandler botonAppModoBlanco.PointerExited, AddressOf EfectosHover.Sale_Boton_ImagenTexto

            Dim appNegro As New Tile("Steam", "app", "steam://open/main", "Assets/App/negro_pequeña.png", "Assets/App/negro_mediana.png", "Assets/App/negro_ancha.png", "Assets/App/negro_grande.png")

            Dim botonAppModoNegro As Button = pagina.FindName("botonAppModoNegro")
            botonAppModoNegro.Tag = appNegro

            AddHandler botonAppModoNegro.Click, AddressOf BotonTile_Click
            AddHandler botonAppModoNegro.PointerEntered, AddressOf EfectosHover.Entra_Boton_ImagenTexto
            AddHandler botonAppModoNegro.PointerExited, AddressOf EfectosHover.Sale_Boton_ImagenTexto

        End Sub

    End Module
End Namespace