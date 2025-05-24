Imports System.Text
Imports Microsoft.Xna.Framework
Imports Microsoft.Xna.Framework.Graphics
Imports Microsoft.Xna.Framework.Input

Public Class Dialog
    ' Preserve initial text in case of modification
    Private _preserveText As String

    ' Base dialog structure
    Public Area As Rectangle
    Public DialogColor As Color
    Public DialogBackground As Texture2D
    Public PixelizeBackground As Boolean
    Public DialogAlpha As Single
    Public DialogText As String ' Display text
    Public DialogLines As New List(Of String) ' Lines of wrapped text
    Public DialogTextColor As Color
    Public DialogTextPosition As Vector2

    ' Additional dialog features
    Private _AdvDialogIconSrc As Rectangle
    Private _AdvDialogIconDest As Rectangle
    Private _AdvDialogIconAni As Integer
    Private _AdvDialogIconAlpha As Single = 1.0

    Public FontScale As Decimal

    Public Sub New(Text As String, Position As Vector2, Size As Vector2, FontSize As Single, TextColor As Color,
                   Optional BG As Texture2D = Nothing,
                   Optional BGColor As Color = Nothing,
                   Optional BGAlpha As Single = 1,
                   Optional Pixelize As Boolean = False)

        ' Initialize primary dialog features
        Area = New Rectangle(Position.X, Position.Y, Size.X, Size.Y)
        If BGColor = Nothing Then DialogColor = New Color(60, 60, 60) Else DialogColor = BGColor  'Color.DarkGray
        If BG Is Nothing Then DialogBackground = Textures.DialogBackground Else DialogBackground = BG
        PixelizeBackground = Pixelize
        DialogAlpha = BGAlpha
        DialogText = Text
        _preserveText = Text
        DialogTextColor = TextColor
        DialogTextPosition = New Vector2(Position.X + 10, Position.Y + 10)
        FontScale = FontSize


        ' Initialize Other Dialog Features
        _AdvDialogIconSrc = New Rectangle(0, 0, 16, 16)
        _AdvDialogIconDest = New Rectangle(Area.X + Area.Width - 40, Area.Y + Area.Height - 40, 24, 24)
        _AdvDialogIconAni = 0
        _AdvDialogIconAlpha = 1.0

        ' Advance Initial Text
        DialogLines = WrapText(Text, Area.Width - 10, FontScale)
        AdvanceDialog()
    End Sub

    ' Detect Horizontal Word Capacity
    Private Function FontTooBig(Increase As Decimal) As Boolean
        Dim words As String() = _preserveText.TrimEnd(vbCrLf).Split(" "c, StringSplitOptions.RemoveEmptyEntries)

        Return words.ToList.Any(Function(x) Fonts.MonoType.MeasureString(x).X * (FontScale + Increase) > Area.Width - 20) = True
    End Function

    ' Detect Vertical Line Capacity
    Private Function CalculateMaxDisplaylines() As Integer
        ' Measure character height over surface padded by 20px
        Dim DisplayLines As Integer = Math.Floor((Area.Height - 20) / (Fonts.MonoType.MeasureString("A").Y * FontScale))

        Return DisplayLines
    End Function

    ' Wraps dialog text to fit box
    Private Function WrapText(text As String, maxWidth As Single, Optional scale As Decimal = 1) As List(Of String)
        If String.IsNullOrWhiteSpace(text) Then Return New List(Of String) From {""}

        Dim words As String() = text.Split(" "c, StringSplitOptions.RemoveEmptyEntries)
        Dim lines As New List(Of String)
        Dim currentLine As New StringBuilder
        Dim lineWidth As Single = 0.0F
        Dim spaceWidth As Single = Fonts.MonoType.MeasureString(" ").X * scale

        For Each word As String In words
            Dim wordWidth As Single = Fonts.MonoType.MeasureString(word).X * scale
            If lineWidth + wordWidth <= maxWidth Then
                currentLine.Append(word)
                lineWidth += wordWidth
                If lineWidth + spaceWidth <= maxWidth Then
                    currentLine.Append(" ")
                    lineWidth += spaceWidth
                End If
            Else
                If currentLine.Length > 0 Then lines.Add(currentLine.ToString().TrimEnd())
                currentLine.Clear().Append(word).Append(" ")
                lineWidth = wordWidth + spaceWidth
            End If
        Next

        If currentLine.Length > 0 Then lines.Add(currentLine.ToString().TrimEnd())
        Return lines
    End Function

    Public Sub ScaleFont(value As Decimal)
        ' Abort if out of bounds or font is unreasonably small
        If FontScale <= 0.35 AndAlso value <= 0 Then Exit Sub
        If FontTooBig(value) Then Exit Sub
        'If Fonts.MonoType.MeasureString(DialogText).Y * (FontScale + value) > Area.Height Then Exit Sub

        DialogText = _preserveText
        FontScale += value
        DialogLines = WrapText(DialogText, Area.Width - 10, FontScale)
        'MsgBox(FontScale)
        AdvanceDialog()
    End Sub

    ' Advances dialog text
    Public Sub AdvanceDialog()
        If DialogLines.Count = 0 Then Exit Sub

        Dim linesToProcess As Integer = Math.Min(CalculateMaxDisplaylines, DialogLines.Count)
        Dim sb As New StringBuilder

        For i As Integer = 0 To linesToProcess - 1
            Dim line As String = DialogLines(0)
            sb.AppendLine(line)
            DialogLines.RemoveAt(0)
        Next

        DialogText = sb.ToString().TrimEnd(vbCrLf)
    End Sub

    ' Flash Continuing Dialog Indicator
    Private Sub FlashAdvDialogIcon()
        If _AdvDialogIconAlpha <= 0.3 Then _AdvDialogIconAni = 1
        If _AdvDialogIconAlpha >= 1 Then _AdvDialogIconAni = 0

        If _AdvDialogIconAni = 0 Then _AdvDialogIconAlpha -= 0.015
        If _AdvDialogIconAni = 1 Then _AdvDialogIconAlpha += 0.015
    End Sub

    Public Sub Update()
        ' Check User Inputs
        If Input.KeyPressed(Keys.Up) Then ScaleFont(0.05)
        If Input.KeyPressed(Keys.Down) Then ScaleFont(-0.05)
        If Input.KeyPressed(Keys.Enter) Then AdvanceDialog()
        If Input.KeyPressed(Keys.M) Then MsgBox(CalculateMaxDisplaylines())


        ' Flash Dialog Advance Icon
        If DialogLines.Count >= 1 Then FlashAdvDialogIcon()
    End Sub

    Public Sub Draw()
        ' Optional: Display as pixel art | Unsmoothed
        If PixelizeBackground = True Then
            Globals.SpriteBatch.End()
            Globals.SpriteBatch.Begin(samplerState:=SamplerState.PointClamp)
        End If

        ' Draw dialog surface
        Globals.SpriteBatch.Draw(DialogBackground, Area, DialogColor * DialogAlpha)

        If PixelizeBackground = True Then
            Globals.SpriteBatch.End()
            Globals.SpriteBatch.Begin()
        End If

        ' Draw dialog text
        Globals.SpriteBatch.DrawString(Fonts.MonoType,
                                DialogText,
                                DialogTextPosition,
                                DialogTextColor,
                                0, Vector2.Zero, FontScale, Nothing, 0)

        ' DRAW ADVANCE DIALOG ICON
        If DialogLines.Count >= 1 Then
            Globals.SpriteBatch.Draw(Textures.AdvDialogIcon, _AdvDialogIconDest, _AdvDialogIconSrc, Color.White * _AdvDialogIconAlpha)
        End If

    End Sub
End Class
