﻿Imports Microsoft.Xna.Framework
Imports Microsoft.Xna.Framework.Graphics

Public Class StartScreen
    ' Screen Area
    Public Area As Rectangle

    ' Add Buttons
    Private FontUp As Button
    Private FontDown As Button

    ' Add Dialog
    Private MyDialog As Dialog

    'Private TestTimer As Integer

    Public Sub New()
        Area = New Rectangle(0, 0, Globals.Screen.Width - 50, Globals.Screen.Height - 50)

        ' Add a dialog box
        MyDialog = New Dialog("HELLO WORLD! " &
                       "I must say that this improved dialog box is exceeding my expectations. " &
                       "The font could, perhaps, use some improvements, but " &
                       "overall, I'd say that this is working rather nicely." &
                       "THIS IS ALL CAPS. 1234567890 <- Numbers ",
                       New Vector2(25, 100), ' Position
                       New Vector2(650, 200), ' Size
                       0.65, ' Font Scale
                       Color.White) ' Font Color

        MyDialog.DialogMode = Dialog.DialogStyle.Typed
        MyDialog.TypeSpeed = 50

        ' ADD BUTTONS
        ' TODO: Create button collections
        FontUp = New Button("Font +", New Rectangle(MyDialog.Area.X, MyDialog.Area.Y, 150, 150))
        FontUp.Area.X += MyDialog.Area.Width - FontUp.Area.Width
        FontUp.Area.Y -= FontUp.Area.Height + 5

        FontDown = New Button("Font -", New Rectangle(MyDialog.Area.X, MyDialog.Area.Y, 150, 150))
        FontDown.Area.X += MyDialog.Area.Width - FontDown.Area.Width - FontUp.Area.Width - 5
        FontDown.Area.Y -= FontDown.Area.Height + 5

    End Sub
    Public Sub Update()
        'TestTimer += Globals.GameTimer

        ' Update Control Events
        If FontUp.Clicked Then MyDialog.ScaleFont(0.05)
        If FontDown.Clicked Then MyDialog.ScaleFont(-0.05)

        ' Control Updates
        FontUp.Update()
        FontDown.Update()

        ' Test dialog
        MyDialog.Update()

        'If TestTimer >= 5000 Then TestTimer = 0 : MsgBox("DOOT!")
    End Sub

    Public Sub Draw()
        Globals.SpriteBatch.End()
        Globals.SpriteBatch.Begin()

        ' Draw Instructions
        Globals.SpriteBatch.DrawString(Fonts.MonoType,
                                "ENTER: ADVANCE DIALOG" & vbCrLf & 'ABCDEFGHIJKLMNOPQRSTUVWXYZ
                                "UP ARROW: SCALE FONT UP" & vbCrLf &
                                "DOWN ARROW: SCALE FONT DOWN",
                                (New Vector2(700, 150)),
                                Color.Pink,
                                0, Vector2.Zero, 0.55, Nothing, 0)


        ' Draw Controls
        FontUp.Draw()
        FontDown.Draw()

        ' Test dialog
        MyDialog.Draw()

        Globals.SpriteBatch.End()
        Globals.SpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone)


    End Sub
End Class
