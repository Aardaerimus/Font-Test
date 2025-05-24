Imports System.ComponentModel.Design
Imports System.Text
Imports Microsoft.Xna.Framework
Imports Microsoft.Xna.Framework.Graphics
Imports Microsoft.Xna.Framework.Input
Imports SharpDX.Direct2D1.Effects
Imports SharpDX.MediaFoundation

Public Class StartScreen

    Public Area As Rectangle

    Private DialogArea As Rectangle
    Private DialogSrcArea As Rectangle
    Private DialogList As New List(Of String)
    Private Dialog As String
    Private AdvDialogIconSrc As Rectangle
    Private AdvDialogIconDest As Rectangle
    Private AdvDialogIconAni As Integer = 0
    Private AdvDialogIconAlpha As Decimal = 1.0

    Private FontUp As Button
    Private FontDown As Button

    ' Test variables
    Private FontScale As Decimal = 0.4
    Private strMixedCase As String = "Once upon a time in a land far away, " &
                              "there was a quick brown fox that jumped over a lazy dog. " &
                              "Because of this singular event, a new font testing scheme was born. " & vbCrLf &
                              "What are your thoughts on this dialog system?"

    Private strUpperCase As String = "ONCE UPON A TIME, " &
                              "THERE WAS A QUICK BROWN FOX THAT JUMPED OVER A LAZY DOG. " &
                              "EVER SINCE, FONT TESTS HAVE NEVER BEEN THE SAME. " & vbCrLf &
                              "WHAT ARE YOUR THOUGHTS ON THIS NEW DIALOG SYSTEM?"

    Public Sub New()
        Area = New Rectangle(0, 0, Globals.Screen.Width - 50, Globals.Screen.Height - 50)

        ' Draw Dialog Box
        DialogArea = New Rectangle(Globals.Screen.Width * 0.2, Globals.Screen.Height * 0.66, Globals.Screen.Width * 0.6, 200)
        DialogSrcArea = New Rectangle(0, 0, 64, 48)

        DialogList = WrapText(strMixedCase, DialogArea.Width - 10, FontScale)

        AdvDialogIconSrc = New Rectangle(0, 0, 16, 16)
        AdvDialogIconDest = New Rectangle(DialogArea.X + DialogArea.Width - 40, DialogArea.Y + DialogArea.Height - 40, 24, 24)

        ' ADD BUTTONS
        FontUp = New Button("Font +", New Rectangle(DialogArea.X, DialogArea.Y, 150, 150))
        FontUp.Area.X += DialogArea.Width - FontUp.Area.Width
        FontUp.Area.Y -= FontUp.Area.Height + 5

        FontDown = New Button("Font -", New Rectangle(DialogArea.X, DialogArea.Y, 150, 150))
        FontDown.Area.X += DialogArea.Width - FontDown.Area.Width - FontUp.Area.Width - 5
        FontDown.Area.Y -= FontDown.Area.Height + 5

        ' Initial Dialog
        AdvanceDialog()
    End Sub
    Public Sub Update()
        'If Globals.State = Globals.GameState.Start AndAlso (Keyboard.GetState.GetPressedKeyCount > 0 Or Mouse.GetState.LeftButton = ButtonState.Pressed) Then Globals.State = Globals.GameState.Running
        If Input.KeyPressed(Keys.Enter) Then AdvanceDialog()

        ' Flash Dialog Advance Icon
        If DialogList.Count >= 1 Then FlashAdvDialogIcon()

        ' Update Control Events
        If FontUp.Clicked Then ScaleFont(0.05)
        If FontDown.Clicked Then ScaleFont(-0.05)


        FontUp.Update()
        FontDown.Update()

    End Sub

    Public Sub ScaleFont(value As Decimal)
        ' Abort if out of bounds or font in unreasonably small
        If FontScale <= 0.35 AndAlso value <= 0 Then Exit Sub
        If Fonts.MonoType.MeasureString(Dialog).Y * (FontScale + value) > DialogArea.Height Then Exit Sub

        Dialog = ""
        FontScale += value
        DialogList = WrapText(strMixedCase, DialogArea.Width - 10, FontScale)
        AdvanceDialog()
    End Sub

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

    ' Advances dialog text
    Public Sub AdvanceDialog(Optional maxLines As Integer = 3)
        If DialogList.Count = 0 Then Exit Sub

        Dim linesToProcess As Integer = Math.Min(maxLines, DialogList.Count)
        Dim sb As New StringBuilder

        For i As Integer = 0 To linesToProcess - 1
            Dim line As String = DialogList(0)
            sb.AppendLine(line)
            DialogList.RemoveAt(0)
        Next

        Dialog = sb.ToString().TrimEnd(vbCrLf)
    End Sub

    Private Sub FlashAdvDialogIcon()
        If AdvDialogIconAlpha <= 0.3 Then AdvDialogIconAni = 1
        If AdvDialogIconAlpha >= 1 Then AdvDialogIconAni = 0

        If AdvDialogIconAni = 0 Then AdvDialogIconAlpha -= 0.015
        If AdvDialogIconAni = 1 Then AdvDialogIconAlpha += 0.015
    End Sub

    Public Sub Draw()
        Globals.SpriteBatch.End()
        Globals.SpriteBatch.Begin()

        Globals.SpriteBatch.Draw(Textures.Menu, DialogArea, DialogSrcArea, Color.White)
        'Globals.SpriteBatch.Draw(Textures.PlayerBoard, New Rectangle(0, 0, Globals.Screen.Width, Globals.Screen.Height), Color.LightBlue)

        Globals.SpriteBatch.DrawString(Fonts.MonoType,
                                "0.75" & vbCrLf &
                                "~ DISCOVERY IS THE BOON OF THE INTREPID ~" & vbCrLf & 'ABCDEFGHIJKLMNOPQRSTUVWXYZ
                                "~ discovery is the boon of the intrepid ~" & vbCrLf &
                                "1234567890" & vbCrLf &
                                " !@#$%^&*()_-=+'"";:,./?[]<>{}|\~`",
                                (New Vector2(15, 40)),
                                Color.White,
                                0, Vector2.Zero, 0.75, Nothing, 0)

        Globals.SpriteBatch.DrawString(Fonts.MonoType,
                                "0.65" & vbCrLf &
                                "ABCDEFGHIJKLMNOPQRSTUVWXYZ" & vbCrLf & "abcdefghijklmnopqrstuvwxyz",
                                (New Vector2(15, 250)),
                                Color.Pink, 'Color.WhiteSmoke,
                                0, Vector2.Zero, 0.6, Nothing, 0)

        ' DIALOG WINDOW TEST
        Globals.SpriteBatch.DrawString(Fonts.MonoType,
                                "SEMARU: [" & DialogList.Count & "]" & vbCrLf &
                                Dialog,
                                (New Vector2(DialogArea.X + 10, DialogArea.Y + 10)),
                                Color.White,
                                0, Vector2.Zero, FontScale, Nothing, 0)


        ' DRAW ADVANCE DIALOG ICON
        If DialogList.Count >= 1 Then
            Globals.SpriteBatch.Draw(Textures.AdvDialogIcon, AdvDialogIconDest, AdvDialogIconSrc, Color.White * AdvDialogIconAlpha)
        End If

        FontUp.Draw()
        FontDown.Draw()

        Globals.SpriteBatch.End()
        Globals.SpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone)


    End Sub
End Class
