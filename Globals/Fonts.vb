Imports Microsoft.Xna.Framework.Graphics

Public Class Fonts

    Public Shared Georgia_16 As SpriteFont
    Public Shared Arial_8 As SpriteFont
    Public Shared Semaru As SpriteFont
    Public Shared ScaleTest As SpriteFont
    Public Shared MonoType As SpriteFont

    Public Shared Sub Load()
        Georgia_16 = Globals.Content.Load(Of SpriteFont)("Fonts/Georgia_16")
        Arial_8 = Globals.Content.Load(Of SpriteFont)("Fonts/Arial_8")
        Semaru = Globals.Content.Load(Of SpriteFont)("Fonts/Semaru1")
        ScaleTest = Globals.Content.Load(Of SpriteFont)("Fonts/ScaleTest")
        MonoType = Globals.Content.Load(Of SpriteFont)("Fonts/MonotypeTest")
        MonoType.LineSpacing = 50
        MonoType.Spacing = -5
    End Sub
End Class
