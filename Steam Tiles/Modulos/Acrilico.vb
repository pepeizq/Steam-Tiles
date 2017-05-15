Imports Microsoft.Graphics.Canvas.Effects
Imports Windows.UI.Composition
Imports Windows.UI.Xaml.Hosting

Module Acrilico

    Public Sub Generar(panel As Panel)

        Dim compositor As Compositor = ElementCompositionPreview.GetElementVisual(panel).Compositor
        Dim hostSprite As SpriteVisual = compositor.CreateSpriteVisual
        hostSprite.Size = New Numerics.Vector2(panel.ActualWidth, panel.ActualHeight)

        Dim blur As GaussianBlurEffect = New GaussianBlurEffect With {
            .BlurAmount = 0.0F,
            .BorderMode = EffectBorderMode.Soft,
            .Optimization = EffectOptimization.Balanced,
            .Source = New CompositionEffectSourceParameter("source")
        }

        Dim factory As CompositionEffectFactory = compositor.CreateEffectFactory(blur, Nothing)
        Dim effectBrush As CompositionEffectBrush = factory.CreateBrush

        effectBrush.SetSourceParameter("source", compositor.CreateHostBackdropBrush())
        hostSprite.Brush = effectBrush
        ElementCompositionPreview.SetElementChildVisual(panel, hostSprite)

        panel.Tag = hostSprite
        AddHandler panel.SizeChanged, AddressOf panel_SizeChanged

    End Sub

    Private Sub Panel_SizeChanged(sender As Object, e As SizeChangedEventArgs)

        Dim panel As Panel = e.OriginalSource

        If Not panel Is Nothing Then
            Dim hostSprite As SpriteVisual = panel.Tag

            If Not hostSprite Is Nothing Then
                hostSprite.Size = New Numerics.Vector2(panel.ActualWidth, panel.ActualHeight)
            End If
        End If

    End Sub

End Module
