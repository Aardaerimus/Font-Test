Imports Microsoft.Xna.Framework
Imports Microsoft.Xna.Framework.Graphics
Imports Microsoft.Xna.Framework.Input

Public Class Button

    Private _Text As String
    Public Area As Rectangle
    Public Clicked As Boolean = False
    Private _Texture As Texture2D
    Private _TextureSrc As Rectangle
    Private _Color As Color

    Private _HasMouse As Boolean = False

    Public Sub New(Text As String, Size As Rectangle, Optional C As Color = Nothing)
        _Text = Text
        Area = Size
        If C = Nothing Then _Color = Color.White Else _Color = C

        _Texture = Textures.Menu
        _TextureSrc = New Rectangle(0, 0, 64, 48)

        ' Dynamic Button Dimensions
        Area.Width = Fonts.Georgia_16.MeasureString(Text).X + 20
        Area.Height = Fonts.Georgia_16.MeasureString(Text).Y + 20

    End Sub

    Public Sub Update()
        If Input.MouseRect.Intersects(Area) Then _Color = Color.Pink Else _Color = Color.White

        ' Ignore re-click checks & check if state is clicked
        If Clicked = False AndAlso Input.MouseRect.Intersects(Area) AndAlso Input.MouseLeftButtonPressed Then
            Clicked = True
        Else
            Clicked = False
        End If
    End Sub

    Public Sub Draw()
        Globals.SpriteBatch.Draw(_Texture, Area, _TextureSrc, _Color)
        Globals.SpriteBatch.DrawString(Fonts.Georgia_16, _Text, New Vector2(Area.X + 10, Area.Y + 10), Color.White)
    End Sub

End Class
