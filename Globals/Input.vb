Imports Microsoft.Xna.Framework
Imports Microsoft.Xna.Framework.Input

Public Class Input
    Public Shared CurrentKeyState As KeyboardState
    Public Shared LastKeyState As KeyboardState
    Shared CurrentMouseState As MouseState
    Shared LastMouseState As MouseState
    Public Shared HasInput As Boolean

    Public Shared MouseRect As New Rectangle(0, 0, 1, 1) ' Reduce size for higher precision

    Public Shared Sub Update()
        If Keyboard.GetState = Nothing Then HasInput = False Else HasInput = True

        LastKeyState = CurrentKeyState
        LastMouseState = CurrentMouseState
        CurrentKeyState = Keyboard.GetState
        CurrentMouseState = Mouse.GetState
        MouseRect.X = HandleMouseInput.X
        MouseRect.Y = HandleMouseInput.Y
    End Sub

    Public Shared Function KeyDown(key As Keys) As Boolean
        Return CurrentKeyState.IsKeyDown(key)
    End Function

    Public Shared Function KeyPressed(key As Keys) As Boolean
        If CurrentKeyState.IsKeyDown(key) And LastKeyState.IsKeyUp(key) Then
            Return True
        End If
        Return False
    End Function

    Public Shared Function NoKeyboardInput() As Boolean
        If CurrentKeyState <> LastKeyState Then Return True
        Return False
    End Function

    Public Shared Function HandleInput(keyName As Keys) As Boolean
        If Keyboard.GetState().IsKeyDown(keyName) Then Return True

        Return False
    End Function

    Public Shared Function HandleMouseInput() As MouseState
        Return Mouse.GetState()

        Return Nothing
    End Function

    ' SPECIFIC MOUSE BUTTON SINGLE CLICK EVENTS
    Public Shared Function MouseLeftButtonPressed() As Boolean
        If CurrentMouseState.LeftButton <> LastMouseState.LeftButton Then
            Return True
        End If
        Return False
    End Function
End Class
